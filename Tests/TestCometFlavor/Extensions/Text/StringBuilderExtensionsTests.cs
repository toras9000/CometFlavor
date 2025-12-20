using System;
using System.Linq;
using System.Text;
using CometFlavor.Extensions.Text;
using AwesomeAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestCometFlavor.Extensions.Text;

[TestClass]
public class StringBuilderExtensionsTests
{
    [TestMethod()]
    public void IsEmpty()
    {
        new StringBuilder().IsEmpty().Should().BeTrue();
        new StringBuilder("").IsEmpty().Should().BeTrue();

        new StringBuilder(" ").IsEmpty().Should().BeFalse();
    }

    [TestMethod()]
    public void IsNotEmpty()
    {
        new StringBuilder().IsNotEmpty().Should().BeFalse();
        new StringBuilder("").IsNotEmpty().Should().BeFalse();

        new StringBuilder(" ").IsNotEmpty().Should().BeTrue();
    }

    [TestMethod()]
    public void IsWhite()
    {
        new StringBuilder().IsWhite().Should().BeTrue();
        new StringBuilder("").IsWhite().Should().BeTrue();
        new StringBuilder(" ").IsWhite().Should().BeTrue();
        new StringBuilder("\r").IsWhite().Should().BeTrue();
        new StringBuilder("\n").IsWhite().Should().BeTrue();

        new StringBuilder("a").IsWhite().Should().BeFalse();
    }

    [TestMethod()]
    public void IsNotWhite()
    {
        new StringBuilder().IsNotWhite().Should().BeFalse();
        new StringBuilder("").IsNotWhite().Should().BeFalse();
        new StringBuilder(" ").IsNotWhite().Should().BeFalse();
        new StringBuilder("\r").IsNotWhite().Should().BeFalse();
        new StringBuilder("\n").IsNotWhite().Should().BeFalse();

        new StringBuilder("a").IsNotWhite().Should().BeTrue();
    }

    [TestMethod()]
    public void StartsWith_Short_NoComparison()
    {
        var data = new StringBuilder("abcdef");

        data.StartsWith("a").Should().BeTrue();
        data.StartsWith("abcdef").Should().BeTrue();

        data.StartsWith("bcdef").Should().BeFalse();
        data.StartsWith("abcdefg").Should().BeFalse();
    }

    [TestMethod()]
    public void StartsWith_Short_WithComparison()
    {
        var data = new StringBuilder("abcdef");

        data.StartsWith("a", StringComparison.OrdinalIgnoreCase).Should().BeTrue();
        data.StartsWith("abcdef", StringComparison.OrdinalIgnoreCase).Should().BeTrue();
        data.StartsWith("A", StringComparison.OrdinalIgnoreCase).Should().BeTrue();
        data.StartsWith("abcdeF", StringComparison.OrdinalIgnoreCase).Should().BeTrue();

        data.StartsWith("bcdef", StringComparison.OrdinalIgnoreCase).Should().BeFalse();
        data.StartsWith("abcdefg", StringComparison.OrdinalIgnoreCase).Should().BeFalse();

        data.StartsWith("A", StringComparison.Ordinal).Should().BeFalse();
    }

    [TestMethod()]
    public void StartsWith_Long_NoComparison()
    {
        var data = new StringBuilder();
        for (var i = 0; i < 100; i++)
        {
            data.Append("abcdef");
        }

        data.StartsWith(Enumerable.Repeat("abcdef", 50).JoinString() + "a").Should().BeTrue();

        data.StartsWith(Enumerable.Repeat("abcdef", 50).JoinString() + "b").Should().BeFalse();
    }

    [TestMethod()]
    public void StartsWith_Long_WithComparison()
    {
        var data = new StringBuilder();
        for (var i = 0; i < 100; i++)
        {
            data.Append("abcdef");
        }

        data.StartsWith(Enumerable.Repeat("abcdef", 50).JoinString() + "A", StringComparison.OrdinalIgnoreCase).Should().BeTrue();

        data.StartsWith(Enumerable.Repeat("abcdef", 50).JoinString() + "A", StringComparison.Ordinal).Should().BeFalse();
        data.StartsWith(Enumerable.Repeat("abcdef", 50).JoinString() + "B", StringComparison.OrdinalIgnoreCase).Should().BeFalse();
    }

    [TestMethod()]
    public void PadLeft()
    {
        new StringBuilder().PadLeft(3).ToString().Should().Be("   ");
        new StringBuilder("").PadLeft(3).ToString().Should().Be("   ");
        new StringBuilder("x").PadLeft(3).ToString().Should().Be("  x");
        new StringBuilder("asd").PadLeft(3).ToString().Should().Be("asd");
        new StringBuilder("qwer").PadLeft(3).ToString().Should().Be("qwer");
        new StringBuilder("").PadLeft(0).ToString().Should().Be("");

        new StringBuilder().PadLeft(3, 'P').ToString().Should().Be("PPP");
        new StringBuilder("").PadLeft(3, 'P').ToString().Should().Be("PPP");
        new StringBuilder("x").PadLeft(3, 'P').ToString().Should().Be("PPx");
        new StringBuilder("asd").PadLeft(3, 'P').ToString().Should().Be("asd");
        new StringBuilder("qwer").PadLeft(3, 'P').ToString().Should().Be("qwer");
        new StringBuilder("").PadLeft(0, 'P').ToString().Should().Be("");

        new Action(() => new StringBuilder("").PadLeft(-1)).Should().Throw<Exception>();
        new Action(() => default(StringBuilder).PadLeft(2)).Should().Throw<Exception>();
    }

    [TestMethod()]
    public void PadRight()
    {
        new StringBuilder().PadRight(3).ToString().Should().Be("   ");
        new StringBuilder("").PadRight(3).ToString().Should().Be("   ");
        new StringBuilder("x").PadRight(3).ToString().Should().Be("x  ");
        new StringBuilder("asd").PadRight(3).ToString().Should().Be("asd");
        new StringBuilder("qwer").PadRight(3).ToString().Should().Be("qwer");
        new StringBuilder("").PadRight(0).ToString().Should().Be("");

        new StringBuilder().PadRight(3, 'P').ToString().Should().Be("PPP");
        new StringBuilder("").PadRight(3, 'P').ToString().Should().Be("PPP");
        new StringBuilder("x").PadRight(3, 'P').ToString().Should().Be("xPP");
        new StringBuilder("asd").PadRight(3, 'P').ToString().Should().Be("asd");
        new StringBuilder("qwer").PadRight(3, 'P').ToString().Should().Be("qwer");
        new StringBuilder("").PadRight(0, 'P').ToString().Should().Be("");

        new Action(() => new StringBuilder("").PadRight(-1)).Should().Throw<Exception>();
        new Action(() => default(StringBuilder).PadRight(2)).Should().Throw<Exception>();
    }

}
