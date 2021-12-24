// このファイル内のコメントを除いたソースコードはパブリックドメインとします。
// The source code except for comments in this file is in the public domain.

namespace CometFlavor.Wpf.Win32.Dialogs;

/// <summary>
/// <see cref="ShellSaveFileDialog"/> の結果
/// </summary>
public class ShellSaveFileDialogResult
{
    #region 結果
    /// <summary>ダイアログの選択結果</summary>
    /// <remarks>ダイアログでアイテムが選択されていない場合はnullとなる。</remarks>
    public string? Item { get; set; }

    /// <summary>選択時のフィルタインデックス</summary>
    public uint FilterIndex { get; set; }
    #endregion
}
