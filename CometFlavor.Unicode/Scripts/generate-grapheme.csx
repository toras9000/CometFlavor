﻿// dotnet-script 用のスクリプト

using System.Globalization;
using System.Net.Http;
using System.Text.RegularExpressions;

// 設定
var settings = new
{
    // 出力ディレクトリ
    OutputDir = @"../Materials",

    // GraphemeClusterBreak取得クラスの生成設定
    GenerateGcb = new GenerateInfo(Namespace: "CometFlavor.Unicode.Materials", ClassName: "UnicodeGraphemeClusterBreak"),

    // バージョンごとの生成ファイルと情報ファイルURL
    Versions = new[]
    {
        new GcbVersion("14.0.0", "V14", @"https://www.unicode.org/Public/14.0.0/ucd/auxiliary/GraphemeBreakProperty.txt"),
        new GcbVersion("13.0.0", "V13", @"https://www.unicode.org/Public/13.0.0/ucd/auxiliary/GraphemeBreakProperty.txt"),
        /*
        new GcbVersion("12.0.0", "V12", @"https://www.unicode.org/Public/12.0.0/ucd/auxiliary/GraphemeBreakProperty.txt"),
        new GcbVersion("11.0.0", "V11", @"https://www.unicode.org/Public/11.0.0/ucd/auxiliary/GraphemeBreakProperty.txt"),
        new GcbVersion("10.0.0", "V10", @"https://www.unicode.org/Public/10.0.0/ucd/auxiliary/GraphemeBreakProperty.txt"),
        new GcbVersion("9.0.0",  "V9",  @"https://www.unicode.org/Public/9.0.0/ucd/auxiliary/GraphemeBreakProperty.txt"),
        */
    }
};

/// <summary>コード生成設定</summary>
record GenerateInfo(string Namespace, string ClassName);

/// <summary>Unicodeバージョン毎のGraphemeClusterBreak情報</summary>
record GcbVersion(string Value, string Symbol, string Source);

/// <summary>GraphemeClusterBreak種別列挙</summary>
enum GraphemeClusterBreak
{
    Other,
    Prepend,
    CR,
    LF,
    Control,
    Extend,
    Regional_Indicator,
    SpacingMark,
    L,
    V,
    T,
    LV,
    LVT,
    E_Base,
    E_Modifier,
    ZWJ,
    Glue_After_Zwj,
    E_Base_GAZ,
}

/// <summary>コードポイント範囲に対するGraphemeClusterBreak定義</summary>
record PropertyDefine(uint Begin, uint End, GraphemeClusterBreak Type);

/// <summary>メイン処理</summary>
async Task MainAsync()
{
    // 出力ディレクトリパス
    var outDir = PathFromRelative(settings.OutputDir);

    // UCDの定義を列挙子に変換するテーブル
    var typeDict = new Dictionary<string, GraphemeClusterBreak>();
    typeDict["Prepend"] = GraphemeClusterBreak.Prepend;
    typeDict["CR"] = GraphemeClusterBreak.CR;
    typeDict["LF"] = GraphemeClusterBreak.LF;
    typeDict["Control"] = GraphemeClusterBreak.Control;
    typeDict["Extend"] = GraphemeClusterBreak.Extend;
    typeDict["Regional_Indicator"] = GraphemeClusterBreak.Regional_Indicator;
    typeDict["SpacingMark"] = GraphemeClusterBreak.SpacingMark;
    typeDict["L"] = GraphemeClusterBreak.L;
    typeDict["V"] = GraphemeClusterBreak.V;
    typeDict["T"] = GraphemeClusterBreak.T;
    typeDict["LV"] = GraphemeClusterBreak.LV;
    typeDict["LVT"] = GraphemeClusterBreak.LVT;
    typeDict["E_Base"] = GraphemeClusterBreak.E_Base;
    typeDict["E_Modifier"] = GraphemeClusterBreak.E_Modifier;
    typeDict["ZWJ"] = GraphemeClusterBreak.ZWJ;
    typeDict["Glue_After_Zwj"] = GraphemeClusterBreak.Glue_After_Zwj;
    typeDict["E_Base_GAZ"] = GraphemeClusterBreak.E_Base_GAZ;

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
        var codeType = GraphemeClusterBreak.Other;
        var propList = new List<PropertyDefine>();
        foreach (var def in defines)
        {
            if (def.begin == (codeEnd + 1) && def.type == codeType)
            {
                codeEnd = def.end;
            }
            else
            {
                if (codeType != GraphemeClusterBreak.Other)
                {
                    propList.Add(new PropertyDefine(codeBegin, codeEnd, codeType));
                }
                codeBegin = def.begin;
                codeEnd = def.end;
                codeType = def.type;
            }
        }
        if (codeType != GraphemeClusterBreak.Other)
        {
            propList.Add(new PropertyDefine(codeBegin, codeEnd, codeType));
        }

        // 取得した定義からソースを生成
        var srcEaw = generateGcbSource(outDir, settings.GenerateGcb, version, propList);
        Console.WriteLine($"Generated ... " + srcEaw.Name);
    }
}

/// <summary>スクリプト相対パスからフルパスを取得する</summary>
string PathFromRelative(string relativePath, [System.Runtime.CompilerServices.CallerFilePath] string path = null) => Path.Combine(Path.GetDirectoryName(path), relativePath);

/// <summary>GraphemeClusterBreak取得クラスソースを生成する</summary>
FileInfo generateGcbSource(string directory, GenerateInfo generate, GcbVersion version, IReadOnlyList<PropertyDefine> defines)
{
    const string PropertyName = "GraphemeClusterBreak";
    const string MissingDefine = "Other";

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