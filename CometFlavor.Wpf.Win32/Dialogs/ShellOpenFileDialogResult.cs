// このファイル内のコメントを除いたソースコードはパブリックドメインとします。
// The source code except for comments in this file is in the public domain.

using System.Collections.Generic;

namespace CometFlavor.Wpf.Win32.Dialogs;

/// <summary>
/// <see cref="ShellOpenFileDialog"/> の結果
/// </summary>
public class ShellOpenFileDialogResult
{
    // 構築
    #region コンストラクタ
    /// <summary>
    /// デフォルトコンストラクタ
    /// </summary>
    public ShellOpenFileDialogResult()
    {
        this.Items = new List<string>();
    }
    #endregion

    // 公開プロパティ
    #region 結果
    /// <summary>ダイアログの選択結果</summary>
    /// <remarks>このプロパティは<see cref="Items"/>の最初の要素を返却する。</remarks>
    /// <remarks>ダイアログでアイテムが選択されていない場合はnullとなる。</remarks>
    public string? Item => (this.Items.Count <= 0) ? null : this.Items[0];

    /// <summary>ダイアログのすべての選択結果</summary>
    /// <remarks>単一選択でも複数選択でも利用可能</remarks>
    /// <remarks>ダイアログでアイテムが選択されていない場合はリストは空となる。</remarks>
    public List<string> Items { get; }

    /// <summary>選択時のフィルタインデックス</summary>
    public uint FilterIndex { get; set; }
    #endregion
}
