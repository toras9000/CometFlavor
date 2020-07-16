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
    public class ExistenceToBooleanConverterTests
    {
        [TestMethod]
        public void TestConvert_NormalLogic()
        {
            var target = new ExistenceToBooleanConverter();
            target.ReverseLogic = false;
            target.Convert(new object(), null, null, null).Should().Be(true);
            target.Convert(null, null, null, null).Should().Be(false);
        }

        [TestMethod]
        public void TestConvert_ReverseLogic()
        {
            var target = new ExistenceToBooleanConverter();
            target.ReverseLogic = true;
            target.Convert(new object(), null, null, null).Should().Be(false);
            target.Convert(null, null, null, null).Should().Be(true);
        }

        [TestMethod]
        public void TestConvert_NullableType()
        {
            var target = new ExistenceToBooleanConverter();
            target.ReverseLogic = false;
            target.Convert(new int?(0), null, null, null).Should().Be(true);
            target.Convert(new int?(), null, null, null).Should().Be(false);
        }

        [TestMethod]
        public void TestConvertBack_NormalLogic()
        {
            var target = new ExistenceToBooleanConverter();
            target.ReverseLogic = false;
            target.ConvertBack(true, null, null, null).Should().Be(DependencyProperty.UnsetValue);
            target.ConvertBack(false, null, null, null).Should().Be(DependencyProperty.UnsetValue);
        }

        [TestMethod]
        public void TestConvertBack_ReverseLogic()
        {
            var target = new ExistenceToBooleanConverter();
            target.ReverseLogic = false;
            target.ConvertBack(true, null, null, null).Should().Be(DependencyProperty.UnsetValue);
            target.ConvertBack(false, null, null, null).Should().Be(DependencyProperty.UnsetValue);
        }
    }
}
