// このファイル内のコメントを除いたソースコードはパブリックドメインとします。
// The source code except for comments in this file is in the public domain.

using System.Runtime.InteropServices;

namespace CometFlavor.Win32.Dialogs;

/// <summary>
/// ネイティブ利用のための定義類
/// </summary>
internal static class NativeDefinitions
{
    /// <summary>
    /// COMクラスID
    /// </summary>
    public static class CLSID
    {
        public const string FileOpenDialog = "DC1C5A9C-E88A-4dde-A5A1-60F82A20AEF7";
        public const string FileSaveDialog = "C0B4E2F3-BA21-4773-8DBA-335EC946EB8B";
    }

    /// <summary>
    /// COMインターフェースID
    /// </summary>
    public static class IID
    {
        public const string IModalWindow = "b4db1657-70d7-485e-8e3e-6fcb5a5c1802";
        public const string IFileDialog = "42f85136-db7e-439c-85f1-e4075d135fc8";
        public const string IFileOpenDialog = "d57c7288-d4ad-4768-be02-9d969532d960";
        public const string IFileSaveDialog = "84bccd23-5fde-4cdb-aea4-af64b83d78ab";
        public const string IFileDialogEvents = "973510db-7d7f-452b-8975-74a85828d354";
        public const string IShellItem = "43826d1e-e718-42ee-bc55-a1e261c37bfe";
        public const string IShellItemFilter = "2659B475-EEB8-48b7-8F07-B378810F48CF";
        public const string IShellItemArray = "b63ea76d-1f85-456f-a19c-48159efa858b";
    }

    /// <summary>
    /// ファイルオープンダイアログ COMインポートインターフェース
    /// </summary>
    [ComImport]
    [Guid(IID.IFileOpenDialog)]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IFileOpenDialog
    {
        #region IModalWindow
        /// <summary>モーダルウィンドウを表示する。</summary>
        /// <param name="hwndParent">オーナーウィンドウハンドル。</param>
        /// <returns>結果のHRESULT値</returns>
        [PreserveSig] Int32 Show(IntPtr hwndParent);
        #endregion

        #region IFileDialog
        /// <summary>ファイルの種類を示すフィルタを設定する。</summary>
        /// <param name="cFileTypes">設定するファイルタイプの数</param>
        /// <param name="rgFilterSpec">ファイルフィルタ情報</param>
        void SetFileTypes(UInt32 cFileTypes, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 0)] COMDLG_FILTERSPEC[] rgFilterSpec);

        /// <summary>ファイルの種類の選択インデックスを設定する。</summary>
        /// <param name="iFileType">選択するファイルタイプフィルタのインデクス。値は1ベース値となる。</param>
        void SetFileTypeIndex(UInt32 iFileType);

        /// <summary>ファイルの種類の選択インデックスを取得する</summary>
        /// <param name="piFileType">選択されたファイルフィルタのインデクス。値は1ベース値となる。</param>
        void GetFileTypeIndex(out UInt32 piFileType);

        /// <summary>ダイアログイベントをリッスンするイベントハンドラを登録する。</summary>
        /// <!--
        /// <param name="pfde">イベントを受け取る IFileDialogEvents の実装へのポインタ</param>
        /// <param name="pdwCookie">登録したイベントハンドラの識別値を格納する変数へのポインタ。ハンドラが不要になった場合はUnadviseにこの値を指定して解除する必要がある。</param>
        /// -->
        /// <remarks>signature : void Advise(IFileDialogEvents pfde, out UInt32 pdwCookie);</remarks>
        void Advise(/* not use. omitted. */);

        /// <summary>ダイアログイベントのイベントハンドラを解除する。</summary>
        /// <param name="dwCookie">Advise()メソッドが返却したイベントハンドラの識別値。</param>
        void Unadvise(UInt32 dwCookie);

        /// <summary>ダイアログの動作フラグを設定する</summary>
        /// <param name="fos">設定するフラグ値</param>
        void SetOptions(FILEOPENDIALOGOPTIONS fos);

        /// <summary>ダイアログの動作フラグを取得する</summary>
        /// <param name="pfos">取得したフラグ値の格納先</param>
        void GetOptions(out FILEOPENDIALOGOPTIONS pfos);

        /// <summary>最近使用したフォルダーが無い場合のデフォルトフォルダを設定する</summary>
        /// <param name="psi">フォルダアイテムを示すインターフェースへのポインタ</param>
        void SetDefaultFolder(IShellItem psi);

        /// <summary>ダイアログを開いた際の初期フォルダを指定する</summary>
        /// <param name="psi">フォルダアイテムを示すインターフェースへのポインタ</param>
        void SetFolder(IShellItem psi);

        /// <summary>ダイアログの初期フォルダ、もしくはダイアログ表示後の選択フォルダを取得する</summary>
        /// <param name="ppsi">取得したフォルダアイテムへのポインタ格納先</param>
        void GetFolder(out IShellItem ppsi);

        /// <summary>ダイアログで現在の選択アイテムを取得する</summary>
        /// <param name="ppsi">取得したアイテムへのポインタ格納先</param>
        void GetCurrentSelection(out IShellItem ppsi);

        /// <summary>ダイアログを開いた際の初期入力ファイル名を指定する</summary>
        /// <param name="pszName">ファイル名</param>
        void SetFileName([MarshalAs(UnmanagedType.LPWStr)] string pszName);

        /// <summary>ダイアログで現在入力されているファイル名</summary>
        /// <param name="pszName">取得したファイル名文字列へのポインタ格納先</param>
        void GetFileName(out IntPtr pszName);

        /// <summary>ダイアログのタイトルテキストを設定する</summary>
        /// <param name="pszTitle">設定するタイトルテキスト</param>
        void SetTitle([MarshalAs(UnmanagedType.LPWStr)] string pszTitle);

        /// <summary>確定ボタンのラベルテキストを設定する</summary>
        /// <param name="pszText">設定するラベルテキスト</param>
        void SetOkButtonLabel([MarshalAs(UnmanagedType.LPWStr)] string pszText);

        /// <summary>ファイル名入力欄キャプションのラベルテキストを設定する</summary>
        /// <param name="pszLabel">設定するキャプションテキスト</param>
        void SetFileNameLabel([MarshalAs(UnmanagedType.LPWStr)] string pszLabel);

        /// <summary>ダイアログの選択結果を取得する</summary>
        /// <param name="ppsi">取得したアイテムへのポインタ格納先</param>
        void GetResult(out IShellItem ppsi);

        /// <summary>ダイアログで選択可能な場所を追加する</summary>
        /// <param name="psi">フォルダアイテムを示すインターフェースへのポインタ</param>
        /// <param name="fdap">フォルダの配置位置</param>
        void AddPlace(IShellItem psi, FDAP fdap);

        /// <summary>選択したファイル名に付与するデフォルト拡張子を設定する</summary>
        /// <param name="pszDefaultExtension">拡張子文字列。ピリオドは含めない。</param>
        void SetDefaultExtension([MarshalAs(UnmanagedType.LPWStr)] string pszDefaultExtension);

        /// <summary>ダイアログを閉じる</summary>
        /// <param name="hr">Show()の返却値とする値</param>
        void Close(UInt32 hr);

        /// <summary>ダイアログ状態の永続化用GUIDを設定する</summary>
        /// <param name="guid">任意のGUID</param>
        void SetClientGuid(in Guid guid);

        /// <summary>ダイアログ状態の永続化データをクリアする</summary>
        void ClearClientData();

        /// <summary>廃止。Windows7以降では非サポート。</summary>
        /// <remarks>signature : void SetFilter(IShellItemFilter pFilter);</remarks>
        void SetFilter(/* not use. omitted. */);
        #endregion

        #region IFileOpenDialog
        /// <summary>ダイアログでの選択結果をすべて取得する</summary>
        /// <param name="ppenum">選択結果アイテムを表すインターフェースポインタの格納先</param>
        void GetResults(out IShellItemArray ppenum);

        /// <summary>ダイアログで現在選択されているアイテムをすべて取得する</summary>
        /// <param name="ppsai">選択状態をアイテム表すインターフェースポインタの格納先</param>
        void GetSelectedItems(out IShellItemArray ppsai);
        #endregion
    }

    /// <summary>
    /// ファイル保存ダイアログ COMインポートインターフェース
    /// </summary>
    [ComImport]
    [Guid(IID.IFileSaveDialog)]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IFileSaveDialog
    {
        #region IModalWindow
        /// <summary>モーダルウィンドウを表示する。</summary>
        /// <param name="hwndParent">オーナーウィンドウハンドル。</param>
        /// <returns>結果のHRESULT値</returns>
        [PreserveSig] Int32 Show(IntPtr hwndParent);
        #endregion

        #region IFileDialog
        /// <summary>ファイルの種類を示すフィルタを設定する。</summary>
        /// <param name="cFileTypes">設定するファイルタイプの数</param>
        /// <param name="rgFilterSpec">ファイルフィルタ情報</param>
        void SetFileTypes(UInt32 cFileTypes, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 0)] COMDLG_FILTERSPEC[] rgFilterSpec);

        /// <summary>ファイルの種類の選択インデックスを設定する。</summary>
        /// <param name="iFileType">選択するファイルタイプフィルタのインデクス。値は1ベース値となる。</param>
        void SetFileTypeIndex(UInt32 iFileType);

        /// <summary>ファイルの種類の選択インデックスを取得する</summary>
        /// <param name="piFileType">選択されたファイルフィルタのインデクス。値は1ベース値となる。</param>
        void GetFileTypeIndex(out UInt32 piFileType);

        /// <summary>ダイアログイベントをリッスンするイベントハンドラを登録する。</summary>
        /// <!--
        /// <param name="pfde">イベントを受け取る IFileDialogEvents の実装へのポインタ</param>
        /// <param name="pdwCookie">登録したイベントハンドラの識別値を格納する変数へのポインタ。ハンドラが不要になった場合はUnadviseにこの値を指定して解除する必要がある。</param>
        /// -->
        /// <remarks>signature : void Advise(IFileDialogEvents pfde, out UInt32 pdwCookie);</remarks>
        void Advise(/* not use. omitted. */);

        /// <summary>ダイアログイベントのイベントハンドラを解除する。</summary>
        /// <param name="dwCookie">Advise()メソッドが返却したイベントハンドラの識別値。</param>
        void Unadvise(UInt32 dwCookie);

        /// <summary>ダイアログの動作フラグを設定する</summary>
        /// <param name="fos">設定するフラグ値</param>
        void SetOptions(FILEOPENDIALOGOPTIONS fos);

        /// <summary>ダイアログの動作フラグを取得する</summary>
        /// <param name="pfos">取得したフラグ値の格納先</param>
        void GetOptions(out FILEOPENDIALOGOPTIONS pfos);

        /// <summary>最近使用したフォルダーが無い場合のデフォルトフォルダを設定する</summary>
        /// <param name="psi">フォルダアイテムを示すインターフェースへのポインタ</param>
        void SetDefaultFolder(IShellItem psi);

        /// <summary>ダイアログを開いた際の初期フォルダを指定する</summary>
        /// <param name="psi">フォルダアイテムを示すインターフェースへのポインタ</param>
        void SetFolder(IShellItem psi);

        /// <summary>ダイアログの初期フォルダ、もしくはダイアログ表示後の選択フォルダを取得する</summary>
        /// <param name="ppsi">取得したフォルダアイテムへのポインタ格納先</param>
        void GetFolder(out IShellItem ppsi);

        /// <summary>ダイアログで現在の選択アイテムを取得する</summary>
        /// <param name="ppsi">取得したアイテムへのポインタ格納先</param>
        void GetCurrentSelection(out IShellItem ppsi);

        /// <summary>ダイアログを開いた際の初期入力ファイル名を指定する</summary>
        /// <param name="pszName">ファイル名</param>
        void SetFileName([MarshalAs(UnmanagedType.LPWStr)] string pszName);

        /// <summary>ダイアログで現在入力されているファイル名</summary>
        /// <param name="pszName">取得したファイル名文字列へのポインタ格納先</param>
        void GetFileName(out IntPtr pszName);

        /// <summary>ダイアログのタイトルテキストを設定する</summary>
        /// <param name="pszTitle">設定するタイトルテキスト</param>
        void SetTitle([MarshalAs(UnmanagedType.LPWStr)] string pszTitle);

        /// <summary>確定ボタンのラベルテキストを設定する</summary>
        /// <param name="pszText">設定するラベルテキスト</param>
        void SetOkButtonLabel([MarshalAs(UnmanagedType.LPWStr)] string pszText);

        /// <summary>ファイル名入力欄キャプションのラベルテキストを設定する</summary>
        /// <param name="pszLabel">設定するキャプションテキスト</param>
        void SetFileNameLabel([MarshalAs(UnmanagedType.LPWStr)] string pszLabel);

        /// <summary>ダイアログの選択結果を取得する</summary>
        /// <param name="ppsi">取得したアイテムへのポインタ格納先</param>
        void GetResult(out IShellItem ppsi);

        /// <summary>ダイアログで選択可能な場所を追加する</summary>
        /// <param name="psi">フォルダアイテムを示すインターフェースへのポインタ</param>
        /// <param name="fdap">フォルダの配置位置</param>
        void AddPlace(IShellItem psi, FDAP fdap);

        /// <summary>選択したファイル名に付与するデフォルト拡張子を設定する</summary>
        /// <param name="pszDefaultExtension">拡張子文字列。ピリオドは含めない。</param>
        void SetDefaultExtension([MarshalAs(UnmanagedType.LPWStr)] string pszDefaultExtension);

        /// <summary>ダイアログを閉じる</summary>
        /// <param name="hr">Show()の返却値とする値</param>
        void Close(UInt32 hr);

        /// <summary>ダイアログ状態の永続化用GUIDを設定する</summary>
        /// <param name="guid">任意のGUID</param>
        void SetClientGuid(in Guid guid);

        /// <summary>ダイアログ状態の永続化データをクリアする</summary>
        void ClearClientData();

        /// <summary>廃止。Windows7以降では非サポート。</summary>
        /// <remarks>signature : void SetFilter(IShellItemFilter pFilter);</remarks>
        void SetFilter(/* not use. omitted. */);
        #endregion

        #region IFileSaveDialog
        /// <summary>初期エントリとするアイテムを設定する</summary>
        /// <param name="psi"></param>
        void SetSaveAsItem(IShellItem psi);

        // uninvestigated
        //void SetProperties(IPropertyStore pStore);
        void SetProperties(/* not use. omitted. */);

        // uninvestigated
        //void SetCollectedProperties(IPropertyDescriptionList pList, UInt32 fAppendDefault);
        void SetCollectedProperties(/* not use. omitted. */);

        // uninvestigated
        //void GetProperties(out IPropertyStore ppStore);
        void GetProperties(/* not use. omitted. */);

        // uninvestigated
        //void ApplyProperties(IShellItem psi, IPropertyStore pStore, IntPtr hwnd, IFileOperationProgressSink pSink);
        void ApplyProperties(/* not use. omitted. */);
        #endregion
    }

    /// <summary>
    /// シェルアイテム COMインポートインターフェース
    /// </summary>
    [ComImport]
    [Guid(IID.IShellItem)]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IShellItem
    {
        // uninvestigated
        void BindToHandler(IntPtr pbc, in Guid bhid, in Guid riid, out IntPtr ppv);

        /// <summary>親アイテムオブジェクトを取得する</summary>
        /// <param name="ppsi">取得したアイテムへのポインタ格納先</param>
        void GetParent(out IShellItem ppsi);

        /// <summary>オブジェクトの表示名を取得する</summary>
        /// <param name="sigdnName">取得する名称種別</param>
        /// <param name="pszName">取得した名称文字列(NUL終端)へのポインタ。不要になった場合呼び出し元で CoTaskMemFree() を呼び出して解放する必要がある。</param>
        void GetDisplayName(SIGDN sigdnName, out IntPtr pszName);

        // uninvestigated
        //void GetAttributes(SFGAOF sfgaoMask, out SFGAOF psfgaoAttribs);
        void GetAttributes(/* not use. omitted. */);

        // uninvestigated
        //void Compare(IShellItem psi, SICHINTF hint, out IntPtr piOrder);
        void Compare(/* not use. omitted. */);
    }

    /// <summary>
    /// シェルアイテム配列 COMインポートインターフェース
    /// </summary>
    [ComImport]
    [Guid(IID.IShellItemArray)]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IShellItemArray
    {
        // uninvestigated
        void BindToHandler(IntPtr pbc, in Guid bhid, in Guid riid, out IntPtr ppv);

        // uninvestigated
        // void GetPropertyStore(GETPROPERTYSTOREFLAGS flags, in Guid riid, out IntPtr ppv);
        void GetPropertyStore(/* not use. omitted. */);

        // uninvestigated
        // void GetPropertyDescriptionList(REFPROPERTYKEY keyType, in Guid riid, out IntPtr ppv);
        void GetPropertyDescriptionList(/* not use. omitted. */);

        // uninvestigated
        // void GetAttributes(SIATTRIBFLAGS AttribFlags, SFGAOF sfgaoMask, out SFGAOF psfgaoAttribs);
        void GetAttributes(/* not use. omitted. */);

        /// <summary>配列内の項目数を取得する</summary>
        /// <param name="pdwNumItems">取得した項目数の格納先</param>
        void GetCount(out UInt32 pdwNumItems);

        /// <summary>配列内の指定インデックスのアイテムを取得する</summary>
        /// <param name="dwIndex">アイテムのインデックス</param>
        /// <param name="ppsi">取得したアイテムへのポインタ格納先</param>
        void GetItemAt(UInt32 dwIndex, out IShellItem ppsi);

        // uninvestigated
        // void EnumItems(out IEnumShellItems ppenumShellItems);
        void EnumItems(/* not use. omitted. */);
    }

    /// <summary>フィルタ仕様を表す汎用データ型</summary>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public struct COMDLG_FILTERSPEC
    {
        /// <summary>フィルタの表示用名称</summary>
        [MarshalAs(UnmanagedType.LPWStr)] public string pszName;
        /// <summary>フィルタパターン文字列</summary>
        [MarshalAs(UnmanagedType.LPWStr)] public string pszSpec;
    }

    /// <summary>オプション定義フラグ</summary>
    [Flags]
    public enum FILEOPENDIALOGOPTIONS : UInt32
    {
        /// <summary>上書きの確認を表示する</summary>
        OVERWRITEPROMPT = 0x2,
        /// <summary>設定されたファイルタイプのファイルのみを選択可能とする</summary>
        STRICTFILETYPES = 0x4,
        /// <summary>カレントディレクトリを変更しない</summary>
        NOCHANGEDIR = 0x8,
        /// <summary>フォルダを選択する</summary>
        PICKFOLDERS = 0x20,
        /// <summary>ファイルシステムアイテムのみを選択可能とする</summary>
        FORCEFILESYSTEM = 0x40,
        /// <summary>シェルネームスペース内のすべてのアイテムを選択可能とする</summary>
        ALLNONSTORAGEITEMS = 0x80,
        /// <summary>ファイルを開けない状態(共有不可やアクセス拒否等)を検証しない</summary>
        NOVALIDATE = 0x100,
        /// <summary>複数選択を許可する</summary>
        ALLOWMULTISELECT = 0x200,
        /// <summary>存在するフォルダのアイテムのみを選択可能とする</summary>
        PATHMUSTEXIST = 0x800,
        /// <summary>存在するアイテムのみを選択可能とする</summary>
        FILEMUSTEXIST = 0x1000,
        /// <summary>ファイルの作成確認を表示する</summary>
        CREATEPROMPT = 0x2000,
        /// <summary>共有違反(アプリケーションが開いている場合)のガイダンスを表示する</summary>
        SHAREAWARE = 0x4000,
        /// <summary>読み取り専用アイテムを選択不可とする</summary>
        NOREADONLYRETURN = 0x8000,
        /// <summary>ファイルが作成可能であるかをテストしない</summary>
        NOTESTFILECREATE = 0x10000,
        /// <summary>最近使用した場所リストを非表示とする。Windows7以降では非サポート。</summary>
        HIDEMRUPLACES = 0x20000,
        /// <summary>ナビゲーションペインのデフォルトアイテムを非表示とする</summary>
        HIDEPINNEDPLACES = 0x40000,
        /// <summary>ショートカットの参照先解決をしない。(ショートカットファイル自体を選択可能とする)</summary>
        NODEREFERENCELINKS = 0x100000,
        /// <summary>確定ボタン操作のためにユーザ操作を必要とする</summary>
        OKBUTTONNEEDSINTERACTION = 0x200000,
        /// <summary>選択アイテムを最近使用したファイルのリストに追加しない</summary>
        DONTADDTORECENT = 0x2000000,
        /// <summary>非表示属性のアイテムを表示する</summary>
        FORCESHOWHIDDEN = 0x10000000,
        /// <summary>デフォルトで拡張モード表示する。Windows7以降では非サポート。</summary>
        DEFAULTNOMINIMODE = 0x20000000,
        /// <summary>オープンダイアログでプレビューペインを常に表示する</summary>
        FORCEPREVIEWPANEON = 0x40000000,
        /// <summary>呼び出し元でストリームアイテムをサポートする(事前のファイルダウンロードを行わない)</summary>
        SUPPORTSTREAMABLEITEMS = 0x80000000,
    }

    /// <summary>リストへの追加位置を指定する列挙子</summary>
    public enum FDAP : Int32
    {
        /// <summary>リストの末尾</summary>
        BOTTOM = 0,
        /// <summary>リストの先頭</summary>
        TOP = 1
    }

    /// <summary>表示名種別</summary>
    public enum SIGDN : UInt32
    {
        /// <summary>親アイテムを基準とした解析名称(UI向け)</summary>
        NORMALDISPLAY = 0,
        /// <summary>親アイテムからの相対解析名称(UI不向き)</summary>
        PARENTRELATIVEPARSING = 0x80018001,
        /// <summary>デスクトップを基準とした解析名称(UI不向き)</summary>
        DESKTOPABSOLUTEPARSING = 0x80028000,
        /// <summary>親アイテムを基準とした編集名称(UI向け)</summary>
        PARENTRELATIVEEDITING = 0x80031001,
        /// <summary>デスクトップを基準とした編集名称(UI向け)</summary>
        DESKTOPABSOLUTEEDITING = 0x8004c000,
        /// <summary>ファイルシステムパス</summary>
        FILESYSPATH = 0x80058000,
        /// <summary>URL</summary>
        URL = 0x80068000,
        /// <summary>親アイテムを基準としたアドレスバー名称(UI向け)</summary>
        PARENTRELATIVEFORADDRESSBAR = 0x8007c001,
        /// <summary>親アイテムからの相対パス</summary>
        PARENTRELATIVE = 0x80080001,
        /// <summary>親アイテムからの相対パス(UI向け)</summary>
        PARENTRELATIVEFORUI = 0x80094001,
    }

    /// <summary>操作のキャンセルを表すエラーコード</summary>
    public const Int32 ERROR_CANCELLED = 1223;

    /// <summary>HRESULTコードのFacilityフィールドでサービス種別Win32を示すコード</summary>
    public const Int32 FACILITY_WIN32 = 7;

    /// <summary>HRESULTが成功を示す値であるかを判定する</summary>
    /// <param name="hResult">HRESULT値</param>
    /// <returns>成功を示す値であるか否か</returns>
    public static bool SUCCEEDED(Int32 hResult)
    {
        return 0 <= hResult;
    }

    /// <summary>システムエラーコードをマップしたHRESULT値を作成する</summary>
    /// <param name="errcode">システムエラーコード</param>
    /// <returns>作成したHRESULT値</returns>
    public static Int32 HRESULT_FROM_WIN32(UInt32 errcode)
    {
        return ((Int32)errcode <= 0) ? (Int32)errcode : (Int32)((errcode & 0x0000FFFF) | (FACILITY_WIN32 << 16) | 0x80000000);
    }

    /// <summary>
    /// APIのプロトタイプ
    /// </summary>
    public static class WinApi
    {
        /// <summary>パス文字列を解析してシェルアイテムを生成する</summary>
        [DllImport("Shell32.dll", EntryPoint = "SHCreateItemFromParsingName", CallingConvention = CallingConvention.Winapi, CharSet = CharSet.Unicode)]
        public static extern Int32 SHCreateItemFromParsingName_ShellItem(string pszPath, IntPtr pbc, in Guid riid, out IShellItem ppv);

        /// <summary>タスクメモリブロックを解放する</summary>
        [DllImport("Ole32.dll", CallingConvention = CallingConvention.Winapi)]
        public static extern void CoTaskMemFree(IntPtr pv);
    }
}
