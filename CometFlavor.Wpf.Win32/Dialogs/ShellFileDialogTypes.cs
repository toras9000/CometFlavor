// このファイル内のコメントを除いたソースコードはパブリックドメインとします。
// The source code except for comments in this file is in the public domain.

using System;

namespace CometFlavor.Wpf.Win32.Dialogs
{
    /// <summary>
    /// シェルファイルダイアログのフィルタ情報
    /// </summary>
    public class ShellFileDialogFilter
    {
        // 構築
        #region コンストラクタ
        /// <summary>
        /// インスタンス値を指定するコンストラクタ
        /// </summary>
        /// <param name="name">表示用名称</param>
        /// <param name="spec">パターン文字列</param>
        public ShellFileDialogFilter(string name, string spec)
        {
            this.Name = name ?? throw new ArgumentNullException(nameof(name));
            this.Spec = spec ?? throw new ArgumentNullException(nameof(spec));
        }
        #endregion

        // 公開プロパティ
        #region フィルタ情報
        /// <summary>表示用名称</summary>
        public string Name { get; }

        /// <summary>パターン文字列</summary>
        public string Spec { get; }
        #endregion
    }

    /// <summary>
    /// シェルファイルダイアログの追加場所の位置識別子
    /// </summary>
    public enum ShellFileDialogPlaceOrder
    {
        /// <summary>末尾</summary>
        Botton,
        /// <summary>先頭</summary>
        Top,
    }

    /// <summary>
    /// シェルファイルダイアログの追加場所情報
    /// </summary>
    public class ShellFileDialogPlace
    {
        // 構築
        #region コンストラクタ
        /// <summary>
        /// インスタンス値を指定するコンストラクタ
        /// </summary>
        /// <param name="path">フォルダパス</param>
        /// <param name="order">追加位置</param>
        public ShellFileDialogPlace(string path, ShellFileDialogPlaceOrder order)
        {
            this.Path = path ?? throw new ArgumentNullException(nameof(path));
            this.Order = order;
        }
        #endregion

        // 公開プロパティ
        #region 場所情報
        /// <summary>フォルダパス</summary>
        public string Path { get; }

        /// <summary>追加位置</summary>
        public ShellFileDialogPlaceOrder Order { get; }
        #endregion
    }
}
