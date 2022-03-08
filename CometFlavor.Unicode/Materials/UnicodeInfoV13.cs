namespace CometFlavor.Unicode.Materials;

/// <summary>
/// Unicode 13.0.0
/// </summary>
public class UnicodeInfoV13 : IUnicodeInfo
{
    /// <inheritdoc />
    public EastAsianWidth GetEastAsianWidth(int code) => UnicodeEastAsianWidthV13.GetEastAsianWidth(code);

    /// <inheritdoc />
    public GraphemeClusterBreak GetGraphemeClusterBreak(int code) => UnicodeGraphemeClusterBreakV13.GetGraphemeClusterBreak(code);
}
