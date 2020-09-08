using System.Windows;
using CometFlavor.Wpf.Converters;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestCometFlavor.Wpf.Converters
{
    [TestClass]
    public class ObjectReferenceToVisibilityConverterTests
    {
        [TestMethod]
        public void Test_Convert_NormalLogic_InvisibleCollapse()
        {
            var target = new ObjectReferenceToVisibilityConverter();
            target.ReverseLogic = false;
            target.InvisibleToHidden = false;
            target.Convert(new object(), null, null, null).Should().Be(Visibility.Visible);
            target.Convert(null, null, null, null).Should().Be(Visibility.Collapsed);
        }

        [TestMethod]
        public void Test_Convert_NormalLogic_InvisibleHidden()
        {
            var target = new ObjectReferenceToVisibilityConverter();
            target.ReverseLogic = false;
            target.InvisibleToHidden = true;
            target.Convert(new object(), null, null, null).Should().Be(Visibility.Visible);
            target.Convert(null, null, null, null).Should().Be(Visibility.Hidden);
        }

        [TestMethod]
        public void Test_Convert_ReverseLogic_InvisibleCollapse()
        {
            var target = new ObjectReferenceToVisibilityConverter();
            target.ReverseLogic = true;
            target.InvisibleToHidden = false;
            target.Convert(new object(), null, null, null).Should().Be(Visibility.Collapsed);
            target.Convert(null, null, null, null).Should().Be(Visibility.Visible);
        }

        [TestMethod]
        public void Test_Convert_ReverseLogic_InvisibleHidden()
        {
            var target = new ObjectReferenceToVisibilityConverter();
            target.ReverseLogic = true;
            target.InvisibleToHidden = true;
            target.Convert(new object(), null, null, null).Should().Be(Visibility.Hidden);
            target.Convert(null, null, null, null).Should().Be(Visibility.Visible);
        }

        [TestMethod]
        public void Test_Convert_NullableType()
        {
            var target = new ObjectReferenceToVisibilityConverter();
            target.ReverseLogic = false;
            target.InvisibleToHidden = true;
            target.Convert(new int?(0), null, null, null).Should().Be(Visibility.Visible);
            target.Convert(new int?(), null, null, null).Should().Be(Visibility.Hidden);
        }

        [TestMethod]
        public void Test_ConvertBack_NormalLogic_InvisibleCollapse()
        {
            var target = new ObjectReferenceToVisibilityConverter();
            target.ReverseLogic = false;
            target.InvisibleToHidden = false;
            target.ConvertBack(Visibility.Visible, null, null, null).Should().Be(DependencyProperty.UnsetValue);
            target.ConvertBack(Visibility.Collapsed, null, null, null).Should().Be(DependencyProperty.UnsetValue);
            target.ConvertBack(Visibility.Hidden, null, null, null).Should().Be(DependencyProperty.UnsetValue);
        }

        [TestMethod]
        public void Test_ConvertBack_NormalLogic_InvisibleHidden()
        {
            var target = new ObjectReferenceToVisibilityConverter();
            target.ReverseLogic = false;
            target.InvisibleToHidden = true;
            target.ConvertBack(Visibility.Visible, null, null, null).Should().Be(DependencyProperty.UnsetValue);
            target.ConvertBack(Visibility.Collapsed, null, null, null).Should().Be(DependencyProperty.UnsetValue);
            target.ConvertBack(Visibility.Hidden, null, null, null).Should().Be(DependencyProperty.UnsetValue);
        }

        [TestMethod]
        public void Test_ConvertBack_ReverseLogic_InvisibleCollapse()
        {
            var target = new ObjectReferenceToVisibilityConverter();
            target.ReverseLogic = true;
            target.InvisibleToHidden = false;
            target.ConvertBack(Visibility.Visible, null, null, null).Should().Be(DependencyProperty.UnsetValue);
            target.ConvertBack(Visibility.Collapsed, null, null, null).Should().Be(DependencyProperty.UnsetValue);
            target.ConvertBack(Visibility.Hidden, null, null, null).Should().Be(DependencyProperty.UnsetValue);
        }

        [TestMethod]
        public void Test_ConvertBack_ReverseLogic_InvisibleHidden()
        {
            var target = new ObjectReferenceToVisibilityConverter();
            target.ReverseLogic = true;
            target.InvisibleToHidden = true;
            target.ConvertBack(Visibility.Visible, null, null, null).Should().Be(DependencyProperty.UnsetValue);
            target.ConvertBack(Visibility.Collapsed, null, null, null).Should().Be(DependencyProperty.UnsetValue);
            target.ConvertBack(Visibility.Hidden, null, null, null).Should().Be(DependencyProperty.UnsetValue);
        }
    }
}
