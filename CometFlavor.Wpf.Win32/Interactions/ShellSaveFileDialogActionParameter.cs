using System;
using CometFlavor.Wpf.Win32.Dialogs;

namespace CometFlavor.Wpf.Win32.Interactions;

/// <summary>
/// <see cref="ShellSaveFileDialogAction"/> のパラメータ
/// </summary>
public class ShellSaveFileDialogActionParameter
{
    #region 動作設定
    /// <summary>ダイアログ表示パラメータ</summary>
    public ShellSaveFileDialogParameter? Parameter { get; set; }
    #endregion

    #region 結果
    /// <summary>ダイアログの結果</summary>
    public ShellSaveFileDialogResult? Result { get; set; }

    /// <summary>表示処理で発生した例外オブジェクト</summary>
    public Exception? Exception { get; set; }
    #endregion
}
