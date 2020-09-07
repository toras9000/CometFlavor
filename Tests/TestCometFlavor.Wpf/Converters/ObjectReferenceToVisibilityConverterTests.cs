using System;
using System.Collections.Generic;
using System.Text;
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
        public void TestConvert_NormalLogic_InvisibleCollapse()
        {
            var target = new ObjectReferenceToVisibilityConverter();
            target.ReverseLogic = false;
            target.InvisibleToHidden = false;
            target.Convert(new object(), null, null, null).Should().Be(Visibility.Visible);
            target.Convert(null, null, null, null).Should().Be(Visibility.Collapsed);
        }

        [TestMethod]
        public void TestConvert_NormalLogic_InvisibleHidden()
        {
            var target = new ObjectReferenceToVisibilityConverter();
            target.ReverseLogic = false;
            target.InvisibleToHidden = true;
            target.Convert(new object(), null, null, null).Should().Be(Visibility.Visible);
            target.Convert(null, null, null, null).Should().Be(Visibility.Hidden);
        }

        [TestMethod]
        public void TestConvert_ReverseLogic_InvisibleCollapse()
        {
            var target = new ObjectReferenceToVisibilityConverter();
            target.ReverseLogic = true;
            target.InvisibleToHidden = false;
            target.Convert(new object(), null, null, null).Should().Be(Visibility.Collapsed);
            target.Convert(null, null, null, null).Should().Be(Visibility.Visible);
        }

        [TestMethod]
        public void TestConvert_ReverseLogic_InvisibleHidden()
        {
            var target = new ObjectReferenceToVisibilityConverter();
            target.ReverseLogic = true;
            target.InvisibleToHidden = true;
            target.Convert(new object(), null, null, null).Should().Be(Visibility.Hidden);
            target.Convert(null, null, null, null).Should().Be(Visibility.Visible);
        }

        [TestMethod]
        public void TestConvert_NullableType()
        {
            var target = new ObjectReferenceToVisibilityConverter();
            target.ReverseLogic = false;
            target.InvisibleToHidden = true;
            target.Convert(new int?(0), null, null, null).Should().Be(Visibility.Visible);
            target.Convert(new int?(), null, null, null).Should().Be(Visibility.Hidden);
        }

        [TestMethod]
        public void TestConvertBack_NormalLogic_InvisibleCollapse()
        {
            var target = new ObjectReferenceToVisibilityConverter();
            target.ReverseLogic = false;
            target.InvisibleToHidden = false;
            target.ConvertBack(Visibility.Visible, null, null, null).Should().Be(DependencyProperty.UnsetValue);
            target.ConvertBack(Visibility.Collapsed, null, null, null).Should().Be(DependencyProperty.UnsetValue);
            target.ConvertBack(Visibility.Hidden, null, null, null).Should().Be(DependencyProperty.UnsetValue);
        }

        [TestMethod]
        public void TestConvertBack_NormalLogic_InvisibleHidden()
        {
            var target = new ObjectReferenceToVisibilityConverter();
            target.ReverseLogic = false;
            target.InvisibleToHidden = true;
            target.ConvertBack(Visibility.Visible, null, null, null).Should().Be(DependencyProperty.UnsetValue);
            target.ConvertBack(Visibility.Collapsed, null, null, null).Should().Be(DependencyProperty.UnsetValue);
            target.ConvertBack(Visibility.Hidden, null, null, null).Should().Be(DependencyProperty.UnsetValue);
        }

        [TestMethod]
        public void TestConvertBack_ReverseLogic_InvisibleCollapse()
        {
            var target = new ObjectReferenceToVisibilityConverter();
            target.ReverseLogic = true;
            target.InvisibleToHidden = false;
            target.ConvertBack(Visibility.Visible, null, null, null).Should().Be(DependencyProperty.UnsetValue);
            target.ConvertBack(Visibility.Collapsed, null, null, null).Should().Be(DependencyProperty.UnsetValue);
            target.ConvertBack(Visibility.Hidden, null, null, null).Should().Be(DependencyProperty.UnsetValue);
        }

        [TestMethod]
        public void TestConvertBack_ReverseLogic_InvisibleHidden()
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
