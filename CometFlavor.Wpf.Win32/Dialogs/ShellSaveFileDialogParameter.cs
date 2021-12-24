// このファイル内のコメントを除いたソースコードはパブリックドメインとします。
// The source code except for comments in this file is in the public domain.

using System;
using System.Collections.Generic;

namespace CometFlavor.Wpf.Win32.Dialogs
{
    /// <summary>
    /// <see cref="ShellSaveFileDialog"/> のパラメータ
    /// </summary>
    public class ShellSaveFileDialogParameter
    {
        // 構築
        #region コンストラクタ
        /// <summary>
        /// デフォルトコンストラクタ
        /// </summary>
        public ShellSaveFileDialogParameter()
        {
            this.Filters = new List<ShellFileDialogFilter>();
            this.AdditionalPlaces = new List<ShellFileDialogPlace>();
            this.ClientGuid = Guid.Empty;

            // オプション設定のデフォルト値
            // ある時点で取得してみたCOMダイアログのデフォルト値を元にしている。
            this.OverwritePrompt = true;
            this.StrictFileTypes = false;
            this.NoChangeDirectory = true;
            this.ForceFileSystem = false;
            this.AllNonStorageItems = false;
            this.NoValidate = false;
            this.PathMustExist = true;
            this.FileMustExist = false;
            this.CreatePrompt = false;
            this.ShareAware = false;
            this.NoReadOnlyReturn = true;
            this.NoTestFileCreate = false;
            this.HidePinnedPlaces = false;
            this.NoDereferenceLinks = false;
            this.OkButtonNeedsInteraction = false;
            this.DontAddToRecent = false;
            this.ForceShowHidden = false;

            // 定義されたオプションのうち、以下に対応する設定は除外。
            //   PICKFOLDERS            : 保存ダイアログでは使用不可のため
            //   ALLOWMULTISELECT       : 保存ダイアログでは使用不可のため
            //   HIDEMRUPLACES          : 新しいOSで非サポートのため
            //   DEFAULTNOMINIMODE      : 新しいOSで非サポートのため
            //   FORCEPREVIEWPANEON     : 保存ダイアログでは使用不可のため
            //   SUPPORTSTREAMABLEITEMS : ストリームアイテムとしてアクセスする手段を提供しないため
        }
        #endregion

        // 公開プロパティ
        #region ダイアログカスタマイズ
        /// <summary>ダイアログのタイトル</summary>
        /// <remarks>未設定(null)の場合はデフォルトのタイトルとなる。</remarks>
        public string? Title { get; set; }

        /// <summary>確定ボタンのラベル</summary>
        /// <remarks>未設定(null)の場合はデフォルトのラベルとなる。</remarks>
        public string? AcceptButtonLabel { get; set; }

        /// <summary>ファイル名入力欄のラベル</summary>
        /// <remarks>未設定(null)の場合はデフォルトのラベルとなる。</remarks>
        public string? FileNameLabel { get; set; }

        /// <summary>追加の選択場所</summary>
        /// <remarks>場所はフォルダでなければならない</remarks>
        public IList<ShellFileDialogPlace> AdditionalPlaces { get; }
        #endregion

        #region 状態設定
        /// <summary>初期フォルダ</summary>
        /// <remarks>nullは未指定を表す。</remarks>
        public string? Directory { get; set; }

        /// <summary>初期入力ファイル名</summary>
        /// <remarks>nullは未指定を表す。</remarks>
        public string? InitialFileName { get; set; }

        /// <summary>デフォルト拡張子</summary>
        /// <remarks>nullは未指定を表す。</remarks>
        public string? DefaultExtension { get; set; }

        /// <summary>選択できるファイルフィルタ</summary>
        public IList<ShellFileDialogFilter> Filters { get; }

        /// <summary>ファイルフィルタの初期選択インデックス(1ベース値)</summary>
        /// <remarks>0は未指定を表す。</remarks>
        public uint InitialFilterIndex { get; set; }

        /// <summary>最近使用したフォルダーが無い場合のデフォルトフォルダ</summary>
        /// <remarks>nullは未指定を表す。</remarks>
        public string? DefaultDirectory { get; set; }

        /// <summary>ダイアログ状態(最終フォルダや位置/サイズ)の永続化のためのGUID</summary>
        /// <remarks>Guid.Emptyは未指定を表す。</remarks>
        public Guid ClientGuid { get; set; }
        #endregion

        #region 動作設定
        /// <summary>上書きの確認を表示する</summary>
        /// <remarks>デフォルト値は false となる。</remarks>
        public bool OverwritePrompt { get; set; }

        /// <summary>設定されたファイルタイプのファイルのみを選択可能とする</summary>
        /// <remarks>デフォルト値は false となる。</remarks>
        public bool StrictFileTypes { get; set; }

        /// <summary>カレントディレクトリを変更しない</summary>
        /// <remarks>デフォルト値は true となる。</remarks>
        public bool NoChangeDirectory { get; set; }

        /// <summary>ファイルシステムアイテムのみを選択可能とする</summary>
        /// <remarks>デフォルト値は false となる。</remarks>
        public bool ForceFileSystem { get; set; }

        /// <summary>シェルネームスペース内のすべてのアイテムを選択可能とする</summary>
        /// <remarks>デフォルト値は false となる。</remarks>
        public bool AllNonStorageItems { get; set; }

        /// <summary>ファイルを開けない状態(共有不可やアクセス拒否等)を検証しない</summary>
        /// <remarks>デフォルト値は false となる。</remarks>
        public bool NoValidate { get; set; }

        /// <summary>存在するフォルダのアイテムのみを選択可能とする</summary>
        /// <remarks>デフォルト値は true となる。</remarks>
        public bool PathMustExist { get; set; }

        /// <summary>存在するアイテムのみを選択可能とする</summary>
        /// <remarks>デフォルト値は true となる。</remarks>
        public bool FileMustExist { get; set; }

        /// <summary>ファイルの作成確認を表示する</summary>
        /// <remarks>デフォルト値は false となる。</remarks>
        /// <remarks>確認は行われるが実際のファイルは作成されない。</remarks>
        /// <remarks>この設定を true にした場合、<see cref="FileMustExist"/>の効果は無効となる。</remarks>
        public bool CreatePrompt { get; set; }

        /// <summary>共有違反(アプリケーションが開いている場合)のガイダンスを表示する</summary>
        /// <remarks>デフォルト値は false となる。</remarks>
        public bool ShareAware { get; set; }

        /// <summary>読み取り専用アイテムを選択不可とする</summary>
        /// <remarks>デフォルト値は false となる。</remarks>
        public bool NoReadOnlyReturn { get; set; }

        /// <summary>ファイルが作成可能であるかをテストしない</summary>
        /// <remarks>デフォルト値は false となる。</remarks>
        public bool NoTestFileCreate { get; set; }

        /// <summary>ナビゲーションペインのデフォルトアイテムを非表示とする</summary>
        /// <remarks>デフォルト値は false となる。</remarks>
        public bool HidePinnedPlaces { get; set; }

        /// <summary>ショートカットの参照先解決をしない。(ショートカットファイル自体を選択可能とする)</summary>
        /// <remarks>デフォルト値は false となる。</remarks>
        public bool NoDereferenceLinks { get; set; }

        /// <summary>確定ボタン操作のためにユーザ操作を必要とする</summary>
        /// <remarks>デフォルト値は false となる。</remarks>
        public bool OkButtonNeedsInteraction { get; set; }

        /// <summary>選択アイテムを最近使用したファイルのリストに追加しない</summary>
        /// <remarks>デフォルト値は false となる。</remarks>
        public bool DontAddToRecent { get; set; }

        /// <summary>非表示属性のアイテムを表示する</summary>
        /// <remarks>デフォルト値は false となる。</remarks>
        public bool ForceShowHidden { get; set; }
        #endregion
    }
}
