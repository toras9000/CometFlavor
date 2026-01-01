using System.Windows;
using AwesomeAssertions;
using CometFlavor.Wpf.Converters;
using TestCometFlavor.Wpf._Test;

namespace TestCometFlavor.Wpf.Converters;

[TestClass]
public class DragEventArgsToFilePathConverterTests
{
    [TestMethod]
    public void Construct()
    {
        var target = new DragEventArgsToFilePathConverter();
        target.AcceptFormats.Should().Contain(DataFormats.FileDrop);
        target.ConvertToUri.Should().Be(false);
    }

    [TestMethod]
    public void Convert_ToString()
    {
        // ドロップテストデータ
        var paths = new string[] { @"c:\directory\file.ext", @"d:\path\to\data" };

        // モック
        var dataMock = new TestDataObject();
        dataMock.Setup_GetDataPresent(DataFormats.FileDrop, () => true);
        dataMock.Setup_GetData(DataFormats.FileDrop, () => paths);

        // テスト用のイベントパラメータ生成
        var args = TestActivator.CreateDragEventArgs(dataMock.Object);

        // 変換テスト
        var target = new DragEventArgsToFilePathConverter();
        target.ConvertToUri = false;
        target.Convert(args, null, null, null)
            .Should().BeOfType<string[]>()
            .Which
            .Should().Equal(paths);
    }

    [TestMethod]
    public void Convert_ToString_Empty()
    {
        // ドロップテストデータ
        var paths = new string[] { };

        // モック
        var dataMock = new TestDataObject();
        dataMock.Setup_GetDataPresent(DataFormats.FileDrop, () => true);
        dataMock.Setup_GetData(DataFormats.FileDrop, () => paths);

        // テスト用のイベントパラメータ生成
        var args = TestActivator.CreateDragEventArgs(dataMock.Object);

        // 変換テスト
        var target = new DragEventArgsToFilePathConverter();
        target.ConvertToUri = false;
        target.Convert(args, null, null, null)
            .Should().BeOfType<string[]>()
            .Which
            .Should().BeEmpty();
    }

    [TestMethod]
    public void Convert_ToUri()
    {
        // ドロップテストデータ
        var paths = new string[] { @"c:\directory\file.ext", @"d:\path\to\data" };

        // モック
        var dataMock = new TestDataObject();
        dataMock.Setup_GetDataPresent(DataFormats.FileDrop, () => true);
        dataMock.Setup_GetData(DataFormats.FileDrop, () => paths);

        // テスト用のイベントパラメータ生成
        var args = TestActivator.CreateDragEventArgs(dataMock.Object);

        // テストデータを期待値の型に変換しておく
        var expects = paths.Select(p => new Uri(p)).ToArray();

        // 変換テスト
        var target = new DragEventArgsToFilePathConverter();
        target.ConvertToUri = true;
        target.Convert(args, null, null, null)
            .Should().BeOfType<Uri[]>()
            .Which
            .Should().Equal(expects);
    }

    [TestMethod]
    public void Convert_ToUri_Empty()
    {
        // ドロップテストデータ
        var paths = new string[] { };

        // モック
        var dataMock = new TestDataObject();
        dataMock.Setup_GetDataPresent(DataFormats.FileDrop, () => true);
        dataMock.Setup_GetData(DataFormats.FileDrop, () => paths);

        // テスト用のイベントパラメータ生成
        var args = TestActivator.CreateDragEventArgs(dataMock.Object);

        // テストデータを期待値の型に変換しておく
        var expects = paths.Select(p => new Uri(p)).ToArray();

        // 変換テスト
        var target = new DragEventArgsToFilePathConverter();
        target.ConvertToUri = true;
        target.Convert(args, null, null, null)
            .Should().BeOfType<Uri[]>()
            .Which
            .Should().BeEmpty();
    }

    [TestMethod]
    public void Convert_ToUri_NotConvert()
    {
        // ドロップテストデータ
        var paths = new string[] { @"::::::::::", @"d:\path\to\data" };

        // モック
        var dataMock = new TestDataObject();
        dataMock.Setup_GetDataPresent(DataFormats.FileDrop, () => true);
        dataMock.Setup_GetData(DataFormats.FileDrop, () => paths);

        // テスト用のイベントパラメータ生成
        var args = TestActivator.CreateDragEventArgs(dataMock.Object);

        // テストデータを期待値の型に変換しておく
        var expects = new[] { new Uri(@"d:\path\to\data") };

        // 変換テスト
        var target = new DragEventArgsToFilePathConverter();
        target.ConvertToUri = true;
        target.Convert(args, null, null, null)
            .Should().BeOfType<Uri[]>()
            .Which
            .Should().Equal(expects);
    }

    [TestMethod]
    public void Convert_ByTargetType_Uri()
    {
        // ドロップテストデータ
        var paths = new string[] { @"c:\directory\file.ext", @"d:\path\to\data" };

        // モック
        var dataMock = new TestDataObject();
        dataMock.Setup_GetDataPresent(DataFormats.FileDrop, () => true);
        dataMock.Setup_GetData(DataFormats.FileDrop, () => paths);

        // テスト用のイベントパラメータ生成
        var args = TestActivator.CreateDragEventArgs(dataMock.Object);

        // テストデータを期待値の型に変換しておく
        var expects = paths.Select(p => new Uri(p)).ToArray();

        // 変換テスト
        var target = new DragEventArgsToFilePathConverter();
        target.ConvertToUri = false;
        target.Convert(args, typeof(Uri), null, null)
            .Should().BeOfType<Uri[]>()
            .Which
            .Should().Equal(expects);
    }

    [TestMethod]
    public void Convert_ByTargetType_NotUri()
    {
        // ドロップテストデータ
        var paths = new string[] { @"c:\directory\file.ext", @"d:\path\to\data" };

        // モック
        var dataMock = new TestDataObject();
        dataMock.Setup_GetDataPresent(DataFormats.FileDrop, () => true);
        dataMock.Setup_GetData(DataFormats.FileDrop, () => paths);

        // テスト用のイベントパラメータ生成
        var args = TestActivator.CreateDragEventArgs(dataMock.Object);

        // テストデータを期待値の型に変換しておく
        var expects = paths;

        // 変換テスト
        var target = new DragEventArgsToFilePathConverter();
        target.ConvertToUri = false;
        target.Convert(args, typeof(int), null, null)
            .Should().BeOfType<string[]>()
            .Which
            .Should().Equal(expects);
    }

    [TestMethod]
    public void Convert_NotFileDrop1()
    {
        // モック
        var dataMock = new TestDataObject();
        dataMock.Setup_GetDataPresent(DataFormats.FileDrop, () => true);
        dataMock.Setup_GetData(DataFormats.FileDrop, () => new object());

        // テスト用のイベントパラメータ生成
        var args = TestActivator.CreateDragEventArgs(dataMock.Object);

        // 変換テスト
        var target = new DragEventArgsToFilePathConverter();
        target.ConvertToUri = false;
        target.Convert(args, null, null, null)
            .Should().BeNull();
    }

    [TestMethod]
    public void Convert_NotFileDrop2()
    {
        // モック
        var dataMock = new TestDataObject();
        dataMock.Setup_GetDataPresent(DataFormats.FileDrop, () => true);
        dataMock.Setup_GetData(DataFormats.FileDrop, () => null!);

        // テスト用のイベントパラメータ生成
        var args = TestActivator.CreateDragEventArgs(dataMock.Object);

        // 変換テスト
        var target = new DragEventArgsToFilePathConverter();
        target.ConvertToUri = false;
        target.Convert(args, null, null, null)
            .Should().BeNull();
    }

    [TestMethod]
    public void Convert_UnexpectType()
    {
        // ドロップテストデータ
        var paths = new string[] { @"c:\directory\file.ext", @"d:\path\to\data" };

        // 変換テスト (変換元データ型が期待と異なる)
        var target = new DragEventArgsToFilePathConverter();
        target.ConvertToUri = false;
        target.Convert(paths, null, null, null)
            .Should().Be(DependencyProperty.UnsetValue);
    }

    [TestMethod]
    public void ConvertBack_NotSupport()
    {
        // ドロップテストデータ
        var paths = new string[] { @"c:\directory\file.ext", @"d:\path\to\data" };

        // 変換テスト (変換元データ型が期待と異なる)
        var target = new DragEventArgsToFilePathConverter();
        target.ConvertToUri = false;
        target.ConvertBack(paths, null, null, null)
            .Should().Be(DependencyProperty.UnsetValue);
    }
}
