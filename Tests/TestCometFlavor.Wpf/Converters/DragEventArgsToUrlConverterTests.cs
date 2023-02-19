using System;
using System.IO;
using System.Text;
using System.Windows;
using CometFlavor.Wpf.Converters;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using TestCometFlavor.Wpf._Test;

namespace TestCometFlavor.Wpf.Converters;

[TestClass]
public class DragEventArgsToUrlConverterTests
{
    [TestMethod]
    public void Construct()
    {
        var target = new DragEventArgsToUrlConverter();
        target.AcceptFormats.Should().Contain("UniformResourceLocatorW", "UniformResourceLocator");
        target.ConvertToUri.Should().Be(false);
    }

    [TestMethod]
    public void Convert_ToString_FromUnicode()
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
        var target = new DragEventArgsToUrlConverter();
        target.ConvertToUri = false;
        target.Convert(args, null, null, null)
            .Should().BeOfType<string>()
            .Which
            .Should().Be(url);
    }

    [TestMethod]
    public void Convert_ToString_FromAnsi()
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
        var target = new DragEventArgsToUrlConverter();
        target.ConvertToUri = false;
        target.Convert(args, null, null, null)
            .Should().BeOfType<string>()
            .Which
            .Should().Be(url);
    }

    [TestMethod]
    public void Convert_ToString_PriorityUnicode()
    {
        // ドロップテストデータ
        var urlUnicode = "https://www.google.com/unicode";
        var urlAnsi = "https://www.google.com/ansi";

        // モック
        var dataMock = new TestDataObject();
        dataMock.Setup_GetDataPresent("UniformResourceLocatorW", () => true);
        dataMock.Setup_GetDataPresent("UniformResourceLocator", () => true);
        dataMock.Setup_GetData("UniformResourceLocatorW", () => new MemoryStream(Encoding.Unicode.GetBytes(urlUnicode)));
        dataMock.Setup_GetData("UniformResourceLocator", () => new MemoryStream(Encoding.ASCII.GetBytes(urlAnsi)));

        // テスト用のイベントパラメータ生成
        var args = TestActivator.CreateDragEventArgs(dataMock.Object);

        // 変換テスト
        var target = new DragEventArgsToUrlConverter();
        target.ConvertToUri = false;
        target.Convert(args, null, null, null)
            .Should().BeOfType<string>()
            .Which
            .Should().Be(urlUnicode);
    }

    [TestMethod]
    public void Convert_ToString_Fallback1()
    {
        // ドロップテストデータ
        var url = "https://www.google.com";

        // モック
        var dataMock = new TestDataObject();
        dataMock.Setup_GetDataPresent("UniformResourceLocatorW", () => true);
        dataMock.Setup_GetDataPresent("UniformResourceLocator", () => true);
        dataMock.Setup_GetData("UniformResourceLocatorW", () => new object());
        dataMock.Setup_GetData("UniformResourceLocator", () => new MemoryStream(Encoding.ASCII.GetBytes(url)));

        // テスト用のイベントパラメータ生成
        var args = TestActivator.CreateDragEventArgs(dataMock.Object);

        // 変換テスト
        var target = new DragEventArgsToUrlConverter();
        target.ConvertToUri = false;
        target.Convert(args, null, null, null)
            .Should().BeOfType<string>()
            .Which
            .Should().Be(url);
    }

    [TestMethod]
    public void Convert_ToString_Fallback2()
    {
        // ドロップテストデータ
        var url = "https://www.google.com";

        // モック
        var resourceMock = new Mock<IDisposable>();
        resourceMock.Setup(m => m.Dispose()).Throws(new Exception());
        var dataMock = new TestDataObject();
        dataMock.Setup_GetDataPresent("UniformResourceLocatorW", () => true);
        dataMock.Setup_GetDataPresent("UniformResourceLocator", () => true);
        dataMock.Setup_GetData("UniformResourceLocatorW", () => resourceMock.Object);
        dataMock.Setup_GetData("UniformResourceLocator", () => new MemoryStream(Encoding.ASCII.GetBytes(url)));

        // テスト用のイベントパラメータ生成
        var args = TestActivator.CreateDragEventArgs(dataMock.Object);

        // 変換テスト
        var target = new DragEventArgsToUrlConverter();
        target.ConvertToUri = false;
        target.Convert(args, null, null, null)
            .Should().BeOfType<string>()
            .Which
            .Should().Be(url);
    }

    [TestMethod]
    public void Convert_ToUri_FromUnicode()
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
        var target = new DragEventArgsToUrlConverter();
        target.ConvertToUri = true;
        target.Convert(args, null, null, null)
            .Should().BeOfType<Uri>()
            .Which
            .Should().Be(expects);
    }

    [TestMethod]
    public void Convert_ToUri_FromAnsi()
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
        var target = new DragEventArgsToUrlConverter();
        target.ConvertToUri = true;
        target.Convert(args, null, null, null)
            .Should().BeOfType<Uri>()
            .Which
            .Should().Be(expects);
    }

    [TestMethod]
    public void Convert_ToUri_NotConvert()
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
        var target = new DragEventArgsToUrlConverter();
        target.ConvertToUri = true;
        target.Convert(args, null, null, null)
            .Should().BeNull();
    }

    [TestMethod]
    public void Convert_ByTargetType_Uri()
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
        var target = new DragEventArgsToUrlConverter();
        target.ConvertToUri = false;
        target.Convert(args, typeof(Uri), null, null)
            .Should().BeOfType<Uri>()
            .Which
            .Should().Be(url);
    }

    [TestMethod]
    public void Convert_ByTargetType_NotUri()
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
        var target = new DragEventArgsToUrlConverter();
        target.ConvertToUri = false;
        target.Convert(args, typeof(int), null, null)
            .Should().BeOfType<string>()
            .Which
            .Should().Be(url);
    }

    [TestMethod]
    public void Convert_NotUrlDrop1()
    {
        // モック
        var dataMock = new TestDataObject();
        dataMock.Setup_GetDataPresent("UniformResourceLocatorW", () => true);
        dataMock.Setup_GetData("UniformResourceLocatorW", () => new object());

        // テスト用のイベントパラメータ生成
        var args = TestActivator.CreateDragEventArgs(dataMock.Object);

        // 変換テスト
        var target = new DragEventArgsToUrlConverter();
        target.ConvertToUri = false;
        target.Convert(args, null, null, null)
            .Should().BeNull();
    }

    [TestMethod]
    public void Convert_NotUrlDrop2()
    {
        // モック
        var dataMock = new TestDataObject();
        dataMock.Setup_GetDataPresent("UniformResourceLocatorW", () => true);
        dataMock.Setup_GetData("UniformResourceLocatorW", () => null);

        // テスト用のイベントパラメータ生成
        var args = TestActivator.CreateDragEventArgs(dataMock.Object);

        // 変換テスト
        var target = new DragEventArgsToUrlConverter();
        target.ConvertToUri = false;
        target.Convert(args, null, null, null)
            .Should().BeNull();
    }

    [TestMethod]
    public void Convert_UnexpectType()
    {
        // ドロップテストデータ
        var url = "https://www.google.com";

        // 変換テスト (変換元データ型が期待と異なる)
        var target = new DragEventArgsToUrlConverter();
        target.ConvertToUri = false;
        target.Convert(url, null, null, null)
            .Should().Be(DependencyProperty.UnsetValue);
    }

    [TestMethod]
    public void ConvertBack_NotSupport()
    {
        // ドロップテストデータ
        var url = "https://www.google.com";

        // 変換テスト (変換元データ型が期待と異なる)
        var target = new DragEventArgsToUrlConverter();
        target.ConvertToUri = false;
        target.ConvertBack(url, null, null, null)
            .Should().Be(DependencyProperty.UnsetValue);
    }

    [TestMethod]
    public void Convert_Dispose()
    {
        // モック
        var resourceUnicodeMock = new Mock<IDisposable>();
        var resourceAnsiMock = new Mock<IDisposable>();
        var dataMock = new TestDataObject();
        dataMock.Setup_GetDataPresent("UniformResourceLocatorW", () => true);
        dataMock.Setup_GetDataPresent("UniformResourceLocator", () => true);
        dataMock.Setup_GetData("UniformResourceLocatorW", () => resourceUnicodeMock.Object);
        dataMock.Setup_GetData("UniformResourceLocator", () => resourceAnsiMock.Object);

        // テスト用のイベントパラメータ生成
        var args = TestActivator.CreateDragEventArgs(dataMock.Object);

        // 変換テスト
        var target = new DragEventArgsToUrlConverter();
        target.ConvertToUri = false;
        target.Convert(args, null, null, null)
            .Should().BeNull();

        resourceUnicodeMock.Verify(m => m.Dispose(), Times.Once());
        resourceAnsiMock.Verify(m => m.Dispose(), Times.Once());
    }
}
