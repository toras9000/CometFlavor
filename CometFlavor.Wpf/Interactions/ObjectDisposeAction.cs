using System;
using System.Windows;
using Microsoft.Xaml.Behaviors;

namespace CometFlavor.Wpf.Interactions
{
    /// <summary>
    /// オブジェクト破棄アクション
    /// </summary>
    public class ObjectDisposeAction : TriggerAction<DependencyObject>
    {
        // 公開プロパティ
        #region 動作設定
        /// <summary>破棄対象オブジェクト</summary>
        public IDisposable? Object
        {
            get { return (IDisposable)GetValue(ObjectProperty); }
            set { SetValue(ObjectProperty, value); }
        }

        /// <summary>アクションパラメータを破棄するか否か</summary>
        /// <remarks>アクションに渡されたパラメータが IDisposable インターフェースを実装している場合に、パラメータに対してDisposeを呼び出す。</remarks>
        public bool DisposeParameter
        {
            get { return (bool)GetValue(DisposeParameterProperty); }
            set { SetValue(DisposeParameterProperty, value); }
        }
        #endregion

        #region 依存プロパティ
        /// <summary><see cref="Object"/> の依存プロパティ</summary>
        public static readonly DependencyProperty ObjectProperty = DependencyProperty.Register(nameof(Object), typeof(IDisposable), typeof(ObjectDisposeAction), new PropertyMetadata(null));

        /// <summary><see cref="DisposeParameter"/> の依存プロパティ</summary>
        public static readonly DependencyProperty DisposeParameterProperty = DependencyProperty.Register(nameof(DisposeParameter), typeof(bool), typeof(ObjectDisposeAction), new PropertyMetadata(false));
        #endregion

        // 保護メソッド
        #region インタラクション
        /// <summary>
        /// アクション呼び出し時処理
        /// </summary>
        /// <param name="parameter">アクション呼び出しパラメータ</param>
        protected override void Invoke(object parameter)
        {
            // プロパティ指定のオブジェクトは常に破棄
            try { this.Object?.Dispose(); } catch { }

            // パラメータを破棄する設定であれば破棄を試みる
            if (this.DisposeParameter)
            {
                // パラメータが IDisposable であれば破棄する
                if (parameter is IDisposable disposable)
                {
                    try { disposable.Dispose(); } catch { }
                }
            }
        }
        #endregion
    }
}
