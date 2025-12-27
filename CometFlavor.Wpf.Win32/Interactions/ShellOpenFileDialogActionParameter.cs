using System;
using CometFlavor.Win32.Dialogs;

namespace CometFlavor.Wpf.Win32.Interactions;

/// <summary>
/// <see cref="ShellOpenFileDialogAction"/> のパラメータ
/// </summary>
public class ShellOpenFileDialogActionParameter
{
    #region 動作設定
    /// <summary>ダイアログ表示パラメータ</summary>
    public ShellOpenFileDialogParameter? Parameter { get; set; }
    #endregion

    #region 結果
    /// <summary>ダイアログの結果</summary>
    public ShellOpenFileDialogResult? Result { get; set; }

    /// <summary>表示処理で発生した例外オブジェクト</summary>
    public Exception? Exception { get; set; }
    #endregion
}
