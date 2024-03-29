﻿using System;
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

    [TestMethod()]
    public void IsEmpty()
    {
        "".IsEmpty().Should().BeTrue();
        default(string).IsEmpty().Should().BeTrue();
        " ".IsEmpty().Should().BeFalse();
        "a".IsEmpty().Should().BeFalse();
    }

    [TestMethod()]
    public void IsNotEmpty()
    {
        "".IsNotEmpty().Should().BeFalse();
        default(string).IsNotEmpty().Should().BeFalse();
        " ".IsNotEmpty().Should().BeTrue();
        "a".IsNotEmpty().Should().BeTrue();
    }

    [TestMethod()]
    public void IsWhite()
    {
        "".IsWhite().Should().BeTrue();
        default(string).IsWhite().Should().BeTrue();
        " ".IsWhite().Should().BeTrue();
        "a".IsWhite().Should().BeFalse();
    }

    [TestMethod()]
    public void IsNotWhite()
    {
        "".IsNotWhite().Should().BeFalse();
        default(string).IsNotWhite().Should().BeFalse();
        " ".IsNotWhite().Should().BeFalse();
        "a".IsNotWhite().Should().BeTrue();
    }

    [TestMethod()]
    public void WhenEmpty()
    {
        "".WhenEmpty("x").Should().Be("x");
        default(string).WhenEmpty("x").Should().Be("x");
        " ".WhenEmpty("x").Should().Be(" ");
        "a".WhenEmpty("x").Should().Be("a");
        "".WhenEmpty(default(string)).Should().BeNull();
        " ".WhenEmpty(default(string)).Should().Be(" ");
        "a".WhenEmpty(default(string)).Should().Be("a");

        "".WhenEmpty(() => "x").Should().Be("x");
        default(string).WhenEmpty(() => "x").Should().Be("x");
        " ".WhenEmpty(() => "x").Should().Be(" ");
        "a".WhenEmpty(() => "x").Should().Be("a");
        "".WhenEmpty(() => null).Should().BeNull();
        " ".WhenEmpty(() => null).Should().Be(" ");
        "a".WhenEmpty(() => null).Should().Be("a");
    }

    [TestMethod()]
    public void WhenWhite()
    {
        "".WhenWhite("x").Should().Be("x");
        default(string).WhenWhite("x").Should().Be("x");
        " ".WhenWhite("x").Should().Be("x");
        "a".WhenWhite("x").Should().Be("a");
        "".WhenWhite(default(string)).Should().BeNull();
        " ".WhenWhite(default(string)).Should().BeNull();
        "a".WhenWhite(default(string)).Should().Be("a");

        "".WhenWhite(() => "x").Should().Be("x");
        default(string).WhenWhite(() => "x").Should().Be("x");
        " ".WhenWhite(() => "x").Should().Be("x");
        "a".WhenWhite(() => "x").Should().Be("a");
        "".WhenWhite(() => null).Should().BeNull();
        " ".WhenWhite(() => null).Should().BeNull();
        "a".WhenWhite(() => null).Should().Be("a");
    }

    [TestMethod]
    public void FirstLine()
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
    public void LastLine()
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
    public void BeforeAt_Char()
    {
        "abcdef".BeforeAt('a', defaultEmpty: false).Should().Be("");
        "abcdef".BeforeAt('c', defaultEmpty: false).Should().Be("ab");
        "abcdef".BeforeAt('f', defaultEmpty: false).Should().Be("abcde");
        "abcdef".BeforeAt('g', defaultEmpty: false).Should().Be("abcdef");
        "".BeforeAt('a', defaultEmpty: false).Should().BeEmpty();
        default(string).BeforeAt('a', defaultEmpty: false).Should().BeNull();

        "abcdef".BeforeAt('a', defaultEmpty: true).Should().Be("");
        "abcdef".BeforeAt('c', defaultEmpty: true).Should().Be("ab");
        "abcdef".BeforeAt('f', defaultEmpty: true).Should().Be("abcde");
        "abcdef".BeforeAt('g', defaultEmpty: true).Should().BeEmpty();
        "".BeforeAt('a', defaultEmpty: true).Should().BeEmpty();
        default(string).BeforeAt('a', defaultEmpty: true).Should().BeEmpty();
    }

    [TestMethod]
    public void BeforeAt_Str()
    {
        "abcdef".BeforeAt("ab", defaultEmpty: false).Should().Be("");
        "abcdef".BeforeAt("cd", defaultEmpty: false).Should().Be("ab");
        "abcdef".BeforeAt("ef", defaultEmpty: false).Should().Be("abcd");
        "abcdef".BeforeAt("ac", defaultEmpty: false).Should().Be("abcdef");
        "".BeforeAt("ab", defaultEmpty: false).Should().BeEmpty();
        default(string).BeforeAt("ab", defaultEmpty: false).Should().BeNull();

        "abcdef".BeforeAt("ab", defaultEmpty: true).Should().Be("");
        "abcdef".BeforeAt("cd", defaultEmpty: true).Should().Be("ab");
        "abcdef".BeforeAt("ef", defaultEmpty: true).Should().Be("abcd");
        "abcdef".BeforeAt("ac", defaultEmpty: true).Should().BeEmpty();
        "".BeforeAt("ab", defaultEmpty: true).Should().BeEmpty();
        default(string).BeforeAt("ab", defaultEmpty: true).Should().BeEmpty();
    }

    [TestMethod]
    public void AfterAt_Char()
    {
        "abcdef".AfterAt('a', defaultEmpty: false).Should().Be("bcdef");
        "abcdef".AfterAt('c', defaultEmpty: false).Should().Be("def");
        "abcdef".AfterAt('f', defaultEmpty: false).Should().Be("");
        "abcdef".AfterAt('g', defaultEmpty: false).Should().Be("abcdef");
        "".AfterAt('a', defaultEmpty: false).Should().BeEmpty();
        default(string).AfterAt('a', defaultEmpty: false).Should().BeNull();

        "abcdef".AfterAt('a', defaultEmpty: true).Should().Be("bcdef");
        "abcdef".AfterAt('c', defaultEmpty: true).Should().Be("def");
        "abcdef".AfterAt('f', defaultEmpty: true).Should().Be("");
        "abcdef".AfterAt('g', defaultEmpty: true).Should().BeEmpty();
        "".AfterAt('a', defaultEmpty: true).Should().BeEmpty();
        default(string).AfterAt('a', defaultEmpty: true).Should().BeEmpty();
    }

    [TestMethod]
    public void AfterAt_Str()
    {
        "abcdef".AfterAt("ab", defaultEmpty: false).Should().Be("cdef");
        "abcdef".AfterAt("cd", defaultEmpty: false).Should().Be("ef");
        "abcdef".AfterAt("ef", defaultEmpty: false).Should().Be("");
        "abcdef".AfterAt("ac", defaultEmpty: false).Should().Be("abcdef");
        "".AfterAt("ab", defaultEmpty: false).Should().BeEmpty();
        default(string).AfterAt("ab", defaultEmpty: false).Should().BeNull();

        "abcdef".AfterAt("ab", defaultEmpty: true).Should().Be("cdef");
        "abcdef".AfterAt("cd", defaultEmpty: true).Should().Be("ef");
        "abcdef".AfterAt("ef", defaultEmpty: true).Should().Be("");
        "abcdef".AfterAt("ac", defaultEmpty: true).Should().BeEmpty();
        "".AfterAt("ab", defaultEmpty: true).Should().BeEmpty();
        default(string).AfterAt("ab", defaultEmpty: true).Should().BeEmpty();
    }

    [TestMethod]
    public void TakeTo_Char()
    {
        "abcdef".TakeTo('a', defaultEmpty: false).Should().Be("a");
        "abcdef".TakeTo('c', defaultEmpty: false).Should().Be("abc");
        "abcdef".TakeTo('f', defaultEmpty: false).Should().Be("abcdef");
        "abcdef".TakeTo('g', defaultEmpty: false).Should().Be("abcdef");
        "".TakeTo('a', defaultEmpty: false).Should().BeEmpty();
        default(string).TakeTo('a', defaultEmpty: false).Should().BeNull();

        "abcdef".TakeTo('a', defaultEmpty: true).Should().Be("a");
        "abcdef".TakeTo('c', defaultEmpty: true).Should().Be("abc");
        "abcdef".TakeTo('f', defaultEmpty: true).Should().Be("abcdef");
        "abcdef".TakeTo('g', defaultEmpty: true).Should().BeEmpty();
        "".TakeTo('a', defaultEmpty: true).Should().BeEmpty();
        default(string).TakeTo('a', defaultEmpty: true).Should().BeEmpty();
    }

    [TestMethod]
    public void TakeTo_Str()
    {
        "abcdef".TakeTo("ab", defaultEmpty: false).Should().Be("ab");
        "abcdef".TakeTo("cd", defaultEmpty: false).Should().Be("abcd");
        "abcdef".TakeTo("ef", defaultEmpty: false).Should().Be("abcdef");
        "abcdef".TakeTo("ac", defaultEmpty: false).Should().Be("abcdef");
        "".TakeTo("ab", defaultEmpty: false).Should().BeEmpty();
        default(string).TakeTo("ab", defaultEmpty: false).Should().BeNull();

        "abcdef".TakeTo("ab", defaultEmpty: true).Should().Be("ab");
        "abcdef".TakeTo("cd", defaultEmpty: true).Should().Be("abcd");
        "abcdef".TakeTo("ef", defaultEmpty: true).Should().Be("abcdef");
        "abcdef".TakeTo("ac", defaultEmpty: true).Should().BeEmpty();
        "".TakeTo("ab", defaultEmpty: true).Should().BeEmpty();
        default(string).TakeTo("ab", defaultEmpty: true).Should().BeEmpty();
    }

    [TestMethod]
    public void TakeFrom_Char()
    {
        "abcdef".TakeFrom('a', defaultEmpty: false).Should().Be("abcdef");
        "abcdef".TakeFrom('c', defaultEmpty: false).Should().Be("cdef");
        "abcdef".TakeFrom('f', defaultEmpty: false).Should().Be("f");
        "abcdef".TakeFrom('g', defaultEmpty: false).Should().Be("abcdef");
        "".TakeFrom('a', defaultEmpty: false).Should().BeEmpty();
        default(string).TakeFrom('a', defaultEmpty: false).Should().BeNull();

        "abcdef".TakeFrom('a', defaultEmpty: true).Should().Be("abcdef");
        "abcdef".TakeFrom('c', defaultEmpty: true).Should().Be("cdef");
        "abcdef".TakeFrom('f', defaultEmpty: true).Should().Be("f");
        "abcdef".TakeFrom('g', defaultEmpty: true).Should().BeEmpty();
        "".TakeFrom('a', defaultEmpty: true).Should().BeEmpty();
        default(string).TakeFrom('a', defaultEmpty: true).Should().BeEmpty();
    }

    [TestMethod]
    public void TakeFrom_Str()
    {
        "abcdef".TakeFrom("ab", defaultEmpty: false).Should().Be("abcdef");
        "abcdef".TakeFrom("cd", defaultEmpty: false).Should().Be("cdef");
        "abcdef".TakeFrom("ef", defaultEmpty: false).Should().Be("ef");
        "abcdef".TakeFrom("ac", defaultEmpty: false).Should().Be("abcdef");
        "".TakeFrom("ab", defaultEmpty: false).Should().BeEmpty();
        default(string).TakeFrom("ab", defaultEmpty: false).Should().BeNull();

        "abcdef".TakeFrom("ab", defaultEmpty: true).Should().Be("abcdef");
        "abcdef".TakeFrom("cd", defaultEmpty: true).Should().Be("cdef");
        "abcdef".TakeFrom("ef", defaultEmpty: true).Should().Be("ef");
        "abcdef".TakeFrom("ac", defaultEmpty: true).Should().BeEmpty();
        "".TakeFrom("ab", defaultEmpty: true).Should().BeEmpty();
        default(string).TakeFrom("ab", defaultEmpty: true).Should().BeEmpty();
    }

    [TestMethod]
    public void JoinString()
    {
        new[] { "aaa", "bbb", "ccc" }.JoinString().Should().Be("aaabbbccc");
        new[] { "aaa", "bbb", "ccc" }.JoinString("/").Should().Be("aaa/bbb/ccc");
        new[] { "aaa", "", "ccc" }.JoinString().Should().Be("aaaccc");
        new[] { "aaa", "", "ccc" }.JoinString("/").Should().Be("aaa//ccc");
        new[] { "aaa", null, "ccc" }.JoinString().Should().Be("aaaccc");
        new[] { "aaa", null, "ccc" }.JoinString("/").Should().Be("aaa//ccc");
    }

    [TestMethod]
    public void DropEmpty()
    {
        new[] { "aaa", "bbb", "ccc" }.DropEmpty().Should().Equal("aaa", "bbb", "ccc");
        new[] { "aaa", null, "ccc" }.DropEmpty().Should().Equal("aaa", "ccc");
        new[] { "", "bbb", "ccc" }.DropEmpty().Should().Equal("bbb", "ccc");
        new[] { "", "bbb", " " }.DropEmpty().Should().Equal("bbb", " ");
    }

    [TestMethod]
    public void DropWhite()
    {
        new[] { "aaa", "bbb", "ccc" }.DropWhite().Should().Equal("aaa", "bbb", "ccc");
        new[] { "aaa", null, "ccc" }.DropWhite().Should().Equal("aaa", "ccc");
        new[] { "", "bbb", "ccc" }.DropWhite().Should().Equal("bbb", "ccc");
        new[] { "", "bbb", " " }.DropWhite().Should().Equal("bbb");
    }

    [TestMethod]
    public void Decorate_Format()
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
    public void Decorate_Delegate()
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
    public void Quote()
    {
        "abc".Quote().Should().Be("\"abc\"");
        "a\"bc".Quote().Should().Be("\"a\"\"bc\"");

        "abc".Quote(quote: '\'').Should().Be("'abc'");
        "ab'c".Quote(quote: '\'').Should().Be("'ab''c'");
        "ab\"c".Quote(quote: '\'').Should().Be("'ab\"c'");

        "abc".Quote(quote: '\'', escape: '/').Should().Be("'abc'");
        "ab'c".Quote(quote: '\'', escape: '/').Should().Be("'ab/'c'");
    }

    [TestMethod]
    public void Unquote()
    {
        "\"abc\"".Unquote().Should().Be("abc");
        "\"a\"\"bc\"".Unquote().Should().Be("a\"bc");

        "'abc'".Unquote(quotes: new[] { '\'' }).Should().Be("abc");
        "'ab''c'".Unquote(quotes: new[] { '\'' }).Should().Be("ab'c");
        "'ab\"c'".Unquote(quotes: new[] { '\'' }).Should().Be("ab\"c");

        "'abc'".Unquote(quotes: new[] { '\'' }, escape: '/').Should().Be("abc");
        "'ab/'c'".Unquote(quotes: new[] { '\'' }, escape: '/').Should().Be("ab'c");
    }

    [TestMethod]
    public void AsTextLines()
    {
        "".AsTextLines().Should().Equal("");
        "a".AsTextLines().Should().Equal("a");
        "a\rb\nc".AsTextLines().Should().Equal("a", "b", "c");
        "a\r\nb\n\rc".AsTextLines().Should().Equal("a", "b", "", "c");
        "\ra\n".AsTextLines().Should().Equal("", "a", "");
        "\r".AsTextLines().Should().Equal("", "");
        "\n".AsTextLines().Should().Equal("", "");
        "\r\n".AsTextLines().Should().Equal("", "");
        "\n\r".AsTextLines().Should().Equal("", "", "");
    }

    [TestMethod]
    public void AsTextElements()
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
    public void TextElementCount()
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
    public void CutLeftElements()
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
    public void CutRightElements()
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
    public void EllipsisByLength_Marker()
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
    public void EllipsisByLength_NoMarker()
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
    public void EllipsisByLength_Error()
    {
        new Action(() => default(string).EllipsisByLength(3)).Should().Throw<Exception>();

        new Action(() => "abcdef".EllipsisByLength(-1)).Should().Throw<Exception>();

        new Action(() => "abcdef".EllipsisByLength(2, "xyz")).Should().Throw<Exception>();
        new Action(() => "".EllipsisByLength(2, "xyz")).Should().Throw<Exception>();
    }

    [TestMethod]
    public void EllipsisByElements_Marker()
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
    public void EllipsisByElements_NoMarker()
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
    public void EllipsisByElements_Error()
    {
        new Action(() => default(string).EllipsisByElements(3)).Should().Throw<Exception>();

        new Action(() => "abcdef".EllipsisByElements(-1)).Should().Throw<Exception>();

        new Action(() => "abcdef".EllipsisByElements(2, "xyz")).Should().Throw<Exception>();
        new Action(() => "".EllipsisByElements(2, "xyz")).Should().Throw<Exception>();
    }
}