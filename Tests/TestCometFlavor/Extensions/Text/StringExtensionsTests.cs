using System;
using CometFlavor.Extensions.Text;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestCometFlavor.Extensions.Text;

[TestClass]
public class StringExtensionsTests
{
    [TestMethod]
    public void TestFirstLine()
    {
        "abc\ndef".FirstLine().Should().Be("abc");
        "aaa\rbbb".FirstLine().Should().Be("aaa");
        "xyz\r\nabc".FirstLine().Should().Be("xyz");
        "abc\n".FirstLine().Should().Be("abc");
        "\nabc".FirstLine().Should().BeEmpty();
        "\n".FirstLine().Should().BeEmpty();
        default(string).FirstLine().Should().BeNull();
    }

    [TestMethod]
    public void TestLastLine()
    {
        "abc\ndef".LastLine().Should().Be("def");
        "aaa\rbbb".LastLine().Should().Be("bbb");
        "xyz\r\nabc".LastLine().Should().Be("abc");
        "abc\n".LastLine().Should().BeEmpty();
        "\nabc".LastLine().Should().Be("abc");
        "\n".LastLine().Should().BeEmpty();
        default(string).LastLine().Should().BeNull();
    }

    [TestMethod]
    public void TestJoinString()
    {
        new[] { "aaa", "bbb", "ccc" }.JoinString().Should().Be("aaabbbccc");
        new[] { "aaa", "bbb", "ccc" }.JoinString("/").Should().Be("aaa/bbb/ccc");
        new[] { "aaa", "", "ccc" }.JoinString().Should().Be("aaaccc");
        new[] { "aaa", "", "ccc" }.JoinString("/").Should().Be("aaa//ccc");
        new[] { "aaa", null, "ccc" }.JoinString().Should().Be("aaaccc");
        new[] { "aaa", null, "ccc" }.JoinString("/").Should().Be("aaa//ccc");
    }

    [TestMethod]
    public void TestDecorate_Format()
    {
        "abc".Decorate("<{0}>").Should().Be("<abc>");
        "abc".Decorate("<xxx>").Should().Be("<xxx>");
        "".Decorate("<{0}>").Should().BeEmpty();
        default(string).Decorate("<{0}>").Should().BeNull();

        "".Decorate("<{1}>").Should().BeEmpty();
        default(string).Decorate("<{1}>").Should().BeNull();

        new Action(() => "abc".Decorate("<{1}>")).Should().Throw<Exception>();
    }

    [TestMethod]
    public void TestDecorate_Delegate()
    {
        "abc".Decorate(s => "@" + s).Should().Be("@abc");
        "abc".Decorate(s => "@xxx").Should().Be("@xxx");
        "".Decorate(s => "@" + s).Should().BeEmpty();
        default(string).Decorate(s => "@" + s).Should().BeNull();

        "".Decorate(s => throw new Exception()).Should().BeEmpty();
        default(string).Decorate(s => throw new Exception()).Should().BeNull();

        new Action(() => "abc".Decorate(s => throw new Exception())).Should().Throw<Exception>();
    }

    [TestMethod]
    public void TestCutLeftElements()
    {
        "abcdef".CutLeftElements(0).Should().Be("");
        "abcdef".CutLeftElements(1).Should().Be("a");
        "abcdef".CutLeftElements(2).Should().Be("ab");
        "abcdef".CutLeftElements(6).Should().Be("abcdef");
        "abcdef".CutLeftElements(7).Should().Be("abcdef");

        "あいうえお".CutLeftElements(1).Should().Be("あ");
        "あいうえお".CutLeftElements(5).Should().Be("あいうえお");
        "あいうえお".CutLeftElements(6).Should().Be("あいうえお");

        // 👩🏻‍👦🏼👨🏽‍👦🏾‍👦🏿👩🏼‍👨🏽‍👦🏼‍👧🏽👩🏻‍👩🏿‍👧🏼‍👧🏾
        "👩🏻‍👦🏼👨🏽‍👦🏾‍👦🏿👩🏼‍👨🏽‍👦🏼‍👧🏽👩🏻‍👩🏿‍👧🏼‍👧🏾".CutLeftElements(1).Should().Be("👩🏻‍👦🏼");
        "👩🏻‍👦🏼👨🏽‍👦🏾‍👦🏿👩🏼‍👨🏽‍👦🏼‍👧🏽👩🏻‍👩🏿‍👧🏼‍👧🏾".CutLeftElements(2).Should().Be("👩🏻‍👦🏼👨🏽‍👦🏾‍👦🏿");
        "👩🏻‍👦🏼👨🏽‍👦🏾‍👦🏿👩🏼‍👨🏽‍👦🏼‍👧🏽👩🏻‍👩🏿‍👧🏼‍👧🏾".CutLeftElements(3).Should().Be("👩🏻‍👦🏼👨🏽‍👦🏾‍👦🏿👩🏼‍👨🏽‍👦🏼‍👧🏽");
        "👩🏻‍👦🏼👨🏽‍👦🏾‍👦🏿👩🏼‍👨🏽‍👦🏼‍👧🏽👩🏻‍👩🏿‍👧🏼‍👧🏾".CutLeftElements(4).Should().Be("👩🏻‍👦🏼👨🏽‍👦🏾‍👦🏿👩🏼‍👨🏽‍👦🏼‍👧🏽👩🏻‍👩🏿‍👧🏼‍👧🏾");
        "👩🏻‍👦🏼👨🏽‍👦🏾‍👦🏿👩🏼‍👨🏽‍👦🏼‍👧🏽👩🏻‍👩🏿‍👧🏼‍👧🏾".CutLeftElements(5).Should().Be("👩🏻‍👦🏼👨🏽‍👦🏾‍👦🏿👩🏼‍👨🏽‍👦🏼‍👧🏽👩🏻‍👩🏿‍👧🏼‍👧🏾");
    }

    [TestMethod]
    public void TestCutRightElements()
    {
        "abcdef".CutRightElements(0).Should().Be("");
        "abcdef".CutRightElements(1).Should().Be("f");
        "abcdef".CutRightElements(2).Should().Be("ef");
        "abcdef".CutRightElements(6).Should().Be("abcdef");
        "abcdef".CutRightElements(7).Should().Be("abcdef");

        "あいうえお".CutRightElements(1).Should().Be("お");
        "あいうえお".CutRightElements(5).Should().Be("あいうえお");
        "あいうえお".CutRightElements(6).Should().Be("あいうえお");

        // 👩🏻‍👦🏼👨🏽‍👦🏾‍👦🏿👩🏼‍👨🏽‍👦🏼‍👧🏽👩🏻‍👩🏿‍👧🏼‍👧🏾
        "👩🏻‍👦🏼👨🏽‍👦🏾‍👦🏿👩🏼‍👨🏽‍👦🏼‍👧🏽👩🏻‍👩🏿‍👧🏼‍👧🏾".CutRightElements(1).Should().Be("👩🏻‍👩🏿‍👧🏼‍👧🏾");
        "👩🏻‍👦🏼👨🏽‍👦🏾‍👦🏿👩🏼‍👨🏽‍👦🏼‍👧🏽👩🏻‍👩🏿‍👧🏼‍👧🏾".CutRightElements(2).Should().Be("👩🏼‍👨🏽‍👦🏼‍👧🏽👩🏻‍👩🏿‍👧🏼‍👧🏾");
        "👩🏻‍👦🏼👨🏽‍👦🏾‍👦🏿👩🏼‍👨🏽‍👦🏼‍👧🏽👩🏻‍👩🏿‍👧🏼‍👧🏾".CutRightElements(3).Should().Be("👨🏽‍👦🏾‍👦🏿👩🏼‍👨🏽‍👦🏼‍👧🏽👩🏻‍👩🏿‍👧🏼‍👧🏾");
        "👩🏻‍👦🏼👨🏽‍👦🏾‍👦🏿👩🏼‍👨🏽‍👦🏼‍👧🏽👩🏻‍👩🏿‍👧🏼‍👧🏾".CutRightElements(4).Should().Be("👩🏻‍👦🏼👨🏽‍👦🏾‍👦🏿👩🏼‍👨🏽‍👦🏼‍👧🏽👩🏻‍👩🏿‍👧🏼‍👧🏾");
        "👩🏻‍👦🏼👨🏽‍👦🏾‍👦🏿👩🏼‍👨🏽‍👦🏼‍👧🏽👩🏻‍👩🏿‍👧🏼‍👧🏾".CutRightElements(5).Should().Be("👩🏻‍👦🏼👨🏽‍👦🏾‍👦🏿👩🏼‍👨🏽‍👦🏼‍👧🏽👩🏻‍👩🏿‍👧🏼‍👧🏾");
    }

    [TestMethod]
    public void TestEllipsisByLength()
    {
        "abcdefghi".EllipsisByLength(6, " ...").Should().Be("ab ...");
        "あいうえおかきくけこ".EllipsisByLength(6, " *").Should().Be("あいうえ *");

        "abc".EllipsisByLength(3, "...").Should().Be("abc");
        "あいう".EllipsisByLength(3, "...").Should().Be("あいう");

        "abcdefghi".EllipsisByLength(6, "").Should().Be("abcdef");
        "あいうえおかきくけこ".EllipsisByLength(6, "").Should().Be("あいうえおか");

        "abcdefghi".EllipsisByLength(6).Should().Be("abcdef");
        "あいうえおかきくけこ".EllipsisByLength(6).Should().Be("あいうえおか");

        // Emoji Combining Sequence
        // 1文字は3charで表現されるようなので EllipsisByLength の結果は見た目とは異なる
        "1️⃣".Length.Should().Be(3);
        "1️⃣2️⃣3️⃣4️⃣5️⃣6️⃣7️⃣8️⃣9️⃣".EllipsisByLength(7).Should().Be("1️⃣2️⃣");
        "abcdefghijklmnopqrstuvwxyz".EllipsisByLength(15, "1️⃣2️⃣3️⃣").Should().Be("abcdef1️⃣2️⃣3️⃣");

        // Emoji Modifier Sequence 
        // 1文字は4charで表現される模様。
        "👏🏻".Length.Should().Be(4);
        "👏🏻👏🏼👏🏽👏🏾👏🏿".EllipsisByLength(7).Should().Be("👏🏻");
        "abcdefghijklmnopqrstuvwxyz".EllipsisByLength(10, "👏🏻👏🏼").Should().Be("ab👏🏻👏🏼");

        // Emoji ZWJ Sequence
        // Unicode 9.0 で定義される ZERO WIDTH JOINER によって結合されるものらしい。
        // 1絵文字がだいぶ長いシーケンスで表現される模様。全部同じ長さなのかは良くわかっていない。組み合わせ色々あったりするのだろうか。
        // なおVS2022(v17.0.5)で見るとコメント内に記載した場合とコード中のリテラルで表示が変わる。各所で解釈の実装が異なるのだろうか。ただコメント内の記載位置でも表示が異なったりもする。ZWJ Sequenceのサポートが完全では無いのだろうか。
        // var t = "👩🏻‍👦🏼👨🏽‍👦🏾‍👦🏿👩🏼‍👨🏽‍👦🏼‍👧🏽👩🏻‍👩🏿‍👧🏼‍👧🏾";
        "👩🏻‍👦🏼".Length.Should().Be(9);
        "👨🏽‍👦🏾‍👦🏿".Length.Should().Be(14);
        "👩🏼‍👨🏽‍👦🏼‍👧🏽".Length.Should().Be(19);
        "👩🏻‍👩🏿‍👧🏼‍👧🏾".Length.Should().Be(19);
        "👩🏻‍👦🏼👨🏽‍👦🏾‍👦🏿👩🏼‍👨🏽‍👦🏼‍👧🏽👩🏻‍👩🏿‍👧🏼‍👧🏾".EllipsisByLength(10).Should().Be("👩🏻‍👦🏼");
        "abcdefghijklmnopqrstuvwxyz".EllipsisByLength(20, "👩🏼‍👨🏽‍👦🏼‍👧🏽").Should().Be("a👩🏼‍👨🏽‍👦🏼‍👧🏽");

    }

    [TestMethod]
    public void TestEllipsisByElements()
    {
        "abcdefghi".EllipsisByElements(6, " ...").Should().Be("ab ...");
        "あいうえおかきくけこ".EllipsisByElements(6, " *").Should().Be("あいうえ *");

        "abc".EllipsisByElements(3, "...").Should().Be("abc");
        "あいう".EllipsisByElements(3, "...").Should().Be("あいう");

        "abcdefghi".EllipsisByElements(6, "").Should().Be("abcdef");
        "あいうえおかきくけこ".EllipsisByElements(6, "").Should().Be("あいうえおか");

        "abcdefghi".EllipsisByElements(6).Should().Be("abcdef");
        "あいうえおかきくけこ".EllipsisByElements(6).Should().Be("あいうえおか");

        // Emoji Combining Sequence
        "1️⃣2️⃣3️⃣4️⃣5️⃣6️⃣7️⃣8️⃣9️⃣".EllipsisByElements(7).Should().Be("1️⃣2️⃣3️⃣4️⃣5️⃣6️⃣7️⃣");
        "abcdefghijklmnopqrstuvwxyz".EllipsisByElements(5, "1️⃣2️⃣3️⃣").Should().Be("ab1️⃣2️⃣3️⃣");

        // Emoji Modifier Sequence 
        "👏🏻👏🏼👏🏽👏🏾👏🏿".EllipsisByElements(4).Should().Be("👏🏻👏🏼👏🏽👏🏾");
        "👏🏻👏🏼👏🏽👏🏾👏🏿".EllipsisByElements(5, "~*").Should().Be("👏🏻👏🏼👏🏽👏🏾👏🏿");
        "👏🏻👏🏼👏🏽👏🏾👏🏿".EllipsisByElements(4, "~*").Should().Be("👏🏻👏🏼~*");
        "abcdefghijklmnopqrstuvwxyz".EllipsisByElements(5, "👏🏻👏🏼").Should().Be("abc👏🏻👏🏼");

        // Emoji ZWJ Sequence
        "👩🏻‍👦🏼👨🏽‍👦🏾‍👦🏿👩🏼‍👨🏽‍👦🏼‍👧🏽👩🏻‍👩🏿‍👧🏼‍👧🏾".EllipsisByElements(3).Should().Be("👩🏻‍👦🏼👨🏽‍👦🏾‍👦🏿👩🏼‍👨🏽‍👦🏼‍👧🏽");
        "👩🏻‍👦🏼👨🏽‍👦🏾‍👦🏿👩🏼‍👨🏽‍👦🏼‍👧🏽👩🏻‍👩🏿‍👧🏼‍👧🏾".EllipsisByElements(4, "~*").Should().Be("👩🏻‍👦🏼👨🏽‍👦🏾‍👦🏿👩🏼‍👨🏽‍👦🏼‍👧🏽👩🏻‍👩🏿‍👧🏼‍👧🏾");
        "👩🏻‍👦🏼👨🏽‍👦🏾‍👦🏿👩🏼‍👨🏽‍👦🏼‍👧🏽👩🏻‍👩🏿‍👧🏼‍👧🏾".EllipsisByElements(3, "~*").Should().Be("👩🏻‍👦🏼~*");
        "abcdefghijklmnopqrstuvwxyz".EllipsisByElements(6, "👩🏻‍👦🏼👨🏽‍👦🏾‍👦🏿👩🏼‍👨🏽‍👦🏼‍👧🏽👩🏻‍👩🏿‍👧🏼‍👧🏾").Should().Be("ab👩🏻‍👦🏼👨🏽‍👦🏾‍👦🏿👩🏼‍👨🏽‍👦🏼‍👧🏽👩🏻‍👩🏿‍👧🏼‍👧🏾");
    }
}
