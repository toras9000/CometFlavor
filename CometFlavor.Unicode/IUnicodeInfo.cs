namespace CometFlavor.Unicode;

/// <summary>
/// Unicode関係の情報取得サービス
/// </summary>
public interface IUnicodeInfo
{
    /// <summary>コードポイントからEastAsianWidth定義値を取得する。</summary>
    /// <param name="code">コードポイント</param>
    /// <returns>EastAsianWidth定義値</returns>
    EastAsianWidth GetEastAsianWidth(int code);

    /// <summary>コードポイントからGraphemeClusterBreak定義値を取得する。</summary>
    /// <param name="code">コードポイント</param>
    /// <returns>GraphemeClusterBreak定義値</returns>
    GraphemeClusterBreak GetGraphemeClusterBreak(int code);
}
