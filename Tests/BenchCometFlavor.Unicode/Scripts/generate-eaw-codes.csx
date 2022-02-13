// dotnet-script 用のスクリプト

using System.Globalization;
using System.Net.Http;
using System.Text.RegularExpressions;

// 設定
var settings = new
{
    // 出力ディレクトリ
    OutputDir = @"../Codes",

    // 生成コードの名前空間
    Namespace = "BenchCometFlavor.Unicode.Codes",

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
        var srcLinear = generateEawSource_Linear(outDir, new GenerateInfo(settings.Namespace, "EawLinear"), version, widthList);
        Console.WriteLine($"Generated ... " + srcLinear.Name);

        var srcSwtchExp = generateEawSource_SwitchExp(outDir, new GenerateInfo(settings.Namespace, "EawSwitchExp"), version, widthList);
        Console.WriteLine($"Generated ... " + srcSwtchExp.Name);

        var srcIfBin = generateEawSource_IfBin(outDir, new GenerateInfo(settings.Namespace, "EawIfBin"), version, widthList);
        Console.WriteLine($"Generated ... " + srcIfBin.Name);
    }
}

/// <summary>スクリプト相対パスからフルパスを取得する</summary>
string PathFromRelative(string relativePath, [System.Runtime.CompilerServices.CallerFilePath] string path = null) => Path.Combine(Path.GetDirectoryName(path), relativePath);

/// <summary>EastAsianWidth取得クラスソースを生成する</summary>
FileInfo generateEawSource_Linear(string directory, GenerateInfo generate, EawVersion version, IReadOnlyList<WidthDefine> defines)
{
    var path = Path.Combine(directory, $"{generate.ClassName}{version.Symbol}.cs");
    var file = new FileInfo(path);
    using var writer = new StreamWriter(file.FullName);
    writer.WriteLine($"using CometFlavor.Unicode;");
    writer.WriteLine();
    writer.WriteLine($"namespace {generate.Namespace};");
    writer.WriteLine();
    writer.WriteLine($"public static class {generate.ClassName}{version.Symbol}");
    writer.WriteLine("{");
    writer.WriteLine("    public static EastAsianWidth GetEastAsianWidth(int code)");
    writer.WriteLine("    {");
    writer.WriteLine("        for (var i = 0; i < Defines.Length; i++)");
    writer.WriteLine("        {");
    writer.WriteLine("            var define = Defines[i];");
    writer.WriteLine("            if (define.Begin <= code && code <= define.End)");
    writer.WriteLine("            {");
    writer.WriteLine("                return define.Width;");
    writer.WriteLine("            }");
    writer.WriteLine("        }");
    writer.WriteLine("        return EastAsianWidth.Unknown;");
    writer.WriteLine("    }");
    writer.WriteLine();
    writer.WriteLine("    private record WidthDefine(uint Begin, uint End, EastAsianWidth Width);");
    writer.WriteLine();
    writer.WriteLine("    private static readonly WidthDefine[] Defines = new[]");
    writer.WriteLine("    {");
    foreach (var info in defines)
    {
        writer.WriteLine($"        new WidthDefine(0x{info.Begin:X06}, 0x{info.End:X06}, EastAsianWidth.{info.Width}),");
    }
    writer.WriteLine("    };");
    writer.WriteLine("}");

    return file;
}

/// <summary>EastAsianWidth取得クラスソースを生成する</summary>
FileInfo generateEawSource_SwitchExp(string directory, GenerateInfo generate, EawVersion version, IReadOnlyList<WidthDefine> defines)
{
    var path = Path.Combine(directory, $"{generate.ClassName}{version.Symbol}.cs");
    var file = new FileInfo(path);
    using var writer = new StreamWriter(file.FullName);
    writer.WriteLine($"using CometFlavor.Unicode;");
    writer.WriteLine($"namespace {generate.Namespace};");
    writer.WriteLine();
    writer.WriteLine($"public static class {generate.ClassName}{version.Symbol}");
    writer.WriteLine("{");
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

/// <summary>EastAsianWidth取得クラスソースを生成する</summary>
FileInfo generateEawSource_IfBin(string directory, GenerateInfo generate, EawVersion version, IReadOnlyList<WidthDefine> defines)
{
    var ordered = defines.OrderBy(d => d.Begin).ToArray();

    var path = Path.Combine(directory, $"{generate.ClassName}{version.Symbol}.cs");
    var file = new FileInfo(path);
    using var writer = new StreamWriter(file.FullName);

    void generateIfBinCode(int level, int first, int last)
    {
        if (last < first) { return; }

        var indent = new string(' ', level * 4);
        if (first == last)
        {
            writer.WriteLine(indent + $"if (0x{ordered[first].Begin:X06} <= code && code <= 0x{ordered[first].End:X06}) return EastAsianWidth.{ordered[first].Width};");
        }
        else
        {
            var middle = first + (last - first) / 2;

            writer.WriteLine(indent + $"if (code < 0x{ordered[middle].Begin:X06})");
            writer.WriteLine(indent + "{");
            generateIfBinCode(level + 1, first, middle - 1);
            writer.WriteLine(indent + "}");
            writer.WriteLine(indent + $"else if (code <= 0x{ordered[middle].End:X06}) return EastAsianWidth.{ordered[middle].Width};");
            writer.WriteLine(indent + "else");
            writer.WriteLine(indent + "{");
            generateIfBinCode(level + 1, middle + 1, last);
            writer.WriteLine(indent + "}");
        }
    }

    writer.WriteLine($"using CometFlavor.Unicode;");
    writer.WriteLine($"namespace {generate.Namespace};");
    writer.WriteLine();
    writer.WriteLine($"public static class {generate.ClassName}{version.Symbol}");
    writer.WriteLine("{");
    writer.WriteLine("    public static EastAsianWidth GetEastAsianWidth(int code)");
    writer.WriteLine("    {");
    generateIfBinCode(2, 0, ordered.Length - 1);
    writer.WriteLine("        return EastAsianWidth.Unknown;");
    writer.WriteLine("    }");
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