using System;
using CometFlavor.Extensions.Text;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestCometFlavor.Extensions.Text;

[TestClass]
public class StringExtensionsTests
{
    // 以下の情報を参考にさせて頂いた。
    // ・Unicode 絵文字にまつわるあれこれ (絵文字の標準とプログラム上でのハンドリング)
    // 　https://qiita.com/_sobataro/items/47989ee4b573e0c2adfc

    [TestMethod]
    public void TestFirstLine()
    {
        "abc\ndef".FirstLine().Should().Be("abc");
        "aaa\rbbb".FirstLine().Should().Be("aaa");
        "xyz\r\nabc".FirstLine().Should().Be("xyz");
        "abc\n".FirstLine().Should().Be("abc");
        "\nabc".FirstLine().Should().BeEmpty();
        "\n".FirstLine().Should().BeEmpty();
        "".FirstLine().Should().BeEmpty();
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
        "".LastLine().Should().BeEmpty();
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
    public void TestAsTextElements()
    {
        "".AsTextElements().Should().BeEmpty();

        "abc".AsTextElements().Should().Equal("a", "b", "c");

        "あいう".AsTextElements().Should().Equal("あ", "い", "う");

        "アイウエ".AsTextElements().Should().Equal("ア", "イ", "ウ", "エ");

        "ｱｲｳｴｵ".AsTextElements().Should().Equal("ｱ", "ｲ", "ｳ", "ｴ", "ｵ");

        "1️⃣2️⃣3️⃣4️⃣5️⃣6️⃣7️⃣8️⃣9️⃣".AsTextElements().Should().Equal("1️⃣", "2️⃣", "3️⃣", "4️⃣", "5️⃣", "6️⃣", "7️⃣", "8️⃣", "9️⃣");

        "👏🏻👏🏼👏🏽👏🏾👏🏿".AsTextElements().Should().Equal("👏🏻", "👏🏼", "👏🏽", "👏🏾", "👏🏿");

        "🇯🇵🇬🇧🇺🇸".AsTextElements().Should().Equal("🇯🇵", "🇬🇧", "🇺🇸");

        // 👩🏻‍👦🏼👨🏽‍👦🏾‍👦🏿👩🏼‍👨🏽‍👦🏼‍👧🏽👩🏻‍👩🏿‍👧🏼‍👧🏾
        // なおVS2022(v17.0.5)で見るとコメント内に記載した場合とコード中のリテラルで表示が変わる。各所で解釈の実装が異なるのだろうか。ただコメント内の記載位置でも表示が異なったりもする。ZWJ Sequenceのサポートが完全では無いのだろうか。
        // var t = "👩🏻‍👦🏼👨🏽‍👦🏾‍👦🏿👩🏼‍👨🏽‍👦🏼‍👧🏽👩🏻‍👩🏿‍👧🏼‍👧🏾";
        "👩🏻‍👦🏼👨🏽‍👦🏾‍👦🏿👩🏼‍👨🏽‍👦🏼‍👧🏽👩🏻‍👩🏿‍👧🏼‍👧🏾".AsTextElements().Should().Equal("👩🏻‍👦🏼", "👨🏽‍👦🏾‍👦🏿", "👩🏼‍👨🏽‍👦🏼‍👧🏽", "👩🏻‍👩🏿‍👧🏼‍👧🏾");
    }

    [TestMethod]
    public void TestTextElementCount()
    {
        "".TextElementCount().Should().Be(0);

        "abcdef".TextElementCount().Should().Be(6);

        "あいう".TextElementCount().Should().Be(3);

        "アイウエ".TextElementCount().Should().Be(4);

        "ｱｲｳｴｵ".TextElementCount().Should().Be(5);

        "1️⃣2️⃣3️⃣4️⃣5️⃣6️⃣7️⃣8️⃣9️⃣".TextElementCount().Should().Be(9);

        "👏🏻👏🏼👏🏽👏🏾👏🏿".TextElementCount().Should().Be(5);

        "🇯🇵🇬🇧🇺🇸".TextElementCount().Should().Be(3);

        // 👩🏻‍👦🏼👨🏽‍👦🏾‍👦🏿👩🏼‍👨🏽‍👦🏼‍👧🏽👩🏻‍👩🏿‍👧🏼‍👧🏾
        "👩🏻‍👦🏼👨🏽‍👦🏾‍👦🏿👩🏼‍👨🏽‍👦🏼‍👧🏽👩🏻‍👩🏿‍👧🏼‍👧🏾".TextElementCount().Should().Be(4);
    }

    [TestMethod]
    public void TestCutLeftElements()
    {
        // Empty
        "".CutLeftElements(0).Should().BeEmpty();
        default(string).CutLeftElements(0).Should().BeNull();
        "abcdef".CutLeftElements(0).Should().BeEmpty();

        "abcdef".CutLeftElements(0).Should().BeEmpty();
        "abcdef".CutLeftElements(1).Should().Be("a");
        "abcdef".CutLeftElements(2).Should().Be("ab");
        "abcdef".CutLeftElements(6).Should().Be("abcdef");
        "abcdef".CutLeftElements(7).Should().Be("abcdef");
        "abcdef".CutLeftElements(999).Should().Be("abcdef");

        "あいうえお".CutLeftElements(0).Should().BeEmpty();
        "あいうえお".CutLeftElements(1).Should().Be("あ");
        "あいうえお".CutLeftElements(5).Should().Be("あいうえお");
        "あいうえお".CutLeftElements(6).Should().Be("あいうえお");
        "あいうえお".CutLeftElements(999).Should().Be("あいうえお");

        "1️⃣2️⃣3️⃣4️⃣5️⃣6️⃣7️⃣8️⃣9️⃣".CutLeftElements(0).Should().BeEmpty();
        "1️⃣2️⃣3️⃣4️⃣5️⃣6️⃣7️⃣8️⃣9️⃣".CutLeftElements(1).Should().Be("1️⃣");
        "1️⃣2️⃣3️⃣4️⃣5️⃣6️⃣7️⃣8️⃣9️⃣".CutLeftElements(2).Should().Be("1️⃣2️⃣");
        "1️⃣2️⃣3️⃣4️⃣5️⃣6️⃣7️⃣8️⃣9️⃣".CutLeftElements(9).Should().Be("1️⃣2️⃣3️⃣4️⃣5️⃣6️⃣7️⃣8️⃣9️⃣");
        "1️⃣2️⃣3️⃣4️⃣5️⃣6️⃣7️⃣8️⃣9️⃣".CutLeftElements(10).Should().Be("1️⃣2️⃣3️⃣4️⃣5️⃣6️⃣7️⃣8️⃣9️⃣");

        "👏🏻👏🏼👏🏽👏🏾👏🏿".CutLeftElements(0).Should().BeEmpty();
        "👏🏻👏🏼👏🏽👏🏾👏🏿".CutLeftElements(1).Should().Be("👏🏻");
        "👏🏻👏🏼👏🏽👏🏾👏🏿".CutLeftElements(2).Should().Be("👏🏻👏🏼");
        "👏🏻👏🏼👏🏽👏🏾👏🏿".CutLeftElements(5).Should().Be("👏🏻👏🏼👏🏽👏🏾👏🏿");
        "👏🏻👏🏼👏🏽👏🏾👏🏿".CutLeftElements(6).Should().Be("👏🏻👏🏼👏🏽👏🏾👏🏿");

        "🇯🇵🇬🇧🇺🇸".CutLeftElements(0).Should().BeEmpty();
        "🇯🇵🇬🇧🇺🇸".CutLeftElements(1).Should().Be("🇯🇵");
        "🇯🇵🇬🇧🇺🇸".CutLeftElements(2).Should().Be("🇯🇵🇬🇧");
        "🇯🇵🇬🇧🇺🇸".CutLeftElements(3).Should().Be("🇯🇵🇬🇧🇺🇸");
        "🇯🇵🇬🇧🇺🇸".CutLeftElements(4).Should().Be("🇯🇵🇬🇧🇺🇸");

        // 👩🏻‍👦🏼👨🏽‍👦🏾‍👦🏿👩🏼‍👨🏽‍👦🏼‍👧🏽👩🏻‍👩🏿‍👧🏼‍👧🏾
        "👩🏻‍👦🏼👨🏽‍👦🏾‍👦🏿👩🏼‍👨🏽‍👦🏼‍👧🏽👩🏻‍👩🏿‍👧🏼‍👧🏾".CutLeftElements(1).Should().Be("👩🏻‍👦🏼");
        "👩🏻‍👦🏼👨🏽‍👦🏾‍👦🏿👩🏼‍👨🏽‍👦🏼‍👧🏽👩🏻‍👩🏿‍👧🏼‍👧🏾".CutLeftElements(2).Should().Be("👩🏻‍👦🏼👨🏽‍👦🏾‍👦🏿");
        "👩🏻‍👦🏼👨🏽‍👦🏾‍👦🏿👩🏼‍👨🏽‍👦🏼‍👧🏽👩🏻‍👩🏿‍👧🏼‍👧🏾".CutLeftElements(3).Should().Be("👩🏻‍👦🏼👨🏽‍👦🏾‍👦🏿👩🏼‍👨🏽‍👦🏼‍👧🏽");
        "👩🏻‍👦🏼👨🏽‍👦🏾‍👦🏿👩🏼‍👨🏽‍👦🏼‍👧🏽👩🏻‍👩🏿‍👧🏼‍👧🏾".CutLeftElements(4).Should().Be("👩🏻‍👦🏼👨🏽‍👦🏾‍👦🏿👩🏼‍👨🏽‍👦🏼‍👧🏽👩🏻‍👩🏿‍👧🏼‍👧🏾");
        "👩🏻‍👦🏼👨🏽‍👦🏾‍👦🏿👩🏼‍👨🏽‍👦🏼‍👧🏽👩🏻‍👩🏿‍👧🏼‍👧🏾".CutLeftElements(5).Should().Be("👩🏻‍👦🏼👨🏽‍👦🏾‍👦🏿👩🏼‍👨🏽‍👦🏼‍👧🏽👩🏻‍👩🏿‍👧🏼‍👧🏾");
        "👩🏻‍👦🏼👨🏽‍👦🏾‍👦🏿👩🏼‍👨🏽‍👦🏼‍👧🏽👩🏻‍👩🏿‍👧🏼‍👧🏾".CutLeftElements(999).Should().Be("👩🏻‍👦🏼👨🏽‍👦🏾‍👦🏿👩🏼‍👨🏽‍👦🏼‍👧🏽👩🏻‍👩🏿‍👧🏼‍👧🏾");
    }

    [TestMethod]
    public void TestCutRightElements()
    {
        // Empty
        "".CutRightElements(0).Should().BeEmpty();
        default(string).CutRightElements(0).Should().BeNull();
        "abcdef".CutRightElements(0).Should().BeEmpty();

        "abcdef".CutRightElements(0).Should().Be("");
        "abcdef".CutRightElements(1).Should().Be("f");
        "abcdef".CutRightElements(2).Should().Be("ef");
        "abcdef".CutRightElements(6).Should().Be("abcdef");
        "abcdef".CutRightElements(7).Should().Be("abcdef");
        "abcdef".CutRightElements(999).Should().Be("abcdef");

        "あいうえお".CutRightElements(1).Should().Be("お");
        "あいうえお".CutRightElements(5).Should().Be("あいうえお");
        "あいうえお".CutRightElements(6).Should().Be("あいうえお");
        "あいうえお".CutRightElements(999).Should().Be("あいうえお");

        "1️⃣2️⃣3️⃣4️⃣5️⃣6️⃣7️⃣8️⃣9️⃣".CutRightElements(0).Should().BeEmpty();
        "1️⃣2️⃣3️⃣4️⃣5️⃣6️⃣7️⃣8️⃣9️⃣".CutRightElements(1).Should().Be("9️⃣");
        "1️⃣2️⃣3️⃣4️⃣5️⃣6️⃣7️⃣8️⃣9️⃣".CutRightElements(2).Should().Be("8️⃣9️⃣");
        "1️⃣2️⃣3️⃣4️⃣5️⃣6️⃣7️⃣8️⃣9️⃣".CutRightElements(9).Should().Be("1️⃣2️⃣3️⃣4️⃣5️⃣6️⃣7️⃣8️⃣9️⃣");
        "1️⃣2️⃣3️⃣4️⃣5️⃣6️⃣7️⃣8️⃣9️⃣".CutRightElements(10).Should().Be("1️⃣2️⃣3️⃣4️⃣5️⃣6️⃣7️⃣8️⃣9️⃣");

        "👏🏻👏🏼👏🏽👏🏾👏🏿".CutRightElements(0).Should().BeEmpty();
        "👏🏻👏🏼👏🏽👏🏾👏🏿".CutRightElements(1).Should().Be("👏🏿");
        "👏🏻👏🏼👏🏽👏🏾👏🏿".CutRightElements(2).Should().Be("👏🏾👏🏿");
        "👏🏻👏🏼👏🏽👏🏾👏🏿".CutRightElements(5).Should().Be("👏🏻👏🏼👏🏽👏🏾👏🏿");
        "👏🏻👏🏼👏🏽👏🏾👏🏿".CutRightElements(6).Should().Be("👏🏻👏🏼👏🏽👏🏾👏🏿");

        "🇯🇵🇬🇧🇺🇸".CutRightElements(0).Should().BeEmpty();
        "🇯🇵🇬🇧🇺🇸".CutRightElements(1).Should().Be("🇺🇸");
        "🇯🇵🇬🇧🇺🇸".CutRightElements(2).Should().Be("🇬🇧🇺🇸");
        "🇯🇵🇬🇧🇺🇸".CutRightElements(3).Should().Be("🇯🇵🇬🇧🇺🇸");
        "🇯🇵🇬🇧🇺🇸".CutRightElements(4).Should().Be("🇯🇵🇬🇧🇺🇸");

        // 👩🏻‍👦🏼👨🏽‍👦🏾‍👦🏿👩🏼‍👨🏽‍👦🏼‍👧🏽👩🏻‍👩🏿‍👧🏼‍👧🏾
        "👩🏻‍👦🏼👨🏽‍👦🏾‍👦🏿👩🏼‍👨🏽‍👦🏼‍👧🏽👩🏻‍👩🏿‍👧🏼‍👧🏾".CutRightElements(1).Should().Be("👩🏻‍👩🏿‍👧🏼‍👧🏾");
        "👩🏻‍👦🏼👨🏽‍👦🏾‍👦🏿👩🏼‍👨🏽‍👦🏼‍👧🏽👩🏻‍👩🏿‍👧🏼‍👧🏾".CutRightElements(2).Should().Be("👩🏼‍👨🏽‍👦🏼‍👧🏽👩🏻‍👩🏿‍👧🏼‍👧🏾");
        "👩🏻‍👦🏼👨🏽‍👦🏾‍👦🏿👩🏼‍👨🏽‍👦🏼‍👧🏽👩🏻‍👩🏿‍👧🏼‍👧🏾".CutRightElements(3).Should().Be("👨🏽‍👦🏾‍👦🏿👩🏼‍👨🏽‍👦🏼‍👧🏽👩🏻‍👩🏿‍👧🏼‍👧🏾");
        "👩🏻‍👦🏼👨🏽‍👦🏾‍👦🏿👩🏼‍👨🏽‍👦🏼‍👧🏽👩🏻‍👩🏿‍👧🏼‍👧🏾".CutRightElements(4).Should().Be("👩🏻‍👦🏼👨🏽‍👦🏾‍👦🏿👩🏼‍👨🏽‍👦🏼‍👧🏽👩🏻‍👩🏿‍👧🏼‍👧🏾");
        "👩🏻‍👦🏼👨🏽‍👦🏾‍👦🏿👩🏼‍👨🏽‍👦🏼‍👧🏽👩🏻‍👩🏿‍👧🏼‍👧🏾".CutRightElements(5).Should().Be("👩🏻‍👦🏼👨🏽‍👦🏾‍👦🏿👩🏼‍👨🏽‍👦🏼‍👧🏽👩🏻‍👩🏿‍👧🏼‍👧🏾");
        "👩🏻‍👦🏼👨🏽‍👦🏾‍👦🏿👩🏼‍👨🏽‍👦🏼‍👧🏽👩🏻‍👩🏿‍👧🏼‍👧🏾".CutRightElements(999).Should().Be("👩🏻‍👦🏼👨🏽‍👦🏾‍👦🏿👩🏼‍👨🏽‍👦🏼‍👧🏽👩🏻‍👩🏿‍👧🏼‍👧🏾");
    }

    [TestMethod]
    public void TestEllipsisByLength_Marker()
    {
        "a".Length.Should().Be(1);
        "abcdefghi".EllipsisByLength(10, "...").Should().Be("abcdefghi");
        "abcdefghi".EllipsisByLength(9, "...").Should().Be("abcdefghi");
        "abcdefghi".EllipsisByLength(8, "...").Should().Be("abcde...");
        "abcdefghi".EllipsisByLength(4, "...").Should().Be("a...");
        "abcdefghi".EllipsisByLength(3, "...").Should().Be("...");

        "あ".Length.Should().Be(1);
        "あいうえおかきくけこ".EllipsisByLength(11, "**").Should().Be("あいうえおかきくけこ");
        "あいうえおかきくけこ".EllipsisByLength(10, "**").Should().Be("あいうえおかきくけこ");
        "あいうえおかきくけこ".EllipsisByLength(9, "**").Should().Be("あいうえおかき**");
        "あいうえおかきくけこ".EllipsisByLength(3, "**").Should().Be("あ**");
        "あいうえおかきくけこ".EllipsisByLength(2, "**").Should().Be("**");

        "1️⃣".Length.Should().Be(3);
        "1️⃣2️⃣3️⃣4️⃣5️⃣6️⃣7️⃣8️⃣9️⃣".EllipsisByLength(28, "@@").Should().Be("1️⃣2️⃣3️⃣4️⃣5️⃣6️⃣7️⃣8️⃣9️⃣");
        "1️⃣2️⃣3️⃣4️⃣5️⃣6️⃣7️⃣8️⃣9️⃣".EllipsisByLength(27, "@@").Should().Be("1️⃣2️⃣3️⃣4️⃣5️⃣6️⃣7️⃣8️⃣9️⃣");
        "1️⃣2️⃣3️⃣4️⃣5️⃣6️⃣7️⃣8️⃣9️⃣".EllipsisByLength(26, "@@").Should().Be("1️⃣2️⃣3️⃣4️⃣5️⃣6️⃣7️⃣8️⃣@@");
        "1️⃣2️⃣3️⃣4️⃣5️⃣6️⃣7️⃣8️⃣9️⃣".EllipsisByLength(7, "@@").Should().Be("1️⃣@@");
        "1️⃣2️⃣3️⃣4️⃣5️⃣6️⃣7️⃣8️⃣9️⃣".EllipsisByLength(5, "@@").Should().Be("1️⃣@@");
        "1️⃣2️⃣3️⃣4️⃣5️⃣6️⃣7️⃣8️⃣9️⃣".EllipsisByLength(2, "@@").Should().Be("@@");
        "abcdefghijklmnopqrstuvwxyz".EllipsisByLength(11, "1️⃣2️⃣3️⃣").Should().Be("ab1️⃣2️⃣3️⃣");
        "abcdefghijklmnopqrstuvwxyz".EllipsisByLength(10, "1️⃣2️⃣3️⃣").Should().Be("a1️⃣2️⃣3️⃣");
        "abcdefghijklmnopqrstuvwxyz".EllipsisByLength(9, "1️⃣2️⃣3️⃣").Should().Be("1️⃣2️⃣3️⃣");

        "👏🏻".Length.Should().Be(4);
        "👏🏻👏🏼👏🏽👏🏾👏🏿".EllipsisByLength(21, "??").Should().Be("👏🏻👏🏼👏🏽👏🏾👏🏿");
        "👏🏻👏🏼👏🏽👏🏾👏🏿".EllipsisByLength(20, "??").Should().Be("👏🏻👏🏼👏🏽👏🏾👏🏿");
        "👏🏻👏🏼👏🏽👏🏾👏🏿".EllipsisByLength(19, "??").Should().Be("👏🏻👏🏼👏🏽👏🏾??");
        "👏🏻👏🏼👏🏽👏🏾👏🏿".EllipsisByLength(9, "??").Should().Be("👏🏻??");
        "👏🏻👏🏼👏🏽👏🏾👏🏿".EllipsisByLength(6, "??").Should().Be("👏🏻??");
        "👏🏻👏🏼👏🏽👏🏾👏🏿".EllipsisByLength(2, "??").Should().Be("??");
        "abcdefghijklmnopqrstuvwxyz".EllipsisByLength(14, "👏🏻👏🏼👏🏽").Should().Be("ab👏🏻👏🏼👏🏽");
        "abcdefghijklmnopqrstuvwxyz".EllipsisByLength(13, "👏🏻👏🏼👏🏽").Should().Be("a👏🏻👏🏼👏🏽");
        "abcdefghijklmnopqrstuvwxyz".EllipsisByLength(12, "👏🏻👏🏼👏🏽").Should().Be("👏🏻👏🏼👏🏽");

        "🇯🇵".Length.Should().Be(4);
        "🇯🇵🇬🇧🇺🇸🇩🇪🇫🇷".EllipsisByLength(21, "!!").Should().Be("🇯🇵🇬🇧🇺🇸🇩🇪🇫🇷");
        "🇯🇵🇬🇧🇺🇸🇩🇪🇫🇷".EllipsisByLength(20, "!!").Should().Be("🇯🇵🇬🇧🇺🇸🇩🇪🇫🇷");
        "🇯🇵🇬🇧🇺🇸🇩🇪🇫🇷".EllipsisByLength(19, "!!").Should().Be("🇯🇵🇬🇧🇺🇸🇩🇪!!");
        "🇯🇵🇬🇧🇺🇸🇩🇪🇫🇷".EllipsisByLength(9, "!!").Should().Be("🇯🇵!!");
        "🇯🇵🇬🇧🇺🇸🇩🇪🇫🇷".EllipsisByLength(6, "!!").Should().Be("🇯🇵!!");
        "🇯🇵🇬🇧🇺🇸🇩🇪🇫🇷".EllipsisByLength(2, "!!").Should().Be("!!");
        "abcdefghijklmnopqrstuvwxyz".EllipsisByLength(10, "🇯🇵🇬🇧").Should().Be("ab🇯🇵🇬🇧");
        "abcdefghijklmnopqrstuvwxyz".EllipsisByLength(9, "🇯🇵🇬🇧").Should().Be("a🇯🇵🇬🇧");
        "abcdefghijklmnopqrstuvwxyz".EllipsisByLength(8, "🇯🇵🇬🇧").Should().Be("🇯🇵🇬🇧");

        // 👩🏻‍👦🏼👨🏽‍👦🏾‍👦🏿👩🏼‍👨🏽‍👦🏼‍👧🏽👩🏻‍👩🏿‍👧🏼‍👧🏾
        "👩🏻‍👦🏼".Length.Should().Be(9);
        "👨🏽‍👦🏾‍👦🏿".Length.Should().Be(14);
        "👩🏼‍👨🏽‍👦🏼‍👧🏽".Length.Should().Be(19);
        "👩🏻‍👩🏿‍👧🏼‍👧🏾".Length.Should().Be(19);
        "👩🏻‍👦🏼👨🏽‍👦🏾‍👦🏿👩🏼‍👨🏽‍👦🏼‍👧🏽👩🏻‍👩🏿‍👧🏼‍👧🏾".EllipsisByLength(9 + 14 + 19 + 19 + 1, "#").Should().Be("👩🏻‍👦🏼👨🏽‍👦🏾‍👦🏿👩🏼‍👨🏽‍👦🏼‍👧🏽👩🏻‍👩🏿‍👧🏼‍👧🏾");
        "👩🏻‍👦🏼👨🏽‍👦🏾‍👦🏿👩🏼‍👨🏽‍👦🏼‍👧🏽👩🏻‍👩🏿‍👧🏼‍👧🏾".EllipsisByLength(9 + 14 + 19 + 19, "#").Should().Be("👩🏻‍👦🏼👨🏽‍👦🏾‍👦🏿👩🏼‍👨🏽‍👦🏼‍👧🏽👩🏻‍👩🏿‍👧🏼‍👧🏾");
        "👩🏻‍👦🏼👨🏽‍👦🏾‍👦🏿👩🏼‍👨🏽‍👦🏼‍👧🏽👩🏻‍👩🏿‍👧🏼‍👧🏾".EllipsisByLength(9 + 14, "#").Should().Be("👩🏻‍👦🏼#");
        "👩🏻‍👦🏼👨🏽‍👦🏾‍👦🏿👩🏼‍👨🏽‍👦🏼‍👧🏽👩🏻‍👩🏿‍👧🏼‍👧🏾".EllipsisByLength(9 + 1, "#").Should().Be("👩🏻‍👦🏼#");
        "👩🏻‍👦🏼👨🏽‍👦🏾‍👦🏿👩🏼‍👨🏽‍👦🏼‍👧🏽👩🏻‍👩🏿‍👧🏼‍👧🏾".EllipsisByLength(9, "#").Should().Be("#");
        "👩🏻‍👦🏼👨🏽‍👦🏾‍👦🏿👩🏼‍👨🏽‍👦🏼‍👧🏽👩🏻‍👩🏿‍👧🏼‍👧🏾".EllipsisByLength(1, "#").Should().Be("#");
        "abcdefghijklmnopqrstuvwxyzabcdefghijklmnopqrstuvwxyz".EllipsisByLength(2 + 9 + 14 + 19, "👩🏻‍👦🏼👨🏽‍👦🏾‍👦🏿👩🏼‍👨🏽‍👦🏼‍👧🏽").Should().Be("ab👩🏻‍👦🏼👨🏽‍👦🏾‍👦🏿👩🏼‍👨🏽‍👦🏼‍👧🏽");
        "abcdefghijklmnopqrstuvwxyzabcdefghijklmnopqrstuvwxyz".EllipsisByLength(1 + 9 + 14 + 19, "👩🏻‍👦🏼👨🏽‍👦🏾‍👦🏿👩🏼‍👨🏽‍👦🏼‍👧🏽").Should().Be("a👩🏻‍👦🏼👨🏽‍👦🏾‍👦🏿👩🏼‍👨🏽‍👦🏼‍👧🏽");
        "abcdefghijklmnopqrstuvwxyzabcdefghijklmnopqrstuvwxyz".EllipsisByLength(0 + 9 + 14 + 19, "👩🏻‍👦🏼👨🏽‍👦🏾‍👦🏿👩🏼‍👨🏽‍👦🏼‍👧🏽").Should().Be("👩🏻‍👦🏼👨🏽‍👦🏾‍👦🏿👩🏼‍👨🏽‍👦🏼‍👧🏽");
    }

    [TestMethod]
    public void TestEllipsisByLength_NoMarker()
    {
        "a".Length.Should().Be(1);
        "abcdefghi".EllipsisByLength(10).Should().Be("abcdefghi");
        "abcdefghi".EllipsisByLength(9).Should().Be("abcdefghi");
        "abcdefghi".EllipsisByLength(1).Should().Be("a");
        "abcdefghi".EllipsisByLength(0).Should().BeEmpty();

        "あ".Length.Should().Be(1);
        "あいうえおかきくけこ".EllipsisByLength(11).Should().Be("あいうえおかきくけこ");
        "あいうえおかきくけこ".EllipsisByLength(10).Should().Be("あいうえおかきくけこ");
        "あいうえおかきくけこ".EllipsisByLength(9).Should().Be("あいうえおかきくけ");
        "あいうえおかきくけこ".EllipsisByLength(1).Should().Be("あ");
        "あいうえおかきくけこ".EllipsisByLength(0).Should().BeEmpty();

        "1️⃣".Length.Should().Be(3);
        "1️⃣2️⃣3️⃣4️⃣5️⃣6️⃣7️⃣8️⃣9️⃣".EllipsisByLength(28).Should().Be("1️⃣2️⃣3️⃣4️⃣5️⃣6️⃣7️⃣8️⃣9️⃣");
        "1️⃣2️⃣3️⃣4️⃣5️⃣6️⃣7️⃣8️⃣9️⃣".EllipsisByLength(27).Should().Be("1️⃣2️⃣3️⃣4️⃣5️⃣6️⃣7️⃣8️⃣9️⃣");
        "1️⃣2️⃣3️⃣4️⃣5️⃣6️⃣7️⃣8️⃣9️⃣".EllipsisByLength(26).Should().Be("1️⃣2️⃣3️⃣4️⃣5️⃣6️⃣7️⃣8️⃣");
        "1️⃣2️⃣3️⃣4️⃣5️⃣6️⃣7️⃣8️⃣9️⃣".EllipsisByLength(3).Should().Be("1️⃣");
        "1️⃣2️⃣3️⃣4️⃣5️⃣6️⃣7️⃣8️⃣9️⃣".EllipsisByLength(2).Should().BeEmpty();
        "1️⃣2️⃣3️⃣4️⃣5️⃣6️⃣7️⃣8️⃣9️⃣".EllipsisByLength(0).Should().BeEmpty();

        "👏🏻".Length.Should().Be(4);
        "👏🏻👏🏼👏🏽👏🏾👏🏿".EllipsisByLength(21).Should().Be("👏🏻👏🏼👏🏽👏🏾👏🏿");
        "👏🏻👏🏼👏🏽👏🏾👏🏿".EllipsisByLength(20).Should().Be("👏🏻👏🏼👏🏽👏🏾👏🏿");
        "👏🏻👏🏼👏🏽👏🏾👏🏿".EllipsisByLength(19).Should().Be("👏🏻👏🏼👏🏽👏🏾");
        "👏🏻👏🏼👏🏽👏🏾👏🏿".EllipsisByLength(4).Should().Be("👏🏻");
        "👏🏻👏🏼👏🏽👏🏾👏🏿".EllipsisByLength(3).Should().BeEmpty();
        "👏🏻👏🏼👏🏽👏🏾👏🏿".EllipsisByLength(0).Should().BeEmpty();

        "🇯🇵".Length.Should().Be(4);
        "🇯🇵🇬🇧🇺🇸🇩🇪🇫🇷".EllipsisByLength(21).Should().Be("🇯🇵🇬🇧🇺🇸🇩🇪🇫🇷");
        "🇯🇵🇬🇧🇺🇸🇩🇪🇫🇷".EllipsisByLength(20).Should().Be("🇯🇵🇬🇧🇺🇸🇩🇪🇫🇷");
        "🇯🇵🇬🇧🇺🇸🇩🇪🇫🇷".EllipsisByLength(19).Should().Be("🇯🇵🇬🇧🇺🇸🇩🇪");
        "🇯🇵🇬🇧🇺🇸🇩🇪🇫🇷".EllipsisByLength(4).Should().Be("🇯🇵");
        "🇯🇵🇬🇧🇺🇸🇩🇪🇫🇷".EllipsisByLength(3).Should().BeEmpty();
        "🇯🇵🇬🇧🇺🇸🇩🇪🇫🇷".EllipsisByLength(0).Should().BeEmpty();

        // 👩🏻‍👦🏼👨🏽‍👦🏾‍👦🏿👩🏼‍👨🏽‍👦🏼‍👧🏽👩🏻‍👩🏿‍👧🏼‍👧🏾
        "👩🏻‍👦🏼".Length.Should().Be(9);
        "👨🏽‍👦🏾‍👦🏿".Length.Should().Be(14);
        "👩🏼‍👨🏽‍👦🏼‍👧🏽".Length.Should().Be(19);
        "👩🏻‍👩🏿‍👧🏼‍👧🏾".Length.Should().Be(19);
        "👩🏻‍👦🏼👨🏽‍👦🏾‍👦🏿👩🏼‍👨🏽‍👦🏼‍👧🏽👩🏻‍👩🏿‍👧🏼‍👧🏾".EllipsisByLength(9 + 14 + 19 + 19 + 1).Should().Be("👩🏻‍👦🏼👨🏽‍👦🏾‍👦🏿👩🏼‍👨🏽‍👦🏼‍👧🏽👩🏻‍👩🏿‍👧🏼‍👧🏾");
        "👩🏻‍👦🏼👨🏽‍👦🏾‍👦🏿👩🏼‍👨🏽‍👦🏼‍👧🏽👩🏻‍👩🏿‍👧🏼‍👧🏾".EllipsisByLength(9 + 14 + 19 + 19).Should().Be("👩🏻‍👦🏼👨🏽‍👦🏾‍👦🏿👩🏼‍👨🏽‍👦🏼‍👧🏽👩🏻‍👩🏿‍👧🏼‍👧🏾");
        "👩🏻‍👦🏼👨🏽‍👦🏾‍👦🏿👩🏼‍👨🏽‍👦🏼‍👧🏽👩🏻‍👩🏿‍👧🏼‍👧🏾".EllipsisByLength(9 + 14 + 19 + 19 - 1).Should().Be("👩🏻‍👦🏼👨🏽‍👦🏾‍👦🏿👩🏼‍👨🏽‍👦🏼‍👧🏽");
        "👩🏻‍👦🏼👨🏽‍👦🏾‍👦🏿👩🏼‍👨🏽‍👦🏼‍👧🏽👩🏻‍👩🏿‍👧🏼‍👧🏾".EllipsisByLength(9).Should().Be("👩🏻‍👦🏼");
        "👩🏻‍👦🏼👨🏽‍👦🏾‍👦🏿👩🏼‍👨🏽‍👦🏼‍👧🏽👩🏻‍👩🏿‍👧🏼‍👧🏾".EllipsisByLength(9 - 1).Should().BeEmpty();
        "👩🏻‍👦🏼👨🏽‍👦🏾‍👦🏿👩🏼‍👨🏽‍👦🏼‍👧🏽👩🏻‍👩🏿‍👧🏼‍👧🏾".EllipsisByLength(0).Should().BeEmpty();
    }

    [TestMethod]
    public void TestEllipsisByLength_Error()
    {
        new Action(() => default(string).EllipsisByLength(3)).Should().Throw<Exception>();

        new Action(() => "abcdef".EllipsisByLength(-1)).Should().Throw<Exception>();

        new Action(() => "abcdef".EllipsisByLength(2, "xyz")).Should().Throw<Exception>();
        new Action(() => "".EllipsisByLength(2, "xyz")).Should().Throw<Exception>();
    }

    [TestMethod]
    public void TestEllipsisByElements_Marker()
    {
        "a".TextElementCount().Should().Be(1);
        "abcdefghi".EllipsisByElements(10, "...").Should().Be("abcdefghi");
        "abcdefghi".EllipsisByElements(9, "...").Should().Be("abcdefghi");
        "abcdefghi".EllipsisByElements(8, "...").Should().Be("abcde...");
        "abcdefghi".EllipsisByElements(4, "...").Should().Be("a...");
        "abcdefghi".EllipsisByElements(3, "...").Should().Be("...");

        "あ".TextElementCount().Should().Be(1);
        "あいうえおかきくけこ".EllipsisByElements(11, "**").Should().Be("あいうえおかきくけこ");
        "あいうえおかきくけこ".EllipsisByElements(10, "**").Should().Be("あいうえおかきくけこ");
        "あいうえおかきくけこ".EllipsisByElements(9, "**").Should().Be("あいうえおかき**");
        "あいうえおかきくけこ".EllipsisByElements(3, "**").Should().Be("あ**");
        "あいうえおかきくけこ".EllipsisByElements(2, "**").Should().Be("**");

        "1️⃣".TextElementCount().Should().Be(1);
        "1️⃣2️⃣3️⃣4️⃣5️⃣6️⃣7️⃣8️⃣9️⃣".EllipsisByElements(10, "@@").Should().Be("1️⃣2️⃣3️⃣4️⃣5️⃣6️⃣7️⃣8️⃣9️⃣");
        "1️⃣2️⃣3️⃣4️⃣5️⃣6️⃣7️⃣8️⃣9️⃣".EllipsisByElements(9, "@@").Should().Be("1️⃣2️⃣3️⃣4️⃣5️⃣6️⃣7️⃣8️⃣9️⃣");
        "1️⃣2️⃣3️⃣4️⃣5️⃣6️⃣7️⃣8️⃣9️⃣".EllipsisByElements(8, "@@").Should().Be("1️⃣2️⃣3️⃣4️⃣5️⃣6️⃣@@");
        "1️⃣2️⃣3️⃣4️⃣5️⃣6️⃣7️⃣8️⃣9️⃣".EllipsisByElements(3, "@@").Should().Be("1️⃣@@");
        "1️⃣2️⃣3️⃣4️⃣5️⃣6️⃣7️⃣8️⃣9️⃣".EllipsisByElements(2, "@@").Should().Be("@@");
        "abcdefghijklmnopqrstuvwxyz".EllipsisByElements(5, "1️⃣2️⃣3️⃣").Should().Be("ab1️⃣2️⃣3️⃣");
        "abcdefghijklmnopqrstuvwxyz".EllipsisByElements(4, "1️⃣2️⃣3️⃣").Should().Be("a1️⃣2️⃣3️⃣");
        "abcdefghijklmnopqrstuvwxyz".EllipsisByElements(3, "1️⃣2️⃣3️⃣").Should().Be("1️⃣2️⃣3️⃣");

        "👏🏻".TextElementCount().Should().Be(1);
        "👏🏻👏🏼👏🏽👏🏾👏🏿".EllipsisByElements(6, "??").Should().Be("👏🏻👏🏼👏🏽👏🏾👏🏿");
        "👏🏻👏🏼👏🏽👏🏾👏🏿".EllipsisByElements(5, "??").Should().Be("👏🏻👏🏼👏🏽👏🏾👏🏿");
        "👏🏻👏🏼👏🏽👏🏾👏🏿".EllipsisByElements(4, "??").Should().Be("👏🏻👏🏼??");
        "👏🏻👏🏼👏🏽👏🏾👏🏿".EllipsisByElements(3, "??").Should().Be("👏🏻??");
        "👏🏻👏🏼👏🏽👏🏾👏🏿".EllipsisByElements(2, "??").Should().Be("??");
        "abcdefghijklmnopqrstuvwxyz".EllipsisByElements(5, "👏🏻👏🏼👏🏽").Should().Be("ab👏🏻👏🏼👏🏽");
        "abcdefghijklmnopqrstuvwxyz".EllipsisByElements(4, "👏🏻👏🏼👏🏽").Should().Be("a👏🏻👏🏼👏🏽");
        "abcdefghijklmnopqrstuvwxyz".EllipsisByElements(3, "👏🏻👏🏼👏🏽").Should().Be("👏🏻👏🏼👏🏽");

        "🇯🇵".TextElementCount().Should().Be(1);
        "🇯🇵🇬🇧🇺🇸🇩🇪🇫🇷".EllipsisByElements(6, "!!").Should().Be("🇯🇵🇬🇧🇺🇸🇩🇪🇫🇷");
        "🇯🇵🇬🇧🇺🇸🇩🇪🇫🇷".EllipsisByElements(5, "!!").Should().Be("🇯🇵🇬🇧🇺🇸🇩🇪🇫🇷");
        "🇯🇵🇬🇧🇺🇸🇩🇪🇫🇷".EllipsisByElements(4, "!!").Should().Be("🇯🇵🇬🇧!!");
        "🇯🇵🇬🇧🇺🇸🇩🇪🇫🇷".EllipsisByElements(3, "!!").Should().Be("🇯🇵!!");
        "🇯🇵🇬🇧🇺🇸🇩🇪🇫🇷".EllipsisByElements(2, "!!").Should().Be("!!");
        "abcdefghijklmnopqrstuvwxyz".EllipsisByElements(4, "🇯🇵🇬🇧").Should().Be("ab🇯🇵🇬🇧");
        "abcdefghijklmnopqrstuvwxyz".EllipsisByElements(3, "🇯🇵🇬🇧").Should().Be("a🇯🇵🇬🇧");
        "abcdefghijklmnopqrstuvwxyz".EllipsisByElements(2, "🇯🇵🇬🇧").Should().Be("🇯🇵🇬🇧");

        // 👩🏻‍👦🏼👨🏽‍👦🏾‍👦🏿👩🏼‍👨🏽‍👦🏼‍👧🏽👩🏻‍👩🏿‍👧🏼‍👧🏾
        "👩🏻‍👦🏼".TextElementCount().Should().Be(1);
        "👨🏽‍👦🏾‍👦🏿".TextElementCount().Should().Be(1);
        "👩🏼‍👨🏽‍👦🏼‍👧🏽".TextElementCount().Should().Be(1);
        "👩🏻‍👩🏿‍👧🏼‍👧🏾".TextElementCount().Should().Be(1);
        "👩🏻‍👦🏼👨🏽‍👦🏾‍👦🏿👩🏼‍👨🏽‍👦🏼‍👧🏽👩🏻‍👩🏿‍👧🏼‍👧🏾".EllipsisByElements(5, "#").Should().Be("👩🏻‍👦🏼👨🏽‍👦🏾‍👦🏿👩🏼‍👨🏽‍👦🏼‍👧🏽👩🏻‍👩🏿‍👧🏼‍👧🏾");
        "👩🏻‍👦🏼👨🏽‍👦🏾‍👦🏿👩🏼‍👨🏽‍👦🏼‍👧🏽👩🏻‍👩🏿‍👧🏼‍👧🏾".EllipsisByElements(4, "#").Should().Be("👩🏻‍👦🏼👨🏽‍👦🏾‍👦🏿👩🏼‍👨🏽‍👦🏼‍👧🏽👩🏻‍👩🏿‍👧🏼‍👧🏾");
        "👩🏻‍👦🏼👨🏽‍👦🏾‍👦🏿👩🏼‍👨🏽‍👦🏼‍👧🏽👩🏻‍👩🏿‍👧🏼‍👧🏾".EllipsisByElements(3, "#").Should().Be("👩🏻‍👦🏼👨🏽‍👦🏾‍👦🏿#");
        "👩🏻‍👦🏼👨🏽‍👦🏾‍👦🏿👩🏼‍👨🏽‍👦🏼‍👧🏽👩🏻‍👩🏿‍👧🏼‍👧🏾".EllipsisByElements(2, "#").Should().Be("👩🏻‍👦🏼#");
        "👩🏻‍👦🏼👨🏽‍👦🏾‍👦🏿👩🏼‍👨🏽‍👦🏼‍👧🏽👩🏻‍👩🏿‍👧🏼‍👧🏾".EllipsisByElements(1, "#").Should().Be("#");
        "abcdefghijklmnopqrstuvwxyz".EllipsisByElements(5, "👩🏻‍👦🏼👨🏽‍👦🏾‍👦🏿👩🏼‍👨🏽‍👦🏼‍👧🏽").Should().Be("ab👩🏻‍👦🏼👨🏽‍👦🏾‍👦🏿👩🏼‍👨🏽‍👦🏼‍👧🏽");
        "abcdefghijklmnopqrstuvwxyz".EllipsisByElements(4, "👩🏻‍👦🏼👨🏽‍👦🏾‍👦🏿👩🏼‍👨🏽‍👦🏼‍👧🏽").Should().Be("a👩🏻‍👦🏼👨🏽‍👦🏾‍👦🏿👩🏼‍👨🏽‍👦🏼‍👧🏽");
        "abcdefghijklmnopqrstuvwxyz".EllipsisByElements(3, "👩🏻‍👦🏼👨🏽‍👦🏾‍👦🏿👩🏼‍👨🏽‍👦🏼‍👧🏽").Should().Be("👩🏻‍👦🏼👨🏽‍👦🏾‍👦🏿👩🏼‍👨🏽‍👦🏼‍👧🏽");
    }

    [TestMethod]
    public void TestEllipsisByElements_NoMarker()
    {
        "a".TextElementCount().Should().Be(1);
        "abcdefghi".EllipsisByElements(10).Should().Be("abcdefghi");
        "abcdefghi".EllipsisByElements(9).Should().Be("abcdefghi");
        "abcdefghi".EllipsisByElements(8).Should().Be("abcdefgh");
        "abcdefghi".EllipsisByElements(3).Should().Be("abc");
        "abcdefghi".EllipsisByElements(1).Should().Be("a");
        "abcdefghi".EllipsisByElements(0).Should().BeEmpty();

        "あ".TextElementCount().Should().Be(1);
        "あいうえおかきくけこ".EllipsisByElements(11).Should().Be("あいうえおかきくけこ");
        "あいうえおかきくけこ".EllipsisByElements(10).Should().Be("あいうえおかきくけこ");
        "あいうえおかきくけこ".EllipsisByElements(9).Should().Be("あいうえおかきくけ");
        "あいうえおかきくけこ".EllipsisByElements(2).Should().Be("あい");
        "あいうえおかきくけこ".EllipsisByElements(1).Should().Be("あ");
        "あいうえおかきくけこ".EllipsisByElements(0).Should().BeEmpty();

        "1️⃣".TextElementCount().Should().Be(1);
        "1️⃣2️⃣3️⃣4️⃣5️⃣6️⃣7️⃣8️⃣9️⃣".EllipsisByElements(10).Should().Be("1️⃣2️⃣3️⃣4️⃣5️⃣6️⃣7️⃣8️⃣9️⃣");
        "1️⃣2️⃣3️⃣4️⃣5️⃣6️⃣7️⃣8️⃣9️⃣".EllipsisByElements(9).Should().Be("1️⃣2️⃣3️⃣4️⃣5️⃣6️⃣7️⃣8️⃣9️⃣");
        "1️⃣2️⃣3️⃣4️⃣5️⃣6️⃣7️⃣8️⃣9️⃣".EllipsisByElements(8).Should().Be("1️⃣2️⃣3️⃣4️⃣5️⃣6️⃣7️⃣8️⃣");
        "1️⃣2️⃣3️⃣4️⃣5️⃣6️⃣7️⃣8️⃣9️⃣".EllipsisByElements(2).Should().Be("1️⃣2️⃣");
        "1️⃣2️⃣3️⃣4️⃣5️⃣6️⃣7️⃣8️⃣9️⃣".EllipsisByElements(1).Should().Be("1️⃣");
        "1️⃣2️⃣3️⃣4️⃣5️⃣6️⃣7️⃣8️⃣9️⃣".EllipsisByElements(0).Should().BeEmpty(); ;

        "👏🏻".TextElementCount().Should().Be(1);
        "👏🏻👏🏼👏🏽👏🏾👏🏿".EllipsisByElements(6).Should().Be("👏🏻👏🏼👏🏽👏🏾👏🏿");
        "👏🏻👏🏼👏🏽👏🏾👏🏿".EllipsisByElements(5).Should().Be("👏🏻👏🏼👏🏽👏🏾👏🏿");
        "👏🏻👏🏼👏🏽👏🏾👏🏿".EllipsisByElements(2).Should().Be("👏🏻👏🏼");
        "👏🏻👏🏼👏🏽👏🏾👏🏿".EllipsisByElements(1).Should().Be("👏🏻");
        "👏🏻👏🏼👏🏽👏🏾👏🏿".EllipsisByElements(0).Should().BeEmpty();

        "🇯🇵".TextElementCount().Should().Be(1);
        "🇯🇵🇬🇧🇺🇸🇩🇪🇫🇷".EllipsisByElements(6).Should().Be("🇯🇵🇬🇧🇺🇸🇩🇪🇫🇷");
        "🇯🇵🇬🇧🇺🇸🇩🇪🇫🇷".EllipsisByElements(5).Should().Be("🇯🇵🇬🇧🇺🇸🇩🇪🇫🇷");
        "🇯🇵🇬🇧🇺🇸🇩🇪🇫🇷".EllipsisByElements(2).Should().Be("🇯🇵🇬🇧");
        "🇯🇵🇬🇧🇺🇸🇩🇪🇫🇷".EllipsisByElements(1).Should().Be("🇯🇵");
        "🇯🇵🇬🇧🇺🇸🇩🇪🇫🇷".EllipsisByElements(0).Should().BeEmpty();

        // 👩🏻‍👦🏼👨🏽‍👦🏾‍👦🏿👩🏼‍👨🏽‍👦🏼‍👧🏽👩🏻‍👩🏿‍👧🏼‍👧🏾
        "👩🏻‍👦🏼".TextElementCount().Should().Be(1);
        "👨🏽‍👦🏾‍👦🏿".TextElementCount().Should().Be(1);
        "👩🏼‍👨🏽‍👦🏼‍👧🏽".TextElementCount().Should().Be(1);
        "👩🏻‍👩🏿‍👧🏼‍👧🏾".TextElementCount().Should().Be(1);
        "👩🏻‍👦🏼👨🏽‍👦🏾‍👦🏿👩🏼‍👨🏽‍👦🏼‍👧🏽👩🏻‍👩🏿‍👧🏼‍👧🏾".EllipsisByElements(5).Should().Be("👩🏻‍👦🏼👨🏽‍👦🏾‍👦🏿👩🏼‍👨🏽‍👦🏼‍👧🏽👩🏻‍👩🏿‍👧🏼‍👧🏾");
        "👩🏻‍👦🏼👨🏽‍👦🏾‍👦🏿👩🏼‍👨🏽‍👦🏼‍👧🏽👩🏻‍👩🏿‍👧🏼‍👧🏾".EllipsisByElements(4).Should().Be("👩🏻‍👦🏼👨🏽‍👦🏾‍👦🏿👩🏼‍👨🏽‍👦🏼‍👧🏽👩🏻‍👩🏿‍👧🏼‍👧🏾");
        "👩🏻‍👦🏼👨🏽‍👦🏾‍👦🏿👩🏼‍👨🏽‍👦🏼‍👧🏽👩🏻‍👩🏿‍👧🏼‍👧🏾".EllipsisByElements(2).Should().Be("👩🏻‍👦🏼👨🏽‍👦🏾‍👦🏿");
        "👩🏻‍👦🏼👨🏽‍👦🏾‍👦🏿👩🏼‍👨🏽‍👦🏼‍👧🏽👩🏻‍👩🏿‍👧🏼‍👧🏾".EllipsisByElements(1).Should().Be("👩🏻‍👦🏼");
        "👩🏻‍👦🏼👨🏽‍👦🏾‍👦🏿👩🏼‍👨🏽‍👦🏼‍👧🏽👩🏻‍👩🏿‍👧🏼‍👧🏾".EllipsisByElements(0).Should().BeEmpty();
    }

    [TestMethod]
    public void TestEllipsisByElements_Error()
    {
        new Action(() => default(string).EllipsisByElements(3)).Should().Throw<Exception>();

        new Action(() => "abcdef".EllipsisByElements(-1)).Should().Throw<Exception>();

        new Action(() => "abcdef".EllipsisByElements(2, "xyz")).Should().Throw<Exception>();
        new Action(() => "".EllipsisByElements(2, "xyz")).Should().Throw<Exception>();
    }
}