using System;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Windows;
using CometFlavor.Wpf.Interactions;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestCometFlavor.Wpf._Test;

namespace TestCometFlavor.Wpf.Interactions;

[TestClass]
public class ReactiveTriggerTests
{
    [TestMethod]
    public void Test_Construct()
    {
        new ReactiveTrigger<string>().Source.Should().BeNull();
        new ReactiveTrigger<int>().Source.Should().BeNull();
        new ReactiveTrigger().Source.Should().BeNull();
    }

    [TestMethod]
    public void Test_InvokeActions_int()
    {
        // テスト用トリガソース
        var cause = new Action<int>((_) => { });
        var source = Observable.FromEvent<int>(h => cause += h, h => cause -= h);

        // テスト対象の準備
        var trigger = new ReactiveTrigger<int>();
        trigger.Source = source;

        // テスト対象のアクションを呼び出すためのトリガ作成
        var element = new DependencyObject();
        var action = new TestAction();
        trigger.Attach(element);
        trigger.Actions.Add(action);

        // ソースシーケンスへの投入と結果確認
        action.InvokedParameters.Should().BeEmpty();

        action.Reset();
        cause(10);
        action.InvokedParameters.Should().AllBeOfType<int>().And.Equal(10);

        action.Reset();
        cause(20);
        cause(30);
        cause(30);
        action.InvokedParameters.Should().AllBeOfType<int>().And.Equal(20, 30, 30);
    }

    [TestMethod]
    public void Test_InvokeActions_int_SourceChange()
    {
        // テスト用トリガソース
        var cause1 = new Action<int>((_) => { });
        var cause2 = new Action<int>((_) => { });
        var source1 = Observable.FromEvent<int>(h => cause1 += h, h => cause1 -= h);
        var source2 = Observable.FromEvent<int>(h => cause2 += h, h => cause2 -= h);

        // テスト対象の準備
        var trigger = new ReactiveTrigger<int>();
        trigger.Source = source2;
        trigger.Source = source1;

        // テスト対象のアクションを呼び出すためのトリガ作成
        var element = new DependencyObject();
        var action = new TestAction();
        trigger.Attach(element);
        trigger.Actions.Add(action);

        // 現在設定しているものとは別のソース
        action.Reset();
        cause2(11);
        action.InvokedParameters.Should().BeEmpty();

        // 現在設定しているソース
        action.Reset();
        cause1(111);
        action.InvokedParameters.Should().AllBeOfType<int>().And.Equal(111);

        // ソースを再切替
        action.Reset();
        trigger.Source = source2;
        cause1(10);
        action.InvokedParameters.Should().BeEmpty();
        cause2(30);
        action.InvokedParameters.Should().AllBeOfType<int>().And.Equal(30);
    }

    [TestMethod]
    public void Test_InvokeActions_string()
    {
        // テスト用トリガソース
        var cause = new Action<string>((_) => { });
        var source = Observable.FromEvent<string>(h => cause += h, h => cause -= h);

        // テスト対象の準備
        var trigger = new ReactiveTrigger<string>();
        trigger.Source = source;

        // テスト対象のアクションを呼び出すためのトリガ作成
        var element = new DependencyObject();
        var action = new TestAction();
        trigger.Attach(element);
        trigger.Actions.Add(action);

        // ソースシーケンスへの投入と結果確認
        action.InvokedParameters.Should().BeEmpty();

        action.Reset();
        cause("a");
        action.InvokedParameters.Should().AllBeOfType<string>().And.Equal("a");

        action.Reset();
        cause("b");
        cause("c");
        cause("c");
        action.InvokedParameters.Should().AllBeOfType<string>().And.Equal("b", "c", "c");
    }

    [TestMethod]
    public void Test_InvokeActions_string_SourceChange()
    {
        // テスト用トリガソース
        var cause1 = new Action<string>((_) => { });
        var cause2 = new Action<string>((_) => { });
        var source1 = Observable.FromEvent<string>(h => cause1 += h, h => cause1 -= h);
        var source2 = Observable.FromEvent<string>(h => cause2 += h, h => cause2 -= h);

        // テスト対象の準備
        var trigger = new ReactiveTrigger<string>();
        trigger.Source = source2;
        trigger.Source = source1;

        // テスト対象のアクションを呼び出すためのトリガ作成
        var element = new DependencyObject();
        var action = new TestAction();
        trigger.Attach(element);
        trigger.Actions.Add(action);

        // 現在設定しているものとは別のソース
        action.Reset();
        cause2("asd");
        action.InvokedParameters.Should().BeEmpty();

        // 現在設定しているソース
        action.Reset();
        cause1("def");
        action.InvokedParameters.Should().AllBeOfType<string>().And.Equal("def");

        // ソースを再切替
        action.Reset();
        trigger.Source = source2;
        cause1("qaz");
        action.InvokedParameters.Should().BeEmpty();
        cause2("wsx");
        action.InvokedParameters.Should().AllBeOfType<string>().And.Equal("wsx");
    }

    [TestMethod]
    public void Test_InvokeActions_any()
    {
        // テスト用トリガソース
        var cause = new Action<object>((_) => { });
        var source = Observable.FromEvent<object>(h => cause += h, h => cause -= h);

        // テスト対象の準備
        var trigger = new ReactiveTrigger();
        trigger.Source = source;

        // テスト対象のアクションを呼び出すためのトリガ作成
        var element = new DependencyObject();
        var action = new TestAction();
        trigger.Attach(element);
        trigger.Actions.Add(action);

        // ソースシーケンスへの投入と結果確認
        action.InvokedParameters.Should().BeEmpty();

        action.Reset();
        cause("a");
        action.InvokedParameters.Should().Equal("a");

        action.Reset();
        cause(100);
        action.InvokedParameters.Should().Equal(100);

        action.Reset();
        cause(200);
        cause("c");
        cause(Tuple.Create(123, TimeSpan.FromSeconds(1)));
        action.InvokedParameters.Should().Equal(200, "c", Tuple.Create(123, TimeSpan.FromSeconds(1)));
    }

    [TestMethod]
    public void Test_InvokeActions_any_SourceChange()
    {
        // テスト用トリガソース
        var cause1 = new Action<object>((_) => { });
        var cause2 = new Action<object>((_) => { });
        var source1 = Observable.FromEvent<object>(h => cause1 += h, h => cause1 -= h);
        var source2 = Observable.FromEvent<object>(h => cause2 += h, h => cause2 -= h);

        // テスト対象の準備
        var trigger = new ReactiveTrigger();
        trigger.Source = source2;
        trigger.Source = source1;

        // テスト対象のアクションを呼び出すためのトリガ作成
        var element = new DependencyObject();
        var action = new TestAction();
        trigger.Attach(element);
        trigger.Actions.Add(action);

        // 現在設定しているものとは別のソース
        action.Reset();
        cause2("asd");
        action.InvokedParameters.Should().BeEmpty();

        // 現在設定しているソース
        action.Reset();
        cause1("def");
        cause1(123);
        action.InvokedParameters.Should().Equal("def", 123);

        // ソースを再切替
        action.Reset();
        trigger.Source = source2;
        cause1(234);
        cause1("qaz");
        action.InvokedParameters.Should().BeEmpty();
        cause2(345);
        cause2("wsx");
        action.InvokedParameters.Should().Equal(345, "wsx");
    }

    [TestMethod]
    public void Test_InvokeActions_Detach()
    {
        // テスト用トリガソース
        var cause = new Action<int>((_) => { });
        var source = Observable.FromEvent<int>(h => cause += h, h => cause -= h);

        // テスト対象の準備
        var trigger = new ReactiveTrigger<int>();
        trigger.Source = source;

        // テスト対象のアクションを呼び出すためのトリガ作成
        var element = new DependencyObject();
        var action = new TestAction();
        trigger.Attach(element);
        trigger.Actions.Add(action);

        // 一応デタッチ前の状態確認
        action.Reset();
        cause(10);
        action.InvokedParameters.Should().AllBeOfType<int>().And.Equal(10);

        // 要素からデタッチ
        trigger.Detach();

        // 一応デタッチ前の状態確認
        action.Reset();
        cause(30);
        action.InvokedParameters.Should().AllBeOfType<int>().And.Equal(30);
    }

    [TestMethod]
    public void Test_Source_OnCompleted()
    {
        // テスト用トリガソース
        var subject = new Subject<int>();

        // テスト対象の準備
        var trigger = new ReactiveTrigger<int>();
        trigger.Source = subject;

        // テスト対象のアクションを呼び出すためのトリガ作成
        var element = new DependencyObject();
        var action = new TestAction();
        trigger.Attach(element);
        trigger.Actions.Add(action);

        // 適当にシーケンスに値を流す
        action.Reset();
        subject.OnNext(10);
        subject.OnNext(11);
        subject.OnNext(12);
        action.InvokedParameters.Should().AllBeOfType<int>().And.Equal(10, 11, 12);

        // シーケンスを完了する
        subject.OnCompleted();
        action.InvokedParameters.Should().AllBeOfType<int>().And.Equal(10, 11, 12);

        // 完了後に値を流してみる
        // これは単に完了したSubjectがOnNextの呼び出しを無視するだけかもしれないが。
        subject.OnNext(20);
        action.InvokedParameters.Should().AllBeOfType<int>().And.Equal(10, 11, 12);
    }

    [TestMethod]
    public void Test_Source_OnError()
    {
        // テスト用トリガソース
        var subject = new Subject<int>();

        // テスト対象の準備
        var trigger = new ReactiveTrigger<int>();
        trigger.Source = subject;

        // テスト対象のアクションを呼び出すためのトリガ作成
        var element = new DependencyObject();
        var action = new TestAction();
        trigger.Attach(element);
        trigger.Actions.Add(action);

        // 適当にシーケンスに値を流す
        action.Reset();
        subject.OnNext(10);
        subject.OnNext(11);
        subject.OnNext(12);
        action.InvokedParameters.Should().AllBeOfType<int>().And.Equal(10, 11, 12);

        // シーケンスをエラー終了する
        subject.OnError(new Exception());
        action.InvokedParameters.Should().AllBeOfType<int>().And.Equal(10, 11, 12);

        // 完了後に値を流してみる
        // これは単に完了したSubjectがOnNextの呼び出しを無視するだけかもしれないが。
        subject.OnNext(20);
        action.InvokedParameters.Should().AllBeOfType<int>().And.Equal(10, 11, 12);
    }

}
