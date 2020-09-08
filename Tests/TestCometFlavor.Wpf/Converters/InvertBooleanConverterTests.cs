using System.Windows;
using CometFlavor.Wpf.Converters;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestCometFlavor.Wpf.Converters
{
    [TestClass]
    public class InvertBooleanConverterTests
    {
        [TestMethod]
        public void Test_Convert()
        {
            var target = new InvertBooleanConverter();
            target.Convert(true, null, null, null).Should().Be(false);
            target.Convert(false, null, null, null).Should().Be(true);
            target.Convert(1, null, null, null).Should().Be(DependencyProperty.UnsetValue);
            target.Convert("0", null, null, null).Should().Be(DependencyProperty.UnsetValue);
        }

        [TestMethod]
        public void Test_ConvertBack()
        {
            var target = new InvertBooleanConverter();
            target.ConvertBack(true, null, null, null).Should().Be(false);
            target.ConvertBack(false, null, null, null).Should().Be(true);
            target.ConvertBack(1, null, null, null).Should().Be(DependencyProperty.UnsetValue);
            target.ConvertBack("0", null, null, null).Should().Be(DependencyProperty.UnsetValue);
        }
    }
}
