using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CometFlavor.Extensions.IO;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestCometFlavor._Test;

namespace TestCometFlavor.Extensions.IO;

[TestClass]
public class CombinedDisposablesTest
{
    [TestMethod]
    public void TestGetRelativeFile()
    {
        new DirectoryInfo(@"X:\abc\def")
            .GetRelativeFile(@"ghi\jkl")
            .Should().BeOfType<FileInfo>()
            .Which.FullName.Should().Be(@"X:\abc\def\ghi\jkl");

        new DirectoryInfo(@"X:\abc\def")
            .GetRelativeFile(@".\ghi\jkl")
            .Should().BeOfType<FileInfo>()
            .Which.FullName.Should().Be(@"X:\abc\def\ghi\jkl");
    }

    [TestMethod]
    public void TestGetRelativeFile_Traversal()
    {
        new DirectoryInfo(@"X:\abc\def")
            .GetRelativeFile(@"..\ghi\jkl")
            .Should().BeOfType<FileInfo>()
            .Which.FullName.Should().Be(@"X:\abc\ghi\jkl");

        new DirectoryInfo(@"X:\abc\def")
            .GetRelativeFile(@"..\def\jkl")
            .Should().BeOfType<FileInfo>()
            .Which.FullName.Should().Be(@"X:\abc\def\jkl");

        new DirectoryInfo(@"X:\abc\def")
            .GetRelativeFile(@"..\..\..\jkl")
            .Should().BeOfType<FileInfo>()
            .Which.FullName.Should().Be(@"X:\jkl");
    }

    [TestMethod]
    public void TestGetRelativeFile_Absolute()
    {
        new DirectoryInfo(@"X:\abc\def")
            .GetRelativeFile(@"V:\hoge\fuga")
            .Should().BeOfType<FileInfo>()
            .Which.FullName.Should().Be(@"V:\hoge\fuga");
    }

    [TestMethod]
    public void TestGetRelativeFile_EmptyRelativeFail()
    {
        FluentActions.Invoking(() => new DirectoryInfo(@"X:\abc\def").GetRelativeFile(""))
            .Should().Throw<ArgumentException>();

        FluentActions.Invoking(() => new DirectoryInfo(@"X:\abc\def").GetRelativeFile(" "))
            .Should().Throw<ArgumentException>();

        FluentActions.Invoking(() => new DirectoryInfo(@"X:\abc\def").GetRelativeFile(null))
            .Should().Throw<ArgumentException>();
    }

    [TestMethod]
    public void TestGetRelativeFile_NullSelfFail()
    {
        FluentActions.Invoking(() => default(DirectoryInfo).GetRelativeFile(@"V:\hoge\fuga"))
            .Should().Throw<ArgumentNullException>();
    }

    [TestMethod]
    public void TestGetRelativeDirectory()
    {
        new DirectoryInfo(@"X:\abc\def")
            .GetRelativeDirectory(@"ghi\jkl")
            .Should().BeOfType<DirectoryInfo>()
            .Which.FullName.Should().Be(@"X:\abc\def\ghi\jkl");

        new DirectoryInfo(@"X:\abc\def")
            .GetRelativeDirectory(@".\ghi\jkl")
            .Should().BeOfType<DirectoryInfo>()
            .Which.FullName.Should().Be(@"X:\abc\def\ghi\jkl");
    }

    [TestMethod]
    public void TestGetRelativeDirectory_Traversal()
    {
        new DirectoryInfo(@"X:\abc\def")
            .GetRelativeDirectory(@"..\ghi\jkl")
            .Should().BeOfType<DirectoryInfo>()
            .Which.FullName.Should().Be(@"X:\abc\ghi\jkl");

        new DirectoryInfo(@"X:\abc\def")
            .GetRelativeDirectory(@"..\def\jkl")
            .Should().BeOfType<DirectoryInfo>()
            .Which.FullName.Should().Be(@"X:\abc\def\jkl");

        new DirectoryInfo(@"X:\abc\def")
            .GetRelativeDirectory(@"..\..\..\jkl")
            .Should().BeOfType<DirectoryInfo>()
            .Which.FullName.Should().Be(@"X:\jkl");
    }

    [TestMethod]
    public void TestGetRelativeDirectory_Absolute()
    {
        new DirectoryInfo(@"X:\abc\def")
            .GetRelativeDirectory(@"V:\hoge\fuga")
            .Should().BeOfType<DirectoryInfo>()
            .Which.FullName.Should().Be(@"V:\hoge\fuga");
    }

    [TestMethod]
    public void TestGetRelativeDirectory_EmptyRelative()
    {
        new DirectoryInfo(@"X:\abc\def")
            .GetRelativeDirectory("")
            .Should().BeOfType<DirectoryInfo>()
            .Which.FullName.Should().Be(@"X:\abc\def");

        new DirectoryInfo(@"X:\abc\def")
            .GetRelativeDirectory(null)
            .Should().BeOfType<DirectoryInfo>()
            .Which.FullName.Should().Be(@"X:\abc\def");
    }

    [TestMethod]
    public void TestGetRelativeDirectory_NullFail()
    {
        FluentActions.Invoking(() => default(DirectoryInfo).GetRelativeDirectory(@"V:\hoge\fuga"))
            .Should().Throw<ArgumentNullException>();
    }

    [TestMethod]
    public void TestWithCreate()
    {
        using var testDir = new TempDirectory();

        var tempDir = testDir.Info.FullName;
        var subDir = Path.Combine(tempDir, "ttt");
        new DirectoryInfo(subDir).WithCreate().FullName.Should().Be(subDir);
        Directory.Exists(subDir).Should().BeTrue();
    }

    [TestMethod]
    public void TestGetPathSegments()
    {
        new DirectoryInfo(@"c:/abc/def/zxc/asd/qwe.txt").GetPathSegments()
            .Should().Equal(new[] { @"c:\", "abc", "def", "zxc", "asd", "qwe.txt", });

        new DirectoryInfo(@"c:/abc/../asd/qwe.txt").GetPathSegments()
            .Should().Equal(new[] { @"c:\", "asd", "qwe.txt", });

        new DirectoryInfo(@"/abc/def/qwe.txt").GetPathSegments()
            .Should().HaveElementAt(0, Path.GetPathRoot(Environment.CurrentDirectory))
            .And.Subject.Skip(1).Should().Equal(new[] { "abc", "def", "qwe.txt", });

        new DirectoryInfo(@"\\aaa\bbb\ccc\ddd").GetPathSegments()
            .Should().Equal(new[] { @"\\aaa\bbb", "ccc", "ddd", });
    }

    [TestMethod]
    public void TestIsDescendantOf()
    {
        new DirectoryInfo(@"c:/abc/def/zxc/asd")
            .IsDescendantOf(new DirectoryInfo(@"c:/abc/def/zxc"), false)
            .Should().BeTrue();

        new DirectoryInfo(@"c:/abc/def/zxc/asd")
            .IsDescendantOf(new DirectoryInfo(@"c:/abc/def"), false)
            .Should().BeTrue();

        new DirectoryInfo(@"c:/abc/def/zxc/asd")
            .IsDescendantOf(new DirectoryInfo(@"c:"), false)
            .Should().BeTrue();

        new DirectoryInfo(@"c:/abc/def/zxc/asd")
            .IsDescendantOf(new DirectoryInfo(@"C:/ABC/DEF/ZXC"), false)
            .Should().BeTrue();

        new DirectoryInfo(@"c:/abc/def/zxc/asd")
            .IsDescendantOf(new DirectoryInfo(@"c:/abc/def/zxc/asd/qwe"), false)
            .Should().BeFalse();

        new DirectoryInfo(@"c:/abc/def/zxc/asd")
            .IsDescendantOf(new DirectoryInfo(@"c:/abc/def/zxc/asd"), false)
            .Should().BeFalse();

        new DirectoryInfo(@"c:/abc/def/zxc/asd")
            .IsDescendantOf(new DirectoryInfo(@"c:/abc/def/zxc/asd"), true)
            .Should().BeTrue();
    }

    [TestMethod]
    public void TestIsAncestorOf()
    {
        new DirectoryInfo(@"c:/abc/def/zxc")
            .IsAncestorOf(new DirectoryInfo(@"c:/abc/def/zxc/asd"), false)
            .Should().BeTrue();

        new DirectoryInfo(@"c:/abc/def")
            .IsAncestorOf(new DirectoryInfo(@"c:/abc/def/zxc/asd"), false)
            .Should().BeTrue();

        new DirectoryInfo(@"c:")
            .IsAncestorOf(new DirectoryInfo(@"c:/abc/def/zxc/asd"), false)
            .Should().BeTrue();

        new DirectoryInfo(@"C:/ABC/DEF/ZXC")
            .IsAncestorOf(new DirectoryInfo(@"c:/abc/def/zxc/asd"), false)
            .Should().BeTrue();

        new DirectoryInfo(@"c:/abc/def/zxc/asd/qwe")
            .IsAncestorOf(new DirectoryInfo(@"c:/abc/def/zxc/asd"), false)
            .Should().BeFalse();

        new DirectoryInfo(@"c:/abc/def/zxc/asd")
            .IsAncestorOf(new DirectoryInfo(@"c:/abc/def/zxc/asd"), false)
            .Should().BeFalse();

        new DirectoryInfo(@"c:/abc/def/zxc/asd")
            .IsAncestorOf(new DirectoryInfo(@"c:/abc/def/zxc/asd"), true)
            .Should().BeTrue();
    }

    [TestMethod]
    public void TestRelativePathFrom()
    {
        new DirectoryInfo(@"c:/abc/def/ghi/asd/qwe").RelativePathFrom(new DirectoryInfo(@"c:/abc/def/ghi/"), ignoreCase: true)
            .Should().Be(@"asd\qwe");

        new DirectoryInfo(@"c:/abc/def/ghi/qwe").RelativePathFrom(new DirectoryInfo(@"c:/abc/def/ghi/"), ignoreCase: true)
            .Should().Be(@"qwe");

        new DirectoryInfo(@"c:/abc/def/zxc/asd/qwe").RelativePathFrom(new DirectoryInfo(@"c:/abc/def/ghi/"), ignoreCase: true)
            .Should().Be(@"..\zxc\asd\qwe");

        new DirectoryInfo(@"D:/abc/def/zxc/asd/qwe").RelativePathFrom(new DirectoryInfo(@"c:/abc/def/ghi/"), ignoreCase: true)
            .Should().Be(@"D:\abc\def\zxc\asd\qwe");

        new DirectoryInfo(@"\\aaa\bbb\ccc").RelativePathFrom(new DirectoryInfo(@"c:/abc/def/ghi/"), ignoreCase: true)
            .Should().Be(@"\\aaa\bbb\ccc");

        new DirectoryInfo(@"\\aaa\bbb\ccc\eee\fff").RelativePathFrom(new DirectoryInfo(@"\\aaa\bbb\ccc\ddd"), ignoreCase: true)
            .Should().Be(@"..\eee\fff");

        new DirectoryInfo(@"\\aaa\ggg\ccc\eee\fff").RelativePathFrom(new DirectoryInfo(@"\\aaa\bbb\ccc\ddd"), ignoreCase: true)
            .Should().Be(@"\\aaa\ggg\ccc\eee\fff");
    }

    [TestMethod]
    public void TestRelativePathFrom_IgnoreCase()
    {
        new DirectoryInfo(@"c:/abc/def/ghi/asd/qwe").RelativePathFrom(new DirectoryInfo(@"c:/abc/def/ghi/"), ignoreCase: false)
            .Should().Be(@"asd\qwe");

        new DirectoryInfo(@"c:/abc/def/Ghi/asd/qwe").RelativePathFrom(new DirectoryInfo(@"c:/abc/def/ghi/"), ignoreCase: false)
            .Should().Be(@"..\Ghi\asd\qwe");
    }

    [TestMethod]
    public async Task TestSelectFiles_TopOnly()
    {
        using var testDir = new TempDirectory();

        var testFiles = new[]
        {
            @"abc/aaa.txt",
            @"def/ghi/bbb.txt",
            @"ccc.txt",
            @"abc/ddd.txt",
            @"eee.txt",
            @"abc/fff.txt",
            @"asd/qwe/ggg.txt",
            @"hhh.txt",
        };

        foreach (var data in testFiles)
        {
            var file = new FileInfo(Path.Combine(testDir.Info.FullName, data));
            file.Directory.Create();
            await file.WriteAllTextAsync(data);
        }

        var testOpt = new SelectFilesOptions
        {
            Recurse = false,
            Buffered = false,
            Sort = false,
        };

        var selector = (FileInfo file) => file.ReadAllText();
        var converter = (IFileConverter<string> context) => context.SetResult(selector(context.File));

        var testExpects = testDir.Info.EnumerateFiles("*", SearchOption.TopDirectoryOnly).Select(selector);

        testDir.Info.SelectFiles(converter, options: testOpt).Should().BeEquivalentTo(testExpects);
    }

    [TestMethod]
    public async Task TestSelectFiles_Recurse()
    {
        using var testDir = new TempDirectory();

        var testFiles = new[]
        {
            @"abc/aaa.txt",
            @"def/ghi/bbb.txt",
            @"ccc.txt",
            @"abc/ddd.txt",
            @"eee.txt",
            @"abc/fff.txt",
            @"asd/qwe/ggg.txt",
            @"hhh.txt",
        };

        foreach (var data in testFiles)
        {
            var file = new FileInfo(Path.Combine(testDir.Info.FullName, data));
            file.Directory.Create();
            await file.WriteAllTextAsync(data);
        }

        var testOpt = new SelectFilesOptions
        {
            Recurse = true,
            Buffered = false,
            Sort = false,
        };

        var selector = (FileInfo file) => file.ReadAllText();
        var converter = (IFileConverter<string> context) =>
        {
            if (context.File == null) context.Item.Should().BeOfType<DirectoryInfo>().And.Be(context.Directory);
            else context.Item.Should().BeOfType<FileInfo>().And.Be(context.File);

            context.SetResult(selector(context.File));
        };
        var testExpects = testDir.Info.EnumerateFiles("*", SearchOption.AllDirectories).Select(selector);

        testDir.Info.SelectFiles(converter, options: testOpt).Should().BeEquivalentTo(testExpects);
    }

    [TestMethod]
    public async Task TestSelectFiles_Filter()
    {
        using var testDir = new TempDirectory();

        var testFiles = new[]
        {
            @"abc/aaa.txt",
            @"def/ghi/bbb.png",
            @"ccc.jpg",
            @"abc/ddd.txt",
            @"eee.png",
            @"abc/fff.jpg",
            @"asd/qwe/ggg.txt",
            @"hhh.txt",
        };

        foreach (var data in testFiles)
        {
            var file = new FileInfo(Path.Combine(testDir.Info.FullName, data));
            file.Directory.Create();
            await file.WriteAllTextAsync(data);
        }

        var testOpt = new SelectFilesOptions
        {
            Recurse = true,
            Buffered = false,
            Sort = false,
        };

        var selector = (FileInfo file) => file.ReadAllText();
        var filter = (FileSystemInfo item) => item is DirectoryInfo || item.Name.Contains(".txt");
        var converter = (IFileConverter<string> context) => { if (filter(context.File)) context.SetResult(selector(context.File)); };

        var testExpects = testDir.Info.EnumerateFiles("*", SearchOption.AllDirectories)
            .Where(f => filter(f)).Select(selector);

        testDir.Info.SelectFiles(converter, options: testOpt).Should().BeEquivalentTo(testExpects);
    }

    [TestMethod]
    public async Task TestSelectFiles_Filter_Dir()
    {
        using var testDir = new TempDirectory();

        var testFiles = new[]
        {
            @"abc/aaa.txt",
            @"def/ghi/bbb.png",
            @"ccc.jpg",
            @"abc/ddd.txt",
            @"eee.png",
            @"abc/fff.jpg",
            @"asd/qwe/ggg.txt",
            @"hhh.txt",
        };

        foreach (var data in testFiles)
        {
            var file = new FileInfo(Path.Combine(testDir.Info.FullName, data));
            file.Directory.Create();
            await file.WriteAllTextAsync(data);
        }

        var testOpt = new SelectFilesOptions
        {
            Recurse = true,
            DirectoryHandling = true,
            Buffered = false,
            Sort = false,
        };

        var converter = (IFileConverter<string> context) =>
        {
            if (context.File == null)
            {
                if (context.Directory.Name == "abc") context.Break = true;
            }
            else
            {
                context.SetResult(context.File.ReadAllText());
            }
        };

        testDir.Info.SelectFiles(converter, options: testOpt).Should().BeEquivalentTo(new[]
        {
            @"def/ghi/bbb.png",
            @"ccc.jpg",
            @"eee.png",
            @"asd/qwe/ggg.txt",
            @"hhh.txt",
        });
    }

    [TestMethod]
    public async Task TestSelectFilesAsync_TopOnly()
    {
        using var testDir = new TempDirectory();

        var testFiles = new[]
        {
            @"abc/aaa.txt",
            @"def/ghi/bbb.txt",
            @"ccc.txt",
            @"abc/ddd.txt",
            @"eee.txt",
            @"abc/fff.txt",
            @"asd/qwe/ggg.txt",
            @"hhh.txt",
        };

        foreach (var data in testFiles)
        {
            var file = new FileInfo(Path.Combine(testDir.Info.FullName, data));
            file.Directory.Create();
            await file.WriteAllTextAsync(data);
        }

        var testOpt = new SelectFilesOptions
        {
            Recurse = false,
            Buffered = false,
            Sort = false,
        };

        var selector = (FileInfo file) => file.ReadAllText();
        var converter = (IFileConverter<string> context) => { context.SetResult(selector(context.File)); return ValueTask.CompletedTask; };

        var testExpects = testDir.Info.EnumerateFiles("*", SearchOption.TopDirectoryOnly).Select(selector);

        (await testDir.Info.SelectFilesAsync(converter, options: testOpt).ToArrayAsync()).Should().BeEquivalentTo(testExpects);
    }

    [TestMethod]
    public async Task TestSelectFilesAsync_Recurse()
    {
        using var testDir = new TempDirectory();

        var testFiles = new[]
        {
            @"abc/aaa.txt",
            @"def/ghi/bbb.txt",
            @"ccc.txt",
            @"abc/ddd.txt",
            @"eee.txt",
            @"abc/fff.txt",
            @"asd/qwe/ggg.txt",
            @"hhh.txt",
        };

        foreach (var data in testFiles)
        {
            var file = new FileInfo(Path.Combine(testDir.Info.FullName, data));
            file.Directory.Create();
            await file.WriteAllTextAsync(data);
        }

        var testOpt = new SelectFilesOptions
        {
            Recurse = true,
            Buffered = false,
            Sort = false,
        };

        var selector = (FileInfo file) => file.ReadAllText();
        var converter = (IFileConverter<string> context) => { context.SetResult(selector(context.File)); return ValueTask.CompletedTask; };

        var testExpects = testDir.Info.EnumerateFiles("*", SearchOption.AllDirectories).Select(selector);

        (await testDir.Info.SelectFilesAsync(converter, options: testOpt).ToArrayAsync()).Should().BeEquivalentTo(testExpects);
    }

    [TestMethod]
    public async Task TestSelectFilesAsync_Filter()
    {
        using var testDir = new TempDirectory();

        var testFiles = new[]
        {
            @"abc/aaa.txt",
            @"def/ghi/bbb.png",
            @"ccc.jpg",
            @"abc/ddd.txt",
            @"eee.png",
            @"abc/fff.jpg",
            @"asd/qwe/ggg.txt",
            @"hhh.txt",
        };

        foreach (var data in testFiles)
        {
            var file = new FileInfo(Path.Combine(testDir.Info.FullName, data));
            file.Directory.Create();
            await file.WriteAllTextAsync(data);
        }

        var testOpt = new SelectFilesOptions
        {
            Recurse = true,
            Buffered = false,
            Sort = false,
        };

        var selector = (FileInfo file) => file.ReadAllText();
        var filter = (FileSystemInfo item) => item is DirectoryInfo || item.Name.Contains(".txt");
        var converter = (IFileConverter<string> context) => { if (filter(context.File)) context.SetResult(selector(context.File)); return ValueTask.CompletedTask; };

        var testExpects = testDir.Info.EnumerateFiles("*", SearchOption.AllDirectories)
            .Where(f => filter(f)).Select(selector);

        (await testDir.Info.SelectFilesAsync(converter, options: testOpt).ToArrayAsync()).Should().BeEquivalentTo(testExpects);
    }

    [TestMethod]
    public async Task TestSelectFilesAsync_Filter_Dir()
    {
        using var testDir = new TempDirectory();

        var testFiles = new[]
        {
            @"abc/aaa.txt",
            @"def/ghi/bbb.png",
            @"ccc.jpg",
            @"abc/ddd.txt",
            @"eee.png",
            @"abc/fff.jpg",
            @"asd/qwe/ggg.txt",
            @"hhh.txt",
        };

        foreach (var data in testFiles)
        {
            var file = new FileInfo(Path.Combine(testDir.Info.FullName, data));
            file.Directory.Create();
            await file.WriteAllTextAsync(data);
        }

        var testOpt = new SelectFilesOptions
        {
            Recurse = true,
            DirectoryHandling = true,
            Buffered = false,
            Sort = false,
        };

        var converter = (IFileConverter<string> context) =>
        {
            if (context.File == null)
            {
                if (context.Directory.Name == "abc") context.Break = true;
            }
            else
            {
                context.SetResult(context.File.ReadAllText());
            }
            return ValueTask.CompletedTask;
        };

        (await testDir.Info.SelectFilesAsync(converter, options: testOpt).ToArrayAsync()).Should().BeEquivalentTo(new[]
        {
            @"def/ghi/bbb.png",
            @"ccc.jpg",
            @"eee.png",
            @"asd/qwe/ggg.txt",
            @"hhh.txt",
        });
    }

    [TestMethod]
    public async Task TestDoFiles_Recurse()
    {
        using var testDir = new TempDirectory();

        var testFiles = new[]
        {
            @"abc/aaa.txt",
            @"def/ghi/bbb.txt",
            @"ccc.txt",
            @"abc/ddd.txt",
            @"eee.txt",
            @"abc/fff.txt",
            @"asd/qwe/ggg.txt",
            @"hhh.txt",
        };

        foreach (var data in testFiles)
        {
            var file = new FileInfo(Path.Combine(testDir.Info.FullName, data));
            file.Directory.Create();
            await file.WriteAllTextAsync(data);
        }

        var testOpt = new SelectFilesOptions
        {
            Recurse = true,
            Buffered = false,
            Sort = false,
        };

        var selector = (FileInfo file) => file.ReadAllText();
        var converter = (IFileConverter<string> context) => context.SetResult(selector(context.File));

        var testExpects = testDir.Info.EnumerateFiles("*", SearchOption.AllDirectories).Select(selector);

        var actionLog = new List<string>();
        testDir.Info.DoFiles(w => actionLog.Add(selector(w.File)), options: testOpt);
        actionLog.Should().BeEquivalentTo(testExpects);
    }

    [TestMethod]
    public async Task TestDoFilesAsync_Recurse()
    {
        using var testDir = new TempDirectory();

        var testFiles = new[]
        {
            @"abc/aaa.txt",
            @"def/ghi/bbb.txt",
            @"ccc.txt",
            @"abc/ddd.txt",
            @"eee.txt",
            @"abc/fff.txt",
            @"asd/qwe/ggg.txt",
            @"hhh.txt",
        };

        foreach (var data in testFiles)
        {
            var file = new FileInfo(Path.Combine(testDir.Info.FullName, data));
            file.Directory.Create();
            await file.WriteAllTextAsync(data);
        }

        var testOpt = new SelectFilesOptions
        {
            Recurse = true,
            Buffered = false,
            Sort = false,
        };

        var selector = (FileInfo file) => file.ReadAllText();
        var converter = (IFileConverter<string> context) => { context.SetResult(selector(context.File)); return ValueTask.CompletedTask; };

        var testExpects = testDir.Info.EnumerateFiles("*", SearchOption.AllDirectories).Select(selector);

        var actionLog = new List<string>();
        await testDir.Info.DoFilesAsync(w => { actionLog.Add(selector(w.File)); return ValueTask.CompletedTask; }, options: testOpt);
        actionLog.Should().BeEquivalentTo(testExpects);
    }
}
