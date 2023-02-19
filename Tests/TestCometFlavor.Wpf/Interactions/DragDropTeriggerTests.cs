using System.Windows;
using CometFlavor.Wpf.Converters;
using CometFlavor.Wpf.Interactions;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using TestCometFlavor.Wpf._Test;

namespace TestCometFlavor.Wpf.Interactions;

[TestClass]
public class DragDropTeriggerTests
{
    [TestMethod]
    public void Construct()
    {
        var target = new DragDropTerigger();
        target.AcceptDropFormats.Should().BeNull();
        target.AcceptDropEffect.Should().Be(DragDropEffects.All);
        target.ParameterConverter.Should().BeNull();
        target.AutoAllowDrop.Should().Be(true);
    }

    [TestMethod]
    public void AcceptDropFormats_NoSpecify()
    {
        // テスト対象のアクションを呼び出すためのトリガ作成
        var element = new UIElement();
        var action = new TestAction();

        // テスト対象の準備
        var trigger = new DragDropTerigger();
        trigger.AcceptDropEffect = DragDropEffects.Move;

        // 要素にアタッチ
        trigger.Attach(element);
        trigger.Actions.Add(action);

        // ドロップデータのモック
        var dataMock = new TestDataObject();

        // テスト用のイベントパラメータ生成
        var args = TestActivator.CreateDragEventArgs(dataMock.Object, allowedEffects: DragDropEffects.All);
        args.RoutedEvent = UIElement.DragOverEvent;

        // 書式指定無ければなんでも受け付けられる
        {
            // イベントデータ設定
            args.Effects = DragDropEffects.None;

            // イベントを発生させる
            args.Handled = false;
            element.RaiseEvent(args);

            // イベント処理結果検証
            args.Effects.Should().Be(DragDropEffects.Move);
        }

        // 書式指定が空
        trigger.AcceptDropFormats = new string[0];

        // 空の場合もなんでも受け付けられる
        {
            // イベントデータ設定
            args.Effects = DragDropEffects.None;

            // イベントを発生させる
            args.Handled = false;
            element.RaiseEvent(args);

            // イベント処理結果検証
            args.Effects.Should().Be(DragDropEffects.Move);
        }
    }

    [TestMethod]
    public void AcceptDropFormats_FromProperty()
    {
        // テスト対象のアクションを呼び出すためのトリガ作成
        var element = new UIElement();
        var action = new TestAction();

        // テスト対象の準備
        var trigger = new DragDropTerigger();
        trigger.AcceptDropFormats = new[] { "Data1", "Data2", };
        trigger.AcceptDropEffect = DragDropEffects.Move;

        // 要素にアタッチ
        trigger.Attach(element);
        trigger.Actions.Add(action);

        // ドロップデータのモック
        var dataMock = new TestDataObject();

        // テスト用のイベントパラメータ生成
        var args = TestActivator.CreateDragEventArgs(dataMock.Object, allowedEffects: DragDropEffects.All);
        args.RoutedEvent = UIElement.DragOverEvent;

        // プロパティで指定される書式に含めば受け付けられる
        {
            // イベントデータ設定
            dataMock.Setup_GetDataPresent("Data1", () => true);
            dataMock.Setup_GetDataPresent("Data2", () => false);
            args.Effects = DragDropEffects.None;

            // イベントを発生させる
            args.Handled = false;
            element.RaiseEvent(args);

            // イベント処理結果検証
            args.Effects.Should().Be(DragDropEffects.Move);
        }

        // プロパティで指定される書式に含めば受け付けられる
        {
            // イベントデータ設定
            dataMock.Setup_GetDataPresent("Data1", () => false);
            dataMock.Setup_GetDataPresent("Data2", () => true);
            args.Effects = DragDropEffects.None;

            // イベントを発生させる
            args.Handled = false;
            element.RaiseEvent(args);

            // イベント処理結果検証
            args.Effects.Should().Be(DragDropEffects.Move);
        }

        // プロパティでの指定に含まなければ受け付けない
        {
            // イベントデータ設定
            dataMock.Setup_GetDataPresent("Data1", () => false);
            dataMock.Setup_GetDataPresent("Data2", () => false);
            args.Effects = DragDropEffects.None;

            // イベントを発生させる
            args.Handled = false;
            element.RaiseEvent(args);

            // イベント処理結果検証
            args.Effects.Should().Be(DragDropEffects.None);
        }
    }

    [TestMethod]
    public void AcceptDropFormats_FromConverter()
    {
        // テスト対象のアクションを呼び出すためのトリガ作成
        var element = new UIElement();
        var action = new TestAction();

        // 専用インターフェースを実装したコンバータ
        var converter = new Mock<IDragDropTriggerParameterConverter>();
        converter.Setup(m => m.AcceptFormats).Returns(() => new[] { "Conv1", "Conv2", });

        // テスト対象の準備
        var trigger = new DragDropTerigger();
        trigger.AcceptDropEffect = DragDropEffects.Move;
        trigger.ParameterConverter = converter.Object;

        // 要素にアタッチ
        trigger.Attach(element);
        trigger.Actions.Add(action);

        // ドロップデータのモック
        var dataMock = new TestDataObject();

        // テスト用のイベントパラメータ生成
        var args = TestActivator.CreateDragEventArgs(dataMock.Object, allowedEffects: DragDropEffects.All);
        args.RoutedEvent = UIElement.DragOverEvent;

        // コンバータで指定される書式に含めば受け付けられる
        {
            // イベントデータ設定
            dataMock.Setup_GetDataPresent("Conv1", () => true);
            dataMock.Setup_GetDataPresent("Conv2", () => false);
            args.Effects = DragDropEffects.None;

            // イベントを発生させる
            args.Handled = false;
            element.RaiseEvent(args);

            // イベント処理結果検証
            args.Effects.Should().Be(DragDropEffects.Move);
        }

        // コンバータで指定される書式に含めば受け付けられる
        {
            // イベントデータ設定
            dataMock.Setup_GetDataPresent("Conv1", () => false);
            dataMock.Setup_GetDataPresent("Conv2", () => true);
            args.Effects = DragDropEffects.None;

            // イベントを発生させる
            args.Handled = false;
            element.RaiseEvent(args);

            // イベント処理結果検証
            args.Effects.Should().Be(DragDropEffects.Move);
        }

        // コンバータでの指定に含まなければ受け付けない
        {
            // イベントデータ設定
            dataMock.Setup_GetDataPresent("Conv1", () => false);
            dataMock.Setup_GetDataPresent("Conv2", () => false);
            args.Effects = DragDropEffects.None;

            // イベントを発生させる
            args.Handled = false;
            element.RaiseEvent(args);

            // イベント処理結果検証
            args.Effects.Should().Be(DragDropEffects.None);
        }
    }

    [TestMethod]
    public void AcceptDropFormats_FromBoth()
    {
        // テスト対象のアクションを呼び出すためのトリガ作成
        var element = new UIElement();
        var action = new TestAction();

        // 専用インターフェースを実装したコンバータ
        var converter = new Mock<IDragDropTriggerParameterConverter>();
        converter.Setup(m => m.AcceptFormats).Returns(() => new[] { "Conv1", "Conv2", });

        // テスト対象の準備
        var trigger = new DragDropTerigger();
        trigger.AcceptDropFormats = new[] { "Data1", "Data2", };
        trigger.AcceptDropEffect = DragDropEffects.Move;
        trigger.ParameterConverter = converter.Object;

        // 要素にアタッチ
        trigger.Attach(element);
        trigger.Actions.Add(action);

        // ドロップデータのモック
        var dataMock = new TestDataObject();

        // テスト用のイベントパラメータ生成
        var args = TestActivator.CreateDragEventArgs(dataMock.Object, allowedEffects: DragDropEffects.All);
        args.RoutedEvent = UIElement.DragOverEvent;

        // 両方指定されていればプロパティ優先。コンバータの持っている書式指定は無効。
        {
            // イベントデータ設定
            dataMock.Setup_GetDataPresent("Conv1", () => true);
            dataMock.Setup_GetDataPresent("Conv2", () => false);
            dataMock.Setup_GetDataPresent("Data1", () => false);
            dataMock.Setup_GetDataPresent("Data2", () => false);
            args.Effects = DragDropEffects.None;

            // イベントを発生させる
            args.Handled = false;
            element.RaiseEvent(args);

            // イベント処理結果検証
            args.Effects.Should().Be(DragDropEffects.None);
        }

        // 両方指定されていればプロパティ優先。プロパティの指定を受け付ける。
        {
            // イベントデータ設定
            dataMock.Setup_GetDataPresent("Conv1", () => false);
            dataMock.Setup_GetDataPresent("Conv2", () => false);
            dataMock.Setup_GetDataPresent("Data1", () => true);
            dataMock.Setup_GetDataPresent("Data2", () => false);
            args.Effects = DragDropEffects.None;

            // イベントを発生させる
            args.Handled = false;
            element.RaiseEvent(args);

            // イベント処理結果検証
            args.Effects.Should().Be(DragDropEffects.Move);
        }

        // プロパティでの書式指定値が null 
        trigger.AcceptDropFormats = null;

        // 明示的な指定があれば全許可の値も効く。コンバータの書式指定ではなく、プロパティに null 設定したものが効く。(なのでなんでも受付になっている)
        {
            // イベントデータ設定
            dataMock.Setup_GetDataPresent("Conv1", () => true);
            dataMock.Setup_GetDataPresent("Conv2", () => false);
            dataMock.Setup_GetDataPresent("Data1", () => false);
            dataMock.Setup_GetDataPresent("Data2", () => false);
            args.Effects = DragDropEffects.None;

            // イベントを発生させる
            args.Handled = false;
            element.RaiseEvent(args);

            // イベント処理結果検証
            args.Effects.Should().Be(DragDropEffects.Move);
        }

    }

    [TestMethod]
    public void AcceptDropFormats_UnrelatedConverter()
    {
        // テスト対象のアクションを呼び出すためのトリガ作成
        var element = new UIElement();
        var action = new TestAction();

        // 専用インターフェースを実装していない(ただAcceptFormatsプロパティがあるだけの)コンバータ
        var converter = new TestHasFormatsValueConverter();
        converter.AcceptFormats = new[] { "Conv1", "Conv2", };

        // テスト対象の準備
        var trigger = new DragDropTerigger();
        trigger.AcceptDropEffect = DragDropEffects.Move;
        trigger.ParameterConverter = converter;

        // 要素にアタッチ
        trigger.Attach(element);
        trigger.Actions.Add(action);

        // ドロップデータのモック
        var dataMock = new TestDataObject();

        // テスト用のイベントパラメータ生成
        var args = TestActivator.CreateDragEventArgs(dataMock.Object, allowedEffects: DragDropEffects.All);
        args.RoutedEvent = UIElement.DragOverEvent;

        // 専用インターフェスでなければコンパータでの指定はできない。プロパティ指定もしていなければ何でも受け付ける状態。
        {
            // イベントデータ設定
            dataMock.Setup_GetDataPresent("Conv1", () => false);
            dataMock.Setup_GetDataPresent("Conv2", () => false);
            args.Effects = DragDropEffects.None;

            // イベントを発生させる
            args.Handled = false;
            element.RaiseEvent(args);

            // イベント処理結果検証
            args.Effects.Should().Be(DragDropEffects.Move);
        }
    }

    [TestMethod]
    public void AutoAllowDrop_Enable()
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

        // 設定の影響を確認
        element.AllowDrop.Should().Be(true);

        // 要素からデタッチ
        trigger.Detach();

        // 元の設定に戻ることを確認
        element.AllowDrop.Should().Be(false);
    }

    [TestMethod]
    public void AutoAllowDrop_Disable1()
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

        // 設定の影響を確認
        element.AllowDrop.Should().Be(false);
    }

    [TestMethod]
    public void AutoAllowDrop_Disable2()
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

        // 設定の影響を確認
        element.AllowDrop.Should().Be(true);
    }

    [TestMethod]
    public void DragOver_Accept()
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
    public void DragOver_Accept_EffectNoMatch()
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
    public void DragOver_AllAccept()
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
    public void DragOver_Reject()
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
    public void DragDrop_Direct()
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
    public void DragDrop_Convert()
    {
        // テスト対象のアクションを呼び出すためのトリガ作成
        var element = new UIElement();
        var action = new TestAction();

        // テスト対象の準備
        var trigger = new DragDropTerigger();
        trigger.ParameterConverter = new DragEventArgsToFilePathConverter();

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
