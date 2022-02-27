using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CometFlavor.Extensions.IO;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestCometFlavor._Test;

namespace TestCometFlavor.Extensions.IO;

[TestClass]
public class FileInfoExtensionsTests
{
    [AssemblyInitialize]
    public static void AssemblyInitialize(TestContext context)
    {
        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
    }

    [TestMethod]
    public void TestGetNameWithoutExtension()
    {
        new FileInfo("testfile.ext").GetNameWithoutExtension().Should().Be("testfile");
        new FileInfo("testfile.tar.gz").GetNameWithoutExtension().Should().Be("testfile.tar");
        new FileInfo(".testfile").GetNameWithoutExtension().Should().Be("");
        new FileInfo(".testfile.ext").GetNameWithoutExtension().Should().Be(".testfile");
        new FileInfo("testfile").GetNameWithoutExtension().Should().Be("testfile");

        new Action(() => default(FileInfo).GetNameWithoutExtension()).Should().Throw<ArgumentNullException>();
    }

    [TestMethod]
    public void TestGetExtension()
    {
        new FileInfo("testfile.ext").GetExtension().Should().Be(".ext");
        new FileInfo("testfile.tar.gz").GetExtension().Should().Be(".gz");
        new FileInfo(".testfile").GetExtension().Should().Be(".testfile");
        new FileInfo(".testfile.ext").GetExtension().Should().Be(".ext");
        new FileInfo("testfile").GetExtension().Should().Be("");

        new Action(() => default(FileInfo).GetExtension()).Should().Throw<ArgumentNullException>();
    }

    [TestMethod]
    public void TestGetAnotherExtension()
    {
        new FileInfo("testfile.ext").GetAnotherExtension(".other").Name.Should().Be("testfile.other");
        new FileInfo("testfile.ext").GetAnotherExtension("other").Name.Should().Be("testfile.other");
        new FileInfo("testfile").GetAnotherExtension("other").Name.Should().Be("testfile.other");

        new Action(() => default(FileInfo).GetAnotherExtension("other")).Should().Throw<ArgumentNullException>();
    }

    [TestMethod]
    public void TestReadAllBytes()
    {
        // テスト用に一時ディレクトリ
        using var tempDir = new TempDirectory();

        // テストファイル
        var target = tempDir.Info.GetRelativeFile("test.txt");

        // テスト対象にテストデータ書き込み
        var data = Enumerable.Range(30, 256).Select(n => (byte)n).ToArray();
        File.WriteAllBytes(target.FullName, data);

        // テスト対象実行＆検証
        var actual = target.ReadAllBytes();
        actual.Should().Equal(data);
    }

    [TestMethod]
    public void TestReadAllText()
    {
        // テスト用に一時ディレクトリ
        using var tempDir = new TempDirectory();

        // テストファイル
        var target = tempDir.Info.GetRelativeFile("test.txt");

        // テスト対象にテストデータ書き込み
        var text = "あいう\nえおか";
        File.WriteAllText(target.FullName, text);

        // テスト対象実行＆検証
        var actual = target.ReadAllText();
        actual.Should().Be(text);
    }

    [TestMethod]
    public void TestReadAllText_enc()
    {
        // テスト用に一時ディレクトリ
        using var tempDir = new TempDirectory();

        // テストファイル
        var target = tempDir.Info.GetRelativeFile("test.txt");

        // テスト対象にテストデータ書き込み
        var enc = Encoding.GetEncoding("euc-jp");   // BOMのような判別方法がないもの
        var text = "あいう\nえおか";
        File.WriteAllText(target.FullName, text, enc);

        // テスト対象実行＆検証
        var actual = target.ReadAllText(enc);
        actual.Should().Be(text);
    }

    [TestMethod]
    public void TestReadAllLines()
    {
        // テスト用に一時ディレクトリ
        using var tempDir = new TempDirectory();

        // テストファイル
        var target = tempDir.Info.GetRelativeFile("test.txt");

        // テスト対象にテストデータ書き込み
        var texts = new[]
        {
            "あいう",
            "えおか",
            "きくけ",
        };
        File.WriteAllLines(target.FullName, texts);

        // テスト対象実行＆検証
        var actual = target.ReadAllLines();
        actual.Should().Equal(texts);
    }

    [TestMethod]
    public void TestReadAllLines_enc()
    {
        // テスト用に一時ディレクトリ
        using var tempDir = new TempDirectory();

        // テストファイル
        var target = tempDir.Info.GetRelativeFile("test.txt");

        // テスト対象にテストデータ書き込み
        var enc = Encoding.GetEncoding("euc-jp");   // BOMのような判別方法がないもの
        var texts = new[]
        {
            "あいう",
            "えおか",
            "きくけ",
        };
        File.WriteAllLines(target.FullName, texts, enc);

        // テスト対象実行＆検証
        var actual = target.ReadAllLines(enc);
        actual.Should().Equal(texts);
    }

    [TestMethod]
    public void TestReadLines()
    {
        // テスト用に一時ディレクトリ
        using var tempDir = new TempDirectory();

        // テストファイル
        var target = tempDir.Info.GetRelativeFile("test.txt");

        // テスト対象にテストデータ書き込み
        var texts = new[]
        {
            "あいう",
            "えおか",
            "きくけ",
        };
        File.WriteAllLines(target.FullName, texts);

        // テスト対象実行＆検証
        var actual = target.ReadLines();
        actual.Should().Equal(texts);
    }

    [TestMethod]
    public void TestReadLines_enc()
    {
        // テスト用に一時ディレクトリ
        using var tempDir = new TempDirectory();

        // テストファイル
        var target = tempDir.Info.GetRelativeFile("test.txt");

        // テスト対象にテストデータ書き込み
        var enc = Encoding.GetEncoding("euc-jp");   // BOMのような判別方法がないもの
        var texts = new[]
        {
            "あいう",
            "えおか",
            "きくけ",
        };
        File.WriteAllLines(target.FullName, texts, enc);

        // テスト対象実行＆検証
        var actual = target.ReadLines(enc);
        actual.Should().Equal(texts);
    }

    [TestMethod]
    public async Task TestReadAllBytesAsync()
    {
        // テスト用に一時ディレクトリ
        using var tempDir = new TempDirectory();

        // テストファイル
        var target = tempDir.Info.GetRelativeFile("test.txt");

        // テスト対象にテストデータ書き込み
        var data = Enumerable.Range(30, 256).Select(n => (byte)n).ToArray();
        File.WriteAllBytes(target.FullName, data);

        // テスト対象実行＆検証
        var actual = await target.ReadAllBytesAsync();
        actual.Should().Equal(data);
    }

    [TestMethod]
    public async Task TestReadAllTextAsync()
    {
        // テスト用に一時ディレクトリ
        using var tempDir = new TempDirectory();

        // テストファイル
        var target = tempDir.Info.GetRelativeFile("test.txt");

        // テスト対象にテストデータ書き込み
        var text = "あいう\nえおか";
        File.WriteAllText(target.FullName, text);

        // テスト対象実行＆検証
        var actual = await target.ReadAllTextAsync();
        actual.Should().Be(text);
    }

    [TestMethod]
    public async Task TestReadAllTextAsync_enc()
    {
        // テスト用に一時ディレクトリ
        using var tempDir = new TempDirectory();

        // テストファイル
        var target = tempDir.Info.GetRelativeFile("test.txt");

        // テスト対象にテストデータ書き込み
        var enc = Encoding.GetEncoding("euc-jp");   // BOMのような判別方法がないもの
        var text = "あいう\nえおか";
        File.WriteAllText(target.FullName, text, enc);

        // テスト対象実行＆検証
        var actual = await target.ReadAllTextAsync(enc);
        actual.Should().Be(text);
    }

    [TestMethod]
    public async Task TestReadAllLinesAsync()
    {
        // テスト用に一時ディレクトリ
        using var tempDir = new TempDirectory();

        // テストファイル
        var target = tempDir.Info.GetRelativeFile("test.txt");

        // テスト対象にテストデータ書き込み
        var texts = new[]
        {
            "あいう",
            "えおか",
            "きくけ",
        };
        File.WriteAllLines(target.FullName, texts);

        // テスト対象実行＆検証
        var actual = await target.ReadAllLinesAsync();
        actual.Should().Equal(texts);
    }

    [TestMethod]
    public async Task TestReadAllLinesAsync_enc()
    {
        // テスト用に一時ディレクトリ
        using var tempDir = new TempDirectory();

        // テストファイル
        var target = tempDir.Info.GetRelativeFile("test.txt");

        // テスト対象にテストデータ書き込み
        var enc = Encoding.GetEncoding("euc-jp");   // BOMのような判別方法がないもの
        var texts = new[]
        {
            "あいう",
            "えおか",
            "きくけ",
        };
        File.WriteAllLines(target.FullName, texts, enc);

        // テスト対象実行＆検証
        var actual = await target.ReadAllLinesAsync(enc);
        actual.Should().Equal(texts);
    }

    [TestMethod]
    public void TestWriteAllBytes()
    {
        // テスト用に一時ディレクトリ
        using var tempDir = new TempDirectory();

        // テストデータ
        var data = Enumerable.Range(30, 256).Select(n => (byte)n).ToArray();

        // テストファイル
        var target = tempDir.Info.GetRelativeFile("test.txt");

        // テスト対象実行
        target.WriteAllBytes(data);

        // 検証
        File.ReadAllBytes(target.FullName).Should().Equal(data);
    }

    [TestMethod]
    public void TestWriteAllText()
    {
        // テスト用に一時ディレクトリ
        using var tempDir = new TempDirectory();

        // テストデータ
        var text = "あいう\nえおか";

        // テストファイル
        var target = tempDir.Info.GetRelativeFile("test.txt");

        // テスト対象実行
        target.WriteAllText(text);

        // 検証
        File.ReadAllText(target.FullName).Should().Be(text);
    }

    [TestMethod]
    public void TestWriteAllText_enc()
    {
        // テスト用に一時ディレクトリ
        using var tempDir = new TempDirectory();

        // テストデータ
        var enc = Encoding.GetEncoding("euc-jp");   // BOMのような判別方法がないもの
        var text = "あいう\nえおか";

        // テストファイル
        var target = tempDir.Info.GetRelativeFile("test.txt");

        // テスト対象実行
        target.WriteAllText(text, enc);

        // 検証
        File.ReadAllText(target.FullName, enc).Should().Be(text);
    }

    [TestMethod]
    public void TestWriteAllLines()
    {
        // テスト用に一時ディレクトリ
        using var tempDir = new TempDirectory();

        // テストデータ
        var texts = new[]
        {
            "あいう",
            "えおか",
            "きくけ",
        };

        // テストファイル
        var target = tempDir.Info.GetRelativeFile("test.txt");

        // テスト対象実行
        target.WriteAllLines(texts);

        // 検証
        File.ReadAllLines(target.FullName).Should().Equal(texts);
    }

    [TestMethod]
    public void TestWriteAllLines_enc()
    {
        // テスト用に一時ディレクトリ
        using var tempDir = new TempDirectory();

        // テストデータ
        var enc = Encoding.GetEncoding("euc-jp");   // BOMのような判別方法がないもの
        var texts = new[]
        {
            "あいう",
            "えおか",
            "きくけ",
        };

        // テストファイル
        var target = tempDir.Info.GetRelativeFile("test.txt");

        // テスト対象実行
        target.WriteAllLines(texts, enc);

        // 検証
        File.ReadAllLines(target.FullName, enc).Should().Equal(texts);
    }

    [TestMethod]
    public async Task TestWriteAllBytesAsync()
    {
        // テスト用に一時ディレクトリ
        using var tempDir = new TempDirectory();

        // テストデータ
        var data = Enumerable.Range(30, 256).Select(n => (byte)n).ToArray();

        // テストファイル
        var target = tempDir.Info.GetRelativeFile("test.txt");

        // テスト対象実行
        await target.WriteAllBytesAsync(data);

        // 検証
        File.ReadAllBytes(target.FullName).Should().Equal(data);
    }

    [TestMethod]
    public async Task TestWriteAllTextAsync()
    {
        // テスト用に一時ディレクトリ
        using var tempDir = new TempDirectory();

        // テストデータ
        var text = "あいう\nえおか";

        // テストファイル
        var target = tempDir.Info.GetRelativeFile("test.txt");

        // テスト対象実行
        await target.WriteAllTextAsync(text);

        // 検証
        File.ReadAllText(target.FullName).Should().Be(text);
    }

    [TestMethod]
    public async Task TestWriteAllTextAsync_enc()
    {
        // テスト用に一時ディレクトリ
        using var tempDir = new TempDirectory();

        // テストデータ
        var enc = Encoding.GetEncoding("euc-jp");   // BOMのような判別方法がないもの
        var text = "あいう\nえおか";

        // テストファイル
        var target = tempDir.Info.GetRelativeFile("test.txt");

        // テスト対象実行
        await target.WriteAllTextAsync(text, enc);

        // 検証
        File.ReadAllText(target.FullName, enc).Should().Be(text);
    }

    [TestMethod]
    public async Task TestWriteAllLinesAsync()
    {
        // テスト用に一時ディレクトリ
        using var tempDir = new TempDirectory();

        // テストデータ
        var texts = new[]
        {
            "あいう",
            "えおか",
            "きくけ",
        };

        // テストファイル
        var target = tempDir.Info.GetRelativeFile("test.txt");

        // テスト対象実行
        await target.WriteAllLinesAsync(texts);

        // 検証
        File.ReadAllLines(target.FullName).Should().Equal(texts);
    }

    [TestMethod]
    public async Task TestWriteAllLinesAsync_enc()
    {
        // テスト用に一時ディレクトリ
        using var tempDir = new TempDirectory();

        // テストデータ
        var enc = Encoding.GetEncoding("euc-jp");   // BOMのような判別方法がないもの
        var texts = new[]
        {
            "あいう",
            "えおか",
            "きくけ",
        };

        // テストファイル
        var target = tempDir.Info.GetRelativeFile("test.txt");

        // テスト対象実行
        await target.WriteAllLinesAsync(texts, enc);

        // 検証
        File.ReadAllLines(target.FullName, enc).Should().Equal(texts);
    }

    [TestMethod]
    public void TestGetPathSegments()
    {
        new FileInfo(@"c:/abc/def/zxc/asd/qwe.txt").GetPathSegments()
            .Should().Equal(new[] { @"c:\", "abc", "def", "zxc", "asd", "qwe.txt", });

        new FileInfo(@"c:/abc/../asd/qwe.txt").GetPathSegments()
            .Should().Equal(new[] { @"c:\", "asd", "qwe.txt", });

        new FileInfo(@"/abc/def/qwe.txt").GetPathSegments()
            .Should().HaveElementAt(0, Path.GetPathRoot(Environment.CurrentDirectory))
            .And.Subject.Skip(1).Should().Equal(new[] { "abc", "def", "qwe.txt", });

        new FileInfo(@"\\aaa\bbb\ccc\ddd").GetPathSegments()
            .Should().Equal(new[] { @"\\aaa\bbb", "ccc", "ddd", });
    }

    [TestMethod]
    public void TestRelativePathFrom()
    {
        new FileInfo(@"c:/abc/def/ghi/asd/qwe.txt").RelativePathFrom(new DirectoryInfo(@"c:/abc/def/ghi/"), ignoreCase: true)
            .Should().Be(@"asd\qwe.txt");

        new FileInfo(@"c:/abc/def/ghi/qwe.txt").RelativePathFrom(new DirectoryInfo(@"c:/abc/def/ghi/"), ignoreCase: true)
            .Should().Be(@"qwe.txt");

        new FileInfo(@"c:/abc/def/zxc/asd/qwe.txt").RelativePathFrom(new DirectoryInfo(@"c:/abc/def/ghi/"), ignoreCase: true)
            .Should().Be(@"..\zxc\asd\qwe.txt");

        new FileInfo(@"D:/abc/def/zxc/asd/qwe.txt").RelativePathFrom(new DirectoryInfo(@"c:/abc/def/ghi/"), ignoreCase: true)
            .Should().Be(@"D:\abc\def\zxc\asd\qwe.txt");

        new FileInfo(@"\\aaa\bbb\ccc").RelativePathFrom(new DirectoryInfo(@"c:/abc/def/ghi/"), ignoreCase: true)
            .Should().Be(@"\\aaa\bbb\ccc");

        new FileInfo(@"\\aaa\bbb\ccc\eee\fff").RelativePathFrom(new DirectoryInfo(@"\\aaa\bbb\ccc\ddd"), ignoreCase: true)
            .Should().Be(@"..\eee\fff");

        new FileInfo(@"\\aaa\ggg\ccc\eee\fff").RelativePathFrom(new DirectoryInfo(@"\\aaa\bbb\ccc\ddd"), ignoreCase: true)
            .Should().Be(@"\\aaa\ggg\ccc\eee\fff");
    }

    [TestMethod]
    public void TestRelativePathFrom_IgnoreCase()
    {
        new FileInfo(@"c:/abc/def/ghi/asd/qwe.txt").RelativePathFrom(new DirectoryInfo(@"c:/abc/def/ghi/"), ignoreCase: false)
            .Should().Be(@"asd\qwe.txt");

        new FileInfo(@"c:/abc/def/Ghi/asd/qwe.txt").RelativePathFrom(new DirectoryInfo(@"c:/abc/def/ghi/"), ignoreCase: false)
            .Should().Be(@"..\Ghi\asd\qwe.txt");
    }


}
