using System;
using CometFlavor.Unicode;
using CometFlavor.Unicode.Extensions.Text;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestCometFlavor.Unicode.Extensions.Text;

[TestClass]
public class EawMeasureTests
{
    [TestMethod]
    public void TestConstructor()
    {
        var param6 = new EawMeasure(1, 2, 3, 4, 5, 6);
        param6.Narrow.Should().Be(1);
        param6.Wide.Should().Be(2);
        param6.Halfwidth.Should().Be(3);
        param6.Fullwidth.Should().Be(4);
        param6.Neutral.Should().Be(5);
        param6.Ambiguous.Should().Be(6);

        var param4 = new EawMeasure(1, 2, 3, 4);
        param4.Narrow.Should().Be(1);
        param4.Wide.Should().Be(2);
        param4.Halfwidth.Should().Be(1);
        param4.Fullwidth.Should().Be(2);
        param4.Neutral.Should().Be(3);
        param4.Ambiguous.Should().Be(4);

        var param3 = new EawMeasure(1, 2, 3);
        param3.Narrow.Should().Be(1);
        param3.Wide.Should().Be(2);
        param3.Halfwidth.Should().Be(1);
        param3.Fullwidth.Should().Be(2);
        param3.Neutral.Should().Be(1);
        param3.Ambiguous.Should().Be(3);
    }

    [TestMethod]
    public void TestConstructor_Error()
    {
        new Action(() => new EawMeasure(-1, 1, 1, 1, 1, 1)).Should().Throw<Exception>();
        new Action(() => new EawMeasure(1, -1, 1, 1, 1, 1)).Should().Throw<Exception>();
        new Action(() => new EawMeasure(1, 1, -1, 1, 1, 1)).Should().Throw<Exception>();
        new Action(() => new EawMeasure(1, 1, 1, -1, 1, 1)).Should().Throw<Exception>();
        new Action(() => new EawMeasure(1, 1, 1, 1, -1, 1)).Should().Throw<Exception>();
        new Action(() => new EawMeasure(1, 1, 1, 1, 1, -1)).Should().Throw<Exception>();

        new Action(() => new EawMeasure(-1, 1, 1, 1)).Should().Throw<Exception>();
        new Action(() => new EawMeasure(1, -1, 1, 1)).Should().Throw<Exception>();
        new Action(() => new EawMeasure(1, 1, -1, 1)).Should().Throw<Exception>();
        new Action(() => new EawMeasure(1, 1, 1, -1)).Should().Throw<Exception>();

        new Action(() => new EawMeasure(-1, 1, 1)).Should().Throw<Exception>();
        new Action(() => new EawMeasure(1, -1, 1)).Should().Throw<Exception>();
        new Action(() => new EawMeasure(1, 1, -1)).Should().Throw<Exception>();
    }

    [TestMethod]
    public void TestGetWidth()
    {
        var measure = new EawMeasure(1, 2, 3, 4, 5, 6);
        measure.GetWidth(EastAsianWidth.Narrow).Should().Be(1);
        measure.GetWidth(EastAsianWidth.Wide).Should().Be(2);
        measure.GetWidth(EastAsianWidth.Half).Should().Be(3);
        measure.GetWidth(EastAsianWidth.Full).Should().Be(4);
        measure.GetWidth(EastAsianWidth.Neutral).Should().Be(5);
        measure.GetWidth(EastAsianWidth.Ambiguous).Should().Be(6);
        measure.GetWidth(EastAsianWidth.Unknown).Should().Be(5);
        measure.GetWidth((EastAsianWidth)(-1)).Should().Be(5);
    }

}
