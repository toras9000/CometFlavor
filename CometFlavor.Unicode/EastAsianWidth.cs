namespace CometFlavor.Unicode;

/// <summary>EastAsianWidth種別列挙</summary>
public enum EastAsianWidth
{
    /// <summary>不明</summary>
    Unknown,
    /// <summary>あいまい</summary>
    Ambiguous,
    /// <summary>全角</summary>
    Full,
    /// <summary>半角</summary>
    Half,
    /// <summary>ニュートラル(非EastAsia)</summary>
    Neutral,
    /// <summary>幅狭</summary>
    Narrow,
    /// <summary>幅広</summary>
    Wide,
}
