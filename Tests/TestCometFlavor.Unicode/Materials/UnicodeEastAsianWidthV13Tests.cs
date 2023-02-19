using CometFlavor.Unicode;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static CometFlavor.Unicode.Materials.UnicodeEastAsianWidthV13;

namespace TestCometFlavor.Unicode.Materials;

[TestClass]
public class UnicodeEastAsianWidthV13Tests
{
    [TestMethod]
    public void GetEastAsianWidth_Pattern()
    {
        GetEastAsianWidth(-1).Should().Be(EastAsianWidth.Unknown);
        GetEastAsianWidth(0x000000).Should().Be(EastAsianWidth.Neutral);
        GetEastAsianWidth(0x00001F).Should().Be(EastAsianWidth.Neutral);
        GetEastAsianWidth(0x000020).Should().Be(EastAsianWidth.Narrow);
        GetEastAsianWidth(0x000023).Should().Be(EastAsianWidth.Narrow);
        GetEastAsianWidth(0x000025).Should().Be(EastAsianWidth.Narrow);
        GetEastAsianWidth(0x0000A0).Should().Be(EastAsianWidth.Neutral);
        GetEastAsianWidth(0x0000A1).Should().Be(EastAsianWidth.Ambiguous);
        GetEastAsianWidth(0x0000A2).Should().Be(EastAsianWidth.Narrow);
        GetEastAsianWidth(0x000870).Should().Be(EastAsianWidth.Unknown);
        GetEastAsianWidth(0x0010FF).Should().Be(EastAsianWidth.Neutral);
        GetEastAsianWidth(0x001100).Should().Be(EastAsianWidth.Wide);
        GetEastAsianWidth(0x00115F).Should().Be(EastAsianWidth.Wide);
        GetEastAsianWidth(0x001160).Should().Be(EastAsianWidth.Neutral);
        GetEastAsianWidth(0x0020A8).Should().Be(EastAsianWidth.Neutral);
        GetEastAsianWidth(0x0020A9).Should().Be(EastAsianWidth.Half);
        GetEastAsianWidth(0x0020AA).Should().Be(EastAsianWidth.Neutral);
        GetEastAsianWidth(0x002FFB).Should().Be(EastAsianWidth.Wide);
        GetEastAsianWidth(0x002FFF).Should().Be(EastAsianWidth.Unknown);
        GetEastAsianWidth(0x003000).Should().Be(EastAsianWidth.Full);
        GetEastAsianWidth(0x003001).Should().Be(EastAsianWidth.Wide);
        GetEastAsianWidth(0x00FFFD).Should().Be(EastAsianWidth.Ambiguous);
        GetEastAsianWidth(0x00FFFF).Should().Be(EastAsianWidth.Unknown);
        GetEastAsianWidth(0x010000).Should().Be(EastAsianWidth.Neutral);
        GetEastAsianWidth(0x01F979).Should().Be(EastAsianWidth.Unknown);
        GetEastAsianWidth(0x0E007F).Should().Be(EastAsianWidth.Neutral);
        GetEastAsianWidth(0x0E0100).Should().Be(EastAsianWidth.Ambiguous);
        GetEastAsianWidth(0x10FFFD).Should().Be(EastAsianWidth.Ambiguous);
        GetEastAsianWidth(0x10FFFE).Should().Be(EastAsianWidth.Unknown);
    }

}
