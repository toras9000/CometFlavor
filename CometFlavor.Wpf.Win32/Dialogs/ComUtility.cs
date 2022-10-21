// このファイル内のコメントを除いたソースコードはパブリックドメインとします。
// The source code except for comments in this file is in the public domain.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using static CometFlavor.Wpf.Win32.Dialogs.NativeDefinitions;

namespace CometFlavor.Wpf.Win32.Dialogs;

/// <summary>
/// COMを扱う関連のユーティリティ処理
/// </summary>
internal static class ComUtility
{
    /// <summary>COMオブジェクトを保持するためのリスト</summary>
    public class ObjectList : List<object?>
    { }

    /// <summary>IShellItemのGuidインスタンスキャッシュ</summary>
    public static readonly Guid IShellItemID = new Guid(IID.IShellItem);

    /// <summary>COMオブジェクトをリストに追加してそのまま返却する拡張メソッド</summary>
    /// <typeparam name="T">追加するオブジェクトの型</typeparam>
    /// <param name="self">リストに追加するオブジェクト</param>
    /// <param name="list">追加先リスト</param>
    /// <returns>追加したオブジェクト自身</returns>
    public static T WithAddTo<T>(this T self, ObjectList list)
    {
        list.Add(self);
        return self;
    }

    /// <summary>パス文字列からシェルアイテムを生成する。</summary>
    /// <param name="path">パス文字列</param>
    /// <returns>シェルアイテム</returns>
    public static IShellItem CreateShellItemFromPath(string path)
    {
        var result = WinApi.SHCreateItemFromParsingName_ShellItem(path, IntPtr.Zero, in IShellItemID, out var item);
        if (!SUCCEEDED(result))
        {
            Marshal.ThrowExceptionForHR(result);
        }

        return item;
    }

    /// <summary>シェルアイテムのファイルシステムパスを取得する。</summary>
    /// <param name="item">シェルアイテム</param>
    /// <returns>アイテムのファイルシステムパス</returns>
    public static string? GetFileSystemPath(IShellItem item)
    {
        var namePtr = IntPtr.Zero;
        var path = default(string);
        try
        {
            // アイテムのファイルパスを取得
            item.GetDisplayName(SIGDN.FILESYSPATH, out namePtr);

            // 文字列取り出し
            path = Marshal.PtrToStringUni(namePtr);
        }
        finally
        {
            // 名称取得用に確保されたメモリを解放
            ReleaseExistTaskMemory(namePtr);
        }

        return path;
    }

    /// <summary>シェルアイテム配列の各アイテムのファイルパスを列挙する</summary>
    /// <param name="items">シェルアイテム配列</param>
    /// <returns>ファイルパスのシーケンス</returns>
    public static IEnumerable<string?> EnumerateItemPaths(IShellItemArray items)
    {
        // 配列内アイテムの個数を取得
        items.GetCount(out var itemCount);

        // 各アイテムからパスを取得
        for (uint i = 0; i < itemCount; i++)
        {
            var item = default(IShellItem);
            var path = default(string);
            try
            {
                // 配列内のシェルアイテム取得
                items.GetItemAt(i, out item);

                // アイテムのパスを取得
                path = GetFileSystemPath(item);
            }
            finally
            {
                // シェルアイテムの参照を解放
                ReleaseExistComObject(item);
            }

            // パスを列挙
            yield return path;
        }
    }

    /// <summary>OLEメモリブロックがあれば解放する。</summary>
    /// <param name="taskMem">メモリブロックのポインタ。NULLポインタでない場合のみ解放を行う。</param>
    public static void ReleaseExistTaskMemory(IntPtr taskMem)
    {
        if (taskMem != IntPtr.Zero)
        {
            try { WinApi.CoTaskMemFree(taskMem); } catch { }
        }
    }

    /// <summary>COMオブジェクトの参照を解放(デクリメント)する。</summary>
    /// <param name="obj">COMオブジェクトのRCWオブジェクト。nullでない場合のみ解放を行う。</param>
    public static void ReleaseExistComObject(object? obj)
    {
        if (obj != null)
        {
            try { Marshal.ReleaseComObject(obj); } catch { }
        }
    }

    /// <summary>COMオブジェクトリスト内のすべてのオブジェクトの参照を解放する。</summary>
    /// <param name="list">COMオブジェクトリスト</param>
    /// <param name="reverse">逆順解放を行うか否か</param>
    public static void ReleaseAllComObject(ObjectList list, bool reverse = true)
    {
        var items = reverse ? list.AsEnumerable().Reverse() : list;
        foreach (var obj in items)
        {
            ReleaseExistComObject(obj);
        }
    }
}
