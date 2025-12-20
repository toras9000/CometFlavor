namespace CometFlavor.Unicode.Materials;

/// <summary>
/// Unicode 17.0.0
/// </summary>
public class UnicodeInfoV17 : IUnicodeInfo
{
    /// <inheritdoc />
    public EastAsianWidth GetEastAsianWidth(int code) => UnicodeEastAsianWidthV17.GetEastAsianWidth(code);

    /// <inheritdoc />
    public GraphemeClusterBreak GetGraphemeClusterBreak(int code) => UnicodeGraphemeClusterBreakV17.GetGraphemeClusterBreak(code);
}
