using System.Windows;
using CometFlavor.Wpf.Converters;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestCometFlavor.Wpf.Converters;

[TestClass]
public class ObjectReferenceToBooleanConverterTests
{
    [TestMethod]
    public void Test_Convert_NormalLogic()
    {
        var target = new ObjectReferenceToBooleanConverter();
        target.ReverseLogic = false;
        target.Convert(new object(), null, null, null).Should().Be(true);
        target.Convert(null, null, null, null).Should().Be(false);
    }

    [TestMethod]
    public void Test_Convert_ReverseLogic()
    {
        var target = new ObjectReferenceToBooleanConverter();
        target.ReverseLogic = true;
        target.Convert(new object(), null, null, null).Should().Be(false);
        target.Convert(null, null, null, null).Should().Be(true);
    }

    [TestMethod]
    public void Test_Convert_NullableType()
    {
        var target = new ObjectReferenceToBooleanConverter();
        target.ReverseLogic = false;
        target.Convert(new int?(0), null, null, null).Should().Be(true);
        target.Convert(new int?(), null, null, null).Should().Be(false);
    }

    [TestMethod]
    public void Test_ConvertBack_NormalLogic()
    {
        var target = new ObjectReferenceToBooleanConverter();
        target.ReverseLogic = false;
        target.ConvertBack(true, null, null, null).Should().Be(DependencyProperty.UnsetValue);
        target.ConvertBack(false, null, null, null).Should().Be(DependencyProperty.UnsetValue);
    }

    [TestMethod]
    public void Test_ConvertBack_ReverseLogic()
    {
        var target = new ObjectReferenceToBooleanConverter();
        target.ReverseLogic = false;
        target.ConvertBack(true, null, null, null).Should().Be(DependencyProperty.UnsetValue);
        target.ConvertBack(false, null, null, null).Should().Be(DependencyProperty.UnsetValue);
    }
}
