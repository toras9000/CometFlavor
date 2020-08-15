using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows;
using System.Windows.Input;
using CometFlavor.Wpf.Interactions;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using TestCometFlavor.Wpf._Test;

namespace TestCometFlavor.Wpf.Interactions
{
    [TestClass]
    public class InvokeCommandActionTests
    {
        [TestMethod]
        public void Test_Construct()
        {
            // プロパティデフォルト値検証
            var target = new InvokeCommandAction();
            target.AutoEnable.Should().Be(true);
            target.Command.Should().BeNull();
            target.CommandParameter.Should().BeNull();
            target.CommandParameterMode.Should().Be(InvokeCommandAction.CommandParameterModeKind.Auto);
        }

        [TestMethod]
        public void Test_Invoke_ParamModeAuto1()
        {
            // Mode=Auto, プロパティ設定あり時

            // コマンドのモック
            var cmdMock = new TestCommand();
            cmdMock.Setup_CanExecute(true);

            // パラメータ用データ
            var argParam = new object();
            var propParam = new object();

            // テスト対象の準備
            var target = new InvokeCommandAction();
            target.Command = cmdMock.Object;
            target.CommandParameterMode = InvokeCommandAction.CommandParameterModeKind.Auto;
            target.CommandParameter = propParam;

            // テスト対象のアクションを呼び出すためのトリガ作成
            var element = new DependencyObject();
            var trigger = new TestTrigger();
            trigger.Attach(element);
            trigger.Actions.Add(target);

            // アクションを呼び出すためにトリガ実行
            trigger.Invoke(argParam);

            // 呼び出し結果の検証
            cmdMock.Verify(c => c.Execute(argParam), Times.Never());
            cmdMock.Verify(c => c.Execute(propParam), Times.Once());
        }

        [TestMethod]
        public void Test_Invoke_ParamModeAuto2()
        {
            // Mode=Auto, プロパティ設定なし時

            // コマンドのモック
            var cmdMock = new TestCommand();
            cmdMock.Setup_CanExecute(true);

            // パラメータ用データ
            var argParam = new object();
            var propParam = new object();

            // テスト対象の準備
            var target = new InvokeCommandAction();
            target.CommandParameterMode = InvokeCommandAction.CommandParameterModeKind.Auto;
            target.Command = cmdMock.Object;
            //target.CommandParameter = propParam; // プロパティ設定なし

            // テスト対象のアクションを呼び出すためのトリガ作成
            var element = new DependencyObject();
            var trigger = new TestTrigger();
            trigger.Attach(element);
            trigger.Actions.Add(target);

            // アクションを呼び出すためにトリガ実行
            trigger.Invoke(argParam);

            // 呼び出し結果の検証
            cmdMock.Verify(c => c.Execute(argParam), Times.Once());
            cmdMock.Verify(c => c.Execute(propParam), Times.Never());
        }

        [TestMethod]
        public void Test_Invoke_ParamModeArgument()
        {
            // Mode=Argument

            // コマンドのモック
            var cmdMock = new TestCommand();
            cmdMock.Setup_CanExecute(true);

            // パラメータ用データ
            var argParam = new object();
            var propParam = new object();

            // テスト対象の準備
            var target = new InvokeCommandAction();
            target.Command = cmdMock.Object;
            target.CommandParameterMode = InvokeCommandAction.CommandParameterModeKind.Argument;
            target.CommandParameter = propParam;

            // テスト対象のアクションを呼び出すためのトリガ作成
            var element = new DependencyObject();
            var trigger = new TestTrigger();
            trigger.Attach(element);
            trigger.Actions.Add(target);

            // アクションを呼び出すためにトリガ実行
            trigger.Invoke(argParam);

            // 呼び出し結果の検証
            cmdMock.Verify(c => c.Execute(argParam), Times.Once());
            cmdMock.Verify(c => c.Execute(propParam), Times.Never());
        }

        [TestMethod]
        public void Test_Invoke_ParamModeProperty1()
        {
            // Mode=Property

            // コマンドのモック
            var cmdMock = new TestCommand();
            cmdMock.Setup_CanExecute(true);

            // パラメータ用データ
            var argParam = new object();
            var propParam = new object();

            // テスト対象の準備
            var target = new InvokeCommandAction();
            target.Command = cmdMock.Object;
            target.CommandParameterMode = InvokeCommandAction.CommandParameterModeKind.Property;
            target.CommandParameter = propParam;

            // テスト対象のアクションを呼び出すためのトリガ作成
            var element = new DependencyObject();
            var trigger = new TestTrigger();
            trigger.Attach(element);
            trigger.Actions.Add(target);

            // アクションを呼び出すためにトリガ実行
            trigger.Invoke(argParam);

            // 呼び出し結果の検証
            cmdMock.Verify(c => c.Execute(argParam), Times.Never());
            cmdMock.Verify(c => c.Execute(propParam), Times.Once());
        }

        [TestMethod]
        public void Test_Invoke_ParamModeProperty2()
        {
            // Mode=Property (null)

            // コマンドのモック
            var cmdMock = new TestCommand();
            cmdMock.Setup_CanExecute(true);

            // パラメータ用データ
            var argParam = new object();
            var propParam = default(object);

            // テスト対象の準備
            var target = new InvokeCommandAction();
            target.Command = cmdMock.Object;
            target.CommandParameterMode = InvokeCommandAction.CommandParameterModeKind.Property;
            target.CommandParameter = propParam;

            // テスト対象のアクションを呼び出すためのトリガ作成
            var element = new DependencyObject();
            var trigger = new TestTrigger();
            trigger.Attach(element);
            trigger.Actions.Add(target);

            // アクションを呼び出すためにトリガ実行
            trigger.Invoke(argParam);

            // 呼び出し結果の検証
            cmdMock.Verify(c => c.Execute(argParam), Times.Never());
            cmdMock.Verify(c => c.Execute(propParam), Times.Once());
        }

        [STATestMethod]
        public void Test_AutoEnable_Disable_SetCommand_AttachElem()
        {
            // AutoEnable=Disable -> Set Command -> Attach Element

            // コマンドのモック
            var cmdMock = new TestCommand();
            cmdMock.Setup_CanExecute(false);

            // リンク先要素
            var element = new UIElement();
            element.IsEnabled.Should().Be(true);    // 初期状態を一応確認

            // テスト対象の準備
            var target = new InvokeCommandAction();
            target.AutoEnable = false;

            // コマンド設定
            target.Command = cmdMock.Object;

            // テスト対象のアクションを呼び出すためのトリガ作成
            var trigger = new TestTrigger();
            trigger.Attach(element);
            trigger.Actions.Add(target);

            // コマンド実行可能状態変更(→true)して、変化無いことを確認
            cmdMock.Setup_CanExecute(true);
            cmdMock.Raise_CanExecuteChanged();
            element.IsEnabled.Should().Be(true);    // 変化無いこと

            // コマンド実行可能状態変更(→false)して、変化無いことを確認
            cmdMock.Setup_CanExecute(false);
            cmdMock.Raise_CanExecuteChanged();
            element.IsEnabled.Should().Be(true);    // 変化無いこと

            // リンク無効なので呼び出されないことの検証
            cmdMock.Verify(c => c.CanExecute(It.IsAny<object>()), Times.Never());
        }

        [STATestMethod]
        public void Test_AutoEnable_Disable_AttachElem_SetCommand()
        {
            // AutoEnable=Disable -> Attach Element -> Set Command

            // コマンドのモック
            var cmdMock = new TestCommand();
            cmdMock.Setup_CanExecute(false);

            // リンク先要素
            var element = new UIElement();
            element.IsEnabled.Should().Be(true);    // 初期状態を一応確認

            // テスト対象の準備
            var target = new InvokeCommandAction();
            target.AutoEnable = false;

            // テスト対象のアクションを呼び出すためのトリガ作成＆アタッチ
            var trigger = new TestTrigger();
            trigger.Attach(element);
            trigger.Actions.Add(target);

            // コマンド設定
            target.Command = cmdMock.Object;

            // コマンド実行可能状態変更(→true)して、変化無いことを確認
            cmdMock.Setup_CanExecute(true);
            cmdMock.Raise_CanExecuteChanged();
            element.IsEnabled.Should().Be(true);    // 変化無いこと

            // コマンド実行可能状態変更(→false)して、変化無いことを確認
            cmdMock.Setup_CanExecute(false);
            cmdMock.Raise_CanExecuteChanged();
            element.IsEnabled.Should().Be(true);    // 変化無いこと

            // リンク無効なので呼び出されないことの検証
            cmdMock.Verify(c => c.CanExecute(It.IsAny<object>()), Times.Never());
        }

        [STATestMethod]
        public void Test_AutoEnable_Disable_to_Enable()
        {
            // AutoEnable=Disable -> Enable -> Disable

            // コマンドのモック
            var cmdMock = new TestCommand();
            cmdMock.Setup_CanExecute(false);

            // リンク先要素
            var element = new UIElement();
            element.IsEnabled.Should().Be(true);    // 初期状態を一応確認

            // テスト対象の準備
            var target = new InvokeCommandAction();
            target.AutoEnable = false;

            // テスト対象のアクションを呼び出すためのトリガ作成＆アタッチ
            var trigger = new TestTrigger();
            trigger.Attach(element);
            trigger.Actions.Add(target);

            // コマンド設定
            target.Command = cmdMock.Object;

            // コマンド実行可能状態変更(→true)して、変化無いことを確認
            cmdMock.Setup_CanExecute(true);
            cmdMock.Raise_CanExecuteChanged();
            element.IsEnabled.Should().Be(true);    // 初期状態から変化無いこと

            // コマンド実行可能状態変更(→false)して、変化無いことを確認
            cmdMock.Setup_CanExecute(false);
            cmdMock.Raise_CanExecuteChanged();
            element.IsEnabled.Should().Be(true);    // 初期状態から変化無いこと

            // リンク無効なので呼び出されないことの検証
            cmdMock.Verify(c => c.CanExecute(It.IsAny<object>()), Times.Never());

            // リンク有効化。状態反映される確認
            target.AutoEnable = true;
            element.IsEnabled.Should().Be(false);   // コマンド状態にリンク
            cmdMock.Verify(c => c.CanExecute(It.IsAny<object>()), Times.Once());

            // 有効状態での状態変化のリンク
            cmdMock.Setup_CanExecute(true);
            cmdMock.Raise_CanExecuteChanged();
            element.IsEnabled.Should().Be(true);    // リンクして変化すること
            cmdMock.Setup_CanExecute(false);
            cmdMock.Raise_CanExecuteChanged();
            element.IsEnabled.Should().Be(false);   // リンクして変化すること
        }

        [STATestMethod]
        public void Test_AutoEnable_Enable_SetCommand_AttachElem()
        {
            // AutoEnable=Enable -> Set Command -> Attach Element

            // コマンドのモック
            var cmdMock = new TestCommand();
            cmdMock.Setup_CanExecute(true);

            // リンク先要素
            var element = new UIElement();
            element.IsEnabled.Should().Be(true);    // 初期状態を一応確認

            // テスト対象の準備
            var target = new InvokeCommandAction();
            target.AutoEnable = true;

            // コマンド設定
            target.Command = cmdMock.Object;

            // テスト対象のアクションを呼び出すためのトリガ作成
            var trigger = new TestTrigger();
            trigger.Attach(element);
            trigger.Actions.Add(target);

            // リンク有効なのでアタッチ時点で呼び出しがあるはず
            cmdMock.Verify(c => c.CanExecute(It.IsAny<object>()), Times.AtLeastOnce());

            // コマンド実行可能状態変更(→false)して、連携することを確認
            cmdMock.Invocations.Clear();
            cmdMock.Setup_CanExecute(false);
            cmdMock.Raise_CanExecuteChanged();
            element.IsEnabled.Should().Be(false);

            // リンク有効なので呼び出しがあるはず
            cmdMock.Verify(c => c.CanExecute(It.IsAny<object>()), Times.AtLeastOnce());

            // コマンド実行可能状態変更(→false)して、連携することを確認
            cmdMock.Invocations.Clear();
            cmdMock.Setup_CanExecute(true);
            cmdMock.Raise_CanExecuteChanged();
            element.IsEnabled.Should().Be(true);

            // リンク有効なので呼び出しがあるはず
            cmdMock.Verify(c => c.CanExecute(It.IsAny<object>()), Times.AtLeastOnce());
        }

        [STATestMethod]
        public void Test_AutoEnable_Enable_AttachElem_SetCommand()
        {
            // AutoEnable=Enable -> Attach Element -> Set Command

            // コマンドのモック
            var cmdMock = new TestCommand();
            cmdMock.Setup_CanExecute(true);

            // リンク先要素
            var element = new UIElement();
            element.IsEnabled.Should().Be(true);    // 初期状態を一応確認

            // テスト対象の準備
            var target = new InvokeCommandAction();
            target.AutoEnable = true;

            // テスト対象のアクションを呼び出すためのトリガ作成
            var trigger = new TestTrigger();
            trigger.Attach(element);
            trigger.Actions.Add(target);

            // コマンド設定
            target.Command = cmdMock.Object;

            // リンク有効なのでアタッチ時点で呼び出しがあるはず
            cmdMock.Verify(c => c.CanExecute(It.IsAny<object>()), Times.AtLeastOnce());

            // コマンド実行可能状態変更(→false)して、連携することを確認
            cmdMock.Invocations.Clear();
            cmdMock.Setup_CanExecute(false);
            cmdMock.Raise_CanExecuteChanged();
            element.IsEnabled.Should().Be(false);

            // リンク有効なので呼び出しがあるはず
            cmdMock.Verify(c => c.CanExecute(It.IsAny<object>()), Times.AtLeastOnce());

            // コマンド実行可能状態変更(→false)して、連携することを確認
            cmdMock.Invocations.Clear();
            cmdMock.Setup_CanExecute(true);
            cmdMock.Raise_CanExecuteChanged();
            element.IsEnabled.Should().Be(true);

            // リンク有効なので呼び出しがあるはず
            cmdMock.Verify(c => c.CanExecute(It.IsAny<object>()), Times.AtLeastOnce());
        }

        [STATestMethod]
        public void Test_AutoEnable_Enable_SwitchCommand()
        {
            // AutoEnable=Enable -> Attach Element -> Set Command -> Switch Command

            // コマンドのモック
            var cmdMock = new TestCommand();
            cmdMock.Setup_CanExecute(false);

            // リンク先要素
            var element = new UIElement();
            element.IsEnabled.Should().Be(true);    // 初期状態を一応確認

            // テスト対象の準備
            var target = new InvokeCommandAction();
            target.AutoEnable = true;

            // テスト対象のアクションを呼び出すためのトリガ作成
            var trigger = new TestTrigger();
            trigger.Attach(element);
            trigger.Actions.Add(target);

            // コマンド設定
            target.Command = cmdMock.Object;

            using (var elementMon = element.Monitor())
            {
                // 同じ状態の別コマンドのモック
                var otherCcmd1Mock = new TestCommand();
                otherCcmd1Mock.Setup_CanExecute(false);

                // コマンド差し替え
                target.Command = otherCcmd1Mock.Object;

                // 状態は維持されるし、一時的な変化も生じていないこと
                element.IsEnabled.Should().Be(false);
                elementMon.Should().NotRaise(nameof(element.IsEnabledChanged));

                // 別状態の別コマンドのモック
                var otherCmd2Mock = new TestCommand();
                otherCmd2Mock.Setup_CanExecute(true);

                // コマンド差し替え
                target.Command = otherCmd2Mock.Object;

                // 差し替え後の状態を反映していること
                element.IsEnabled.Should().Be(true);
                elementMon.Should().Raise(nameof(element.IsEnabledChanged));
            }
        }

        [STATestMethod]
        public void Test_AutoEnable_Restore_by_Disable()
        {
            // コマンドのモック
            var cmdMock = new TestCommand();
            cmdMock.Setup_CanExecute(false);

            // リンク先要素
            var element = new UIElement();
            element.IsEnabled.Should().Be(true);    // 初期状態を一応確認

            // テスト対象の準備
            var target = new InvokeCommandAction();
            target.AutoEnable = true;

            // テスト対象のアクションを呼び出すためのトリガ作成
            var trigger = new TestTrigger();
            trigger.Attach(element);
            trigger.Actions.Add(target);

            // コマンド設定
            target.Command = cmdMock.Object;

            // コマンド実行可能状態変更が反映されて状態適用されるはず
            element.IsEnabled.Should().Be(false);

            // リンク無効化
            target.AutoEnable = false;

            // 元の有効状態に戻ること
            element.IsEnabled.Should().Be(true);
        }

        [STATestMethod]
        public void Test_AutoEnable_Restore_by_Detach()
        {
            // コマンドのモック
            var cmdMock = new TestCommand();
            cmdMock.Setup_CanExecute(false);

            // リンク先要素
            var element = new UIElement();
            element.IsEnabled.Should().Be(true);    // 初期状態を一応確認

            // テスト対象の準備
            var target = new InvokeCommandAction();
            target.AutoEnable = true;

            // テスト対象のアクションを呼び出すためのトリガ作成
            var trigger = new TestTrigger();
            trigger.Attach(element);
            trigger.Actions.Add(target);

            // コマンド設定
            target.Command = cmdMock.Object;

            // コマンド実行可能状態変更が反映されて状態適用されるはず
            element.IsEnabled.Should().Be(false);

            // 要素からデタッチ
            trigger.Actions.Clear();

            // 元の有効状態に戻ること
            element.IsEnabled.Should().Be(true);
        }

        [STATestMethod]
        public void Test_AutoEnable_Restore_by_ClearCommand()
        {
            // コマンドのモック
            var cmdMock = new TestCommand();
            cmdMock.Setup_CanExecute(false);

            // リンク先要素
            var element = new UIElement();
            element.IsEnabled.Should().Be(true);    // 初期状態を一応確認

            // テスト対象の準備
            var target = new InvokeCommandAction();
            target.AutoEnable = true;

            // テスト対象のアクションを呼び出すためのトリガ作成
            var trigger = new TestTrigger();
            trigger.Attach(element);
            trigger.Actions.Add(target);

            // コマンド設定
            target.Command = cmdMock.Object;

            // コマンド実行可能状態変更が反映されて状態適用されるはず
            element.IsEnabled.Should().Be(false);

            // コマンドクリア
            target.Command = null;

            // 元の有効状態に戻ること
            element.IsEnabled.Should().Be(true);
        }

        [TestMethod]
        public void Test_ParameterConverter_ModeArgument()
        {
            // Mode=Auto, プロパティ設定あり時

            // コマンドのモック
            var cmdMock = new TestCommand();
            cmdMock.Setup_CanExecute(true);

            // コンバータのモック
            var convMock = new TestValueConverter();
            convMock.Setup_Convert((value, t, param, c) => "test");

            // パラメータ用データ
            var argParam = new object();
            var propParam = new object();

            // テスト対象の準備
            var target = new InvokeCommandAction();
            target.Command = cmdMock.Object;
            target.CommandParameterMode = InvokeCommandAction.CommandParameterModeKind.Argument;
            target.CommandParameter = propParam;
            target.ParameterConverter = convMock.Object;

            // テスト対象のアクションを呼び出すためのトリガ作成
            var element = new DependencyObject();
            var trigger = new TestTrigger();
            trigger.Attach(element);
            trigger.Actions.Add(target);

            // アクションを呼び出すためにトリガ実行
            trigger.Invoke(argParam);

            // 呼び出し結果の検証
            cmdMock.Verify(c => c.Execute(argParam), Times.Never());
            cmdMock.Verify(c => c.Execute(propParam), Times.Never());
            cmdMock.Verify(c => c.Execute("test"), Times.Once());
            convMock.Verify(c => c.Convert(argParam, It.IsAny<Type>(), propParam, It.IsAny<CultureInfo>()), Times.Once());
        }

        [TestMethod]
        public void Test_ParameterConverter_ModeProperty()
        {
            // Mode=Auto, プロパティ設定あり時

            // コマンドのモック
            var cmdMock = new TestCommand();
            cmdMock.Setup_CanExecute(true);

            // コンバータのモック
            var convMock = new TestValueConverter();
            convMock.Setup_Convert((value, t, param, c) => "test");

            // パラメータ用データ
            var argParam = new object();
            var propParam = new object();

            // テスト対象の準備
            var target = new InvokeCommandAction();
            target.Command = cmdMock.Object;
            target.CommandParameterMode = InvokeCommandAction.CommandParameterModeKind.Property;
            target.CommandParameter = propParam;
            target.ParameterConverter = convMock.Object;

            // テスト対象のアクションを呼び出すためのトリガ作成
            var element = new DependencyObject();
            var trigger = new TestTrigger();
            trigger.Attach(element);
            trigger.Actions.Add(target);

            // アクションを呼び出すためにトリガ実行
            trigger.Invoke(argParam);

            // 呼び出し結果の検証
            cmdMock.Verify(c => c.Execute(argParam), Times.Never());
            cmdMock.Verify(c => c.Execute(propParam), Times.Never());
            cmdMock.Verify(c => c.Execute("test"), Times.Once());
            convMock.Verify(c => c.Convert(propParam, It.IsAny<Type>(), argParam, It.IsAny<CultureInfo>()), Times.Once());
        }

        [TestMethod]
        public void Test_ParameterConverter_ModeAuto1()
        {
            // Mode=Auto, プロパティ設定あり時

            // コマンドのモック
            var cmdMock = new TestCommand();
            cmdMock.Setup_CanExecute(true);

            // コンバータのモック
            var convMock = new TestValueConverter();
            convMock.Setup_Convert((value, t, param, c) => "test");

            // パラメータ用データ
            var argParam = new object();
            var propParam = new object();

            // テスト対象の準備
            var target = new InvokeCommandAction();
            target.Command = cmdMock.Object;
            target.CommandParameterMode = InvokeCommandAction.CommandParameterModeKind.Auto;
            target.CommandParameter = propParam;
            target.ParameterConverter = convMock.Object;

            // テスト対象のアクションを呼び出すためのトリガ作成
            var element = new DependencyObject();
            var trigger = new TestTrigger();
            trigger.Attach(element);
            trigger.Actions.Add(target);

            // アクションを呼び出すためにトリガ実行
            trigger.Invoke(argParam);

            // 呼び出し結果の検証
            cmdMock.Verify(c => c.Execute(argParam), Times.Never());
            cmdMock.Verify(c => c.Execute(propParam), Times.Never());
            cmdMock.Verify(c => c.Execute("test"), Times.Once());
            convMock.Verify(c => c.Convert(propParam, It.IsAny<Type>(), argParam, It.IsAny<CultureInfo>()), Times.Once());
        }

        [TestMethod]
        public void Test_ParameterConverter_ModeAuto2()
        {
            // Mode=Auto, プロパティ設定あり時

            // コマンドのモック
            var cmdMock = new TestCommand();
            cmdMock.Setup_CanExecute(true);

            // コンバータのモック
            var convMock = new TestValueConverter();
            convMock.Setup_Convert((value, t, param, c) => "test");

            // パラメータ用データ
            var argParam = new object();
            var propParam = new object();

            // テスト対象の準備
            var target = new InvokeCommandAction();
            target.Command = cmdMock.Object;
            target.CommandParameterMode = InvokeCommandAction.CommandParameterModeKind.Auto;
            //target.CommandParameter = propParam; // プロパティ設定なし
            target.ParameterConverter = convMock.Object;

            // テスト対象のアクションを呼び出すためのトリガ作成
            var element = new DependencyObject();
            var trigger = new TestTrigger();
            trigger.Attach(element);
            trigger.Actions.Add(target);

            // アクションを呼び出すためにトリガ実行
            trigger.Invoke(argParam);

            // 呼び出し結果の検証
            cmdMock.Verify(c => c.Execute(argParam), Times.Never());
            cmdMock.Verify(c => c.Execute(propParam), Times.Never());
            cmdMock.Verify(c => c.Execute("test"), Times.Once());
            convMock.Verify(c => c.Convert(argParam, It.IsAny<Type>(), null, It.IsAny<CultureInfo>()), Times.Once());
        }

    }
}
