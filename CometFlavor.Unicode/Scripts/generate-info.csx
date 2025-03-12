// dotnet-script 用のスクリプト

using System.Globalization;

// 設定
var settings = new
{
    // Info出力ディレクトリ
    InfoOutputDir = @"../Materials",

    // Unicode情報クラスの生成設定
    GenerateInfo = new GenerateInfo(Namespace: "CometFlavor.Unicode.Materials", ClassName: "UnicodeInfo"),

    // Factory出力ディレクトリ
    FactoryOutputDir = @"../",

    // Unicode情報ファクトリクラスの生成設定
    GenerateFactory = new GenerateInfo(Namespace: "CometFlavor.Unicode", ClassName: "UnicodeInfo"),

    // バージョンごとの生成ファイルと情報ファイルURL
    Versions = new[]
    {
        new UnicodeVersion("16.0.0", "V16"),
        new UnicodeVersion("15.0.0", "V15"),
        new UnicodeVersion("14.0.0", "V14"),
        new UnicodeVersion("13.0.0", "V13"),
        /*
        new UnicodeVersion("12.0.0", "V12"),
        new UnicodeVersion("11.0.0", "V11"),
        new UnicodeVersion("10.0.0", "V10"),
        new UnicodeVersion("9.0.0", "V9"),
        */
    }
};

/// <summary>コード生成設定</summary>
record GenerateInfo(string Namespace, string ClassName);

/// <summary>Unicodeバージョン毎のEastAsianWidth情報</summary>
record UnicodeVersion(string Value, string Symbol);

/// <summary>メイン処理</summary>
Task MainAsync()
{
    // 出力ディレクトリパス
    var infoOutDir = PathFromRelative(settings.InfoOutputDir);

    // バージョンごとの定義ファイル処理
    foreach (var version in settings.Versions)
    {
        var srcInfo = generateInfoSource(infoOutDir, settings.GenerateInfo, version);
        Console.WriteLine($"Generated ... " + srcInfo.Name);
    }

    // ファクトリクラスソース
    var srcFactory = generateFactorySource(PathFromRelative(settings.FactoryOutputDir), settings.GenerateFactory, settings.GenerateInfo.Namespace, settings.Versions);
    Console.WriteLine($"Generated ... " + srcFactory.Name);

    return Task.CompletedTask;
}

/// <summary>スクリプト相対パスからフルパスを取得する</summary>
string PathFromRelative(string relativePath, [System.Runtime.CompilerServices.CallerFilePath] string path = null) => Path.Combine(Path.GetDirectoryName(path), relativePath);

/// <summary>Unicode情報取得クラスソースを生成する</summary>
FileInfo generateInfoSource(string directory, GenerateInfo generate, UnicodeVersion version)
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
    writer.WriteLine($"    public EastAsianWidth GetEastAsianWidth(int code) => UnicodeEastAsianWidth{version.Symbol}.GetEastAsianWidth(code);");
    writer.WriteLine("");
    writer.WriteLine("    /// <inheritdoc />");
    writer.WriteLine($"    public GraphemeClusterBreak GetGraphemeClusterBreak(int code) => UnicodeGraphemeClusterBreak{version.Symbol}.GetGraphemeClusterBreak(code);");
    writer.WriteLine("}");
    return file;
}

/// <summary>Unicode情報クラスファクトリソースを生成する</summary>
FileInfo generateFactorySource(string directory, GenerateInfo generate, string useNs, IEnumerable<UnicodeVersion> versions)
{
    var path = Path.Combine(directory, $"{generate.ClassName}.cs");
    var file = new FileInfo(path);
    using var writer = new StreamWriter(file.FullName, false, new UTF8Encoding(true));

    writer.WriteLine($"using {useNs};");
    writer.WriteLine();
    writer.WriteLine($"namespace {generate.Namespace};");
    writer.WriteLine();
    writer.WriteLine($"/// <summary>");
    writer.WriteLine($"/// Unicode関係の情報取得サービスファクトリ");
    writer.WriteLine($"/// </summary>");
    writer.WriteLine($"public static class {generate.ClassName}");
    writer.WriteLine("{");
    var sortedVersions = versions.OrderBy(v => Version.Parse(v.Value)).ToArray();
    foreach (var version in sortedVersions)
    {
        writer.WriteLine($"    /// <summary>Unicode バージョン {version.Value} の情報取得サービスを生成する</summary>");
        writer.WriteLine($"    /// <returns>情報取得サービス</returns>");
        writer.WriteLine($"    public static IUnicodeInfo Create{version.Symbol}() => new UnicodeInfo{version.Symbol}();");
        writer.WriteLine("");
    }

    var latestVersion = sortedVersions.Last();
    writer.WriteLine("    /// <summary>利用可能な最新 Unicode バージョン の情報取得サービスを生成する</summary>");
    writer.WriteLine("    /// <returns>情報取得サービス</returns>");
    writer.WriteLine($"    public static IUnicodeInfo CreateDefault() => new UnicodeInfo{latestVersion.Symbol}();");
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