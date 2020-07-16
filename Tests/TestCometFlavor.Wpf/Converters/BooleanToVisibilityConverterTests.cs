using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using CometFlavor.Wpf.Converters;
using System.Windows;

namespace TestCometFlavor.Wpf.Converters
{
    [TestClass]
    public class BooleanToVisibilityConverterTests
    {
        [TestMethod]
        public void TestConvert_NormalLogic_InvisibleCollapse()
        {
            var target = new BooleanToVisibilityConverter();
            target.ReverseLogic = false;
            target.InvisibleToHidden = false;
            target.Convert(true, null, null, null).Should().Be(Visibility.Visible);
            target.Convert(false, null, null, null).Should().Be(Visibility.Collapsed);
        }

        [TestMethod]
        public void TestConvert_NormalLogic_InvisibleHidden()
        {
            var target = new BooleanToVisibilityConverter();
            target.ReverseLogic = false;
            target.InvisibleToHidden = true;
            target.Convert(true, null, null, null).Should().Be(Visibility.Visible);
            target.Convert(false, null, null, null).Should().Be(Visibility.Hidden);
        }

        [TestMethod]
        public void TestConvert_ReverseLogic_InvisibleCollapse()
        {
            var target = new BooleanToVisibilityConverter();
            target.ReverseLogic = true;
            target.InvisibleToHidden = false;
            target.Convert(true, null, null, null).Should().Be(Visibility.Collapsed);
            target.Convert(false, null, null, null).Should().Be(Visibility.Visible);
        }

        [TestMethod]
        public void TestConvert_ReverseLogic_InvisibleHidden()
        {
            var target = new BooleanToVisibilityConverter();
            target.ReverseLogic = true;
            target.InvisibleToHidden = true;
            target.Convert(true, null, null, null).Should().Be(Visibility.Hidden);
            target.Convert(false, null, null, null).Should().Be(Visibility.Visible);
        }

        [TestMethod]
        public void TestConvert_NotExpectType()
        {
            var target = new BooleanToVisibilityConverter();
            target.InvisibleToHidden = true;
            target.Convert(1, null, null, null).Should().Be(DependencyProperty.UnsetValue);
            target.Convert("0", null, null, null).Should().Be(DependencyProperty.UnsetValue);
        }

        [TestMethod]
        public void TestConvertBack_NormalLogic_InvisibleCollapse()
        {
            var target = new BooleanToVisibilityConverter();
            target.ReverseLogic = false;
            target.InvisibleToHidden = false;
            target.ConvertBack(Visibility.Visible, null, null, null).Should().Be(true);
            target.ConvertBack(Visibility.Collapsed, null, null, null).Should().Be(false);
            target.ConvertBack(Visibility.Hidden, null, null, null).Should().Be(false);
        }

        [TestMethod]
        public void TestConvertBack_NormalLogic_InvisibleHidden()
        {
            var target = new BooleanToVisibilityConverter();
            target.ReverseLogic = false;
            target.InvisibleToHidden = true;
            target.ConvertBack(Visibility.Visible, null, null, null).Should().Be(true);
            target.ConvertBack(Visibility.Collapsed, null, null, null).Should().Be(false);
            target.ConvertBack(Visibility.Hidden, null, null, null).Should().Be(false);
        }

        [TestMethod]
        public void TestConvertBack_ReverseLogic_InvisibleCollapse()
        {
            var target = new BooleanToVisibilityConverter();
            target.ReverseLogic = true;
            target.InvisibleToHidden = false;
            target.ConvertBack(Visibility.Visible, null, null, null).Should().Be(false);
            target.ConvertBack(Visibility.Collapsed, null, null, null).Should().Be(true);
            target.ConvertBack(Visibility.Hidden, null, null, null).Should().Be(true);
        }

        [TestMethod]
        public void TestConvertBack_ReverseLogic_InvisibleHidden()
        {
            var target = new BooleanToVisibilityConverter();
            target.ReverseLogic = true;
            target.InvisibleToHidden = true;
            target.ConvertBack(Visibility.Visible, null, null, null).Should().Be(false);
            target.ConvertBack(Visibility.Collapsed, null, null, null).Should().Be(true);
            target.ConvertBack(Visibility.Hidden, null, null, null).Should().Be(true);
        }

        [TestMethod]
        public void TestConvertBack_NotExpectType()
        {
            var target = new BooleanToVisibilityConverter();
            target.InvisibleToHidden = true;
            target.ConvertBack(1, null, null, null).Should().Be(DependencyProperty.UnsetValue);
            target.ConvertBack("0", null, null, null).Should().Be(DependencyProperty.UnsetValue);
        }
    }
}
