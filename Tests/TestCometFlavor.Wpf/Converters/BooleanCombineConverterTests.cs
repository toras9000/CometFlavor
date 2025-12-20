using System.Windows;
using CometFlavor.Wpf.Converters;
using AwesomeAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestCometFlavor.Wpf.Converters;

[TestClass]
public class BooleanCombineConverterTests
{
    [TestMethod]
    public void Construct()
    {
        var target = new BooleanCombineConverter();
        target.Mode.Should().Be(BooleanCombineConverter.CombineMode.AllTrue);
        target.IgnoreNotBool.Should().Be(false);
    }

    [TestMethod]
    public void Convert_AllTrue_NeedBool()
    {
        var target = new BooleanCombineConverter();
        target.Mode = BooleanCombineConverter.CombineMode.AllTrue;
        target.IgnoreNotBool = false;

        target.Convert(new object[] { true, }, null, null, null).Should().Be(true);
        target.Convert(new object[] { true, true, true, }, null, null, null).Should().Be(true);

        target.Convert(new object[] { false, }, null, null, null).Should().Be(false);
        target.Convert(new object[] { true, false, true, }, null, null, null).Should().Be(false);
        target.Convert(new object[] { false, false, false, }, null, null, null).Should().Be(false);

        // 空の場合は変換不可
        target.Convert(new object[] { }, null, null, null).Should().Be(DependencyProperty.UnsetValue);
        target.Convert(null, null, null, null).Should().Be(DependencyProperty.UnsetValue);

        // 非boolを無視しない設定時は変換不可になる
        target.Convert(new object[] { true, 1, true, }, null, null, null).Should().Be(DependencyProperty.UnsetValue);
        target.Convert(new object[] { true, "true", true, }, null, null, null).Should().Be(DependencyProperty.UnsetValue);
        target.Convert(new object[] { false, 1, false, }, null, null, null).Should().Be(DependencyProperty.UnsetValue);
        target.Convert(new object[] { false, "true", false, }, null, null, null).Should().Be(DependencyProperty.UnsetValue);
    }

    [TestMethod]
    public void Convert_AllTrue_IgnoreNotBool()
    {
        var target = new BooleanCombineConverter();
        target.Mode = BooleanCombineConverter.CombineMode.AllTrue;
        target.IgnoreNotBool = true;

        target.Convert(new object[] { true, }, null, null, null).Should().Be(true);
        target.Convert(new object[] { true, true, true, }, null, null, null).Should().Be(true);

        target.Convert(new object[] { false, }, null, null, null).Should().Be(false);
        target.Convert(new object[] { true, false, true, }, null, null, null).Should().Be(false);
        target.Convert(new object[] { false, false, false, }, null, null, null).Should().Be(false);

        // 空の場合は変換不可
        target.Convert(new object[] { }, null, null, null).Should().Be(DependencyProperty.UnsetValue);
        target.Convert(null, null, null, null).Should().Be(DependencyProperty.UnsetValue);

        // 非boolを無視する設定時は結果に影響しない
        target.Convert(new object[] { true, 1, true, }, null, null, null).Should().Be(true);
        target.Convert(new object[] { true, "true", true, }, null, null, null).Should().Be(true);
        target.Convert(new object[] { false, 1, false, }, null, null, null).Should().Be(false);
        target.Convert(new object[] { false, "true", false, }, null, null, null).Should().Be(false);

        // 全部無視される場合は空と同じ
        target.Convert(new object[] { 1, 1, 1, }, null, null, null).Should().Be(DependencyProperty.UnsetValue);
    }

    [TestMethod]
    public void Convert_AllFalse_NeedBool()
    {
        var target = new BooleanCombineConverter();
        target.Mode = BooleanCombineConverter.CombineMode.AllFalse;
        target.IgnoreNotBool = false;

        target.Convert(new object[] { false, }, null, null, null).Should().Be(true);
        target.Convert(new object[] { false, false, false, }, null, null, null).Should().Be(true);

        target.Convert(new object[] { true, }, null, null, null).Should().Be(false);
        target.Convert(new object[] { false, true, false, }, null, null, null).Should().Be(false);
        target.Convert(new object[] { true, true, true, }, null, null, null).Should().Be(false);

        // 空の場合は変換不可
        target.Convert(new object[] { }, null, null, null).Should().Be(DependencyProperty.UnsetValue);
        target.Convert(null, null, null, null).Should().Be(DependencyProperty.UnsetValue);

        // 非boolを無視しない設定時は変換不可になる
        target.Convert(new object[] { false, 1, false, }, null, null, null).Should().Be(DependencyProperty.UnsetValue);
        target.Convert(new object[] { false, "false", false, }, null, null, null).Should().Be(DependencyProperty.UnsetValue);
        target.Convert(new object[] { true, 1, true, }, null, null, null).Should().Be(DependencyProperty.UnsetValue);
        target.Convert(new object[] { true, "false", true, }, null, null, null).Should().Be(DependencyProperty.UnsetValue);
    }

    [TestMethod]
    public void Convert_AllFalse_IgnoreNotBool()
    {
        var target = new BooleanCombineConverter();
        target.Mode = BooleanCombineConverter.CombineMode.AllFalse;
        target.IgnoreNotBool = true;

        target.Convert(new object[] { false, }, null, null, null).Should().Be(true);
        target.Convert(new object[] { false, false, false, }, null, null, null).Should().Be(true);

        target.Convert(new object[] { true, }, null, null, null).Should().Be(false);
        target.Convert(new object[] { false, true, false, }, null, null, null).Should().Be(false);
        target.Convert(new object[] { true, true, true, }, null, null, null).Should().Be(false);

        // 空の場合は変換不可
        target.Convert(new object[] { }, null, null, null).Should().Be(DependencyProperty.UnsetValue);
        target.Convert(null, null, null, null).Should().Be(DependencyProperty.UnsetValue);

        // 非boolを無視する設定時は結果に影響しない
        target.Convert(new object[] { false, 1, false, }, null, null, null).Should().Be(true);
        target.Convert(new object[] { false, "false", false, }, null, null, null).Should().Be(true);
        target.Convert(new object[] { true, 1, true, }, null, null, null).Should().Be(false);
        target.Convert(new object[] { true, "false", true, }, null, null, null).Should().Be(false);

        // 全部無視される場合は空と同じ
        target.Convert(new object[] { 1, 1, 1, }, null, null, null).Should().Be(DependencyProperty.UnsetValue);
    }

    [TestMethod]
    public void Convert_AnyTrue_NeedBool()
    {
        var target = new BooleanCombineConverter();
        target.Mode = BooleanCombineConverter.CombineMode.AnyTrue;
        target.IgnoreNotBool = false;

        target.Convert(new object[] { true, }, null, null, null).Should().Be(true);
        target.Convert(new object[] { true, true, true, }, null, null, null).Should().Be(true);

        target.Convert(new object[] { false, }, null, null, null).Should().Be(false);
        target.Convert(new object[] { false, true, false, }, null, null, null).Should().Be(true);
        target.Convert(new object[] { false, false, false, }, null, null, null).Should().Be(false);

        // 空の場合は変換不可
        target.Convert(new object[] { }, null, null, null).Should().Be(DependencyProperty.UnsetValue);
        target.Convert(null, null, null, null).Should().Be(DependencyProperty.UnsetValue);

        // 非boolを無視しない設定時は変換不可になる
        target.Convert(new object[] { false, 1, true, }, null, null, null).Should().Be(DependencyProperty.UnsetValue);
        target.Convert(new object[] { false, "true", true, }, null, null, null).Should().Be(DependencyProperty.UnsetValue);
        target.Convert(new object[] { false, 1, false, }, null, null, null).Should().Be(DependencyProperty.UnsetValue);
        target.Convert(new object[] { false, "true", false, }, null, null, null).Should().Be(DependencyProperty.UnsetValue);
    }

    [TestMethod]
    public void Convert_AnyTrue_IgnoreNotBool()
    {
        var target = new BooleanCombineConverter();
        target.Mode = BooleanCombineConverter.CombineMode.AnyTrue;
        target.IgnoreNotBool = true;

        target.Convert(new object[] { true, }, null, null, null).Should().Be(true);
        target.Convert(new object[] { true, true, true, }, null, null, null).Should().Be(true);

        target.Convert(new object[] { false, }, null, null, null).Should().Be(false);
        target.Convert(new object[] { false, true, false, }, null, null, null).Should().Be(true);
        target.Convert(new object[] { false, false, false, }, null, null, null).Should().Be(false);

        // 空の場合は変換不可
        target.Convert(new object[] { }, null, null, null).Should().Be(DependencyProperty.UnsetValue);
        target.Convert(null, null, null, null).Should().Be(DependencyProperty.UnsetValue);

        // 非boolを無視する設定時は結果に影響しない
        target.Convert(new object[] { false, 1, true, }, null, null, null).Should().Be(true);
        target.Convert(new object[] { false, "true", true, }, null, null, null).Should().Be(true);
        target.Convert(new object[] { false, 1, false, }, null, null, null).Should().Be(false);
        target.Convert(new object[] { false, "true", false, }, null, null, null).Should().Be(false);

        // 全部無視される場合は空と同じ
        target.Convert(new object[] { 1, 1, 1, }, null, null, null).Should().Be(DependencyProperty.UnsetValue);
    }

    [TestMethod]
    public void Convert_AnyFalse_NeedBool()
    {
        var target = new BooleanCombineConverter();
        target.Mode = BooleanCombineConverter.CombineMode.AnyFalse;
        target.IgnoreNotBool = false;

        target.Convert(new object[] { false, }, null, null, null).Should().Be(true);
        target.Convert(new object[] { false, false, false, }, null, null, null).Should().Be(true);

        target.Convert(new object[] { true, }, null, null, null).Should().Be(false);
        target.Convert(new object[] { true, false, true, }, null, null, null).Should().Be(true);
        target.Convert(new object[] { true, true, true, }, null, null, null).Should().Be(false);

        // 空の場合は変換不可
        target.Convert(new object[] { }, null, null, null).Should().Be(DependencyProperty.UnsetValue);
        target.Convert(null, null, null, null).Should().Be(DependencyProperty.UnsetValue);

        // 非boolを無視しない設定時は変換不可になる
        target.Convert(new object[] { true, 1, false, }, null, null, null).Should().Be(DependencyProperty.UnsetValue);
        target.Convert(new object[] { true, "true", false, }, null, null, null).Should().Be(DependencyProperty.UnsetValue);
        target.Convert(new object[] { true, 1, true, }, null, null, null).Should().Be(DependencyProperty.UnsetValue);
        target.Convert(new object[] { true, "true", true, }, null, null, null).Should().Be(DependencyProperty.UnsetValue);
    }

    [TestMethod]
    public void Convert_AnyFalse_IgnoreNotBool()
    {
        var target = new BooleanCombineConverter();
        target.Mode = BooleanCombineConverter.CombineMode.AnyFalse;
        target.IgnoreNotBool = true;

        target.Convert(new object[] { false, }, null, null, null).Should().Be(true);
        target.Convert(new object[] { false, false, false, }, null, null, null).Should().Be(true);

        target.Convert(new object[] { true, }, null, null, null).Should().Be(false);
        target.Convert(new object[] { true, false, true, }, null, null, null).Should().Be(true);
        target.Convert(new object[] { true, true, true, }, null, null, null).Should().Be(false);

        // 空の場合は変換不可
        target.Convert(new object[] { }, null, null, null).Should().Be(DependencyProperty.UnsetValue);
        target.Convert(null, null, null, null).Should().Be(DependencyProperty.UnsetValue);

        // 非boolを無視する設定時は結果に影響しない
        target.Convert(new object[] { true, 1, false, }, null, null, null).Should().Be(true);
        target.Convert(new object[] { true, "true", false, }, null, null, null).Should().Be(true);
        target.Convert(new object[] { true, 1, true, }, null, null, null).Should().Be(false);
        target.Convert(new object[] { true, "true", true, }, null, null, null).Should().Be(false);

        // 全部無視される場合は空と同じ
        target.Convert(new object[] { 1, 1, 1, }, null, null, null).Should().Be(DependencyProperty.UnsetValue);
    }

    [TestMethod]
    public void Convert_NotSupport()
    {
        var target = new BooleanCombineConverter();
        target.Mode = (BooleanCombineConverter.CombineMode)(-1);
        target.IgnoreNotBool = true;

        // 定義されていないモードではなんであっても変換結果無し

        target.Convert(new object[] { false, }, null, null, null).Should().Be(DependencyProperty.UnsetValue);
        target.Convert(new object[] { false, false, false, }, null, null, null).Should().Be(DependencyProperty.UnsetValue);

        target.Convert(new object[] { true, }, null, null, null).Should().Be(DependencyProperty.UnsetValue);
        target.Convert(new object[] { true, false, true, }, null, null, null).Should().Be(DependencyProperty.UnsetValue);
        target.Convert(new object[] { true, true, true, }, null, null, null).Should().Be(DependencyProperty.UnsetValue);

        target.Convert(new object[] { }, null, null, null).Should().Be(DependencyProperty.UnsetValue);
        target.Convert(null, null, null, null).Should().Be(DependencyProperty.UnsetValue);

        target.Convert(new object[] { true, 1, false, }, null, null, null).Should().Be(DependencyProperty.UnsetValue);
        target.Convert(new object[] { true, "true", false, }, null, null, null).Should().Be(DependencyProperty.UnsetValue);
        target.Convert(new object[] { true, 1, true, }, null, null, null).Should().Be(DependencyProperty.UnsetValue);
        target.Convert(new object[] { true, "true", true, }, null, null, null).Should().Be(DependencyProperty.UnsetValue);

        target.Convert(new object[] { 1, 1, 1, }, null, null, null).Should().Be(DependencyProperty.UnsetValue);
    }

    [TestMethod]
    public void ConvertBack()
    {
        var target = new BooleanCombineConverter();

        // 逆変換は不可。どうやっても null となる。

        target.Mode = BooleanCombineConverter.CombineMode.AllTrue;
        target.ConvertBack(true, null, null, null).Should().BeNull();
        target.ConvertBack(false, null, null, null).Should().BeNull();
        target.ConvertBack(1, null, null, null).Should().BeNull();
        target.ConvertBack("a", null, null, null).Should().BeNull();

        target.Mode = BooleanCombineConverter.CombineMode.AllFalse;
        target.ConvertBack(true, null, null, null).Should().BeNull();
        target.ConvertBack(false, null, null, null).Should().BeNull();
        target.ConvertBack(1, null, null, null).Should().BeNull();
        target.ConvertBack("a", null, null, null).Should().BeNull();

        target.Mode = BooleanCombineConverter.CombineMode.AnyTrue;
        target.ConvertBack(true, null, null, null).Should().BeNull();
        target.ConvertBack(false, null, null, null).Should().BeNull();
        target.ConvertBack(1, null, null, null).Should().BeNull();
        target.ConvertBack("a", null, null, null).Should().BeNull();

        target.Mode = BooleanCombineConverter.CombineMode.AnyFalse;
        target.ConvertBack(true, null, null, null).Should().BeNull();
        target.ConvertBack(false, null, null, null).Should().BeNull();
        target.ConvertBack(1, null, null, null).Should().BeNull();
        target.ConvertBack("a", null, null, null).Should().BeNull();
    }

}
