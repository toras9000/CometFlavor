using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Xaml.Behaviors;

namespace CometFlavor.Wpf.Interactions;

/// <summary>
/// ビューの検証エラーをプロパティに表現するビヘイビア
/// </summary>
public class ViewValidationErrorBehavior : Behavior<DependencyObject>
{
    // 構築
    #region コンストラクタ
    /// <summary>
    /// デフォルトコンストラクタ
    /// </summary>
    public ViewValidationErrorBehavior()
    {
        this.ScanInitialState = true;
    }
    #endregion

    // 公開プロパティ
    #region 動作設定
    /// <summary>要素へのアタッチ時にエラー状態を確認するか否か</summary>
    public bool ScanInitialState { get; set; }
    #endregion

    #region 状態情報
    /// <summary>ビューの検証エラー有無</summary>
    /// <remarks>検証エラーが1つ以上あれば ture となる。</remarks>
    public bool HasViewError
    {
        get { return (bool)GetValue(HasViewErrorProperty); }
        set { SetValue(HasViewErrorProperty, value); }
    }
    #endregion

    #region 依存プロパティ
    /// <summary><see cref="HasViewError"/> の依存プロパティ</summary>
    public static readonly DependencyProperty HasViewErrorProperty = DependencyProperty.Register(nameof(HasViewError), typeof(bool), typeof(ViewValidationErrorBehavior), new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, null, onHasViewErrorCoerceValue));
    #endregion

    // 保護メソッド
    #region 配置
    /// <summary>
    /// 要素にアタッチした際の処理
    /// </summary>
    protected override void OnAttached()
    {
        // 基本クラス処理
        base.OnAttached();

        // 初期エラー状態を調べる設定の場合は要素を辿ってエラー状態を数える
        if (this.ScanInitialState)
        {
            // 現時点のエラーの数を取得
            // ちなみにValidation.GetErrors()で取得したエラーコレクションは一時的なものであるようで、
            // 返されたインスタンスの中身が更新されることはなかったし、別のタイミングで再度取得すると返されるインスタンスは別であった。
            this.errorCount = logicalDescendants(this.AssociatedObject).Select(e => Validation.GetErrors(e)?.Count ?? 0).Sum();
        }

        // エラーイベントのハンドラを登録
        Validation.AddErrorHandler(this.AssociatedObject, onValidationError);

        // エラーの数でプロパティ値を更新
        this.HasViewError = 0 < this.errorCount;
    }

    /// <summary>
    /// 要素からデタッチした際の処理
    /// </summary>
    protected override void OnDetaching()
    {
        // エラーイベントのハンドラを解除
        Validation.RemoveErrorHandler(this.AssociatedObject, onValidationError);

        // エラー情報をクリア
        this.errorCount = 0;
        this.HasViewError = false;

        // 基本クラス処理
        base.OnDetaching();
    }
    #endregion

    #region エラー情報
    /// <summary>現在のエラー数</summary>
    private int errorCount;
    #endregion

    // 非公開メソッド
    #region 依存プロパティのイベントハンドラ
    /// <summary>
    /// <see cref="HasViewError"/> 依存プロパティの値の矯正ハンドラ
    /// </summary>
    private static object? onHasViewErrorCoerceValue(DependencyObject? d, object? baseValue)
    {
        // 一応自このクラス型であることを確認
        if (d is ViewValidationErrorBehavior self)
        {
            // HasViewError はValidationエラーの状態をただ示したいので、どんな値を指定されようとも無視して把握しているエラー状態から値を作る。
            return (0 < self.errorCount);
        }

        // もしも知らない型だったらそのままの値を返却しておく
        return baseValue;
    }
    #endregion

    #region 添付プロパティイベントハンドラ
    /// <summary>
    /// 検証エラーイベントハンドラ
    /// </summary>
    private void onValidationError(object? sender, ValidationErrorEventArgs? e)
    {
        // イベントの種類によって状態更新
        switch (e?.Action)
        {
            case ValidationErrorEventAction.Added:
                // エラーが増えた場合
                this.errorCount++;
                this.HasViewError = 0 < this.errorCount;
                break;

            case ValidationErrorEventAction.Removed:
                // エラーが減った場合
                if (0 < this.errorCount)
                {
                    this.errorCount--;
                    this.HasViewError = 0 < this.errorCount;
                }
                break;

            default:
                break;
        }
    }
    #endregion

    #region 補助処理
    /// <summary>
    /// 論理ツリー上の全ての子孫要素を列挙する
    /// </summary>
    /// <param name="element">起点となる要素</param>
    /// <returns>論理ツリー上の要素を列挙するシーケンス</returns>
    private IEnumerable<DependencyObject> logicalDescendants(DependencyObject element)
    {
        // 起点要素自体を列挙
        yield return element;

        // 子要素を列挙
        foreach (var child in LogicalTreeHelper.GetChildren(element).OfType<DependencyObject>())
        {
            // 子要素とその子孫を列挙
            foreach (var descendant in logicalDescendants(child))
            {
                yield return descendant;
            }
        }
    }
    #endregion
}
