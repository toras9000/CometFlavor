using CometFlavor.Unicode.Materials;

namespace CometFlavor.Unicode;

/// <summary>
/// Unicode関係の情報取得サービスファクトリ
/// </summary>
public static class UnicodeInfo
{
    /// <summary>Unicode バージョン 13.0.0 の情報取得サービスを生成する</summary>
    /// <returns>情報取得サービス</returns>
    public static IUnicodeInfo CreateV13() => new UnicodeInfoV13();

    /// <summary>Unicode バージョン 14.0.0 の情報取得サービスを生成する</summary>
    /// <returns>情報取得サービス</returns>
    public static IUnicodeInfo CreateV14() => new UnicodeInfoV14();

    /// <summary>Unicode バージョン 15.0.0 の情報取得サービスを生成する</summary>
    /// <returns>情報取得サービス</returns>
    public static IUnicodeInfo CreateV15() => new UnicodeInfoV15();

    /// <summary>Unicode バージョン 16.0.0 の情報取得サービスを生成する</summary>
    /// <returns>情報取得サービス</returns>
    public static IUnicodeInfo CreateV16() => new UnicodeInfoV16();

    /// <summary>Unicode バージョン 17.0.0 の情報取得サービスを生成する</summary>
    /// <returns>情報取得サービス</returns>
    public static IUnicodeInfo CreateV17() => new UnicodeInfoV17();

    /// <summary>利用可能な最新 Unicode バージョン の情報取得サービスを生成する</summary>
    /// <returns>情報取得サービス</returns>
    public static IUnicodeInfo CreateDefault() => new UnicodeInfoV17();
}
