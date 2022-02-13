using CometFlavor.Unicode.Materials;

namespace CometFlavor.Unicode;

/// <summary>
/// Unicode関係の情報
/// </summary>
public static class UnicodeInfo
{
    /// <summary>Unicode バージョン 13 の情報取得サービスを生成する</summary>
    /// <returns>情報取得サービス</returns>
    public static IUnicodeInfo CreateV13() => new UnicodeInfoV13();

    /// <summary>Unicode バージョン 14 の情報取得サービスを生成する</summary>
    /// <returns>情報取得サービス</returns>
    public static IUnicodeInfo CreateV14() => new UnicodeInfoV14();

    /// <summary>利用可能な最新 Unicode バージョン の情報取得サービスを生成する</summary>
    /// <returns>情報取得サービス</returns>
    public static IUnicodeInfo CreateDefault() => new UnicodeInfoV14();
}
