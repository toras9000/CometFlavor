using System;
using System.Collections.Generic;
using System.IO;

namespace CometFlavor.Extensions.IO;

/// <summary>
/// FileSystemInfo に対する拡張メソッド
/// </summary>
public static class FileSystemInfoExtensions
{
    #region Path
    /// <summary>ファイルパスの構成セグメントを取得する。</summary>
    /// <param name="self">対象ファイルシステムアイテムのFileSystemInfo</param>
    /// <returns>パス構成セグメントのリスト</returns>
    public static IList<string> GetPathSegments(this FileSystemInfo self)
    {
        return self switch
        {
            FileInfo file => file.GetPathSegments(),
            DirectoryInfo dir => dir.GetPathSegments(),
            _ => throw new InvalidCastException(),
        };
    }

    /// <summary>ファイルが指定のディレクトリの子孫であるかを判定する。</summary>
    /// <remarks></remarks>
    /// <param name="self">対象ファイルシステムアイテムのFileSystemInfo</param>
    /// <param name="other">比較するディレクトリ</param>
    /// <param name="sameIs">同一階層を真とするか否か</param>
    /// <returns>指定ディレクトリの子孫であるか否か</returns>
    public static bool IsDescendantOf(this FileSystemInfo self, DirectoryInfo other, bool sameIs = true)
    {
        return self switch
        {
            FileInfo file => file.IsDescendantOf(other),
            DirectoryInfo dir => dir.IsDescendantOf(other, sameIs),
            _ => throw new InvalidCastException(),
        };
    }

    /// <summary>指定のディレクトリを起点としたファイルの相対パスを取得する。</summary>
    /// <remarks>
    /// 単純なパス文字列処理であり、リパースポイントなどを解釈することはない。
    /// </remarks>
    /// <param name="self">対象ファイルシステムアイテムのFileSystemInfo</param>
    /// <param name="baseDir">基準ディレクトリのDirectoryInfo</param>
    /// <param name="ignoreCase">大文字と小文字を同一視するか否か</param>
    /// <returns>相対パス</returns>
    public static string RelativePathFrom(this FileSystemInfo self, DirectoryInfo baseDir, bool ignoreCase)
    {
        return self switch
        {
            FileInfo file => file.RelativePathFrom(baseDir, ignoreCase),
            DirectoryInfo dir => dir.RelativePathFrom(baseDir, ignoreCase),
            _ => throw new InvalidCastException(),
        };
    }
    #endregion

}
