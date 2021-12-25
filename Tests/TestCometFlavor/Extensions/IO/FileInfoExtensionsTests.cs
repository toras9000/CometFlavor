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
        actual.Should().BeEquivalentTo(data);
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
        actual.Should().BeEquivalentTo(texts);
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
        actual.Should().BeEquivalentTo(texts);
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
        actual.Should().BeEquivalentTo(texts);
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
        actual.Should().BeEquivalentTo(texts);
    }

    [TestMethod]
    public async ValueTask TestReadAllBytesAsync()
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
        actual.Should().BeEquivalentTo(data);
    }

    [TestMethod]
    public async ValueTask TestReadAllTextAsync()
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
    public async ValueTask TestReadAllTextAsync_enc()
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
    public async ValueTask TestReadAllLinesAsync()
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
        actual.Should().BeEquivalentTo(texts);
    }

    [TestMethod]
    public async ValueTask TestReadAllLinesAsync_enc()
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
        actual.Should().BeEquivalentTo(texts);
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
        File.ReadAllBytes(target.FullName).Should().BeEquivalentTo(data);
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
        File.ReadAllLines(target.FullName).Should().BeEquivalentTo(texts);
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
        File.ReadAllLines(target.FullName, enc).Should().BeEquivalentTo(texts);
    }

    [TestMethod]
    public async ValueTask TestWriteAllBytesAsync()
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
        File.ReadAllBytes(target.FullName).Should().BeEquivalentTo(data);
    }

    [TestMethod]
    public async ValueTask TestWriteAllTextAsync()
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
    public async ValueTask TestWriteAllTextAsync_enc()
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
    public async ValueTask TestWriteAllLinesAsync()
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
        File.ReadAllLines(target.FullName).Should().BeEquivalentTo(texts);
    }

    [TestMethod]
    public async ValueTask TestWriteAllLinesAsync_enc()
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
        File.ReadAllLines(target.FullName, enc).Should().BeEquivalentTo(texts);
    }

}
