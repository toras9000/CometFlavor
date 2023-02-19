using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using CometFlavor.Wpf.Interactions;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Reactive.Bindings;
using TestCometFlavor.Wpf._Test;

namespace TestCometFlavor.Wpf.Interactions;

[TestClass]
public class ViewValidationErrorBehaviorTests
{
    [TestMethod]
    public void Construct()
    {
        var target = new ViewValidationErrorBehavior();
        target.HasViewError.Should().BeFalse();
    }

    [STATestMethod]
    public void HasViewError_BeforeAttach()
    {
        // テスト対象の準備
        var target = new ViewValidationErrorBehavior();

        // テスト対象をアタッチするコントロールを生成
        var element = new TextBox();

        // バインドソース
        var rxp = new ReactiveProperty<int>();

        // バインド
        var binding = new Binding();
        binding.Mode = BindingMode.TwoWay;
        binding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
        binding.Source = rxp;
        binding.Path = new PropertyPath(nameof(rxp.Value));
        binding.NotifyOnValidationError = true;
        BindingOperations.SetBinding(element, TextBox.TextProperty, binding);

        // パース出来ない値でエラーを発生させておく
        element.Text = "asd";

        // アタッチ
        target.Attach(element);

        // 事前のエラー状態が反映されること
        target.HasViewError.Should().BeTrue();

        // エラー無く更新可能な値
        element.Text = "10";

        // エラー状態がなくなること
        target.HasViewError.Should().BeFalse();
    }

    [STATestMethod]
    public void HasViewError_BeforeAttach_NoInitScan()
    {
        // テスト対象の準備
        var target = new ViewValidationErrorBehavior();
        target.ScanInitialState = false;    // 初期エラー状態チェックなし

        // テスト対象をアタッチするコントロールを生成
        var element = new TextBox();

        // バインドソース
        var rxp = new ReactiveProperty<int>();

        // バインド
        var binding = new Binding();
        binding.Mode = BindingMode.TwoWay;
        binding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
        binding.Source = rxp;
        binding.Path = new PropertyPath(nameof(rxp.Value));
        binding.NotifyOnValidationError = true;
        BindingOperations.SetBinding(element, TextBox.TextProperty, binding);

        // パース出来ない値でエラーを発生させておく
        element.Text = "asd";

        // アタッチ
        target.Attach(element);

        // 事前のエラー状態は考慮されない
        target.HasViewError.Should().BeFalse();

        // 別のパース不可エラー
        element.Text = "def";

        // エラーあり状態に変わりはないので変化しない
        target.HasViewError.Should().BeFalse();

        // エラー無く更新可能な値
        element.Text = "10";

        // もちろんエラーはなく
        target.HasViewError.Should().BeFalse();

        // 再度エラーとなる値にする
        element.Text = "qwe";

        // エラーあり状態となる
        target.HasViewError.Should().BeTrue();
    }

    [STATestMethod]
    public void HasViewError_AfterAttach()
    {
        // テスト対象の準備
        var target = new ViewValidationErrorBehavior();

        // テスト対象をアタッチするコントロールを生成
        var element = new TextBox();

        // バインドソース
        var rxp = new ReactiveProperty<int>();

        // バインド
        var binding = new Binding();
        binding.Mode = BindingMode.TwoWay;
        binding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
        binding.Source = rxp;
        binding.Path = new PropertyPath(nameof(rxp.Value));
        binding.NotifyOnValidationError = true;
        BindingOperations.SetBinding(element, TextBox.TextProperty, binding);

        // アタッチ
        target.Attach(element);

        // 事前のエラーがない場合
        target.HasViewError.Should().BeFalse();

        // パース出来ない値でエラーを発生させる
        element.Text = "asd";

        // 発生したエラーを検出すること
        target.HasViewError.Should().BeTrue();

        // エラー無く更新可能な値
        element.Text = "10";

        // エラー状態がなくなること
        target.HasViewError.Should().BeFalse();
    }

    [STATestMethod]
    public void HasViewError_Detach()
    {
        // テスト対象の準備
        var target = new ViewValidationErrorBehavior();

        // テスト対象をアタッチするコントロールを生成
        var element = new TextBox();

        // バインドソース
        var rxp = new ReactiveProperty<int>();

        // バインド
        var binding = new Binding();
        binding.Mode = BindingMode.TwoWay;
        binding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
        binding.Source = rxp;
        binding.Path = new PropertyPath(nameof(rxp.Value));
        binding.NotifyOnValidationError = true;
        BindingOperations.SetBinding(element, TextBox.TextProperty, binding);

        // アタッチ
        target.Attach(element);

        // 事前のエラーがない場合
        target.HasViewError.Should().BeFalse();

        // パース出来ない値でエラーを発生させる
        element.Text = "asd";

        // 発生したエラーを検出すること
        target.HasViewError.Should().BeTrue();

        // アタッチ
        target.Detach();

        // エラー状態はクリアされることを確認
        target.HasViewError.Should().BeFalse();
    }

    [STATestMethod]
    public void HasViewError_BeforeAttach_SourceError()
    {
        // テスト対象の準備
        var target = new ViewValidationErrorBehavior();

        // テスト対象をアタッチするコントロールを生成
        var element = new TextBox();

        // バインドソース＆エラーソース
        var errors = new List<string>();
        var rxp = new ReactiveProperty<int>();
        rxp.SetValidateNotifyError(v => errors);

        // バインド
        var binding = new Binding();
        binding.Mode = BindingMode.TwoWay;
        binding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
        binding.Source = rxp;
        binding.Path = new PropertyPath(nameof(rxp.Value));
        binding.NotifyOnValidationError = true;
        BindingOperations.SetBinding(element, TextBox.TextProperty, binding);

        // ソース更新時に検証エラーありにする
        errors.Add("tekito");

        // ソースに到達する値
        element.Text = "10";

        // アタッチ
        target.Attach(element);

        // 発生したエラーを検出すること
        target.HasViewError.Should().BeTrue();

        // ソース更新時に検証エラーなしにする
        errors.Clear();

        // エラー無く更新可能な値
        element.Text = "20";

        // エラー状態がなくなること
        target.HasViewError.Should().BeFalse();
    }

    [STATestMethod]
    public void HasViewError_BeforeAttach_SourceError_NoInitScan()
    {
        // テスト対象の準備
        var target = new ViewValidationErrorBehavior();
        target.ScanInitialState = false;    // 初期エラー状態チェックなし

        // テスト対象をアタッチするコントロールを生成
        var element = new TextBox();

        // バインドソース＆エラーソース
        var errors = new List<string>();
        var rxp = new ReactiveProperty<int>();
        rxp.SetValidateNotifyError(v => errors);

        // バインド
        var binding = new Binding();
        binding.Mode = BindingMode.TwoWay;
        binding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
        binding.Source = rxp;
        binding.Path = new PropertyPath(nameof(rxp.Value));
        binding.NotifyOnValidationError = true;
        BindingOperations.SetBinding(element, TextBox.TextProperty, binding);

        // エラーありにする
        errors.Add("tekito");
        element.Text = "10";

        // アタッチ
        target.Attach(element);

        // 事前のエラー状態は考慮されない
        target.HasViewError.Should().BeFalse();

        // 別のエラーありにする
        errors.Add("hogehoge");
        element.Text = "20";

        // 違うエラーは見つけられる
        target.HasViewError.Should().BeTrue();

        // エラーなしにする
        errors.Clear();
        element.Text = "30";

        // もちろんエラーはなく
        target.HasViewError.Should().BeFalse();

        // エラーありにする
        errors.Add("tekito");
        element.Text = "40";

        // 一旦なくなった後なので検出できる
        target.HasViewError.Should().BeTrue();

    }

    [STATestMethod]
    public void HasViewError_AfterAttach_SourceError()
    {
        // テスト対象の準備
        var target = new ViewValidationErrorBehavior();

        // テスト対象をアタッチするコントロールを生成
        var element = new TextBox();

        // バインドソース＆エラーソース
        var errors = new List<string>();
        var rxp = new ReactiveProperty<int>();
        rxp.SetValidateNotifyError(v => errors);

        // バインド
        var binding = new Binding();
        binding.Mode = BindingMode.TwoWay;
        binding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
        binding.Source = rxp;
        binding.Path = new PropertyPath(nameof(rxp.Value));
        binding.NotifyOnValidationError = true;
        BindingOperations.SetBinding(element, TextBox.TextProperty, binding);

        // アタッチ
        target.Attach(element);

        // 事前のエラーがない場合
        target.HasViewError.Should().BeFalse();

        // ソース更新時に検証エラーありにする
        errors.Add("tekito");

        // ソースに到達する値
        element.Text = "10";

        // 発生したエラーを検出すること
        target.HasViewError.Should().BeTrue();

        // ソース更新時に検証エラーなしにする
        errors.Clear();

        // エラー無く更新可能な値
        element.Text = "20";

        // エラー状態がなくなること
        target.HasViewError.Should().BeFalse();
    }

    [STATestMethod]
    public void HasViewError_Multiple_BeforeAttach()
    {
        // テスト対象の準備
        var target = new ViewValidationErrorBehavior();

        // テスト対象をアタッチするコントロールを生成
        var element = new StackPanel();
        var textbox1 = new TextBox();
        var textbox2 = new TextBox();
        element.Children.Add(textbox1);
        element.Children.Add(textbox2);

        // テキストボックスにバインディング設定
        void setBinding(TextBox textbox)
        {
            var rxp = new ReactiveProperty<int>();
            var binding = new Binding();
            binding.Mode = BindingMode.TwoWay;
            binding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
            binding.Source = rxp;
            binding.Path = new PropertyPath(nameof(rxp.Value));
            binding.NotifyOnValidationError = true;
            BindingOperations.SetBinding(textbox, TextBox.TextProperty, binding);
        }
        setBinding(textbox1);
        setBinding(textbox2);

        // パース出来ない値でエラーを発生させておく
        textbox1.Text = "asd";
        textbox2.Text = "asd";

        // アタッチ
        target.Attach(element);

        // 事前のエラー状態が反映されること
        target.HasViewError.Should().BeTrue();

        // 1つのエラー状態を消去
        textbox1.Text = "100";

        // まだエラー状態であること
        target.HasViewError.Should().BeTrue();

        // もう1つもエラー状態を消去
        textbox2.Text = "200";

        // エラー状態がなくなること
        target.HasViewError.Should().BeFalse();
    }

    [STATestMethod]
    public void HasViewError_Multiple_AfterAttach()
    {
        // テスト対象の準備
        var target = new ViewValidationErrorBehavior();

        // テスト対象をアタッチするコントロールを生成
        var element = new StackPanel();
        var textbox1 = new TextBox();
        var textbox2 = new TextBox();
        element.Children.Add(textbox1);
        element.Children.Add(textbox2);

        // テキストボックスにバインディング設定
        void setBinding(TextBox textbox)
        {
            var rxp = new ReactiveProperty<int>();
            var binding = new Binding();
            binding.Mode = BindingMode.TwoWay;
            binding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
            binding.Source = rxp;
            binding.Path = new PropertyPath(nameof(rxp.Value));
            binding.NotifyOnValidationError = true;
            BindingOperations.SetBinding(textbox, TextBox.TextProperty, binding);
        }
        setBinding(textbox1);
        setBinding(textbox2);

        // アタッチ
        target.Attach(element);

        // 事前のエラー状態はなし
        target.HasViewError.Should().BeFalse();

        // パース出来ない値でエラーを発生させておく
        textbox1.Text = "asd";
        textbox2.Text = "asd";

        // 事前のエラー状態が反映されること
        target.HasViewError.Should().BeTrue();

        // 1つのエラー状態を消去
        textbox1.Text = "100";

        // まだエラー状態であること
        target.HasViewError.Should().BeTrue();

        // もう1つもエラー状態を消去
        textbox2.Text = "200";

        // エラー状態がなくなること
        target.HasViewError.Should().BeFalse();
    }
}
