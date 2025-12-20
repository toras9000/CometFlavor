using System;
using System.Windows;
using CometFlavor.Wpf.Interactions;
using AwesomeAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using TestCometFlavor.Wpf._Test;

namespace TestCometFlavor.Wpf.Interactions;

[TestClass]
public class ObjectDisposeActionTests
{
    [TestMethod]
    public void Construct()
    {
        // プロパティデフォルト値検証
        var target = new ObjectDisposeAction();
        target.Object.Should().BeNull();
        target.DisposeParameter.Should().BeFalse();
    }

    [TestMethod]
    public void Invoke_without_Param()
    {
        // テスト用データモック
        var propMock = new Mock<IDisposable>();
        var argMock = new Mock<IDisposable>();

        // テスト対象の準備
        var target = new ObjectDisposeAction();
        target.Object = propMock.Object;
        target.DisposeParameter = false;

        // テスト対象のアクションを呼び出すためのトリガ作成
        var element = new DependencyObject();
        var trigger = new TestTrigger();
        trigger.Attach(element);
        trigger.Actions.Add(target);

        // アクションを呼び出すためにトリガ実行
        trigger.Invoke(argMock.Object);

        // 呼び出し結果の検証
        propMock.Verify(c => c.Dispose(), Times.Once());
        argMock.Verify(c => c.Dispose(), Times.Never());
    }

    [TestMethod]
    public void Invoke_with_Param()
    {
        // テスト用データモック
        var propMock = new Mock<IDisposable>();
        var argMock = new Mock<IDisposable>();

        // テスト対象の準備
        var target = new ObjectDisposeAction();
        target.Object = propMock.Object;
        target.DisposeParameter = true;

        // テスト対象のアクションを呼び出すためのトリガ作成
        var element = new DependencyObject();
        var trigger = new TestTrigger();
        trigger.Attach(element);
        trigger.Actions.Add(target);

        // アクションを呼び出すためにトリガ実行
        trigger.Invoke(argMock.Object);

        // 呼び出し結果の検証
        propMock.Verify(c => c.Dispose(), Times.Once());
        argMock.Verify(c => c.Dispose(), Times.Once());
    }

    [TestMethod]
    public void Invoke_Multiple()
    {
        // テスト用データモック
        var propMock = new Mock<IDisposable>();
        var argMock = new Mock<IDisposable>();

        // テスト対象の準備
        var target = new ObjectDisposeAction();
        target.Object = propMock.Object;
        target.DisposeParameter = true;

        // テスト対象のアクションを呼び出すためのトリガ作成
        var element = new DependencyObject();
        var trigger = new TestTrigger();
        trigger.Attach(element);
        trigger.Actions.Add(target);

        // アクションを2回実行させる
        trigger.Invoke(argMock.Object);
        trigger.Invoke(argMock.Object);

        // 呼び出し結果の検証
        propMock.Verify(c => c.Dispose(), Times.Exactly(2));
        argMock.Verify(c => c.Dispose(), Times.Exactly(2));
    }

    [TestMethod]
    public void Invoke_DisposeError()
    {
        // テスト用データモック
        var propMock = new Mock<IDisposable>();
        var argMock = new Mock<IDisposable>();

        propMock.Setup(m => m.Dispose()).Throws(new Exception());
        argMock.Setup(m => m.Dispose()).Throws(new Exception());

        // テスト対象の準備
        var target = new ObjectDisposeAction();
        target.Object = propMock.Object;
        target.DisposeParameter = true;

        // テスト対象のアクションを呼び出すためのトリガ作成
        var element = new DependencyObject();
        var trigger = new TestTrigger();
        trigger.Attach(element);
        trigger.Actions.Add(target);

        // アクションを呼び出すためにトリガ実行
        new Action(() => trigger.Invoke(argMock.Object)).Should().NotThrow();
    }
}
