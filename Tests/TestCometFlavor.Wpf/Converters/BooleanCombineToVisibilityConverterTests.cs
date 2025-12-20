using System.Windows;
using CometFlavor.Wpf.Converters;
using AwesomeAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestCometFlavor.Wpf.Converters;

[TestClass]
public class BooleanCombineToVisibilityConverterTests
{
    [TestMethod]
    public void Construct()
    {
        var target = new BooleanCombineToVisibilityConverter();
        target.Mode.Should().Be(BooleanCombineToVisibilityConverter.CombineMode.AllTrue);
        target.IgnoreNotBool.Should().Be(false);
        target.InvisibleToHidden.Should().Be(false);
    }

    [TestMethod]
    public void Convert_AllTrue_NeedBool()
    {
        var target = new BooleanCombineToVisibilityConverter();
        target.Mode = BooleanCombineToVisibilityConverter.CombineMode.AllTrue;
        target.IgnoreNotBool = false;
        target.InvisibleToHidden = false;

        target.Convert(new object[] { true, }, null, null, null).Should().Be(Visibility.Visible);
        target.Convert(new object[] { true, true, true, }, null, null, null).Should().Be(Visibility.Visible);

        target.Convert(new object[] { false, }, null, null, null).Should().Be(Visibility.Collapsed);
        target.Convert(new object[] { true, false, true, }, null, null, null).Should().Be(Visibility.Collapsed);
        target.Convert(new object[] { false, false, false, }, null, null, null).Should().Be(Visibility.Collapsed);

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
        var target = new BooleanCombineToVisibilityConverter();
        target.Mode = BooleanCombineToVisibilityConverter.CombineMode.AllTrue;
        target.IgnoreNotBool = true;
        target.InvisibleToHidden = false;

        target.Convert(new object[] { true, }, null, null, null).Should().Be(Visibility.Visible);
        target.Convert(new object[] { true, true, true, }, null, null, null).Should().Be(Visibility.Visible);

        target.Convert(new object[] { false, }, null, null, null).Should().Be(Visibility.Collapsed);
        target.Convert(new object[] { true, false, true, }, null, null, null).Should().Be(Visibility.Collapsed);
        target.Convert(new object[] { false, false, false, }, null, null, null).Should().Be(Visibility.Collapsed);

        // 空の場合は変換不可
        target.Convert(new object[] { }, null, null, null).Should().Be(DependencyProperty.UnsetValue);
        target.Convert(null, null, null, null).Should().Be(DependencyProperty.UnsetValue);

        // 非boolを無視する設定時は結果に影響しない
        target.Convert(new object[] { true, 1, true, }, null, null, null).Should().Be(Visibility.Visible);
        target.Convert(new object[] { true, "true", true, }, null, null, null).Should().Be(Visibility.Visible);
        target.Convert(new object[] { false, 1, false, }, null, null, null).Should().Be(Visibility.Collapsed);
        target.Convert(new object[] { false, "true", false, }, null, null, null).Should().Be(Visibility.Collapsed);

        // 全部無視される場合は空と同じ
        target.Convert(new object[] { 1, 1, 1, }, null, null, null).Should().Be(DependencyProperty.UnsetValue);
    }

    [TestMethod]
    public void Convert_AllFalse_NeedBool()
    {
        var target = new BooleanCombineToVisibilityConverter();
        target.Mode = BooleanCombineToVisibilityConverter.CombineMode.AllFalse;
        target.IgnoreNotBool = false;
        target.InvisibleToHidden = false;

        target.Convert(new object[] { false, }, null, null, null).Should().Be(Visibility.Visible);
        target.Convert(new object[] { false, false, false, }, null, null, null).Should().Be(Visibility.Visible);

        target.Convert(new object[] { true, }, null, null, null).Should().Be(Visibility.Collapsed);
        target.Convert(new object[] { false, true, false, }, null, null, null).Should().Be(Visibility.Collapsed);
        target.Convert(new object[] { true, true, true, }, null, null, null).Should().Be(Visibility.Collapsed);

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
        var target = new BooleanCombineToVisibilityConverter();
        target.Mode = BooleanCombineToVisibilityConverter.CombineMode.AllFalse;
        target.IgnoreNotBool = true;
        target.InvisibleToHidden = false;

        target.Convert(new object[] { false, }, null, null, null).Should().Be(Visibility.Visible);
        target.Convert(new object[] { false, false, false, }, null, null, null).Should().Be(Visibility.Visible);

        target.Convert(new object[] { true, }, null, null, null).Should().Be(Visibility.Collapsed);
        target.Convert(new object[] { false, true, false, }, null, null, null).Should().Be(Visibility.Collapsed);
        target.Convert(new object[] { true, true, true, }, null, null, null).Should().Be(Visibility.Collapsed);

        // 空の場合は変換不可
        target.Convert(new object[] { }, null, null, null).Should().Be(DependencyProperty.UnsetValue);
        target.Convert(null, null, null, null).Should().Be(DependencyProperty.UnsetValue);

        // 非boolを無視する設定時は結果に影響しない
        target.Convert(new object[] { false, 1, false, }, null, null, null).Should().Be(Visibility.Visible);
        target.Convert(new object[] { false, "false", false, }, null, null, null).Should().Be(Visibility.Visible);
        target.Convert(new object[] { true, 1, true, }, null, null, null).Should().Be(Visibility.Collapsed);
        target.Convert(new object[] { true, "false", true, }, null, null, null).Should().Be(Visibility.Collapsed);

        // 全部無視される場合は空と同じ
        target.Convert(new object[] { 1, 1, 1, }, null, null, null).Should().Be(DependencyProperty.UnsetValue);
    }

    [TestMethod]
    public void Convert_AnyTrue_NeedBool()
    {
        var target = new BooleanCombineToVisibilityConverter();
        target.Mode = BooleanCombineToVisibilityConverter.CombineMode.AnyTrue;
        target.IgnoreNotBool = false;
        target.InvisibleToHidden = false;

        target.Convert(new object[] { true, }, null, null, null).Should().Be(Visibility.Visible);
        target.Convert(new object[] { true, true, true, }, null, null, null).Should().Be(Visibility.Visible);

        target.Convert(new object[] { false, }, null, null, null).Should().Be(Visibility.Collapsed);
        target.Convert(new object[] { false, true, false, }, null, null, null).Should().Be(Visibility.Visible);
        target.Convert(new object[] { false, false, false, }, null, null, null).Should().Be(Visibility.Collapsed);

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
        var target = new BooleanCombineToVisibilityConverter();
        target.Mode = BooleanCombineToVisibilityConverter.CombineMode.AnyTrue;
        target.IgnoreNotBool = true;
        target.InvisibleToHidden = false;

        target.Convert(new object[] { true, }, null, null, null).Should().Be(Visibility.Visible);
        target.Convert(new object[] { true, true, true, }, null, null, null).Should().Be(Visibility.Visible);

        target.Convert(new object[] { false, }, null, null, null).Should().Be(Visibility.Collapsed);
        target.Convert(new object[] { false, true, false, }, null, null, null).Should().Be(Visibility.Visible);
        target.Convert(new object[] { false, false, false, }, null, null, null).Should().Be(Visibility.Collapsed);

        // 空の場合は変換不可
        target.Convert(new object[] { }, null, null, null).Should().Be(DependencyProperty.UnsetValue);
        target.Convert(null, null, null, null).Should().Be(DependencyProperty.UnsetValue);

        // 非boolを無視する設定時は結果に影響しない
        target.Convert(new object[] { false, 1, true, }, null, null, null).Should().Be(Visibility.Visible);
        target.Convert(new object[] { false, "true", true, }, null, null, null).Should().Be(Visibility.Visible);
        target.Convert(new object[] { false, 1, false, }, null, null, null).Should().Be(Visibility.Collapsed);
        target.Convert(new object[] { false, "true", false, }, null, null, null).Should().Be(Visibility.Collapsed);

        // 全部無視される場合は空と同じ
        target.Convert(new object[] { 1, 1, 1, }, null, null, null).Should().Be(DependencyProperty.UnsetValue);
    }

    [TestMethod]
    public void Convert_AnyFalse_NeedBool()
    {
        var target = new BooleanCombineToVisibilityConverter();
        target.Mode = BooleanCombineToVisibilityConverter.CombineMode.AnyFalse;
        target.IgnoreNotBool = false;
        target.InvisibleToHidden = false;

        target.Convert(new object[] { false, }, null, null, null).Should().Be(Visibility.Visible);
        target.Convert(new object[] { false, false, false, }, null, null, null).Should().Be(Visibility.Visible);

        target.Convert(new object[] { true, }, null, null, null).Should().Be(Visibility.Collapsed);
        target.Convert(new object[] { true, false, true, }, null, null, null).Should().Be(Visibility.Visible);
        target.Convert(new object[] { true, true, true, }, null, null, null).Should().Be(Visibility.Collapsed);

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
        var target = new BooleanCombineToVisibilityConverter();
        target.Mode = BooleanCombineToVisibilityConverter.CombineMode.AnyFalse;
        target.IgnoreNotBool = true;
        target.InvisibleToHidden = false;

        target.Convert(new object[] { false, }, null, null, null).Should().Be(Visibility.Visible);
        target.Convert(new object[] { false, false, false, }, null, null, null).Should().Be(Visibility.Visible);

        target.Convert(new object[] { true, }, null, null, null).Should().Be(Visibility.Collapsed);
        target.Convert(new object[] { true, false, true, }, null, null, null).Should().Be(Visibility.Visible);
        target.Convert(new object[] { true, true, true, }, null, null, null).Should().Be(Visibility.Collapsed);

        // 空の場合は変換不可
        target.Convert(new object[] { }, null, null, null).Should().Be(DependencyProperty.UnsetValue);
        target.Convert(null, null, null, null).Should().Be(DependencyProperty.UnsetValue);

        // 非boolを無視する設定時は結果に影響しない
        target.Convert(new object[] { true, 1, false, }, null, null, null).Should().Be(Visibility.Visible);
        target.Convert(new object[] { true, "true", false, }, null, null, null).Should().Be(Visibility.Visible);
        target.Convert(new object[] { true, 1, true, }, null, null, null).Should().Be(Visibility.Collapsed);
        target.Convert(new object[] { true, "true", true, }, null, null, null).Should().Be(Visibility.Collapsed);

        // 全部無視される場合は空と同じ
        target.Convert(new object[] { 1, 1, 1, }, null, null, null).Should().Be(DependencyProperty.UnsetValue);
    }

    [TestMethod]
    public void Convert_InvisibleToHidden()
    {
        var target = new BooleanCombineToVisibilityConverter();
        target.IgnoreNotBool = true;
        target.InvisibleToHidden = true;

        target.Mode = BooleanCombineToVisibilityConverter.CombineMode.AllTrue;
        target.Convert(new object[] { true, }, null, null, null).Should().Be(Visibility.Visible);
        target.Convert(new object[] { false, }, null, null, null).Should().Be(Visibility.Hidden);

        target.Mode = BooleanCombineToVisibilityConverter.CombineMode.AllFalse;
        target.Convert(new object[] { true, }, null, null, null).Should().Be(Visibility.Hidden);
        target.Convert(new object[] { false, }, null, null, null).Should().Be(Visibility.Visible);

        target.Mode = BooleanCombineToVisibilityConverter.CombineMode.AnyTrue;
        target.Convert(new object[] { true, }, null, null, null).Should().Be(Visibility.Visible);
        target.Convert(new object[] { false, }, null, null, null).Should().Be(Visibility.Hidden);

        target.Mode = BooleanCombineToVisibilityConverter.CombineMode.AnyFalse;
        target.Convert(new object[] { true, }, null, null, null).Should().Be(Visibility.Hidden);
        target.Convert(new object[] { false, }, null, null, null).Should().Be(Visibility.Visible);
    }

    [TestMethod]
    public void Convert_NotSupport()
    {
        var target = new BooleanCombineToVisibilityConverter();
        target.Mode = (BooleanCombineToVisibilityConverter.CombineMode)(-1);
        target.IgnoreNotBool = true;
        target.InvisibleToHidden = false;

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
        var target = new BooleanCombineToVisibilityConverter();

        // 逆変換は不可。どうやっても null となる。

        target.Mode = BooleanCombineToVisibilityConverter.CombineMode.AllTrue;
        target.ConvertBack(true, null, null, null).Should().BeNull();
        target.ConvertBack(false, null, null, null).Should().BeNull();
        target.ConvertBack(1, null, null, null).Should().BeNull();
        target.ConvertBack("a", null, null, null).Should().BeNull();

        target.Mode = BooleanCombineToVisibilityConverter.CombineMode.AllFalse;
        target.ConvertBack(true, null, null, null).Should().BeNull();
        target.ConvertBack(false, null, null, null).Should().BeNull();
        target.ConvertBack(1, null, null, null).Should().BeNull();
        target.ConvertBack("a", null, null, null).Should().BeNull();

        target.Mode = BooleanCombineToVisibilityConverter.CombineMode.AnyTrue;
        target.ConvertBack(true, null, null, null).Should().BeNull();
        target.ConvertBack(false, null, null, null).Should().BeNull();
        target.ConvertBack(1, null, null, null).Should().BeNull();
        target.ConvertBack("a", null, null, null).Should().BeNull();

        target.Mode = BooleanCombineToVisibilityConverter.CombineMode.AnyFalse;
        target.ConvertBack(true, null, null, null).Should().BeNull();
        target.ConvertBack(false, null, null, null).Should().BeNull();
        target.ConvertBack(1, null, null, null).Should().BeNull();
        target.ConvertBack("a", null, null, null).Should().BeNull();
    }
}
