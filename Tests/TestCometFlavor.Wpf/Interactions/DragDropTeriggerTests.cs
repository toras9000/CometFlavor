using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using CometFlavor.Wpf.Converters;
using CometFlavor.Wpf.Interactions;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using TestCometFlavor.Wpf._Test;

namespace TestCometFlavor.Wpf.Interactions
{
    [TestClass]
    public class DragDropTeriggerTests
    {
        [TestMethod]
        public void Test_Construct()
        {
            var target = new DragDropTerigger();
            target.AcceptDropFormats.Should().BeNull();
            target.AcceptDropEffect.Should().Be(DragDropEffects.All);
            target.ParameterConverter.Should().BeNull();
            target.AutoAllowDrop.Should().Be(true);
        }

        [TestMethod]
        public void Test_AutoAllowDrop_Enable()
        {
            // テスト対象のアクションを呼び出すためのトリガ作成
            var element = new UIElement();
            var action = new TestAction();

            // テスト対象の準備
            var trigger = new DragDropTerigger();
            trigger.AcceptDropFormats = new[] { DataFormats.FileDrop };
            trigger.AcceptDropEffect = DragDropEffects.Move;
            trigger.AutoAllowDrop = true;

            // 事前の状態チェック
            element.AllowDrop.Should().Be(false);

            // 要素にアタッチ
            trigger.Attach(element);
            trigger.Actions.Add(action);

            // 事前の状態チェック
            element.AllowDrop.Should().Be(true);
        }

        [TestMethod]
        public void Test_AutoAllowDrop_Disable1()
        {
            // テスト対象のアクションを呼び出すためのトリガ作成
            var element = new UIElement();
            var action = new TestAction();

            // テスト対象の準備
            var trigger = new DragDropTerigger();
            trigger.AcceptDropFormats = new[] { DataFormats.FileDrop };
            trigger.AcceptDropEffect = DragDropEffects.Move;
            trigger.AutoAllowDrop = false;

            // 事前の状態チェック
            element.AllowDrop.Should().Be(false);

            // 要素にアタッチ
            trigger.Attach(element);
            trigger.Actions.Add(action);

            // 事前の状態チェック
            element.AllowDrop.Should().Be(false);
        }

        [TestMethod]
        public void Test_AutoAllowDrop_Disable2()
        {
            // テスト対象のアクションを呼び出すためのトリガ作成
            var element = new UIElement();
            var action = new TestAction();

            // テスト対象の準備
            var trigger = new DragDropTerigger();
            trigger.AcceptDropFormats = new[] { DataFormats.FileDrop };
            trigger.AcceptDropEffect = DragDropEffects.Move;
            trigger.AutoAllowDrop = false;

            // 事前の状態チェック
            element.AllowDrop = true;
            element.AllowDrop.Should().Be(true);

            // 要素にアタッチ
            trigger.Attach(element);
            trigger.Actions.Add(action);

            // 事前の状態チェック
            element.AllowDrop.Should().Be(true);
        }

        [TestMethod]
        public void Test_DragOver_Accept()
        {
            // テスト対象のアクションを呼び出すためのトリガ作成
            var element = new UIElement();
            var action = new TestAction();

            // テスト対象の準備
            var trigger = new DragDropTerigger();
            trigger.AcceptDropFormats = new[] { DataFormats.FileDrop };
            trigger.AcceptDropEffect = DragDropEffects.Move;

            // 要素にアタッチ
            trigger.Attach(element);
            trigger.Actions.Add(action);

            // ドロップデータのモック
            var dataMock = new TestDataObject();
            dataMock.Setup_GetDataPresent(DataFormats.FileDrop, () => true);

            // テスト用のイベントパラメータ生成
            var args = TestActivator.CreateDragEventArgs(dataMock.Object, allowedEffects: DragDropEffects.All);
            args.RoutedEvent = UIElement.DragOverEvent;
            args.Effects = DragDropEffects.Copy;

            // イベントを発生させる
            element.RaiseEvent(args);

            // イベント処理結果検証
            args.Effects.Should().Be(DragDropEffects.Move);
        }

        [TestMethod]
        public void Test_DragOver_Accept_EffectNoMatch()
        {
            // テスト対象のアクションを呼び出すためのトリガ作成
            var element = new UIElement();
            var action = new TestAction();

            // テスト対象の準備
            var trigger = new DragDropTerigger();
            trigger.AcceptDropFormats = new[] { DataFormats.FileDrop };
            trigger.AcceptDropEffect = DragDropEffects.Move;

            // 要素にアタッチ
            trigger.Attach(element);
            trigger.Actions.Add(action);

            // ドロップデータのモック
            var dataMock = new TestDataObject();
            dataMock.Setup_GetDataPresent(DataFormats.FileDrop, () => true);

            // テスト用のイベントパラメータ生成
            var args = TestActivator.CreateDragEventArgs(dataMock.Object, allowedEffects: DragDropEffects.Copy);
            args.RoutedEvent = UIElement.DragOverEvent;
            args.Effects = DragDropEffects.Copy;

            // イベントを発生させる
            element.RaiseEvent(args);

            // イベント処理結果検証
            args.Effects.Should().Be(DragDropEffects.None);
        }

        [TestMethod]
        public void Test_DragOver_AllAccept()
        {
            // テスト対象のアクションを呼び出すためのトリガ作成
            var element = new UIElement();
            var action = new TestAction();

            // テスト対象の準備
            var trigger = new DragDropTerigger();
            trigger.AcceptDropFormats = new string[] { };
            trigger.AcceptDropEffect = DragDropEffects.Move;

            // 要素にアタッチ
            trigger.Attach(element);
            trigger.Actions.Add(action);

            // ドロップデータのモック
            var dataMock = new TestDataObject();
            dataMock.Setup_GetDataPresent((format) => false);

            // テスト用のイベントパラメータ生成
            var args = TestActivator.CreateDragEventArgs(dataMock.Object, allowedEffects: DragDropEffects.All);
            args.RoutedEvent = UIElement.DragOverEvent;
            args.Effects = DragDropEffects.Copy;

            // イベントを発生させる
            element.RaiseEvent(args);

            // イベント処理結果検証
            args.Effects.Should().Be(DragDropEffects.Move);
        }

        [TestMethod]
        public void Test_DragOver_Reject()
        {
            // テスト対象のアクションを呼び出すためのトリガ作成
            var element = new UIElement();
            var action = new TestAction();

            // テスト対象の準備
            var trigger = new DragDropTerigger();
            trigger.AcceptDropFormats = new[] { DataFormats.FileDrop };
            trigger.AcceptDropEffect = DragDropEffects.Move;

            // 要素にアタッチ
            trigger.Attach(element);
            trigger.Actions.Add(action);

            // ドロップデータのモック
            var dataMock = new TestDataObject();
            dataMock.Setup_GetDataPresent(DataFormats.FileDrop, () => false);

            // テスト用のイベントパラメータ生成
            var args = TestActivator.CreateDragEventArgs(dataMock.Object, allowedEffects: DragDropEffects.All);
            args.RoutedEvent = UIElement.DragOverEvent;
            args.Effects = DragDropEffects.Copy;

            // イベントを発生させる
            element.RaiseEvent(args);

            // イベント処理結果検証
            args.Effects.Should().Be(DragDropEffects.None);
        }

        [TestMethod]
        public void Test_DragDrop_Direct()
        {
            // テスト対象のアクションを呼び出すためのトリガ作成
            var element = new UIElement();
            var action = new TestAction();

            // テスト対象の準備
            var trigger = new DragDropTerigger();
            trigger.ParameterConverter = null;

            // 要素にアタッチ
            trigger.Attach(element);
            trigger.Actions.Add(action);

            // ドロップテストデータ
            var paths = new string[] { @"d:\path\to\data" };

            // モック
            var dataMock = new TestDataObject();
            dataMock.Setup_GetDataPresent(DataFormats.FileDrop, () => true);
            dataMock.Setup_GetData(DataFormats.FileDrop, () => paths);

            // テスト用のイベントパラメータ生成
            var args = TestActivator.CreateDragEventArgs(dataMock.Object, allowedEffects: DragDropEffects.All);
            args.RoutedEvent = UIElement.DropEvent;
            args.Effects = DragDropEffects.Copy;

            // イベントを発生させる
            element.RaiseEvent(args);

            // イベント処理結果検証
            action.InvokedParameters.Should().Contain(args);
        }

        [TestMethod]
        public void Test_DragDrop_Convert()
        {
            // テスト対象のアクションを呼び出すためのトリガ作成
            var element = new UIElement();
            var action = new TestAction();

            // テスト対象の準備
            var trigger = new DragDropTerigger();
            trigger.ParameterConverter = new FileDropParameterConverter();

            // 要素にアタッチ
            trigger.Attach(element);
            trigger.Actions.Add(action);

            // ドロップテストデータ
            var paths = new string[] { @"c:\directory\file.ext", @"d:\path\to\data" };

            // モック
            var dataMock = new TestDataObject();
            dataMock.Setup_GetDataPresent(DataFormats.FileDrop, () => true);
            dataMock.Setup_GetData(DataFormats.FileDrop, () => paths);

            // テスト用のイベントパラメータ生成
            var args = TestActivator.CreateDragEventArgs(dataMock.Object, allowedEffects: DragDropEffects.All);
            args.RoutedEvent = UIElement.DropEvent;
            args.Effects = DragDropEffects.Copy;

            // イベントを発生させる
            element.RaiseEvent(args);

            // イベント処理結果検証
            action.InvokedParameters.Should().Contain(new[] { paths });
        }
    }
}
