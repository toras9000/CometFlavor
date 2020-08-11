// このファイル内のコメントを除いたソースコードはパブリックドメインとします。
// The source code except for comments in this file is in the public domain.

using System;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;
using static CometFlavor.Wpf.Win32.Dialogs.NativeDefinitions;

namespace CometFlavor.Wpf.Win32.Dialogs
{
    /// <summary>
    /// COMのSaveFileDialogオブジェクトによるファイルを開くダイアログ
    /// </summary>
    public class ShellSaveFileDialog
    {
        // 公開メソッド
        #region 表示
        /// <summary>
        /// ダイアログを表示する。
        /// </summary>
        /// <param name="owner">オーナーウィンドウハンドル。nullを指定するとオーナーウィンドウ無しとなる。</param>
        /// <param name="parameter">ダイアログ表示パラメータ</param>
        /// <returns>ダイアログの結果</returns>
        public ShellSaveFileDialogResult ShowDialog(Window owner, ShellSaveFileDialogParameter parameter)
        {
            // 現在のプラットフォームで利用可能かを判定
            var os = Environment.OSVersion;
            if (os.Platform != PlatformID.Win32NT
             || os.Version.Major < 6)   // Windows Vistaより前では利用不可
            {
                throw new InvalidOperationException("Not available on current platforms.");
            }

            // ダイアログを表示する
            return showInternal(owner, parameter);
        }

        /// <summary>
        /// オーナーウィンドウ無しでダイアログを表示する。
        /// </summary>
        /// <param name="parameter">ダイアログ表示パラメータ</param>
        /// <returns>ダイアログの結果</returns>
        public ShellSaveFileDialogResult ShowDialog(ShellSaveFileDialogParameter parameter)
        {
            // ダイアログを表示する
            return showInternal(null, parameter);
        }
        #endregion

        // 非公開メソッド
        #region 表示
        /// <summary>
        /// ダイアログを表示する。
        /// </summary>
        /// <param name="owner">オーナーウィンドウハンドル。nullを指定するとオーナーウィンドウ無しとなる。</param>
        /// <param name="parameter">ダイアログ表示パラメータ</param>
        /// <returns>ダイアログの結果</returns>
        private ShellSaveFileDialogResult showInternal(Window owner, ShellSaveFileDialogParameter parameter)
        {
            var comObjects = new ComUtility.ObjectList();
            var dlgResult = new ShellSaveFileDialogResult();
            try
            {
                // FileSaveDialog COMオブジェクト生成
                var dlgClsId = new Guid(CLSID.FileSaveDialog);
                var dlgType = Type.GetTypeFromCLSID(dlgClsId);
                var dialogObject = Activator.CreateInstance(dlgType).WithAddTo(comObjects);

                // IFileSaveDialogインターフェース取得(インポートインターフェースへのキャストにて)
                var shellDialog = (IFileSaveDialog)dialogObject;

                // タイトル指定があれば設定
                if (parameter.Title != null)
                {
                    shellDialog.SetTitle(parameter.Title);
                }

                // 確定ボタンのラベル指定があれば設定
                if (parameter.AcceptButtonLabel != null)
                {
                    shellDialog.SetOkButtonLabel(parameter.AcceptButtonLabel);
                }

                // ファイル名入力欄のラベル指定があれば設定
                if (parameter.FileNameLabel != null)
                {
                    shellDialog.SetFileNameLabel(parameter.FileNameLabel);
                }

                // フィルタがあれば設定
                if (0 < parameter.Filters.Count)
                {
                    var fileTypes = parameter.Filters.Select(f => new COMDLG_FILTERSPEC { pszName = f.Name, pszSpec = f.Spec }).ToArray();
                    shellDialog.SetFileTypes((UInt32)fileTypes.Length, fileTypes);

                    // フィルタの初期選択インデクス指定があれば設定
                    if (0 < parameter.InitialFilterIndex)
                    {
                        shellDialog.SetFileTypeIndex(parameter.InitialFilterIndex);
                    }
                }

                // クライアントGUID指定があれば設定
                if (parameter.ClientGuid != Guid.Empty)
                {
                    shellDialog.SetClientGuid(parameter.ClientGuid);
                }

                // デフォルト拡張子指定があれば設定
                if (parameter.DefaultExtension != null)
                {
                    shellDialog.SetDefaultExtension(parameter.DefaultExtension);
                }

                // デフォルトディレクトリ指定があれば設定
                if (parameter.DefaultDirectory != null)
                {
                    var defDir = ComUtility.CreateShellItemFromPath(parameter.DefaultDirectory).WithAddTo(comObjects);
                    shellDialog.SetDefaultFolder(defDir);
                }

                // 初期ディレクトリ指定があれば設定
                if (parameter.Directory != null)
                {
                    var iniDir = ComUtility.CreateShellItemFromPath(parameter.Directory).WithAddTo(comObjects);
                    shellDialog.SetFolder(iniDir);
                }

                // 初期ファイル名指定があれば設定
                if (parameter.InitialFileName != null)
                {
                    shellDialog.SetFileName(parameter.InitialFileName);
                }

                // 追加の場所指定があれば追加
                foreach (var place in parameter.AdditionalPlaces)
                {
                    var placeItem = ComUtility.CreateShellItemFromPath(place.Path).WithAddTo(comObjects);
                    var placeOrder = place.Order == ShellFileDialogPlaceOrder.Top ? FDAP.TOP : FDAP.BOTTOM;
                    shellDialog.AddPlace(placeItem, placeOrder);
                }

                // ダイアログ表示時のオプション設定を作成
                var dlgOpt = default(FILEOPENDIALOGOPTIONS);
                if (parameter.OverwritePrompt) dlgOpt |= FILEOPENDIALOGOPTIONS.OVERWRITEPROMPT;
                if (parameter.StrictFileTypes) dlgOpt |= FILEOPENDIALOGOPTIONS.STRICTFILETYPES;
                if (parameter.NoChangeDirectory) dlgOpt |= FILEOPENDIALOGOPTIONS.NOCHANGEDIR;
                if (parameter.ForceFileSystem) dlgOpt |= FILEOPENDIALOGOPTIONS.FORCEFILESYSTEM;
                if (parameter.AllNonStorageItems) dlgOpt |= FILEOPENDIALOGOPTIONS.ALLNONSTORAGEITEMS;
                if (parameter.NoValidate) dlgOpt |= FILEOPENDIALOGOPTIONS.NOVALIDATE;
                if (parameter.PathMustExist) dlgOpt |= FILEOPENDIALOGOPTIONS.PATHMUSTEXIST;
                if (parameter.FileMustExist) dlgOpt |= FILEOPENDIALOGOPTIONS.FILEMUSTEXIST;
                if (parameter.CreatePrompt) dlgOpt |= FILEOPENDIALOGOPTIONS.CREATEPROMPT;
                if (parameter.ShareAware) dlgOpt |= FILEOPENDIALOGOPTIONS.SHAREAWARE;
                if (parameter.NoReadOnlyReturn) dlgOpt |= FILEOPENDIALOGOPTIONS.NOREADONLYRETURN;
                if (parameter.NoTestFileCreate) dlgOpt |= FILEOPENDIALOGOPTIONS.NOTESTFILECREATE;
                if (parameter.HidePinnedPlaces) dlgOpt |= FILEOPENDIALOGOPTIONS.HIDEPINNEDPLACES;
                if (parameter.NoDereferenceLinks) dlgOpt |= FILEOPENDIALOGOPTIONS.NODEREFERENCELINKS;
                if (parameter.OkButtonNeedsInteraction) dlgOpt |= FILEOPENDIALOGOPTIONS.OKBUTTONNEEDSINTERACTION;
                if (parameter.DontAddToRecent) dlgOpt |= FILEOPENDIALOGOPTIONS.DONTADDTORECENT;
                if (parameter.ForceShowHidden) dlgOpt |= FILEOPENDIALOGOPTIONS.FORCESHOWHIDDEN;

                // オプション設定を適用
                shellDialog.SetOptions(dlgOpt);

                // ダイアログを表示
                var ownerHandle = (owner == null) ? IntPtr.Zero : new WindowInteropHelper(owner).Handle;
                var result = shellDialog.Show(ownerHandle);
                if (SUCCEEDED(result))
                {
                    // ダイアログの選択結果を取得
                    shellDialog.GetResult(out var item);
                    comObjects.Add(item);

                    // 各アイテムのファイルパスを列挙して結果リストに格納
                    dlgResult.Item = ComUtility.GetFileSystemPath(item);

                    // 選択されたフィルタインデクスを取得
                    shellDialog.GetFileTypeIndex(out var resultIndex);
                    dlgResult.FilterIndex = resultIndex;
                }
                else if (result == HRESULT_FROM_WIN32(ERROR_CANCELLED))
                {
                    // ダイアログキャンセルされた場合も結果は正常
                }
                else
                {
                    // キャンセル以外の失敗コードの場合は例外に変換
                    Marshal.ThrowExceptionForHR(result);
                }
            }
            finally
            {
                // COMオブジェクトの参照を逆順で解放
                ComUtility.ReleaseAllComObject(comObjects);
            }

            return dlgResult;
        }
        #endregion
    }
}
