namespace CometFlavor.Unicode.Materials;

/// <summary>
/// Unicode 16.0.0
/// </summary>
public class UnicodeInfoV16 : IUnicodeInfo
{
    /// <inheritdoc />
    public EastAsianWidth GetEastAsianWidth(int code) => UnicodeEastAsianWidthV16.GetEastAsianWidth(code);

    /// <inheritdoc />
    public GraphemeClusterBreak GetGraphemeClusterBreak(int code) => UnicodeGraphemeClusterBreakV16.GetGraphemeClusterBreak(code);
}
