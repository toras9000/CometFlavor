using System;
using System.IO;
using System.Linq;
using CometFlavor.Extensions.IO;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

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
    public void TestGetRelativeFile_NullFail()
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
    public void TestGetRelativeDirectory_NullFail()
    {
        FluentActions.Invoking(() => default(DirectoryInfo).GetRelativeDirectory(@"V:\hoge\fuga"))
            .Should().Throw<ArgumentNullException>();
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

}
