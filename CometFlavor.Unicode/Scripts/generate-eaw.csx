// dotnet-script 用のスクリプト

using System.Globalization;
using System.Net.Http;
using System.Text.RegularExpressions;

// 設定
var settings = new
{
    // 出力ディレクトリ
    OutputDir = @"../Materials",

    // EAW取得クラスの生成設定
    GenerateEaw = new GenerateInfo(Namespace: "CometFlavor.Unicode.Materials", ClassName: "UnicodeEastAsianWidth"),

    // Unicode情報クラスの生成設定
    GenerateInfo = new GenerateInfo(Namespace: "CometFlavor.Unicode.Materials", ClassName: "UnicodeInfo"),

    // バージョンごとの生成ファイルと情報ファイルURL
    Versions = new[]
    {
        new EawVersion("14.0.0", "V14", @"https://www.unicode.org/Public/14.0.0/ucd/EastAsianWidth.txt"),
        new EawVersion("13.0.0", "V13", @"https://www.unicode.org/Public/13.0.0/ucd/EastAsianWidth.txt"),
        /*
        new EawVersion("12.0.0", "V12", @"https://www.unicode.org/Public/12.0.0/ucd/EastAsianWidth.txt"),
        new EawVersion("11.0.0", "V11", @"https://www.unicode.org/Public/11.0.0/ucd/EastAsianWidth.txt"),
        new EawVersion("10.0.0", "V10", @"https://www.unicode.org/Public/10.0.0/ucd/EastAsianWidth.txt"),
        new EawVersion("9.0.0",  "V9",  @"https://www.unicode.org/Public/9.0.0/ucd/EastAsianWidth.txt"),
        */
    }
};

/// <summary>コード生成設定</summary>
record GenerateInfo(string Namespace, string ClassName);

/// <summary>Unicodeバージョン毎のEastAsianWidth情報</summary>
record EawVersion(string Value, string Symbol, string Source);

/// <summary>EastAsianWidth種別列挙</summary>
enum EastAsianWidth
{
    Unknown,
    Ambiguous,
    Full,
    Half,
    Neutral,
    Narrow,
    Wide,
}

/// <summary>コードポイント範囲に対するEastAsianWidth定義</summary>
record WidthDefine(uint Begin, uint End, EastAsianWidth Width);

/// <summary>メイン処理</summary>
async Task MainAsync()
{
    // 出力ディレクトリパス
    var outDir = PathFromRelative(settings.OutputDir);

    // UCDの定義を列挙子に変換するテーブル
    var widthDict = new Dictionary<string, EastAsianWidth>();
    widthDict["A"] = EastAsianWidth.Ambiguous;
    widthDict["F"] = EastAsianWidth.Full;
    widthDict["H"] = EastAsianWidth.Half;
    widthDict["N"] = EastAsianWidth.Neutral;
    widthDict["Na"] = EastAsianWidth.Narrow;
    widthDict["W"] = EastAsianWidth.Wide;

    // 定義ファイル処理用
    var lineTerms = new[] { '\r', '\n', };
    var extractor = new Regex(@"^([0-9a-fA-F]+)(?:\.\.([0-9a-fA-F]+))?;(\w+)");
    using var client = new HttpClient();

    // バージョンごとの定義ファイル処理
    foreach (var version in settings.Versions)
    {
        // UDC定義ファイルのダウンロードと定義内容の取得
        var source = await client.GetStringAsync(version.Source);
        var defines = source.Split(lineTerms, StringSplitOptions.RemoveEmptyEntries)
            .Select(l => extractor.Match(l))
            .Where(l => l.Success)
            .Select(m => new { begin = m.Groups[1].Value, end = m.Groups[2].Value, type = m.Groups[3].Value, });

        // 連続する定義をまとめる
        var codeBegin = 0u;
        var codeEnd = 0u;
        var widthType = EastAsianWidth.Unknown;
        var widthList = new List<WidthDefine>();
        foreach (var def in defines)
        {
            var defBegin = uint.Parse(def.begin, NumberStyles.HexNumber, CultureInfo.InvariantCulture);
            var defEnd = string.IsNullOrEmpty(def.end) ? defBegin : uint.Parse(def.end, NumberStyles.HexNumber, CultureInfo.InvariantCulture);
            var defWidth = widthDict[def.type];

            if (defBegin == (codeEnd + 1) && defWidth == widthType)
            {
                codeEnd = defEnd;
            }
            else
            {
                if (widthType != EastAsianWidth.Unknown)
                {
                    widthList.Add(new WidthDefine(codeBegin, codeEnd, widthType));
                }
                codeBegin = defBegin;
                codeEnd = defEnd;
                widthType = defWidth;
            }
        }
        if (widthType != EastAsianWidth.Unknown)
        {
            widthList.Add(new WidthDefine(codeBegin, codeEnd, widthType));
        }

        // 取得した定義からソースを生成
        var srcEaw = generateEawSource(outDir, settings.GenerateEaw, version, widthList);
        Console.WriteLine($"Generated ... " + srcEaw.Name);

        var srcInfo = generateInfoSource(outDir, settings.GenerateInfo, version);
        Console.WriteLine($"Generated ... " + srcInfo.Name);
    }
}

/// <summary>スクリプト相対パスからフルパスを取得する</summary>
string PathFromRelative(string relativePath, [System.Runtime.CompilerServices.CallerFilePath] string path = null) => Path.Combine(Path.GetDirectoryName(path), relativePath);

/// <summary>EastAsianWidth取得クラスソースを生成する</summary>
FileInfo generateEawSource(string directory, GenerateInfo generate, EawVersion version, IReadOnlyList<WidthDefine> defines)
{
    var path = Path.Combine(directory, $"{generate.ClassName}{version.Symbol}.cs");
    var file = new FileInfo(path);
    using var writer = new StreamWriter(file.FullName, false, new UTF8Encoding(true));
    writer.WriteLine($"namespace {generate.Namespace};");
    writer.WriteLine();
    writer.WriteLine($"/// <summary>");
    writer.WriteLine($"/// Unicode {version.Value} EastAsianWidth");
    writer.WriteLine($"/// </summary>");
    writer.WriteLine($"public static class {generate.ClassName}{version.Symbol}");
    writer.WriteLine("{");
    writer.WriteLine("    /// <summary>");
    writer.WriteLine("    /// コードポイントからEastAsianWidth定義値を取得する。");
    writer.WriteLine("    /// </summary>");
    writer.WriteLine("    /// <param name=\"code\">コードポイント</param>");
    writer.WriteLine("    /// <returns>EastAsianWidth定義値</returns>");
    writer.WriteLine("    public static EastAsianWidth GetEastAsianWidth(int code)");
    writer.WriteLine("    {");
    writer.WriteLine("        return code switch");
    writer.WriteLine("        {");
    foreach (var info in defines)
    {
        if (info.Begin == info.End)
        {
            writer.WriteLine($"            0x{info.Begin:X06} => EastAsianWidth.{info.Width},");
        }
        else
        {
            writer.WriteLine($"            (>= 0x{info.Begin:X06}) and (<= 0x{info.End:X06}) => EastAsianWidth.{info.Width},");
        }
    }
    writer.WriteLine("            _ => EastAsianWidth.Unknown");
    writer.WriteLine("        };");
    writer.WriteLine("    }");
    writer.WriteLine("}");

    return file;
}

/// <summary>Unicode情報取得クラスソースを生成する</summary>
FileInfo generateInfoSource(string directory, GenerateInfo generate, EawVersion version)
{
    var path = Path.Combine(directory, $"{generate.ClassName}{version.Symbol}.cs");
    var file = new FileInfo(path);
    using var writer = new StreamWriter(file.FullName, false, new UTF8Encoding(true));
    writer.WriteLine($"namespace {generate.Namespace};");
    writer.WriteLine();
    writer.WriteLine($"/// <summary>");
    writer.WriteLine($"/// Unicode {version.Value}");
    writer.WriteLine($"/// </summary>");
    writer.WriteLine($"public class {generate.ClassName}{version.Symbol} : IUnicodeInfo");
    writer.WriteLine("{");
    writer.WriteLine("    /// <inheritdoc />");
    writer.WriteLine("    public EastAsianWidth GetEastAsianWidth(int code) => UnicodeEastAsianWidthV14.GetEastAsianWidth(code);");
    writer.WriteLine("}");
    return file;
}

// スクリプト実行
try
{
    await MainAsync();
}
catch (Exception ex)
{
    Console.WriteLine("Failed: " + ex);
}
if (!Console.IsInputRedirected)
{
    Console.WriteLine("Press any key to exit.");
    Console.ReadKey(true);
}