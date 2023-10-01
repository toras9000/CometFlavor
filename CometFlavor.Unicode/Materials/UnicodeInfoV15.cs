namespace CometFlavor.Unicode.Materials;

/// <summary>
/// Unicode 15.0.0
/// </summary>
public class UnicodeInfoV15 : IUnicodeInfo
{
    /// <inheritdoc />
    public EastAsianWidth GetEastAsianWidth(int code) => UnicodeEastAsianWidthV15.GetEastAsianWidth(code);

    /// <inheritdoc />
    public GraphemeClusterBreak GetGraphemeClusterBreak(int code) => UnicodeGraphemeClusterBreakV15.GetGraphemeClusterBreak(code);
}
