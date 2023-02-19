using System;
using System.Linq;
using CometFlavor.Extensions.Text;
using CometFlavor.Unicode.Extensions.Text;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestCometFlavor.Unicode.Extensions.Text;

[TestClass]
public class StringExtensionsTests
{
    // 以下の情報を参考にさせて頂いた。
    // ・Unicode 絵文字にまつわるあれこれ (絵文字の標準とプログラム上でのハンドリング)
    // 　https://qiita.com/_sobataro/items/47989ee4b573e0c2adfc

    [TestMethod]
    public void EvaluateEaw_Kind()
    {
        var measure = new EawMeasure(
            narrow: 1,
            wide: 2,
            half: 3,
            full: 4,
            neutral: 5,
            ambiguous: 6
        );

        // Narrow
        "a".EvaluateEaw(measure).Should().Be(1);
        "abcdef".EvaluateEaw(measure).Should().Be(1 * 6);

        // Wide
        "ア".EvaluateEaw(measure).Should().Be(2);
        "アイウエオ".EvaluateEaw(measure).Should().Be(2 * 5);

        // Halfwidth
        "ｱ".EvaluateEaw(measure).Should().Be(3);
        "ｱｲｳｴｵ".EvaluateEaw(measure).Should().Be(3 * 5);

        // Fullwidth
        "ａ".EvaluateEaw(measure).Should().Be(4);
        "ａｂｃｄｅｆ".EvaluateEaw(measure).Should().Be(4 * 6);

        // Neutral
        "©".EvaluateEaw(measure).Should().Be(5);
        "©©©©".EvaluateEaw(measure).Should().Be(5 * 4);

        // Ambiguous
        "§".EvaluateEaw(measure).Should().Be(6);
        "§¶¿¡".EvaluateEaw(measure).Should().Be(6 * 4);

        // mix
        "aａｱア🇯§".EvaluateEaw(measure).Should().Be(1 + 2 + 3 + 4 + 5 + 6);

        // ZERO WIDTH SPACE
        "\u200B".EvaluateEaw(measure).Should().Be(5);

        // 書記素クラスタは1つの文字として扱われ、構成しているコードポイントの中で最も大きな幅として評価になる値が採用される。

        // Emoji Combining Sequence
        "1️⃣".EvaluateEaw(measure).Should().Be(6);           // 1️⃣ (Narrow + Ambiguous + Neutral)
        "1️⃣1️⃣1️⃣".EvaluateEaw(measure).Should().Be(6 * 3);

        // Emoji Modifier Sequence (Narrow + Ambiguous + Neutral)
        "👏🏿".EvaluateEaw(measure).Should().Be(2);          // 👏🏿 (Wide + Wide)
        "👏🏿👏🏿👏🏿".EvaluateEaw(measure).Should().Be(2 * 3);

        // Emoji Flag Sequence
        "🇯🇵".EvaluateEaw(measure).Should().Be(5);           // 🇯🇵 (Neutral + Neutral)
        "🇯🇵🇯🇵🇯🇵".EvaluateEaw(measure).Should().Be(5 * 3);

        // Emoji ZWJ Sequence
        "👩🏻‍👩🏿‍👧🏼‍👧🏾".EvaluateEaw(measure).Should().Be(5); // 👩🏻‍👩🏿‍👧🏼‍👧🏾 (Wide + Wide + Neutral + Wide + Wide + Neutral + Wide + Wide + Neutral + Wide + Wide)
        "👩🏻‍👩🏿‍👧🏼‍👧🏾👩🏻‍👩🏿‍👧🏼‍👧🏾👩🏻‍👩🏿‍👧🏼‍👧🏾".EvaluateEaw(measure).Should().Be(5 * 3);
    }

    [TestMethod]
    public void EvaluateEaw_Width()
    {
        // Empty
        "".EvaluateEaw(new EawMeasure(1, 1, 1, 1, 1, 1)).Should().Be(0);
        default(string).EvaluateEaw(new EawMeasure(1, 1, 1, 1, 1, 1)).Should().Be(0);

        // Narrow
        "abcdef".EvaluateEaw(new EawMeasure(1, 0, 0, 0, 0, 0)).Should().Be(6 * 1);
        "abcdef".EvaluateEaw(new EawMeasure(2, 0, 0, 0, 0, 0)).Should().Be(6 * 2);
        "abcdef".EvaluateEaw(new EawMeasure(3, 0, 0, 0, 0, 0)).Should().Be(6 * 3);
        "abcdef".EvaluateEaw(new EawMeasure(4, 0, 0, 0, 0, 0)).Should().Be(6 * 4);

        // Wide
        "アイウエオ".EvaluateEaw(new EawMeasure(0, 1, 0, 0, 0, 0)).Should().Be(5 * 1);
        "アイウエオ".EvaluateEaw(new EawMeasure(0, 2, 0, 0, 0, 0)).Should().Be(5 * 2);
        "アイウエオ".EvaluateEaw(new EawMeasure(0, 3, 0, 0, 0, 0)).Should().Be(5 * 3);
        "アイウエオ".EvaluateEaw(new EawMeasure(0, 4, 0, 0, 0, 0)).Should().Be(5 * 4);

        // Halfwidth
        "ｱｲｳｴｵ".EvaluateEaw(new EawMeasure(0, 0, 1, 0, 0, 0)).Should().Be(5 * 1);
        "ｱｲｳｴｵ".EvaluateEaw(new EawMeasure(0, 0, 2, 0, 0, 0)).Should().Be(5 * 2);
        "ｱｲｳｴｵ".EvaluateEaw(new EawMeasure(0, 0, 3, 0, 0, 0)).Should().Be(5 * 3);
        "ｱｲｳｴｵ".EvaluateEaw(new EawMeasure(0, 0, 4, 0, 0, 0)).Should().Be(5 * 4);

        // Fullwidth
        "ａｂｃｄｅｆ".EvaluateEaw(new EawMeasure(0, 0, 0, 1, 0, 0)).Should().Be(6 * 1);
        "ａｂｃｄｅｆ".EvaluateEaw(new EawMeasure(0, 0, 0, 2, 0, 0)).Should().Be(6 * 2);
        "ａｂｃｄｅｆ".EvaluateEaw(new EawMeasure(0, 0, 0, 3, 0, 0)).Should().Be(6 * 3);
        "ａｂｃｄｅｆ".EvaluateEaw(new EawMeasure(0, 0, 0, 4, 0, 0)).Should().Be(6 * 4);

        // Neutral
        "©µ«»".EvaluateEaw(new EawMeasure(0, 0, 0, 0, 1, 0)).Should().Be(4 * 1);
        "©µ«»".EvaluateEaw(new EawMeasure(0, 0, 0, 0, 2, 0)).Should().Be(4 * 2);
        "©µ«»".EvaluateEaw(new EawMeasure(0, 0, 0, 0, 3, 0)).Should().Be(4 * 3);
        "©µ«»".EvaluateEaw(new EawMeasure(0, 0, 0, 0, 4, 0)).Should().Be(4 * 4);

        // Ambiguous
        "§¶¿¡".EvaluateEaw(new EawMeasure(0, 0, 0, 0, 0, 1)).Should().Be(4 * 1);
        "§¶¿¡".EvaluateEaw(new EawMeasure(0, 0, 0, 0, 0, 2)).Should().Be(4 * 2);
        "§¶¿¡".EvaluateEaw(new EawMeasure(0, 0, 0, 0, 0, 3)).Should().Be(4 * 3);
        "§¶¿¡".EvaluateEaw(new EawMeasure(0, 0, 0, 0, 0, 4)).Should().Be(4 * 4);
    }

    [TestMethod]
    public void EvaluateEaw_Error()
    {
        var measure = new EawMeasure(1, 1, 1);

        new Action(() => "abc".EvaluateEaw(null)).Should().Throw<Exception>();
        new Action(() => default(string).EvaluateEaw(measure)).Should().NotThrow();
        new Action(() => "".EvaluateEaw(measure)).Should().NotThrow();
    }

    [TestMethod]
    public void CutLeftEaw_Kind()
    {
        var measure = new EawMeasure(
            narrow: 1,
            wide: 2,
            half: 3,
            full: 4,
            neutral: 5,
            ambiguous: 6
        );

        // Narrow
        "abcdef".CutLeftEaw(0, measure).Should().Be("");
        "abcdef".CutLeftEaw(1, measure).Should().Be("a");
        "abcdef".CutLeftEaw(2, measure).Should().Be("ab");
        "abcdef".CutLeftEaw(6, measure).Should().Be("abcdef");
        "abcdef".CutLeftEaw(7, measure).Should().Be("abcdef");
        "abcdef".CutLeftEaw(999, measure).Should().Be("abcdef");

        // Wide
        "アイウエオ".CutLeftEaw(0, measure).Should().Be("");
        "アイウエオ".CutLeftEaw(1, measure).Should().Be("");
        "アイウエオ".CutLeftEaw(2, measure).Should().Be("ア");
        "アイウエオ".CutLeftEaw(3, measure).Should().Be("ア");
        "アイウエオ".CutLeftEaw(4, measure).Should().Be("アイ");
        "アイウエオ".CutLeftEaw(999, measure).Should().Be("アイウエオ");

        // Halfwidth
        "ｱｲｳｴｵ".CutLeftEaw(0, measure).Should().Be("");
        "ｱｲｳｴｵ".CutLeftEaw(2, measure).Should().Be("");
        "ｱｲｳｴｵ".CutLeftEaw(3, measure).Should().Be("ｱ");
        "ｱｲｳｴｵ".CutLeftEaw(5, measure).Should().Be("ｱ");
        "ｱｲｳｴｵ".CutLeftEaw(6, measure).Should().Be("ｱｲ");
        "ｱｲｳｴｵ".CutLeftEaw(999, measure).Should().Be("ｱｲｳｴｵ");

        // Fullwidth
        "ａｂｃｄｅｆ".CutLeftEaw(0, measure).Should().Be("");
        "ａｂｃｄｅｆ".CutLeftEaw(3, measure).Should().Be("");
        "ａｂｃｄｅｆ".CutLeftEaw(4, measure).Should().Be("ａ");
        "ａｂｃｄｅｆ".CutLeftEaw(7, measure).Should().Be("ａ");
        "ａｂｃｄｅｆ".CutLeftEaw(8, measure).Should().Be("ａｂ");
        "ａｂｃｄｅｆ".CutLeftEaw(999, measure).Should().Be("ａｂｃｄｅｆ");

        // Neutral
        "©µ«»".CutLeftEaw(0, measure).Should().Be("");
        "©µ«»".CutLeftEaw(4, measure).Should().Be("");
        "©µ«»".CutLeftEaw(5, measure).Should().Be("©");
        "©µ«»".CutLeftEaw(9, measure).Should().Be("©");
        "©µ«»".CutLeftEaw(10, measure).Should().Be("©µ");
        "©µ«»".CutLeftEaw(999, measure).Should().Be("©µ«»");

        // Ambiguous
        "§¶¿¡".CutLeftEaw(0, measure).Should().Be("");
        "§¶¿¡".CutLeftEaw(5, measure).Should().Be("");
        "§¶¿¡".CutLeftEaw(6, measure).Should().Be("§");
        "§¶¿¡".CutLeftEaw(11, measure).Should().Be("§");
        "§¶¿¡".CutLeftEaw(12, measure).Should().Be("§¶");

        // grapheme

        // Emoji Combining Sequence
        "1️⃣1️⃣1️⃣1️⃣1️⃣".CutLeftEaw(0, measure).Should().Be("");           // 1️⃣ (Narrow + Ambiguous + Neutral)
        "1️⃣1️⃣1️⃣1️⃣1️⃣".CutLeftEaw(5, measure).Should().Be("");
        "1️⃣1️⃣1️⃣1️⃣1️⃣".CutLeftEaw(6, measure).Should().Be("1️⃣");
        "1️⃣1️⃣1️⃣1️⃣1️⃣".CutLeftEaw(11, measure).Should().Be("1️⃣");
        "1️⃣1️⃣1️⃣1️⃣1️⃣".CutLeftEaw(12, measure).Should().Be("1️⃣1️⃣");
        "1️⃣1️⃣1️⃣1️⃣1️⃣".CutLeftEaw(999, measure).Should().Be("1️⃣1️⃣1️⃣1️⃣1️⃣");

        // Emoji Modifier Sequence (Narrow + Ambiguous + Neutral)
        "👏🏿👏🏿👏🏿".CutLeftEaw(0, measure).Should().Be("");          // 👏🏿 (Wide + Wide)
        "👏🏿👏🏿👏🏿".CutLeftEaw(1, measure).Should().Be("");
        "👏🏿👏🏿👏🏿".CutLeftEaw(2, measure).Should().Be("👏🏿");
        "👏🏿👏🏿👏🏿".CutLeftEaw(3, measure).Should().Be("👏🏿");
        "👏🏿👏🏿👏🏿".CutLeftEaw(4, measure).Should().Be("👏🏿👏🏿");
        "👏🏿👏🏿👏🏿".CutLeftEaw(999, measure).Should().Be("👏🏿👏🏿👏🏿");

        // Emoji Flag Sequence
        "🇯🇵🇯🇵🇯🇵🇯🇵".CutLeftEaw(0, measure).Should().Be("");              // 🇯🇵 (Neutral + Neutral)
        "🇯🇵🇯🇵🇯🇵🇯🇵".CutLeftEaw(4, measure).Should().Be("");
        "🇯🇵🇯🇵🇯🇵🇯🇵".CutLeftEaw(5, measure).Should().Be("🇯🇵");
        "🇯🇵🇯🇵🇯🇵🇯🇵".CutLeftEaw(9, measure).Should().Be("🇯🇵");
        "🇯🇵🇯🇵🇯🇵🇯🇵".CutLeftEaw(10, measure).Should().Be("🇯🇵🇯🇵");
        "🇯🇵🇯🇵🇯🇵🇯🇵".CutLeftEaw(999, measure).Should().Be("🇯🇵🇯🇵🇯🇵🇯🇵");

        // Emoji ZWJ Sequence
        "👩🏻‍👩🏿‍👧🏼‍👧🏾👩🏻‍👩🏿‍👧🏼‍👧🏾👩🏻‍👩🏿‍👧🏼‍👧🏾".CutLeftEaw(0, measure).Should().Be(""); // 👩🏻‍👩🏿‍👧🏼‍👧🏾 (Wide + Wide + Neutral + Wide + Wide + Neutral + Wide + Wide + Neutral + Wide + Wide)
        "👩🏻‍👩🏿‍👧🏼‍👧🏾👩🏻‍👩🏿‍👧🏼‍👧🏾👩🏻‍👩🏿‍👧🏼‍👧🏾".CutLeftEaw(4, measure).Should().Be("");
        "👩🏻‍👩🏿‍👧🏼‍👧🏾👩🏻‍👩🏿‍👧🏼‍👧🏾👩🏻‍👩🏿‍👧🏼‍👧🏾".CutLeftEaw(5, measure).Should().Be("👩🏻‍👩🏿‍👧🏼‍👧🏾");
        "👩🏻‍👩🏿‍👧🏼‍👧🏾👩🏻‍👩🏿‍👧🏼‍👧🏾👩🏻‍👩🏿‍👧🏼‍👧🏾".CutLeftEaw(9, measure).Should().Be("👩🏻‍👩🏿‍👧🏼‍👧🏾");
        "👩🏻‍👩🏿‍👧🏼‍👧🏾👩🏻‍👩🏿‍👧🏼‍👧🏾👩🏻‍👩🏿‍👧🏼‍👧🏾".CutLeftEaw(10, measure).Should().Be("👩🏻‍👩🏿‍👧🏼‍👧🏾👩🏻‍👩🏿‍👧🏼‍👧🏾");
        "👩🏻‍👩🏿‍👧🏼‍👧🏾👩🏻‍👩🏿‍👧🏼‍👧🏾👩🏻‍👩🏿‍👧🏼‍👧🏾".CutLeftEaw(999, measure).Should().Be("👩🏻‍👩🏿‍👧🏼‍👧🏾👩🏻‍👩🏿‍👧🏼‍👧🏾👩🏻‍👩🏿‍👧🏼‍👧🏾");
    }

    [TestMethod]
    public void CutLeftEaw_Width()
    {
        // Empty
        "".CutLeftEaw(3, new EawMeasure(1, 1, 1, 1, 1, 1)).Should().BeEmpty();
        default(string).CutLeftEaw(3, new EawMeasure(1, 1, 1, 1, 1, 1)).Should().BeNull();
        "abcdef".CutLeftEaw(0, new EawMeasure(1, 1, 1, 1, 1, 1)).Should().BeEmpty();

        // Narrow
        "abcdef".CutLeftEaw(3, new EawMeasure(1, 0, 0, 0, 0, 0)).Should().Be("abc");
        "abcdef".CutLeftEaw(3, new EawMeasure(2, 0, 0, 0, 0, 0)).Should().Be("a");
        "abcdef".CutLeftEaw(3, new EawMeasure(3, 0, 0, 0, 0, 0)).Should().Be("a");
        "abcdef".CutLeftEaw(3, new EawMeasure(4, 0, 0, 0, 0, 0)).Should().Be("");

        // Wide
        "アイウエオ".CutLeftEaw(3, new EawMeasure(0, 1, 0, 0, 0, 0)).Should().Be("アイウ");
        "アイウエオ".CutLeftEaw(3, new EawMeasure(0, 2, 0, 0, 0, 0)).Should().Be("ア");
        "アイウエオ".CutLeftEaw(3, new EawMeasure(0, 3, 0, 0, 0, 0)).Should().Be("ア");
        "アイウエオ".CutLeftEaw(3, new EawMeasure(0, 4, 0, 0, 0, 0)).Should().Be("");

        // Halfwidth
        "ｱｲｳｴｵ".CutLeftEaw(3, new EawMeasure(0, 0, 1, 0, 0, 0)).Should().Be("ｱｲｳ");
        "ｱｲｳｴｵ".CutLeftEaw(3, new EawMeasure(0, 0, 2, 0, 0, 0)).Should().Be("ｱ");
        "ｱｲｳｴｵ".CutLeftEaw(3, new EawMeasure(0, 0, 3, 0, 0, 0)).Should().Be("ｱ");
        "ｱｲｳｴｵ".CutLeftEaw(3, new EawMeasure(0, 0, 4, 0, 0, 0)).Should().Be("");

        // Fullwidth
        "ａｂｃｄｅｆ".CutLeftEaw(3, new EawMeasure(0, 0, 0, 1, 0, 0)).Should().Be("ａｂｃ");
        "ａｂｃｄｅｆ".CutLeftEaw(3, new EawMeasure(0, 0, 0, 2, 0, 0)).Should().Be("ａ");
        "ａｂｃｄｅｆ".CutLeftEaw(3, new EawMeasure(0, 0, 0, 3, 0, 0)).Should().Be("ａ");
        "ａｂｃｄｅｆ".CutLeftEaw(3, new EawMeasure(0, 0, 0, 4, 0, 0)).Should().Be("");

        // Neutral
        "©µ«»".CutLeftEaw(3, new EawMeasure(0, 0, 0, 0, 1, 0)).Should().Be("©µ«");
        "©µ«»".CutLeftEaw(3, new EawMeasure(0, 0, 0, 0, 2, 0)).Should().Be("©");
        "©µ«»".CutLeftEaw(3, new EawMeasure(0, 0, 0, 0, 3, 0)).Should().Be("©");
        "©µ«»".CutLeftEaw(3, new EawMeasure(0, 0, 0, 0, 4, 0)).Should().Be("");

        // Ambiguous
        "§¶¿¡".CutLeftEaw(3, new EawMeasure(0, 0, 0, 0, 0, 1)).Should().Be("§¶¿");
        "§¶¿¡".CutLeftEaw(3, new EawMeasure(0, 0, 0, 0, 0, 2)).Should().Be("§");
        "§¶¿¡".CutLeftEaw(3, new EawMeasure(0, 0, 0, 0, 0, 3)).Should().Be("§");
        "§¶¿¡".CutLeftEaw(3, new EawMeasure(0, 0, 0, 0, 0, 4)).Should().Be("");
    }

    [TestMethod]
    public void CutLeftEaw_Error()
    {
        var measure = new EawMeasure(1, 1, 1);

        new Action(() => "abc".CutLeftEaw(-1, measure)).Should().Throw<Exception>();
        new Action(() => "abc".CutLeftEaw(1, null)).Should().Throw<Exception>();
        new Action(() => default(string).CutRightEaw(1, measure)).Should().NotThrow();
        new Action(() => "".CutRightEaw(1, measure)).Should().NotThrow();
    }

    [TestMethod]
    public void CutRightEaw_Kind()
    {
        var measure = new EawMeasure(
            narrow: 1,
            wide: 2,
            half: 3,
            full: 4,
            neutral: 5,
            ambiguous: 6
        );

        // Narrow
        "abcdef".CutRightEaw(0, measure).Should().Be("");
        "abcdef".CutRightEaw(1, measure).Should().Be("f");
        "abcdef".CutRightEaw(2, measure).Should().Be("ef");
        "abcdef".CutRightEaw(6, measure).Should().Be("abcdef");
        "abcdef".CutRightEaw(7, measure).Should().Be("abcdef");
        "abcdef".CutRightEaw(999, measure).Should().Be("abcdef");

        // Wide
        "アイウエオ".CutRightEaw(0, measure).Should().Be("");
        "アイウエオ".CutRightEaw(1, measure).Should().Be("");
        "アイウエオ".CutRightEaw(2, measure).Should().Be("オ");
        "アイウエオ".CutRightEaw(3, measure).Should().Be("オ");
        "アイウエオ".CutRightEaw(4, measure).Should().Be("エオ");
        "アイウエオ".CutRightEaw(999, measure).Should().Be("アイウエオ");

        // Halfwidth
        "ｱｲｳｴｵ".CutRightEaw(0, measure).Should().Be("");
        "ｱｲｳｴｵ".CutRightEaw(2, measure).Should().Be("");
        "ｱｲｳｴｵ".CutRightEaw(3, measure).Should().Be("ｵ");
        "ｱｲｳｴｵ".CutRightEaw(5, measure).Should().Be("ｵ");
        "ｱｲｳｴｵ".CutRightEaw(6, measure).Should().Be("ｴｵ");
        "ｱｲｳｴｵ".CutRightEaw(999, measure).Should().Be("ｱｲｳｴｵ");

        // Fullwidth
        "ａｂｃｄｅｆ".CutRightEaw(0, measure).Should().Be("");
        "ａｂｃｄｅｆ".CutRightEaw(3, measure).Should().Be("");
        "ａｂｃｄｅｆ".CutRightEaw(4, measure).Should().Be("ｆ");
        "ａｂｃｄｅｆ".CutRightEaw(7, measure).Should().Be("ｆ");
        "ａｂｃｄｅｆ".CutRightEaw(8, measure).Should().Be("ｅｆ");
        "ａｂｃｄｅｆ".CutRightEaw(999, measure).Should().Be("ａｂｃｄｅｆ");

        // Neutral
        "©µ«»".CutRightEaw(0, measure).Should().Be("");
        "©µ«»".CutRightEaw(4, measure).Should().Be("");
        "©µ«»".CutRightEaw(5, measure).Should().Be("»");
        "©µ«»".CutRightEaw(9, measure).Should().Be("»");
        "©µ«»".CutRightEaw(10, measure).Should().Be("«»");
        "©µ«»".CutRightEaw(999, measure).Should().Be("©µ«»");

        // Ambiguous
        "§¶¿¡".CutRightEaw(0, measure).Should().Be("");
        "§¶¿¡".CutRightEaw(5, measure).Should().Be("");
        "§¶¿¡".CutRightEaw(6, measure).Should().Be("¡");
        "§¶¿¡".CutRightEaw(11, measure).Should().Be("¡");
        "§¶¿¡".CutRightEaw(12, measure).Should().Be("¿¡");

        // grapheme

        // Emoji Combining Sequence
        "1️⃣1️⃣1️⃣1️⃣1️⃣".CutRightEaw(0, measure).Should().Be("");           // 1️⃣ (Narrow + Ambiguous + Neutral)
        "1️⃣1️⃣1️⃣1️⃣1️⃣".CutRightEaw(5, measure).Should().Be("");
        "1️⃣1️⃣1️⃣1️⃣1️⃣".CutRightEaw(6, measure).Should().Be("1️⃣");
        "1️⃣1️⃣1️⃣1️⃣1️⃣".CutRightEaw(11, measure).Should().Be("1️⃣");
        "1️⃣1️⃣1️⃣1️⃣1️⃣".CutRightEaw(12, measure).Should().Be("1️⃣1️⃣");
        "1️⃣1️⃣1️⃣1️⃣1️⃣".CutRightEaw(999, measure).Should().Be("1️⃣1️⃣1️⃣1️⃣1️⃣");

        // Emoji Modifier Sequence (Narrow + Ambiguous + Neutral)
        "👏🏿👏🏿👏🏿".CutRightEaw(0, measure).Should().Be("");          // 👏🏿 (Wide + Wide)
        "👏🏿👏🏿👏🏿".CutRightEaw(1, measure).Should().Be("");
        "👏🏿👏🏿👏🏿".CutRightEaw(2, measure).Should().Be("👏🏿");
        "👏🏿👏🏿👏🏿".CutRightEaw(3, measure).Should().Be("👏🏿");
        "👏🏿👏🏿👏🏿".CutRightEaw(4, measure).Should().Be("👏🏿👏🏿");
        "👏🏿👏🏿👏🏿".CutRightEaw(999, measure).Should().Be("👏🏿👏🏿👏🏿");

        // Emoji Flag Sequence
        "🇯🇵🇯🇵🇯🇵🇯🇵".CutRightEaw(0, measure).Should().Be("");              // 🇯🇵 (Neutral + Neutral)
        "🇯🇵🇯🇵🇯🇵🇯🇵".CutRightEaw(4, measure).Should().Be("");
        "🇯🇵🇯🇵🇯🇵🇯🇵".CutRightEaw(5, measure).Should().Be("🇯🇵");
        "🇯🇵🇯🇵🇯🇵🇯🇵".CutRightEaw(9, measure).Should().Be("🇯🇵");
        "🇯🇵🇯🇵🇯🇵🇯🇵".CutRightEaw(10, measure).Should().Be("🇯🇵🇯🇵");
        "🇯🇵🇯🇵🇯🇵🇯🇵".CutRightEaw(999, measure).Should().Be("🇯🇵🇯🇵🇯🇵🇯🇵");

        // Emoji ZWJ Sequence
        "👩🏻‍👩🏿‍👧🏼‍👧🏾👩🏻‍👩🏿‍👧🏼‍👧🏾👩🏻‍👩🏿‍👧🏼‍👧🏾".CutRightEaw(0, measure).Should().Be(""); // 👩🏻‍👩🏿‍👧🏼‍👧🏾 (Wide + Wide + Neutral + Wide + Wide + Neutral + Wide + Wide + Neutral + Wide + Wide)
        "👩🏻‍👩🏿‍👧🏼‍👧🏾👩🏻‍👩🏿‍👧🏼‍👧🏾👩🏻‍👩🏿‍👧🏼‍👧🏾".CutRightEaw(4, measure).Should().Be("");
        "👩🏻‍👩🏿‍👧🏼‍👧🏾👩🏻‍👩🏿‍👧🏼‍👧🏾👩🏻‍👩🏿‍👧🏼‍👧🏾".CutRightEaw(5, measure).Should().Be("👩🏻‍👩🏿‍👧🏼‍👧🏾");
        "👩🏻‍👩🏿‍👧🏼‍👧🏾👩🏻‍👩🏿‍👧🏼‍👧🏾👩🏻‍👩🏿‍👧🏼‍👧🏾".CutRightEaw(9, measure).Should().Be("👩🏻‍👩🏿‍👧🏼‍👧🏾");
        "👩🏻‍👩🏿‍👧🏼‍👧🏾👩🏻‍👩🏿‍👧🏼‍👧🏾👩🏻‍👩🏿‍👧🏼‍👧🏾".CutRightEaw(10, measure).Should().Be("👩🏻‍👩🏿‍👧🏼‍👧🏾👩🏻‍👩🏿‍👧🏼‍👧🏾");
        "👩🏻‍👩🏿‍👧🏼‍👧🏾👩🏻‍👩🏿‍👧🏼‍👧🏾👩🏻‍👩🏿‍👧🏼‍👧🏾".CutRightEaw(999, measure).Should().Be("👩🏻‍👩🏿‍👧🏼‍👧🏾👩🏻‍👩🏿‍👧🏼‍👧🏾👩🏻‍👩🏿‍👧🏼‍👧🏾");
    }

    [TestMethod]
    public void CutRightEaw_Width()
    {
        // Empty
        "".CutRightEaw(3, new EawMeasure(1, 1, 1, 1, 1, 1)).Should().BeEmpty();
        default(string).CutRightEaw(3, new EawMeasure(1, 1, 1, 1, 1, 1)).Should().BeNull();
        "abcdef".CutRightEaw(0, new EawMeasure(1, 1, 1, 1, 1, 1)).Should().BeEmpty();

        // Narrow
        "abcdef".CutRightEaw(3, new EawMeasure(1, 0, 0, 0, 0, 0)).Should().Be("def");
        "abcdef".CutRightEaw(3, new EawMeasure(2, 0, 0, 0, 0, 0)).Should().Be("f");
        "abcdef".CutRightEaw(3, new EawMeasure(3, 0, 0, 0, 0, 0)).Should().Be("f");
        "abcdef".CutRightEaw(3, new EawMeasure(4, 0, 0, 0, 0, 0)).Should().Be("");

        // Wide
        "アイウエオ".CutRightEaw(3, new EawMeasure(0, 1, 0, 0, 0, 0)).Should().Be("ウエオ");
        "アイウエオ".CutRightEaw(3, new EawMeasure(0, 2, 0, 0, 0, 0)).Should().Be("オ");
        "アイウエオ".CutRightEaw(3, new EawMeasure(0, 3, 0, 0, 0, 0)).Should().Be("オ");
        "アイウエオ".CutRightEaw(3, new EawMeasure(0, 4, 0, 0, 0, 0)).Should().Be("");

        // Halfwidth
        "ｱｲｳｴｵ".CutRightEaw(3, new EawMeasure(0, 0, 1, 0, 0, 0)).Should().Be("ｳｴｵ");
        "ｱｲｳｴｵ".CutRightEaw(3, new EawMeasure(0, 0, 2, 0, 0, 0)).Should().Be("ｵ");
        "ｱｲｳｴｵ".CutRightEaw(3, new EawMeasure(0, 0, 3, 0, 0, 0)).Should().Be("ｵ");
        "ｱｲｳｴｵ".CutRightEaw(3, new EawMeasure(0, 0, 4, 0, 0, 0)).Should().Be("");

        // Fullwidth
        "ａｂｃｄｅｆ".CutRightEaw(3, new EawMeasure(0, 0, 0, 1, 0, 0)).Should().Be("ｄｅｆ");
        "ａｂｃｄｅｆ".CutRightEaw(3, new EawMeasure(0, 0, 0, 2, 0, 0)).Should().Be("ｆ");
        "ａｂｃｄｅｆ".CutRightEaw(3, new EawMeasure(0, 0, 0, 3, 0, 0)).Should().Be("ｆ");
        "ａｂｃｄｅｆ".CutRightEaw(3, new EawMeasure(0, 0, 0, 4, 0, 0)).Should().Be("");

        // Neutral
        "©µ«»".CutRightEaw(3, new EawMeasure(0, 0, 0, 0, 1, 0)).Should().Be("µ«»");
        "©µ«»".CutRightEaw(3, new EawMeasure(0, 0, 0, 0, 2, 0)).Should().Be("»");
        "©µ«»".CutRightEaw(3, new EawMeasure(0, 0, 0, 0, 3, 0)).Should().Be("»");
        "©µ«»".CutRightEaw(3, new EawMeasure(0, 0, 0, 0, 4, 0)).Should().Be("");

        // Ambiguous
        "§¶¿¡".CutRightEaw(3, new EawMeasure(0, 0, 0, 0, 0, 1)).Should().Be("¶¿¡");
        "§¶¿¡".CutRightEaw(3, new EawMeasure(0, 0, 0, 0, 0, 2)).Should().Be("¡");
        "§¶¿¡".CutRightEaw(3, new EawMeasure(0, 0, 0, 0, 0, 3)).Should().Be("¡");
        "§¶¿¡".CutRightEaw(3, new EawMeasure(0, 0, 0, 0, 0, 4)).Should().Be("");
    }

    [TestMethod]
    public void CutRightEaw_Error()
    {
        var measure = new EawMeasure(1, 1, 1);

        new Action(() => "abc".CutRightEaw(-1, measure)).Should().Throw<Exception>();
        new Action(() => "abc".CutRightEaw(1, null)).Should().Throw<Exception>();
        new Action(() => default(string).CutRightEaw(1, measure)).Should().NotThrow();
        new Action(() => "".CutRightEaw(1, measure)).Should().NotThrow();
    }

    [TestMethod]
    public void EllipsisByWidth_Marker()
    {
        // Narrow
        "abcdef".EllipsisByWidth(7, new EawMeasure(1, 0, 0, 0, 0, 0), "xyz").Should().Be("abcdef");
        "abcdef".EllipsisByWidth(6, new EawMeasure(1, 0, 0, 0, 0, 0), "xyz").Should().Be("abcdef");
        "abcdef".EllipsisByWidth(5, new EawMeasure(1, 0, 0, 0, 0, 0), "xyz").Should().Be("abxyz");
        "abcdef".EllipsisByWidth(4, new EawMeasure(1, 0, 0, 0, 0, 0), "xyz").Should().Be("axyz");

        "abcdef".EllipsisByWidth(19, new EawMeasure(3, 0, 0, 0, 0, 0), "xyz").Should().Be("abcdef");
        "abcdef".EllipsisByWidth(18, new EawMeasure(3, 0, 0, 0, 0, 0), "xyz").Should().Be("abcdef");
        "abcdef".EllipsisByWidth(17, new EawMeasure(3, 0, 0, 0, 0, 0), "xyz").Should().Be("abxyz");
        "abcdef".EllipsisByWidth(15, new EawMeasure(3, 0, 0, 0, 0, 0), "xyz").Should().Be("abxyz");
        "abcdef".EllipsisByWidth(14, new EawMeasure(3, 0, 0, 0, 0, 0), "xyz").Should().Be("axyz");

        // Wide
        "アイウエオ".EllipsisByWidth(6, new EawMeasure(0, 1, 0, 0, 0, 0), "カキク").Should().Be("アイウエオ");
        "アイウエオ".EllipsisByWidth(5, new EawMeasure(0, 1, 0, 0, 0, 0), "カキク").Should().Be("アイウエオ");
        "アイウエオ".EllipsisByWidth(4, new EawMeasure(0, 1, 0, 0, 0, 0), "カキク").Should().Be("アカキク");
        "アイウエオ".EllipsisByWidth(3, new EawMeasure(0, 1, 0, 0, 0, 0), "カキク").Should().Be("カキク");

        "アイウエオ".EllipsisByWidth(16, new EawMeasure(0, 3, 0, 0, 0, 0), "カキク").Should().Be("アイウエオ");
        "アイウエオ".EllipsisByWidth(15, new EawMeasure(0, 3, 0, 0, 0, 0), "カキク").Should().Be("アイウエオ");
        "アイウエオ".EllipsisByWidth(14, new EawMeasure(0, 3, 0, 0, 0, 0), "カキク").Should().Be("アカキク");
        "アイウエオ".EllipsisByWidth(12, new EawMeasure(0, 3, 0, 0, 0, 0), "カキク").Should().Be("アカキク");
        "アイウエオ".EllipsisByWidth(11, new EawMeasure(0, 3, 0, 0, 0, 0), "カキク").Should().Be("カキク");

        // Halfwidth
        "ｱｲｳｴｵ".EllipsisByWidth(6, new EawMeasure(0, 0, 1, 0, 0, 0), "ｶｷｸ").Should().Be("ｱｲｳｴｵ");
        "ｱｲｳｴｵ".EllipsisByWidth(5, new EawMeasure(0, 0, 1, 0, 0, 0), "ｶｷｸ").Should().Be("ｱｲｳｴｵ");
        "ｱｲｳｴｵ".EllipsisByWidth(4, new EawMeasure(0, 0, 1, 0, 0, 0), "ｶｷｸ").Should().Be("ｱｶｷｸ");
        "ｱｲｳｴｵ".EllipsisByWidth(3, new EawMeasure(0, 0, 1, 0, 0, 0), "ｶｷｸ").Should().Be("ｶｷｸ");

        "ｱｲｳｴｵ".EllipsisByWidth(16, new EawMeasure(0, 0, 3, 0, 0, 0), "ｶｷｸ").Should().Be("ｱｲｳｴｵ");
        "ｱｲｳｴｵ".EllipsisByWidth(15, new EawMeasure(0, 0, 3, 0, 0, 0), "ｶｷｸ").Should().Be("ｱｲｳｴｵ");
        "ｱｲｳｴｵ".EllipsisByWidth(14, new EawMeasure(0, 0, 3, 0, 0, 0), "ｶｷｸ").Should().Be("ｱｶｷｸ");
        "ｱｲｳｴｵ".EllipsisByWidth(12, new EawMeasure(0, 0, 3, 0, 0, 0), "ｶｷｸ").Should().Be("ｱｶｷｸ");
        "ｱｲｳｴｵ".EllipsisByWidth(11, new EawMeasure(0, 0, 3, 0, 0, 0), "ｶｷｸ").Should().Be("ｶｷｸ");

        // Fullwidth
        "ａｂｃｄｅｆ".EllipsisByWidth(7, new EawMeasure(0, 0, 0, 1, 0, 0), "ｘｙｚ").Should().Be("ａｂｃｄｅｆ");
        "ａｂｃｄｅｆ".EllipsisByWidth(6, new EawMeasure(0, 0, 0, 1, 0, 0), "ｘｙｚ").Should().Be("ａｂｃｄｅｆ");
        "ａｂｃｄｅｆ".EllipsisByWidth(5, new EawMeasure(0, 0, 0, 1, 0, 0), "ｘｙｚ").Should().Be("ａｂｘｙｚ");
        "ａｂｃｄｅｆ".EllipsisByWidth(4, new EawMeasure(0, 0, 0, 1, 0, 0), "ｘｙｚ").Should().Be("ａｘｙｚ");

        "ａｂｃｄｅｆ".EllipsisByWidth(19, new EawMeasure(0, 0, 0, 3, 0, 0), "ｘｙｚ").Should().Be("ａｂｃｄｅｆ");
        "ａｂｃｄｅｆ".EllipsisByWidth(18, new EawMeasure(0, 0, 0, 3, 0, 0), "ｘｙｚ").Should().Be("ａｂｃｄｅｆ");
        "ａｂｃｄｅｆ".EllipsisByWidth(17, new EawMeasure(0, 0, 0, 3, 0, 0), "ｘｙｚ").Should().Be("ａｂｘｙｚ");
        "ａｂｃｄｅｆ".EllipsisByWidth(15, new EawMeasure(0, 0, 0, 3, 0, 0), "ｘｙｚ").Should().Be("ａｂｘｙｚ");
        "ａｂｃｄｅｆ".EllipsisByWidth(14, new EawMeasure(0, 0, 0, 3, 0, 0), "ｘｙｚ").Should().Be("ａｘｙｚ");

        // Neutral
        "©µ«»⁜".EllipsisByWidth(6, new EawMeasure(0, 0, 0, 0, 1, 0), "‱‼‽").Should().Be("©µ«»⁜");
        "©µ«»⁜".EllipsisByWidth(5, new EawMeasure(0, 0, 0, 0, 1, 0), "‱‼‽").Should().Be("©µ«»⁜");
        "©µ«»⁜".EllipsisByWidth(4, new EawMeasure(0, 0, 0, 0, 1, 0), "‱‼‽").Should().Be("©‱‼‽");
        "©µ«»⁜".EllipsisByWidth(3, new EawMeasure(0, 0, 0, 0, 1, 0), "‱‼‽").Should().Be("‱‼‽");

        "©µ«»⁜".EllipsisByWidth(16, new EawMeasure(0, 0, 0, 0, 3, 0), "‱‼‽").Should().Be("©µ«»⁜");
        "©µ«»⁜".EllipsisByWidth(15, new EawMeasure(0, 0, 0, 0, 3, 0), "‱‼‽").Should().Be("©µ«»⁜");
        "©µ«»⁜".EllipsisByWidth(14, new EawMeasure(0, 0, 0, 0, 3, 0), "‱‼‽").Should().Be("©‱‼‽");
        "©µ«»⁜".EllipsisByWidth(12, new EawMeasure(0, 0, 0, 0, 3, 0), "‱‼‽").Should().Be("©‱‼‽");
        "©µ«»⁜".EllipsisByWidth(11, new EawMeasure(0, 0, 0, 0, 3, 0), "‱‼‽").Should().Be("‱‼‽");

        // Ambiguous
        "§¶¿¡®".EllipsisByWidth(6, new EawMeasure(0, 0, 0, 0, 0, 1), "±¼¾").Should().Be("§¶¿¡®");
        "§¶¿¡®".EllipsisByWidth(5, new EawMeasure(0, 0, 0, 0, 0, 1), "±¼¾").Should().Be("§¶¿¡®");
        "§¶¿¡®".EllipsisByWidth(4, new EawMeasure(0, 0, 0, 0, 0, 1), "±¼¾").Should().Be("§±¼¾");
        "§¶¿¡®".EllipsisByWidth(3, new EawMeasure(0, 0, 0, 0, 0, 1), "±¼¾").Should().Be("±¼¾");

        "§¶¿¡®".EllipsisByWidth(16, new EawMeasure(0, 0, 0, 0, 0, 3), "±¼¾").Should().Be("§¶¿¡®");
        "§¶¿¡®".EllipsisByWidth(15, new EawMeasure(0, 0, 0, 0, 0, 3), "±¼¾").Should().Be("§¶¿¡®");
        "§¶¿¡®".EllipsisByWidth(14, new EawMeasure(0, 0, 0, 0, 0, 3), "±¼¾").Should().Be("§±¼¾");
        "§¶¿¡®".EllipsisByWidth(12, new EawMeasure(0, 0, 0, 0, 0, 3), "±¼¾").Should().Be("§±¼¾");
        "§¶¿¡®".EllipsisByWidth(11, new EawMeasure(0, 0, 0, 0, 0, 3), "±¼¾").Should().Be("±¼¾");
    }

    [TestMethod]
    public void EllipsisByWidth_NoMarker()
    {
        // Narrow
        "abcdef".EllipsisByWidth(7, new EawMeasure(1, 0, 0, 0, 0, 0)).Should().Be("abcdef");
        "abcdef".EllipsisByWidth(6, new EawMeasure(1, 0, 0, 0, 0, 0)).Should().Be("abcdef");
        "abcdef".EllipsisByWidth(5, new EawMeasure(1, 0, 0, 0, 0, 0)).Should().Be("abcde");
        "abcdef".EllipsisByWidth(4, new EawMeasure(1, 0, 0, 0, 0, 0)).Should().Be("abcd");
        "abcdef".EllipsisByWidth(1, new EawMeasure(1, 0, 0, 0, 0, 0)).Should().Be("a");
        "abcdef".EllipsisByWidth(0, new EawMeasure(1, 0, 0, 0, 0, 0)).Should().BeEmpty();

        "abcdef".EllipsisByWidth(19, new EawMeasure(3, 0, 0, 0, 0, 0)).Should().Be("abcdef");
        "abcdef".EllipsisByWidth(18, new EawMeasure(3, 0, 0, 0, 0, 0)).Should().Be("abcdef");
        "abcdef".EllipsisByWidth(17, new EawMeasure(3, 0, 0, 0, 0, 0)).Should().Be("abcde");
        "abcdef".EllipsisByWidth(15, new EawMeasure(3, 0, 0, 0, 0, 0)).Should().Be("abcde");
        "abcdef".EllipsisByWidth(14, new EawMeasure(3, 0, 0, 0, 0, 0)).Should().Be("abcd");
        "abcdef".EllipsisByWidth(3, new EawMeasure(3, 0, 0, 0, 0, 0)).Should().Be("a");
        "abcdef".EllipsisByWidth(2, new EawMeasure(3, 0, 0, 0, 0, 0)).Should().BeEmpty();
        "abcdef".EllipsisByWidth(0, new EawMeasure(3, 0, 0, 0, 0, 0)).Should().BeEmpty();

        // Wide
        "アイウエオ".EllipsisByWidth(6, new EawMeasure(0, 1, 0, 0, 0, 0)).Should().Be("アイウエオ");
        "アイウエオ".EllipsisByWidth(5, new EawMeasure(0, 1, 0, 0, 0, 0)).Should().Be("アイウエオ");
        "アイウエオ".EllipsisByWidth(4, new EawMeasure(0, 1, 0, 0, 0, 0)).Should().Be("アイウエ");
        "アイウエオ".EllipsisByWidth(3, new EawMeasure(0, 1, 0, 0, 0, 0)).Should().Be("アイウ");
        "アイウエオ".EllipsisByWidth(1, new EawMeasure(0, 1, 0, 0, 0, 0)).Should().Be("ア");
        "アイウエオ".EllipsisByWidth(0, new EawMeasure(0, 1, 0, 0, 0, 0)).Should().BeEmpty();

        "アイウエオ".EllipsisByWidth(16, new EawMeasure(0, 3, 0, 0, 0, 0)).Should().Be("アイウエオ");
        "アイウエオ".EllipsisByWidth(15, new EawMeasure(0, 3, 0, 0, 0, 0)).Should().Be("アイウエオ");
        "アイウエオ".EllipsisByWidth(14, new EawMeasure(0, 3, 0, 0, 0, 0)).Should().Be("アイウエ");
        "アイウエオ".EllipsisByWidth(12, new EawMeasure(0, 3, 0, 0, 0, 0)).Should().Be("アイウエ");
        "アイウエオ".EllipsisByWidth(11, new EawMeasure(0, 3, 0, 0, 0, 0)).Should().Be("アイウ");
        "アイウエオ".EllipsisByWidth(3, new EawMeasure(0, 3, 0, 0, 0, 0)).Should().Be("ア");
        "アイウエオ".EllipsisByWidth(2, new EawMeasure(0, 3, 0, 0, 0, 0)).Should().BeEmpty();
        "アイウエオ".EllipsisByWidth(0, new EawMeasure(0, 3, 0, 0, 0, 0)).Should().BeEmpty();

        // Halfwidth
        "ｱｲｳｴｵ".EllipsisByWidth(6, new EawMeasure(0, 0, 1, 0, 0, 0)).Should().Be("ｱｲｳｴｵ");
        "ｱｲｳｴｵ".EllipsisByWidth(5, new EawMeasure(0, 0, 1, 0, 0, 0)).Should().Be("ｱｲｳｴｵ");
        "ｱｲｳｴｵ".EllipsisByWidth(4, new EawMeasure(0, 0, 1, 0, 0, 0)).Should().Be("ｱｲｳｴ");
        "ｱｲｳｴｵ".EllipsisByWidth(3, new EawMeasure(0, 0, 1, 0, 0, 0)).Should().Be("ｱｲｳ");
        "ｱｲｳｴｵ".EllipsisByWidth(1, new EawMeasure(0, 0, 1, 0, 0, 0)).Should().Be("ｱ");
        "ｱｲｳｴｵ".EllipsisByWidth(0, new EawMeasure(0, 0, 1, 0, 0, 0)).Should().BeEmpty();

        "ｱｲｳｴｵ".EllipsisByWidth(16, new EawMeasure(0, 0, 3, 0, 0, 0)).Should().Be("ｱｲｳｴｵ");
        "ｱｲｳｴｵ".EllipsisByWidth(15, new EawMeasure(0, 0, 3, 0, 0, 0)).Should().Be("ｱｲｳｴｵ");
        "ｱｲｳｴｵ".EllipsisByWidth(14, new EawMeasure(0, 0, 3, 0, 0, 0)).Should().Be("ｱｲｳｴ");
        "ｱｲｳｴｵ".EllipsisByWidth(12, new EawMeasure(0, 0, 3, 0, 0, 0)).Should().Be("ｱｲｳｴ");
        "ｱｲｳｴｵ".EllipsisByWidth(11, new EawMeasure(0, 0, 3, 0, 0, 0)).Should().Be("ｱｲｳ");
        "ｱｲｳｴｵ".EllipsisByWidth(3, new EawMeasure(0, 0, 3, 0, 0, 0)).Should().Be("ｱ");
        "ｱｲｳｴｵ".EllipsisByWidth(2, new EawMeasure(0, 0, 3, 0, 0, 0)).Should().BeEmpty();
        "ｱｲｳｴｵ".EllipsisByWidth(0, new EawMeasure(0, 0, 3, 0, 0, 0)).Should().BeEmpty();

        // Fullwidth
        "ａｂｃｄｅｆ".EllipsisByWidth(7, new EawMeasure(0, 0, 0, 1, 0, 0)).Should().Be("ａｂｃｄｅｆ");
        "ａｂｃｄｅｆ".EllipsisByWidth(6, new EawMeasure(0, 0, 0, 1, 0, 0)).Should().Be("ａｂｃｄｅｆ");
        "ａｂｃｄｅｆ".EllipsisByWidth(5, new EawMeasure(0, 0, 0, 1, 0, 0)).Should().Be("ａｂｃｄｅ");
        "ａｂｃｄｅｆ".EllipsisByWidth(4, new EawMeasure(0, 0, 0, 1, 0, 0)).Should().Be("ａｂｃｄ");
        "ａｂｃｄｅｆ".EllipsisByWidth(1, new EawMeasure(0, 0, 0, 1, 0, 0)).Should().Be("ａ");
        "ａｂｃｄｅｆ".EllipsisByWidth(0, new EawMeasure(0, 0, 0, 1, 0, 0)).Should().BeEmpty();

        "ａｂｃｄｅｆ".EllipsisByWidth(19, new EawMeasure(0, 0, 0, 3, 0, 0)).Should().Be("ａｂｃｄｅｆ");
        "ａｂｃｄｅｆ".EllipsisByWidth(18, new EawMeasure(0, 0, 0, 3, 0, 0)).Should().Be("ａｂｃｄｅｆ");
        "ａｂｃｄｅｆ".EllipsisByWidth(17, new EawMeasure(0, 0, 0, 3, 0, 0)).Should().Be("ａｂｃｄｅ");
        "ａｂｃｄｅｆ".EllipsisByWidth(15, new EawMeasure(0, 0, 0, 3, 0, 0)).Should().Be("ａｂｃｄｅ");
        "ａｂｃｄｅｆ".EllipsisByWidth(14, new EawMeasure(0, 0, 0, 3, 0, 0)).Should().Be("ａｂｃｄ");
        "ａｂｃｄｅｆ".EllipsisByWidth(3, new EawMeasure(0, 0, 0, 3, 0, 0)).Should().Be("ａ");
        "ａｂｃｄｅｆ".EllipsisByWidth(2, new EawMeasure(0, 0, 0, 3, 0, 0)).Should().BeEmpty();
        "ａｂｃｄｅｆ".EllipsisByWidth(0, new EawMeasure(0, 0, 0, 3, 0, 0)).Should().BeEmpty();

        // Neutral
        "©µ«»⁜".EllipsisByWidth(6, new EawMeasure(0, 0, 0, 0, 1, 0)).Should().Be("©µ«»⁜");
        "©µ«»⁜".EllipsisByWidth(5, new EawMeasure(0, 0, 0, 0, 1, 0)).Should().Be("©µ«»⁜");
        "©µ«»⁜".EllipsisByWidth(4, new EawMeasure(0, 0, 0, 0, 1, 0)).Should().Be("©µ«»");
        "©µ«»⁜".EllipsisByWidth(3, new EawMeasure(0, 0, 0, 0, 1, 0)).Should().Be("©µ«");
        "©µ«»⁜".EllipsisByWidth(1, new EawMeasure(0, 0, 0, 0, 1, 0)).Should().Be("©");
        "©µ«»⁜".EllipsisByWidth(0, new EawMeasure(0, 0, 0, 0, 1, 0)).Should().BeEmpty();

        "©µ«»⁜".EllipsisByWidth(16, new EawMeasure(0, 0, 0, 0, 3, 0)).Should().Be("©µ«»⁜");
        "©µ«»⁜".EllipsisByWidth(15, new EawMeasure(0, 0, 0, 0, 3, 0)).Should().Be("©µ«»⁜");
        "©µ«»⁜".EllipsisByWidth(14, new EawMeasure(0, 0, 0, 0, 3, 0)).Should().Be("©µ«»");
        "©µ«»⁜".EllipsisByWidth(12, new EawMeasure(0, 0, 0, 0, 3, 0)).Should().Be("©µ«»");
        "©µ«»⁜".EllipsisByWidth(11, new EawMeasure(0, 0, 0, 0, 3, 0)).Should().Be("©µ«");
        "©µ«»⁜".EllipsisByWidth(3, new EawMeasure(0, 0, 0, 0, 3, 0)).Should().Be("©");
        "©µ«»⁜".EllipsisByWidth(2, new EawMeasure(0, 0, 0, 0, 3, 0)).Should().BeEmpty();
        "©µ«»⁜".EllipsisByWidth(0, new EawMeasure(0, 0, 0, 0, 3, 0)).Should().BeEmpty();

        // Ambiguous
        "§¶¿¡®".EllipsisByWidth(6, new EawMeasure(0, 0, 0, 0, 0, 1)).Should().Be("§¶¿¡®");
        "§¶¿¡®".EllipsisByWidth(5, new EawMeasure(0, 0, 0, 0, 0, 1)).Should().Be("§¶¿¡®");
        "§¶¿¡®".EllipsisByWidth(4, new EawMeasure(0, 0, 0, 0, 0, 1)).Should().Be("§¶¿¡");
        "§¶¿¡®".EllipsisByWidth(3, new EawMeasure(0, 0, 0, 0, 0, 1)).Should().Be("§¶¿");
        "§¶¿¡®".EllipsisByWidth(1, new EawMeasure(0, 0, 0, 0, 0, 1)).Should().Be("§");
        "§¶¿¡®".EllipsisByWidth(0, new EawMeasure(0, 0, 0, 0, 0, 1)).Should().BeEmpty();

        "§¶¿¡®".EllipsisByWidth(16, new EawMeasure(0, 0, 0, 0, 0, 3)).Should().Be("§¶¿¡®");
        "§¶¿¡®".EllipsisByWidth(15, new EawMeasure(0, 0, 0, 0, 0, 3)).Should().Be("§¶¿¡®");
        "§¶¿¡®".EllipsisByWidth(14, new EawMeasure(0, 0, 0, 0, 0, 3)).Should().Be("§¶¿¡");
        "§¶¿¡®".EllipsisByWidth(12, new EawMeasure(0, 0, 0, 0, 0, 3)).Should().Be("§¶¿¡");
        "§¶¿¡®".EllipsisByWidth(11, new EawMeasure(0, 0, 0, 0, 0, 3)).Should().Be("§¶¿");
        "§¶¿¡®".EllipsisByWidth(3, new EawMeasure(0, 0, 0, 0, 0, 3)).Should().Be("§");
        "§¶¿¡®".EllipsisByWidth(2, new EawMeasure(0, 0, 0, 0, 0, 3)).Should().BeEmpty();
        "§¶¿¡®".EllipsisByWidth(0, new EawMeasure(0, 0, 0, 0, 0, 3)).Should().BeEmpty();
    }

    [TestMethod]
    public void EllipsisByWidth_Mix()
    {
        "aアｱａ©®".EllipsisByWidth(21, new EawMeasure(6, 5, 4, 3, 2, 1)).Should().Be("aアｱａ©®");
        "aアｱａ©®".EllipsisByWidth(20, new EawMeasure(6, 5, 4, 3, 2, 1)).Should().Be("aアｱａ©");
        "aアｱａ©®".EllipsisByWidth(19, new EawMeasure(6, 5, 4, 3, 2, 1)).Should().Be("aアｱａ");
        "aアｱａ©®".EllipsisByWidth(18, new EawMeasure(6, 5, 4, 3, 2, 1)).Should().Be("aアｱａ");
        "aアｱａ©®".EllipsisByWidth(17, new EawMeasure(6, 5, 4, 3, 2, 1)).Should().Be("aアｱ");
        "aアｱａ©®".EllipsisByWidth(15, new EawMeasure(6, 5, 4, 3, 2, 1)).Should().Be("aアｱ");
        "aアｱａ©®".EllipsisByWidth(14, new EawMeasure(6, 5, 4, 3, 2, 1)).Should().Be("aア");
        "aアｱａ©®".EllipsisByWidth(11, new EawMeasure(6, 5, 4, 3, 2, 1)).Should().Be("aア");
        "aアｱａ©®".EllipsisByWidth(10, new EawMeasure(6, 5, 4, 3, 2, 1)).Should().Be("a");
        "aアｱａ©®".EllipsisByWidth(6, new EawMeasure(6, 5, 4, 3, 2, 1)).Should().Be("a");
        "aアｱａ©®".EllipsisByWidth(5, new EawMeasure(6, 5, 4, 3, 2, 1)).Should().Be("");

        "aaaaa".EllipsisByWidth(4, new EawMeasure(1, 2, 0, 0, 0, 0), "ア").Should().Be("aaア");
        "aaaaa".EllipsisByWidth(3, new EawMeasure(1, 2, 0, 0, 0, 0), "ア").Should().Be("aア");
        "アアアアア".EllipsisByWidth(4, new EawMeasure(0, 1, 2, 0, 0, 0), "ｱ").Should().Be("アアｱ");
        "アアアアア".EllipsisByWidth(3, new EawMeasure(0, 1, 2, 0, 0, 0), "ｱ").Should().Be("アｱ");
        "ｱｱｱｱｱ".EllipsisByWidth(4, new EawMeasure(0, 0, 1, 2, 0, 0), "ａ").Should().Be("ｱｱａ");
        "ｱｱｱｱｱ".EllipsisByWidth(3, new EawMeasure(0, 0, 1, 2, 0, 0), "ａ").Should().Be("ｱａ");
        "ａａａａａ".EllipsisByWidth(4, new EawMeasure(0, 0, 0, 1, 2, 0), "©").Should().Be("ａａ©");
        "ａａａａａ".EllipsisByWidth(3, new EawMeasure(0, 0, 0, 1, 2, 0), "©").Should().Be("ａ©");
        "©©©©©".EllipsisByWidth(4, new EawMeasure(0, 0, 0, 0, 1, 2), "®").Should().Be("©©®");
        "©©©©©".EllipsisByWidth(3, new EawMeasure(0, 0, 0, 0, 1, 2), "®").Should().Be("©®");
    }

    [TestMethod]
    public void EllipsisByWidth_Grapheme()
    {
        var measure = new EawMeasure(
            narrow: 10,
            wide: 20,
            half: 30,
            full: 40,
            neutral: 50,
            ambiguous: 60
        );

        // 1️⃣ (Narrow + Ambiguous + Neutral)
        "1️⃣".TextElementCount().Should().Be(1);
        "1️⃣".EnumerateRunes().Count().Should().Be(3);
        "1️⃣".Length.Should().Be(3);
        "1️⃣".EvaluateEaw(measure).Should().Be(60);
        "1️⃣1️⃣1️⃣".EllipsisByWidth(181, measure).Should().Be("1️⃣1️⃣1️⃣");
        "1️⃣1️⃣1️⃣".EllipsisByWidth(180, measure).Should().Be("1️⃣1️⃣1️⃣");
        "1️⃣1️⃣1️⃣".EllipsisByWidth(179, measure).Should().Be("1️⃣1️⃣");
        "1️⃣1️⃣1️⃣".EllipsisByWidth(120, measure).Should().Be("1️⃣1️⃣");
        "1️⃣1️⃣1️⃣".EllipsisByWidth(119, measure).Should().Be("1️⃣");
        "1️⃣1️⃣1️⃣1️⃣1️⃣1️⃣".EllipsisByWidth(120 + 120, measure, "2️⃣2️⃣").Should().Be("1️⃣1️⃣2️⃣2️⃣");
        "1️⃣1️⃣1️⃣1️⃣1️⃣1️⃣".EllipsisByWidth(119 + 120, measure, "2️⃣2️⃣").Should().Be("1️⃣2️⃣2️⃣");
        "1️⃣1️⃣1️⃣1️⃣1️⃣1️⃣".EllipsisByWidth(60 + 120, measure, "2️⃣2️⃣").Should().Be("1️⃣2️⃣2️⃣");
        "1️⃣1️⃣1️⃣1️⃣1️⃣1️⃣".EllipsisByWidth(59 + 120, measure, "2️⃣2️⃣").Should().Be("2️⃣2️⃣");
        "1️⃣1️⃣1️⃣1️⃣1️⃣1️⃣".EllipsisByWidth(0 + 120, measure, "2️⃣2️⃣").Should().Be("2️⃣2️⃣");

        // 👏🏿 (Wide + Wide)
        "👏🏿".TextElementCount().Should().Be(1);
        "👏🏿".EnumerateRunes().Count().Should().Be(2);
        "👏🏿".Length.Should().Be(4);
        "👏🏿".EvaluateEaw(measure).Should().Be(20);
        "👏🏿👏🏿👏🏿".EllipsisByWidth(61, measure).Should().Be("👏🏿👏🏿👏🏿");
        "👏🏿👏🏿👏🏿".EllipsisByWidth(60, measure).Should().Be("👏🏿👏🏿👏🏿");
        "👏🏿👏🏿👏🏿".EllipsisByWidth(59, measure).Should().Be("👏🏿👏🏿");
        "👏🏿👏🏿👏🏿".EllipsisByWidth(40, measure).Should().Be("👏🏿👏🏿");
        "👏🏿👏🏿👏🏿".EllipsisByWidth(39, measure).Should().Be("👏🏿");
        "👏🏿👏🏿👏🏿👏🏿👏🏿👏🏿".EllipsisByWidth(40 + 40, measure, "👍🏿👍🏿").Should().Be("👏🏿👏🏿👍🏿👍🏿");
        "👏🏿👏🏿👏🏿👏🏿👏🏿👏🏿".EllipsisByWidth(39 + 40, measure, "👍🏿👍🏿").Should().Be("👏🏿👍🏿👍🏿");
        "👏🏿👏🏿👏🏿👏🏿👏🏿👏🏿".EllipsisByWidth(20 + 40, measure, "👍🏿👍🏿").Should().Be("👏🏿👍🏿👍🏿");
        "👏🏿👏🏿👏🏿👏🏿👏🏿👏🏿".EllipsisByWidth(19 + 40, measure, "👍🏿👍🏿").Should().Be("👍🏿👍🏿");
        "👏🏿👏🏿👏🏿👏🏿👏🏿👏🏿".EllipsisByWidth(0 + 40, measure, "👍🏿👍🏿").Should().Be("👍🏿👍🏿");

        // 🇯🇵 (Neutral + Neutral)
        "🇯🇵".TextElementCount().Should().Be(1);
        "🇯🇵".EnumerateRunes().Count().Should().Be(2);
        "🇯🇵".Length.Should().Be(4);
        "🇯🇵".EvaluateEaw(measure).Should().Be(50);
        "🇯🇵🇯🇵🇯🇵".EllipsisByWidth(151, measure).Should().Be("🇯🇵🇯🇵🇯🇵");
        "🇯🇵🇯🇵🇯🇵".EllipsisByWidth(150, measure).Should().Be("🇯🇵🇯🇵🇯🇵");
        "🇯🇵🇯🇵🇯🇵".EllipsisByWidth(149, measure).Should().Be("🇯🇵🇯🇵");
        "🇯🇵🇯🇵🇯🇵".EllipsisByWidth(100, measure).Should().Be("🇯🇵🇯🇵");
        "🇯🇵🇯🇵🇯🇵".EllipsisByWidth(99, measure).Should().Be("🇯🇵");
        "🇯🇵🇯🇵🇯🇵🇯🇵🇯🇵🇯🇵".EllipsisByWidth(100 + 100, measure, "🇬🇧🇬🇧").Should().Be("🇯🇵🇯🇵🇬🇧🇬🇧");
        "🇯🇵🇯🇵🇯🇵🇯🇵🇯🇵🇯🇵".EllipsisByWidth(99 + 100, measure, "🇬🇧🇬🇧").Should().Be("🇯🇵🇬🇧🇬🇧");
        "🇯🇵🇯🇵🇯🇵🇯🇵🇯🇵🇯🇵".EllipsisByWidth(50 + 100, measure, "🇬🇧🇬🇧").Should().Be("🇯🇵🇬🇧🇬🇧");
        "🇯🇵🇯🇵🇯🇵🇯🇵🇯🇵🇯🇵".EllipsisByWidth(49 + 100, measure, "🇬🇧🇬🇧").Should().Be("🇬🇧🇬🇧");
        "🇯🇵🇯🇵🇯🇵🇯🇵🇯🇵🇯🇵".EllipsisByWidth(0 + 100, measure, "🇬🇧🇬🇧").Should().Be("🇬🇧🇬🇧");

        // 👩🏻‍👩🏿‍👧🏼‍👧🏾 (Wide + Wide + Neutral + Wide + Wide + Neutral + Wide + Wide + Neutral + Wide + Wide)
        "👩🏻‍👩🏿‍👧🏼‍👧🏾".TextElementCount().Should().Be(1);
        "👩🏻‍👩🏿‍👧🏼‍👧🏾".EnumerateRunes().Count().Should().Be(11);
        "👩🏻‍👩🏿‍👧🏼‍👧🏾".Length.Should().Be(19);
        "👩🏻‍👩🏿‍👧🏼‍👧🏾".EvaluateEaw(measure).Should().Be(50);
        "👩🏻‍👩🏿‍👧🏼‍👧🏾👩🏻‍👩🏿‍👧🏼‍👧🏾👩🏻‍👩🏿‍👧🏼‍👧🏾".EllipsisByWidth(151, measure).Should().Be("👩🏻‍👩🏿‍👧🏼‍👧🏾👩🏻‍👩🏿‍👧🏼‍👧🏾👩🏻‍👩🏿‍👧🏼‍👧🏾");
        "👩🏻‍👩🏿‍👧🏼‍👧🏾👩🏻‍👩🏿‍👧🏼‍👧🏾👩🏻‍👩🏿‍👧🏼‍👧🏾".EllipsisByWidth(150, measure).Should().Be("👩🏻‍👩🏿‍👧🏼‍👧🏾👩🏻‍👩🏿‍👧🏼‍👧🏾👩🏻‍👩🏿‍👧🏼‍👧🏾");
        "👩🏻‍👩🏿‍👧🏼‍👧🏾👩🏻‍👩🏿‍👧🏼‍👧🏾👩🏻‍👩🏿‍👧🏼‍👧🏾".EllipsisByWidth(149, measure).Should().Be("👩🏻‍👩🏿‍👧🏼‍👧🏾👩🏻‍👩🏿‍👧🏼‍👧🏾");
        "👩🏻‍👩🏿‍👧🏼‍👧🏾👩🏻‍👩🏿‍👧🏼‍👧🏾👩🏻‍👩🏿‍👧🏼‍👧🏾".EllipsisByWidth(100, measure).Should().Be("👩🏻‍👩🏿‍👧🏼‍👧🏾👩🏻‍👩🏿‍👧🏼‍👧🏾");
        "👩🏻‍👩🏿‍👧🏼‍👧🏾👩🏻‍👩🏿‍👧🏼‍👧🏾👩🏻‍👩🏿‍👧🏼‍👧🏾".EllipsisByWidth(99, measure).Should().Be("👩🏻‍👩🏿‍👧🏼‍👧🏾");
        // 👩🏻‍👦🏼 (Wide + Wide + Neutral + Wide + Wide)
        "👩🏻‍👦🏼".EvaluateEaw(measure).Should().Be(50);
        "👩🏻‍👩🏿‍👧🏼‍👧🏾👩🏻‍👩🏿‍👧🏼‍👧🏾👩🏻‍👩🏿‍👧🏼‍👧🏾👩🏻‍👩🏿‍👧🏼‍👧🏾👩🏻‍👩🏿‍👧🏼‍👧🏾👩🏻‍👩🏿‍👧🏼‍👧🏾".EllipsisByWidth(100 + 100, measure, "👩🏻‍👦🏼👩🏻‍👦🏼").Should().Be("👩🏻‍👩🏿‍👧🏼‍👧🏾👩🏻‍👩🏿‍👧🏼‍👧🏾👩🏻‍👦🏼👩🏻‍👦🏼");
        "👩🏻‍👩🏿‍👧🏼‍👧🏾👩🏻‍👩🏿‍👧🏼‍👧🏾👩🏻‍👩🏿‍👧🏼‍👧🏾👩🏻‍👩🏿‍👧🏼‍👧🏾👩🏻‍👩🏿‍👧🏼‍👧🏾👩🏻‍👩🏿‍👧🏼‍👧🏾".EllipsisByWidth(99 + 100, measure, "👩🏻‍👦🏼👩🏻‍👦🏼").Should().Be("👩🏻‍👩🏿‍👧🏼‍👧🏾👩🏻‍👦🏼👩🏻‍👦🏼");
        "👩🏻‍👩🏿‍👧🏼‍👧🏾👩🏻‍👩🏿‍👧🏼‍👧🏾👩🏻‍👩🏿‍👧🏼‍👧🏾👩🏻‍👩🏿‍👧🏼‍👧🏾👩🏻‍👩🏿‍👧🏼‍👧🏾👩🏻‍👩🏿‍👧🏼‍👧🏾".EllipsisByWidth(50 + 100, measure, "👩🏻‍👦🏼👩🏻‍👦🏼").Should().Be("👩🏻‍👩🏿‍👧🏼‍👧🏾👩🏻‍👦🏼👩🏻‍👦🏼");
        "👩🏻‍👩🏿‍👧🏼‍👧🏾👩🏻‍👩🏿‍👧🏼‍👧🏾👩🏻‍👩🏿‍👧🏼‍👧🏾👩🏻‍👩🏿‍👧🏼‍👧🏾👩🏻‍👩🏿‍👧🏼‍👧🏾👩🏻‍👩🏿‍👧🏼‍👧🏾".EllipsisByWidth(49 + 100, measure, "👩🏻‍👦🏼👩🏻‍👦🏼").Should().Be("👩🏻‍👦🏼👩🏻‍👦🏼");
        "👩🏻‍👩🏿‍👧🏼‍👧🏾👩🏻‍👩🏿‍👧🏼‍👧🏾👩🏻‍👩🏿‍👧🏼‍👧🏾👩🏻‍👩🏿‍👧🏼‍👧🏾👩🏻‍👩🏿‍👧🏼‍👧🏾👩🏻‍👩🏿‍👧🏼‍👧🏾".EllipsisByWidth(0 + 100, measure, "👩🏻‍👦🏼👩🏻‍👦🏼").Should().Be("👩🏻‍👦🏼👩🏻‍👦🏼");
    }

    [TestMethod]
    public void EllipsisByWidth_Zero()
    {
        var zero = new EawMeasure(0, 0, 0, 0, 0, 0);

        "abcdef".EllipsisByWidth(1, zero, "xyz").Should().Be("abcdef");
        "アイウエオ".EllipsisByWidth(1, zero, "カキク").Should().Be("アイウエオ");
        "ｱｲｳｴｵ".EllipsisByWidth(6, zero, "ｶｷｸ").Should().Be("ｱｲｳｴｵ");
        "ａｂｃｄｅｆ".EllipsisByWidth(7, zero, "ｘｙｚ").Should().Be("ａｂｃｄｅｆ");
        "©µ«»⁜".EllipsisByWidth(6, zero, "‱‼‽").Should().Be("©µ«»⁜");
        "§¶¿¡®".EllipsisByWidth(6, zero, "±¼¾").Should().Be("§¶¿¡®");
    }

    [TestMethod]
    public void EllipsisByWidth_Error()
    {
        var measure = new EawMeasure(1, 1, 1);

        new Action(() => default(string).EllipsisByWidth(3, measure, "xyz")).Should().Throw<Exception>();

        new Action(() => "abcdef".EllipsisByWidth(-1, measure, "xyz")).Should().Throw<Exception>();

        new Action(() => "abcdef".EllipsisByWidth(3, null, "xyz")).Should().Throw<Exception>();

        new Action(() => "abcdef".EllipsisByWidth(3, measure, "xyz")).Should().NotThrow();
        new Action(() => "abcdef".EllipsisByWidth(2, measure, "xyz")).Should().Throw<Exception>();
        new Action(() => "".EllipsisByWidth(2, measure, "xyz")).Should().Throw<Exception>();
    }

}
