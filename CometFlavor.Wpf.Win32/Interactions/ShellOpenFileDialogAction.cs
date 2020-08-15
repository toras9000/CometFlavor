using System;
using System.Windows;
using CometFlavor.Wpf.Win32.Dialogs;
using Microsoft.Xaml.Behaviors;

namespace CometFlavor.Wpf.Win32.Interactions
{
    /// <summary>
    /// <see cref="ShellOpenFileDialog"/> 呼び出しアクション
    /// </summary>
    public class ShellOpenFileDialogAction : TriggerAction<DependencyObject>
    {
        // 公開プロパティ
        #region 動作設定
        /// <summary>ダイアログにオーナーを設定するか否か</summary>
        /// <remarks>trueの場合、アクションがアタッチされた要素が所属するウィンドウをダイアログのオーナーとする</remarks>
        public bool Owned
        {
            get { return (bool)GetValue(OwnedProperty); }
            set { SetValue(OwnedProperty, value); }
        }
        #endregion

        #region 依存プロパティ
        /// <summary><see cref="Owned"/>の依存プロパティ</summary>
        public static readonly DependencyProperty OwnedProperty = DependencyProperty.Register(nameof(Owned), typeof(bool), typeof(ShellOpenFileDialogAction), new PropertyMetadata(true));
        #endregion

        // 保護メソッド
        #region アクション
        /// <summary>
        /// アクション呼び出し時処理
        /// </summary>
        /// <param name="parameter">アクション呼び出しパラメータ。ShellOpenFileDialogActionParameter のインスタンスである必要がある。</param>
        protected override void Invoke(object parameter)
        {
            // パラメータはダイアログのパラメータ型である必要がある(異なる場合はキャスト例外とする)
            var actionParam = (ShellOpenFileDialogActionParameter)parameter ?? throw new NullReferenceException();

            // 結果状態をクリア
            actionParam.Result = null;
            actionParam.Exception = null;

            // 表示用パラメータは必須。
            if (actionParam.Parameter == null)
            {
                actionParam.Exception = new ArgumentNullException(nameof(actionParam.Parameter));
                return;
            }

            try
            {
                // オーナーウィンドウを決定する
                var owner = default(Window);
                if (this.Owned)
                {
                    owner = Window.GetWindow(this.AssociatedObject);
                }

                // ダイアログを表示する
                var shellDlg = new ShellOpenFileDialog();
                actionParam.Result = shellDlg.ShowDialog(actionParam.Parameter);
            }
            catch (Exception ex)
            {
                // エラー発生時は例外を保持
                actionParam.Exception = ex;
            }
        }
        #endregion
    }
}
