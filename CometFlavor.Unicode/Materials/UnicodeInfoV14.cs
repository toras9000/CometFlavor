namespace CometFlavor.Unicode.Materials;

/// <summary>
/// Unicode 14.0.0
/// </summary>
public class UnicodeInfoV14 : IUnicodeInfo
{
    /// <inheritdoc />
    public EastAsianWidth GetEastAsianWidth(int code) => UnicodeEastAsianWidthV14.GetEastAsianWidth(code);

    /// <inheritdoc />
    public GraphemeClusterBreak GetGraphemeClusterBreak(int code) => UnicodeGraphemeClusterBreakV14.GetGraphemeClusterBreak(code);
}
