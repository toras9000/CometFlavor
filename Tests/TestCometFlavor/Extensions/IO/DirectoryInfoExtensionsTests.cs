using System;
using System.IO;
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

}
