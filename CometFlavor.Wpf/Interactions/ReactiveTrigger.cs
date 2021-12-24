using System;
using System.Windows;
using Microsoft.Xaml.Behaviors;

namespace CometFlavor.Wpf.Interactions
{
    /// <summary>
    /// IObservable{object}シーケンスを契機とするトリガ
    /// </summary>
    public class ReactiveTrigger : ReactiveTrigger<object?>
    {
    }

    /// <summary>
    /// IObservable{T}シーケンスを契機とするトリガ
    /// </summary>
    /// <typeparam name="T">シーケンスの要素型</typeparam>
    public class ReactiveTrigger<T> : TriggerBase<DependencyObject>
    {
        // 公開プロパティ
        #region 動作設定
        /// <summary>トリガの発生契機となる要素を流すシーケンス</summary>
        public IObservable<T> Source
        {
            get { return (IObservable<T>)GetValue(SourceProperty); }
            set { SetValue(SourceProperty, value); }
        }
        #endregion

        #region 依存プロパティ
        /// <summary><see cref="Source"/> の依存プロパティ</summary>
        public static readonly DependencyProperty SourceProperty = DependencyProperty.Register(nameof(Source), typeof(IObservable<T>), typeof(ReactiveTrigger<T>), new PropertyMetadata(null, onSourceChanged));
        #endregion

        // 保護メソッド
        #region 配置
        /// <summary>
        /// トリガを要素にアタッチした際の処理
        /// </summary>
        protected override void OnAttached()
        {
            // 基本クラス処理
            base.OnAttached();

            // ソースシーケンスの購読を開始する
            subscribeSource(this.Source);
        }

        /// <summary>
        /// トリガを要素からデタッチした際の処理
        /// </summary>
        protected override void OnDetaching()
        {
            // シーケンスの行動を解除する
            subscribeSource(this.Source);

            // 基本クラス処理
            base.OnDetaching();
        }
        #endregion

        // 非公開型
        #region シーケンス
        /// <summary>
        /// シーケンスを購読してアクションを呼び出すオブザーバ
        /// </summary>
        private class TriggerObserver : IObserver<T>
        {
            // 構築
            #region コンストラクタ
            /// <summary>
            /// トリガインスタンスとの連携情報を指定するコンストラクタ
            /// </summary>
            /// <param name="outer">所属するトリガ</param>
            /// <param name="completeHandler">シーケンス完了時コールバックデリゲート</param>
            public TriggerObserver(ReactiveTrigger<T> outer, Action completeHandler)
            {
                this.outer = outer;
                this.completeHandler = completeHandler;
            }
            #endregion

            // 公開メソッド
            #region シーケンス購読
            /// <summary>シーケンスの要素処理</summary>
            /// <param name="value">シーケンスで提供される要素</param>
            public void OnNext(T value) => this.outer?.InvokeActions(value);

            /// <summary>シーケンス完了時処理</summary>
            public void OnCompleted() => notifyComplete();

            /// <summary>シーケンスエラー終了時処理</summary>
            public void OnError(Exception error) => notifyComplete();
            #endregion

            // 非公開フィールド
            #region 連携情報
            /// <summary>所属するトリガ</summary>
            private ReactiveTrigger<T>? outer;

            /// <summary>シーケンス完了時コールバックデリゲート</summary>
            private Action? completeHandler;
            #endregion

            // 非公開メソッド
            #region 連携情報
            /// <summary>
            /// シーケンス完了時処理
            /// </summary>
            private void notifyComplete()
            {
                this.completeHandler?.Invoke();
                this.completeHandler = null;
                this.outer = null;
            }
            #endregion
        }
        #endregion

        // 非公開フィールド
        #region 状態管理
        /// <summary>トリガソースの購読解除用オブジェクト</summary>
        private IDisposable? sourceUnsubscriber;
        #endregion

        // 非公開メソッド
        #region 依存プロパティ変更ハンドラ
        /// <summary>
        /// <see cref="Source"/> 依存プロパティ値の変更ハンドラ
        /// </summary>
        /// <param name="d"></param>
        /// <param name="e"></param>
        private static void onSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is ReactiveTrigger<T> self)
            {
                if (e.NewValue is IObservable<T> source)
                {
                    // 新しい値で購読を更新
                    self.subscribeSource(source);
                }
            }
        }
        #endregion

        #region 動作処理
        /// <summary>
        /// シーケンスの購読を開始する。
        /// </summary>
        /// <param name="source">シーケンス</param>
        private void subscribeSource(IObservable<T> source)
        {
            // 既存の購読を解除
            this.sourceUnsubscriber?.Dispose();
            this.sourceUnsubscriber = null;

            // 新しいシーケンスが有効であるか
            if (source != null)
            {
                // シーケンスの購読を開始
                this.sourceUnsubscriber = source.Subscribe(
                    new TriggerObserver(this, () =>
                    {
                        // シーケンス終了時ハンドラ。
                        // 必ずしも必要ではないが
                        this.sourceUnsubscriber = null;
                    })
                );
            }
        }
        #endregion
    }
}
