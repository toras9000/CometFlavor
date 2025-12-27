using System.Runtime.InteropServices;

namespace CometFlavor.Win32.Interaction;

/// <summary>Windows API のメッセージボックス</summary>
public static partial class MessageBox
{
    /// <summary>ボタン種別</summary>
    public enum Button : uint
    {
        /// <summary>OK ボタン</summary>
        OK = 0x00000000u,
        /// <summary>OK, Cancel の 2 ボタン</summary>
        OKCancel = 0x00000001u,
        /// <summary>Yes, No の 2 ボタン</summary>
        YesNo = 0x00000004u,
        /// <summary>Yes, No, Cancel の 3 ボタン</summary>
        YesNoCancel = 0x00000003u,
        /// <summary>Retry, Cancel の 2 ボタン</summary>
        RetryCancel = 0x00000005u,
        /// <summary>Abort, Retry, Ignore の 3 ボタン</summary>
        AbortRetryIgnore = 0x00000002u,
        /// <summary>Cancel, Try Again, Continue の 3 ボタン</summary>
        CancelTryContinue = 0x00000006u,
        /// <summary>ヘルプボタンを追加</summary>
        Help = 0x00004000u,
    }

    /// <summary>アイコン種別</summary>
    public enum Icon : uint
    {
        /// <summary>なし</summary>
        None = 0x00000000u,
        /// <summary>情報</summary>
        Information = 0x00000040u,
        /// <summary>情報</summary>
        Asterisk = 0x00000040u,
        /// <summary>感嘆符</summary>
        Exclamation = 0x00000030u,
        /// <summary>感嘆符</summary>
        Warning = 0x00000030u,
        /// <summary>疑問符</summary>
        Question = 0x00000020u,
        /// <summary>停止記号</summary>
        Stop = 0x00000010u,
        /// <summary>停止記号</summary>
        Error = 0x00000010u,
    }

    /// <summary>規定のボタン</summary>
    public enum DefaultButton : uint
    {
        /// <summary>指定なし</summary>
        Unspecified = 0x00000000u,
        /// <summary>1つ目のボタン</summary>
        Button1 = 0x00000000u,
        /// <summary>2つ目のボタン</summary>
        Button2 = 0x00000100u,
        /// <summary>3つ目のボタン</summary>
        Button3 = 0x00000200u,
        /// <summary>4つ目のボタン</summary>
        Button4 = 0x00000300u,
    }

    /// <summary>モダリティ</summary>
    public enum Modality : uint
    {
        /// <summary>指定なし</summary>
        Unspecified = 0x00000000u,
        /// <summary>親ウィンドウモーダル</summary>
        App = 0x00000000u,
        /// <summary>システムモーダル </summary>
        System = 0x00001000u,
        /// <summary>スレッドトップレベルウィンドウモーダル </summary>
        Task = 0x00002000u,
    }

    /// <summary>オプション</summary>
    public enum Options : uint
    {
        /// <summary>なし</summary>
        None = 0x00000000u,
        /// <summary>規定のデスクトップ</summary>
        DefaultDesktopOnly = 0x00020000u,
        /// <summary>テキストの右揃え</summary>
        Right = 0x00080000u,
        /// <summary>右から左のキャラクタ並び</summary>
        RtlReading = 0x00100000u,
        /// <summary>フォアグラウンド表示</summary>
        Foreground = 0x00010000u,
        /// <summary>最前面表示</summary>
        TopMost = 0x00040000u,
        /// <summary>サービスコンテキストからの表示</summary>
        ServiceNotification = 0x00200000u,
    }

    /// <summary>メッセージボックス結果</summary>
    public enum Result
    {
        /// <summary>不明</summary>
        Unknown = 0,
        /// <summary>OK ボタン</summary>
        OK = 1,
        /// <summary>Cancel ボタン</summary>
        Cancel = 2,
        /// <summary>Yes ボタン</summary>
        Yes = 6,
        /// <summary>No ボタン</summary>
        No = 7,
        /// <summary>Continue ボタン</summary>
        Continue = 11,
        /// <summary>Retry ボタン</summary>
        Retry = 4,
        /// <summary>Ignore ボタン</summary>
        Ignore = 5,
        /// <summary>TryAgain ボタン</summary>
        TryAgain = 10,
        /// <summary>Abort ボタン</summary>
        Abort = 3,
    }

    /// <summary>メッセージボックスの表示設定</summary>
    /// <param name="Button">ボタン種別</param>
    /// <param name="Icon">アイコン種別</param>
    /// <param name="DefaultButton">規定のボタン</param>
    /// <param name="Modality">モダリティ</param>
    /// <param name="Options">オプション</param>
    public record Settings(
        Button Button = Button.OK,
        Icon Icon = Icon.None,
        DefaultButton DefaultButton = DefaultButton.Unspecified,
        Modality Modality = Modality.Unspecified,
        Options Options = Options.None
    );

    /// <summary>メッセージボックスを表示する</summary>
    /// <param name="owner">オーナーウィンドウハンドル</param>
    /// <param name="text">メッセージ文字列</param>
    /// <param name="caption">キャプション文字列</param>
    /// <param name="button">ボタン種別</param>
    /// <param name="icon">アイコン種別</param>
    /// <param name="defaultButton">規定のボタン</param>
    /// <param name="modality">モダリティ</param>
    /// <param name="options">オプション</param>
    /// <returns>メッセージボックスの選択結果</returns>
    public static Result Show(IntPtr owner, string text, string caption, Button button, Icon icon, DefaultButton defaultButton, Modality modality, Options options)
    {
        var type = 0u;
        type |= (uint)button;
        type |= (uint)icon;
        type |= (uint)defaultButton;
        type |= (uint)modality;
        type |= (uint)options;
        var result = NativeMethods.MessageBox(owner, text, caption, type);
        return (Result)result;
    }

    /// <summary>メッセージボックスを表示する</summary>
    /// <param name="owner">オーナーウィンドウハンドル</param>
    /// <param name="text">メッセージ文字列</param>
    /// <param name="caption">キャプション文字列</param>
    /// <param name="settings">メッセージボックスの表示設定</param>
    /// <returns>メッセージボックスの選択結果</returns>
    public static Result Show(IntPtr owner, string text, string caption, Settings settings)
        => Show(owner, text, caption, settings.Button, settings.Icon, settings.DefaultButton, settings.Modality, settings.Options);

    /// <summary>メッセージボックスを表示する</summary>
    /// <param name="text">メッセージ文字列</param>
    /// <param name="caption">キャプション文字列</param>
    /// <param name="settings">メッセージボックスの表示設定</param>
    /// <returns>メッセージボックスの選択結果</returns>
    public static Result Show(string text, string caption, Settings settings)
        => Show(IntPtr.Zero, text, caption, settings.Button, settings.Icon, settings.DefaultButton, settings.Modality, settings.Options);

    /// <summary>メッセージボックスを表示する</summary>
    /// <param name="text">メッセージ文字列</param>
    /// <param name="caption">キャプション文字列</param>
    /// <param name="button">ボタン種別</param>
    /// <param name="icon">アイコン種別</param>
    /// <returns>メッセージボックスの選択結果</returns>
    public static Result Show(string text, string caption, Button button = Button.OK, Icon icon = Icon.None)
        => Show(IntPtr.Zero, text, caption, button, icon, DefaultButton.Unspecified, Modality.Unspecified, Options.None);

    /// <summary>APIインポート</summary>
    private static partial class NativeMethods
    {
        /// <summary>メッセージボックス</summary>
        [LibraryImport("user32.dll", EntryPoint = "MessageBoxW", StringMarshalling = StringMarshalling.Utf16)]
        public static partial int MessageBox(IntPtr owner, string? text, string? caption, uint type);
    }
}
