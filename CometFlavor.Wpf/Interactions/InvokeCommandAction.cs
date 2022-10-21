using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using Microsoft.Xaml.Behaviors;

namespace CometFlavor.Wpf.Interactions;

/// <summary>
/// コマンド呼び出しアクション
/// </summary>
/// <remarks>
/// 同様の機能のアクションがMicrosoft.Xaml.BehaviorsやPrism等に存在するが、この実装では以下を目的として独自に実装している。
/// ・Microsoft.Xaml.Behaviors.Wpf パッケージを使用 (System.Windows.Interactivity.dll ではなく)
/// ・トリガから渡されるパラメータの使用 (プロパティでの指定と両対応)
/// ・コマンドの有効状態をとUI要素に連携
/// 
/// # 目的は Prism の実装とほぼ同じで、Prism8では依存が変更されるようなので、それがリリースされたらこのアクションは必要ないかもしれない。
/// 
/// パラメータ使用種別を <see cref="CommandParameterModeKind.Auto"/> にした場合、以下のルールで使用するパラメータを決定する。
/// ・<see cref="CommandParameter"/>プロパティになにか設定があれば、必ずそれを利用する。(nullであっても)
/// ・プロパティ値が未設定の場合、アクションが呼び出された際の引数を利用。
/// </remarks>
public class InvokeCommandAction : TriggerAction<DependencyObject>
{
    // 公開型
    #region 種別定義
    /// <summary>コマンドパラメータ使用種別</summary>
    public enum CommandParameterModeKind
    {
        /// <summary>自動</summary>
        Auto,
        /// <summary>アクション引数を使用</summary>
        Argument,
        /// <summary><see cref="CommandParameter"/>プロパティを使用</summary>
        Property,
    }
    #endregion

    // 公開プロパティ
    #region 動作設定
    /// <summary>コマンドの有効状態をアタッチしているUI要素に反映する</summary>
    public bool AutoEnable
    {
        get { return (bool)GetValue(AutoEnableProperty); }
        set { SetValue(AutoEnableProperty, value); }
    }
    #endregion

    #region 呼び出し設定
    /// <summary>呼び出しコマンド</summary>
    public ICommand? Command
    {
        get { return (ICommand)GetValue(CommandProperty); }
        set { SetValue(CommandProperty, value); }
    }

    /// <summary>コマンドパラメータ</summary>
    public object? CommandParameter
    {
        get { return (object)GetValue(CommandParameterProperty); }
        set { SetValue(CommandParameterProperty, value); }
    }

    /// <summary>パラメータコンバータ</summary>
    public IValueConverter? ParameterConverter
    {
        get { return (IValueConverter)GetValue(ParameterConverterProperty); }
        set { SetValue(ParameterConverterProperty, value); }
    }

    /// <summary>コマンドパラメータ使用モード</summary>
    public CommandParameterModeKind CommandParameterMode
    {
        get { return (CommandParameterModeKind)GetValue(CommandParameterModeProperty); }
        set { SetValue(CommandParameterModeProperty, value); }
    }
    #endregion

    #region 依存プロパティ
    /// <summary><see cref="AutoEnable"/>の依存プロパティ</summary>
    public static readonly DependencyProperty AutoEnableProperty = DependencyProperty.Register(nameof(AutoEnable), typeof(bool), typeof(InvokeCommandAction), new PropertyMetadata(true, onAutoEnableChanged));

    /// <summary><see cref="Command"/>の依存プロパティ</summary>
    public static readonly DependencyProperty CommandProperty = DependencyProperty.Register(nameof(Command), typeof(ICommand), typeof(InvokeCommandAction), new PropertyMetadata(null, onCommandChanged));

    /// <summary><see cref="CommandParameter"/>の依存プロパティ</summary>
    public static readonly DependencyProperty CommandParameterProperty = DependencyProperty.Register(nameof(CommandParameter), typeof(object), typeof(InvokeCommandAction), new PropertyMetadata(null));

    /// <summary><see cref="ParameterConverter"/>の依存プロパティ</summary>
    public static readonly DependencyProperty ParameterConverterProperty = DependencyProperty.Register(nameof(ParameterConverter), typeof(IValueConverter), typeof(InvokeCommandAction), new PropertyMetadata(null));

    /// <summary><see cref="CommandParameterMode"/>の依存プロパティ</summary>
    public static readonly DependencyProperty CommandParameterModeProperty = DependencyProperty.Register(nameof(CommandParameterMode), typeof(CommandParameterModeKind), typeof(InvokeCommandAction), new PropertyMetadata(CommandParameterModeKind.Auto));
    #endregion

    // 保護メソッド
    #region 配置
    /// <inheritdoc />
    protected override void OnAttached()
    {
        // ベース処理を先に実施してアタッチを完了させる
        base.OnAttached();

        // 有効状態リンク設定に応じてコマンドとアタッチ要素の有効状態を紐づける
        updateEnableLink(this.AutoEnable, this.Command, this.AssociatedObject as UIElement);
    }

    /// <inheritdoc />
    protected override void OnDetaching()
    {
        // 有効状態の紐づけを解除
        updateEnableLink(false, null, null);

        // 上記を済ませてからデタッチ
        base.OnDetaching();
    }
    #endregion

    #region アクション
    /// <summary>アクション呼び出し時処理</summary>
    /// <param name="parameter">アクション呼び出しパラメータ</param>
    protected override void Invoke(object parameter)
    {
        // コマンドパラメータが指定されている場合のみ実行
        var command = this.Command;
        if (command == null)
        {
            return;
        }

        // パラメータを決定
        var cmdParam = getCommandParameter(parameter);

        // コマンドが実行可能であれば呼び出す
        if (command.CanExecute(cmdParam))
        {
            command.Execute(cmdParam);
        }
    }
    #endregion

    // 非公開フィールド
    #region 有効状態リンク関連
    /// <summary>有効状態参照元コマンド</summary>
    private ICommand? binCommand;

    /// <summary>有効状態反映先要素</summary>
    private UIElement? bindElement;

    /// <summary>有効状態リンク</summary>
    private bool? restoreEnabled;
    #endregion

    // 非公開フィールド
    #region コマンド呼び出し補助
    /// <summary>コマンドパラメータを決定する。</summary>
    /// <param name="parameter">アクションパラメータ</param>
    /// <returns>決定されたパラメータ</returns>
    private object? getCommandParameter(object? parameter)
    {
        // パラメータ使用モードによって選択する
        var cmdParam = default(object);
        var otherParam = default(object);
        switch (this.CommandParameterMode)
        {
        case CommandParameterModeKind.Argument: // 引数
            cmdParam = parameter;
            otherParam = this.CommandParameter;
            break;

        case CommandParameterModeKind.Property: // プロパティ
            cmdParam = this.CommandParameter;
            otherParam = parameter;
            break;

        case CommandParameterModeKind.Auto:     // 自動
        default:
            // プロパティに設定があればそれを、無ければ引数を。
            cmdParam = this.ReadLocalValue(CommandParameterProperty);
            if (cmdParam == DependencyProperty.UnsetValue)
            {
                cmdParam = parameter;
                otherParam = null;
            }
            else
            {
                cmdParam = this.CommandParameter;
                otherParam = parameter;
            }
            break;
        }

        // コンバータが設定されていればパラメータ値を変換する
        var converter = this.ParameterConverter;
        if (converter != null)
        {
            try
            {
                // パラメータの変換
                // 使用するパラメータと逆側のパラメータ値をコンバータへのパラメータとする
                var convParam = converter.Convert(cmdParam, typeof(object), otherParam, CultureInfo.CurrentCulture);
                if (convParam != DependencyProperty.UnsetValue)
                {
                    // 変換が行われた場合に結果を利用する
                    cmdParam = convParam;
                }
            }
            catch
            {
                // 変換エラーが生じたらパラメータ値無しということにする。
                // 元の値に維持したいのならばコンバータ内でそれを返して。
                cmdParam = null;
            }
        }

        return cmdParam;
    }
    #endregion

    #region 依存プロパティ状態変更ハンドラ
    /// <summary><see cref="AutoEnable"/> 依存プロパティ変更ハンドラ</summary>
    /// <param name="d"></param>
    /// <param name="e"></param>
    private static void onAutoEnableChanged(DependencyObject? d, DependencyPropertyChangedEventArgs e)
    {
        // 変更対象インスタンス取得
        if (d is InvokeCommandAction self)
        {
            // 変更後の値が有効状態リンクONであるかを判定
            var autoEnable = e.NewValue is bool newEnable && newEnable;

            // 有効状態リンクの状態を更新
            self.updateEnableLink(autoEnable, self.Command, self.AssociatedObject as UIElement);
        }
    }

    /// <summary><see cref="Command"/>  依存プロパティ変更ハンドラ</summary>
    /// <param name="d"></param>
    /// <param name="e"></param>
    private static void onCommandChanged(DependencyObject? d, DependencyPropertyChangedEventArgs e)
    {
        // 変更対象インスタンス取得
        if (d is InvokeCommandAction self)
        {
            // 有効状態リンク設定に応じてコマンドとアタッチ要素の有効状態を紐づける
            self.updateEnableLink(self.AutoEnable, e.NewValue as ICommand, self.AssociatedObject as UIElement);
        }
    }
    #endregion

    #region 有効状態リンク対象状態変更ハンドラ
    /// <summary>有効状態リンク対象のコマンド有効状態変化ハンドラ</summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void onCommandCanExecuteChanged(object? sender, EventArgs? e)
    {
        // 有効状態リンク中であれば状態反映する
        if (this.binCommand != null && this.bindElement != null)
        {
            // コマンド有効状態を取得して要素に反映
            // コマンド呼び出しタイミングではないので引数にあたるパラメータ値はnullとしている。
            var cmdParam = getCommandParameter(null);
            this.bindElement.IsEnabled = this.binCommand.CanExecute(cmdParam);
        }
    }
    #endregion

    #region 有効状態リンク
    /// <summary>有効状態リンクを更新する</summary>
    /// <param name="enableLink">状態リンクを有効とするか否か</param>
    /// <param name="command">有効状態参照元コマンド</param>
    /// <param name="element">有効状態反映先要素</param>
    private void updateEnableLink(bool enableLink, ICommand? command, UIElement? element)
    {
        // 有効状態リンク可能であるかを判定
        if (!enableLink || command == null || element == null)
        {
            // 状態リンク出来ない場合は状態解除する
            if (this.binCommand != null)
            {
                // コマンド有効状態の監視を停止
                this.binCommand.CanExecuteChanged -= onCommandCanExecuteChanged;
                this.binCommand = null;
            }
            if (this.bindElement != null)
            {
                // 要素の有効状態を元に戻す
                if (this.restoreEnabled.HasValue)
                {
                    this.bindElement.IsEnabled = this.restoreEnabled.Value;
                }
                this.bindElement = null;
                this.restoreEnabled = null;
            }
        }
        else
        {
            var changed = false;

            // 対象コマンドが変化する場合に更新処理
            if (!object.ReferenceEquals(this.binCommand, command))
            {
                // 既存が有効なコマンドであれば状態監視を解除
                if (this.binCommand != null)
                {
                    this.binCommand.CanExecuteChanged -= onCommandCanExecuteChanged;
                }
                // 新しい対象を保持＆状態変更監視
                this.binCommand = command;
                this.binCommand.CanExecuteChanged += onCommandCanExecuteChanged;

                changed = true;
            }

            // 対象要素が変化する場合に更新処理
            if (!object.ReferenceEquals(this.bindElement, element))
            {
                // 既存の対象要素は状態を戻す
                if (this.restoreEnabled.HasValue && this.bindElement != null)
                {
                    this.bindElement.IsEnabled = this.restoreEnabled.Value;
                }
                // 新しい対象を保持して現在の有効状態を反映する
                this.bindElement = element;
                this.restoreEnabled = this.bindElement.IsEnabled;

                changed = true;
            }

            // コマンドまたは要素に変更があった場合は要素に状態を反映する
            if (changed)
            {
                var cmdParam = getCommandParameter(null);
                this.bindElement.IsEnabled = this.binCommand.CanExecute(cmdParam);
            }
        }
    }
    #endregion
}
