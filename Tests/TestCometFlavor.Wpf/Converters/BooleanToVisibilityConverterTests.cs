using System.Windows;
using AwesomeAssertions;
using CometFlavor.Wpf.Converters;

namespace TestCometFlavor.Wpf.Converters;

[TestClass]
public class BooleanToVisibilityConverterTests
{
    [TestMethod]
    public void Convert_NormalLogic_InvisibleCollapse()
    {
        var target = new BooleanToVisibilityConverter();
        target.ReverseLogic = false;
        target.InvisibleToHidden = false;
        target.Convert(true, null, null, null).Should().Be(Visibility.Visible);
        target.Convert(false, null, null, null).Should().Be(Visibility.Collapsed);
    }

    [TestMethod]
    public void Convert_NormalLogic_InvisibleHidden()
    {
        var target = new BooleanToVisibilityConverter();
        target.ReverseLogic = false;
        target.InvisibleToHidden = true;
        target.Convert(true, null, null, null).Should().Be(Visibility.Visible);
        target.Convert(false, null, null, null).Should().Be(Visibility.Hidden);
    }

    [TestMethod]
    public void Convert_ReverseLogic_InvisibleCollapse()
    {
        var target = new BooleanToVisibilityConverter();
        target.ReverseLogic = true;
        target.InvisibleToHidden = false;
        target.Convert(true, null, null, null).Should().Be(Visibility.Collapsed);
        target.Convert(false, null, null, null).Should().Be(Visibility.Visible);
    }

    [TestMethod]
    public void Convert_ReverseLogic_InvisibleHidden()
    {
        var target = new BooleanToVisibilityConverter();
        target.ReverseLogic = true;
        target.InvisibleToHidden = true;
        target.Convert(true, null, null, null).Should().Be(Visibility.Hidden);
        target.Convert(false, null, null, null).Should().Be(Visibility.Visible);
    }

    [TestMethod]
    public void Convert_NotExpectType()
    {
        var target = new BooleanToVisibilityConverter();
        target.InvisibleToHidden = true;
        target.Convert(1, null, null, null).Should().Be(DependencyProperty.UnsetValue);
        target.Convert("0", null, null, null).Should().Be(DependencyProperty.UnsetValue);
    }

    [TestMethod]
    public void ConvertBack_NormalLogic_InvisibleCollapse()
    {
        var target = new BooleanToVisibilityConverter();
        target.ReverseLogic = false;
        target.InvisibleToHidden = false;
        target.ConvertBack(Visibility.Visible, null, null, null).Should().Be(true);
        target.ConvertBack(Visibility.Collapsed, null, null, null).Should().Be(false);
        target.ConvertBack(Visibility.Hidden, null, null, null).Should().Be(false);
    }

    [TestMethod]
    public void ConvertBack_NormalLogic_InvisibleHidden()
    {
        var target = new BooleanToVisibilityConverter();
        target.ReverseLogic = false;
        target.InvisibleToHidden = true;
        target.ConvertBack(Visibility.Visible, null, null, null).Should().Be(true);
        target.ConvertBack(Visibility.Collapsed, null, null, null).Should().Be(false);
        target.ConvertBack(Visibility.Hidden, null, null, null).Should().Be(false);
    }

    [TestMethod]
    public void ConvertBack_ReverseLogic_InvisibleCollapse()
    {
        var target = new BooleanToVisibilityConverter();
        target.ReverseLogic = true;
        target.InvisibleToHidden = false;
        target.ConvertBack(Visibility.Visible, null, null, null).Should().Be(false);
        target.ConvertBack(Visibility.Collapsed, null, null, null).Should().Be(true);
        target.ConvertBack(Visibility.Hidden, null, null, null).Should().Be(true);
    }

    [TestMethod]
    public void ConvertBack_ReverseLogic_InvisibleHidden()
    {
        var target = new BooleanToVisibilityConverter();
        target.ReverseLogic = true;
        target.InvisibleToHidden = true;
        target.ConvertBack(Visibility.Visible, null, null, null).Should().Be(false);
        target.ConvertBack(Visibility.Collapsed, null, null, null).Should().Be(true);
        target.ConvertBack(Visibility.Hidden, null, null, null).Should().Be(true);
    }

    [TestMethod]
    public void ConvertBack_UndefinedValue()
    {
        var target = new BooleanToVisibilityConverter();
        target.ReverseLogic = false;
        target.InvisibleToHidden = true;
        target.ConvertBack(unchecked((Visibility)(-1)), null, null, null).Should().Be(DependencyProperty.UnsetValue);
    }

    [TestMethod]
    public void ConvertBack_NotExpectType()
    {
        var target = new BooleanToVisibilityConverter();
        target.InvisibleToHidden = true;
        target.ConvertBack(1, null, null, null).Should().Be(DependencyProperty.UnsetValue);
        target.ConvertBack("0", null, null, null).Should().Be(DependencyProperty.UnsetValue);
    }
}
