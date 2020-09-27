using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using CometFlavor.Wpf.Converters;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestCometFlavor.Wpf._Test;

namespace TestCometFlavor.Wpf.Converters
{
    [TestClass]
    public class UrlDropParameterConverterTests
    {
        [TestMethod]
        public void Test_Construct()
        {
            var target = new UrlDropParameterConverter();
            target.AcceptFormats.Should().Contain("UniformResourceLocatorW", "UniformResourceLocator");
            target.ConvertToUri.Should().Be(false);
        }

        [TestMethod]
        public void Test_Convert_ToString_FromUnicode()
        {
            // ドロップテストデータ
            var url = "https://www.google.com";

            // モック
            var dataMock = new TestDataObject();
            dataMock.Setup_GetDataPresent("UniformResourceLocatorW", () => true);
            dataMock.Setup_GetData("UniformResourceLocatorW", () => new MemoryStream(Encoding.Unicode.GetBytes(url)));

            // テスト用のイベントパラメータ生成
            var args = TestActivator.CreateDragEventArgs(dataMock.Object);

            // 変換テスト
            var target = new UrlDropParameterConverter();
            target.ConvertToUri = false;
            target.Convert(args, null, null, null)
                .Should().BeOfType<string>()
                .Which
                .Should().Be(url);
        }

        [TestMethod]
        public void Test_Convert_ToString_FromAnsi()
        {
            // ドロップテストデータ
            var url = "https://www.google.com";

            // モック
            var dataMock = new TestDataObject();
            dataMock.Setup_GetDataPresent("UniformResourceLocator", () => true);
            dataMock.Setup_GetData("UniformResourceLocator", () => new MemoryStream(Encoding.ASCII.GetBytes(url)));

            // テスト用のイベントパラメータ生成
            var args = TestActivator.CreateDragEventArgs(dataMock.Object);

            // 変換テスト
            var target = new UrlDropParameterConverter();
            target.ConvertToUri = false;
            target.Convert(args, null, null, null)
                .Should().BeOfType<string>()
                .Which
                .Should().Be(url);
        }

        [TestMethod]
        public void Test_Convert_ToUri_FromUnicode()
        {
            // ドロップテストデータ
            var url = "https://www.google.com";

            // モック
            var dataMock = new TestDataObject();
            dataMock.Setup_GetDataPresent("UniformResourceLocatorW", () => true);
            dataMock.Setup_GetData("UniformResourceLocatorW", () => new MemoryStream(Encoding.Unicode.GetBytes(url)));

            // テスト用のイベントパラメータ生成
            var args = TestActivator.CreateDragEventArgs(dataMock.Object);

            // テストデータを期待値の型に変換しておく
            var expects = new Uri(url);

            // 変換テスト
            var target = new UrlDropParameterConverter();
            target.ConvertToUri = true;
            target.Convert(args, null, null, null)
                .Should().BeOfType<Uri>()
                .Which
                .Should().Be(expects);
        }

        [TestMethod]
        public void Test_Convert_ToUri_FromAnsi()
        {
            // ドロップテストデータ
            var url = "https://www.google.com";

            // モック
            var dataMock = new TestDataObject();
            dataMock.Setup_GetDataPresent("UniformResourceLocator", () => true);
            dataMock.Setup_GetData("UniformResourceLocator", () => new MemoryStream(Encoding.ASCII.GetBytes(url)));

            // テスト用のイベントパラメータ生成
            var args = TestActivator.CreateDragEventArgs(dataMock.Object);

            // テストデータを期待値の型に変換しておく
            var expects = new Uri(url);

            // 変換テスト
            var target = new UrlDropParameterConverter();
            target.ConvertToUri = true;
            target.Convert(args, null, null, null)
                .Should().BeOfType<Uri>()
                .Which
                .Should().Be(expects);
        }

        [TestMethod]
        public void Test_Convert_ToUri_NotConvert()
        {
            // ドロップテストデータ
            var url = "::::::::::::";

            // モック
            var dataMock = new TestDataObject();
            dataMock.Setup_GetDataPresent("UniformResourceLocatorW", () => true);
            dataMock.Setup_GetData("UniformResourceLocatorW", () => new MemoryStream(Encoding.Unicode.GetBytes(url)));

            // テスト用のイベントパラメータ生成
            var args = TestActivator.CreateDragEventArgs(dataMock.Object);

            // 変換テスト
            var target = new UrlDropParameterConverter();
            target.ConvertToUri = true;
            target.Convert(args, null, null, null)
                .Should().BeNull();
        }

        [TestMethod]
        public void Test_Convert_ByTargetType_Uri()
        {
            // ドロップテストデータ
            var url = "https://www.google.com";

            // モック
            var dataMock = new TestDataObject();
            dataMock.Setup_GetDataPresent("UniformResourceLocatorW", () => true);
            dataMock.Setup_GetData("UniformResourceLocatorW", () => new MemoryStream(Encoding.Unicode.GetBytes(url)));

            // テスト用のイベントパラメータ生成
            var args = TestActivator.CreateDragEventArgs(dataMock.Object);

            // 変換テスト
            var target = new UrlDropParameterConverter();
            target.ConvertToUri = false;
            target.Convert(args, typeof(Uri), null, null)
                .Should().BeOfType<Uri>()
                .Which
                .Should().Be(url);
        }

        [TestMethod]
        public void Test_Convert_ByTargetType_NotUri()
        {
            // ドロップテストデータ
            var url = "https://www.google.com";

            // モック
            var dataMock = new TestDataObject();
            dataMock.Setup_GetDataPresent("UniformResourceLocatorW", () => true);
            dataMock.Setup_GetData("UniformResourceLocatorW", () => new MemoryStream(Encoding.Unicode.GetBytes(url)));

            // テスト用のイベントパラメータ生成
            var args = TestActivator.CreateDragEventArgs(dataMock.Object);

            // 変換テスト
            var target = new UrlDropParameterConverter();
            target.ConvertToUri = false;
            target.Convert(args, typeof(int), null, null)
                .Should().BeOfType<string>()
                .Which
                .Should().Be(url);
        }

        [TestMethod]
        public void Test_Convert_NotUrlDrop1()
        {
            // モック
            var dataMock = new TestDataObject();
            dataMock.Setup_GetDataPresent("UniformResourceLocatorW", () => true);
            dataMock.Setup_GetData("UniformResourceLocatorW", () => new object());

            // テスト用のイベントパラメータ生成
            var args = TestActivator.CreateDragEventArgs(dataMock.Object);

            // 変換テスト
            var target = new UrlDropParameterConverter();
            target.ConvertToUri = false;
            target.Convert(args, null, null, null)
                .Should().BeNull();
        }

        [TestMethod]
        public void Test_Convert_NotUrlDrop2()
        {
            // モック
            var dataMock = new TestDataObject();
            dataMock.Setup_GetDataPresent("UniformResourceLocatorW", () => true);
            dataMock.Setup_GetData("UniformResourceLocatorW", () => null);

            // テスト用のイベントパラメータ生成
            var args = TestActivator.CreateDragEventArgs(dataMock.Object);

            // 変換テスト
            var target = new UrlDropParameterConverter();
            target.ConvertToUri = false;
            target.Convert(args, null, null, null)
                .Should().BeNull();
        }

        [TestMethod]
        public void Test_Convert_UnexpectType()
        {
            // ドロップテストデータ
            var url = "https://www.google.com";

            // 変換テスト (変換元データ型が期待と異なる)
            var target = new UrlDropParameterConverter();
            target.ConvertToUri = false;
            target.Convert(url, null, null, null)
                .Should().Be(DependencyProperty.UnsetValue);
        }

        [TestMethod]
        public void Test_ConvertBack_NotSupport()
        {
            // ドロップテストデータ
            var url = "https://www.google.com";

            // 変換テスト (変換元データ型が期待と異なる)
            var target = new UrlDropParameterConverter();
            target.ConvertToUri = false;
            target.ConvertBack(url, null, null, null)
                .Should().Be(DependencyProperty.UnsetValue);
        }
    }
}
