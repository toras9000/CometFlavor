﻿// dotnet-script 用のスクリプト

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

    // バージョンごとの生成ファイルと情報ファイルURL
    Versions = new[]
    {
        new EawVersion("16.0.0", "V16", @"https://www.unicode.org/Public/16.0.0/ucd/EastAsianWidth.txt"),
        new EawVersion("15.0.0", "V15", @"https://www.unicode.org/Public/15.0.0/ucd/EastAsianWidth.txt"),
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
record PropertyDefine(uint Begin, uint End, EastAsianWidth Type);

/// <summary>メイン処理</summary>
async Task MainAsync()
{
    // 出力ディレクトリパス
    var outDir = PathFromRelative(settings.OutputDir);

    // UCDの定義を列挙子に変換するテーブル
    var typeDict = new Dictionary<string, EastAsianWidth>();
    typeDict["A"] = EastAsianWidth.Ambiguous;
    typeDict["F"] = EastAsianWidth.Full;
    typeDict["H"] = EastAsianWidth.Half;
    typeDict["N"] = EastAsianWidth.Neutral;
    typeDict["Na"] = EastAsianWidth.Narrow;
    typeDict["W"] = EastAsianWidth.Wide;

    // 定義ファイル処理用
    var lineTerms = new[] { '\r', '\n', };
    var extractor = new Regex(@"^([0-9a-fA-F]+)(?:\.\.([0-9a-fA-F]+))?\s*;\s*(\w+)");
    using var client = new HttpClient();

    // バージョンごとの定義ファイル処理
    foreach (var version in settings.Versions)
    {
        // UDC定義ファイルのダウンロードと定義内容の取得
        var source = await client.GetStringAsync(version.Source);
        var defines = source.Split(lineTerms, StringSplitOptions.RemoveEmptyEntries)
            .Select(l => extractor.Match(l))
            .Where(l => l.Success)
            .Select(m => new
            {
                begin = m.Groups[1].Value,
                end = string.IsNullOrEmpty(m.Groups[2].Value) ? m.Groups[1].Value : m.Groups[2].Value,
                type = m.Groups[3].Value,
            })
            .Select(p => new
            {
                begin = uint.Parse(p.begin, NumberStyles.HexNumber, CultureInfo.InvariantCulture),
                end = uint.Parse(p.end, NumberStyles.HexNumber, CultureInfo.InvariantCulture),
                type = typeDict[p.type],
            })
            .OrderBy(p => p.begin)
            ;

        // 連続する定義をまとめる
        var codeBegin = 0u;
        var codeEnd = 0u;
        var codeType = EastAsianWidth.Unknown;
        var propList = new List<PropertyDefine>();
        foreach (var def in defines)
        {
            if (def.begin == (codeEnd + 1) && def.type == codeType)
            {
                codeEnd = def.end;
            }
            else
            {
                if (codeType != EastAsianWidth.Unknown)
                {
                    propList.Add(new PropertyDefine(codeBegin, codeEnd, codeType));
                }
                codeBegin = def.begin;
                codeEnd = def.end;
                codeType = def.type;
            }
        }
        if (codeType != EastAsianWidth.Unknown)
        {
            propList.Add(new PropertyDefine(codeBegin, codeEnd, codeType));
        }

        // 取得した定義からソースを生成
        var srcEaw = generateEawSource(outDir, settings.GenerateEaw, version, propList);
        Console.WriteLine($"Generated ... " + srcEaw.Name);
    }
}

/// <summary>スクリプト相対パスからフルパスを取得する</summary>
string PathFromRelative(string relativePath, [System.Runtime.CompilerServices.CallerFilePath] string path = null) => Path.Combine(Path.GetDirectoryName(path), relativePath);

/// <summary>EastAsianWidth取得クラスソースを生成する</summary>
FileInfo generateEawSource(string directory, GenerateInfo generate, EawVersion version, IReadOnlyList<PropertyDefine> defines)
{
    const string PropertyName = "EastAsianWidth";
    const string MissingDefine = "Unknown";

    var path = Path.Combine(directory, $"{generate.ClassName}{version.Symbol}.cs");
    var file = new FileInfo(path);
    using var writer = new StreamWriter(file.FullName, false, new UTF8Encoding(true));
    writer.WriteLine($"namespace {generate.Namespace};");
    writer.WriteLine();
    writer.WriteLine($"/// <summary>");
    writer.WriteLine($"/// Unicode {version.Value} {PropertyName}");
    writer.WriteLine($"/// </summary>");
    writer.WriteLine($"public static class {generate.ClassName}{version.Symbol}");
    writer.WriteLine("{");
    writer.WriteLine("    /// <summary>");
    writer.WriteLine($"    /// コードポイントから{PropertyName}定義値を取得する。");
    writer.WriteLine("    /// </summary>");
    writer.WriteLine("    /// <param name=\"code\">コードポイント</param>");
    writer.WriteLine($"    /// <returns>{PropertyName}定義値</returns>");
    writer.WriteLine($"    public static {PropertyName} Get{PropertyName}(int code)");
    writer.WriteLine("    {");
    writer.WriteLine("        return code switch");
    writer.WriteLine("        {");
    foreach (var info in defines)
    {
        if (info.Begin == info.End)
        {
            writer.WriteLine($"            0x{info.Begin:X06} => {PropertyName}.{info.Type},");
        }
        else
        {
            writer.WriteLine($"            (>= 0x{info.Begin:X06}) and (<= 0x{info.End:X06}) => {PropertyName}.{info.Type},");
        }
    }
    writer.WriteLine($"            _ => {PropertyName}.{MissingDefine}");
    writer.WriteLine("        };");
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