using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CometFlavor.Extensions.IO;

/// <summary>
/// FileInfo に対する拡張メソッド
/// </summary>
public static class FileInfoExtensions
{
    /// <summary>
    /// ファイル内容の全バイト列を読み出す。
    /// </summary>
    /// <param name="self">対象ファイルのFileInfo</param>
    /// <returns>ファイルから読みだしたバイト列</returns>
    public static byte[] ReadAllBytes(this FileInfo self)
    {
        if (self == null) throw new ArgumentNullException(nameof(self));
        return File.ReadAllBytes(self.FullName);
    }

    /// <summary>
    /// ファイル内容の全テキストを読み出す。
    /// </summary>
    /// <param name="self">対象ファイルのFileInfo</param>
    /// <returns>ファイルから読みだした全テキスト</returns>
    public static string ReadAllText(this FileInfo self)
    {
        if (self == null) throw new ArgumentNullException(nameof(self));
        return File.ReadAllText(self.FullName);
    }

    /// <summary>
    /// ファイル内容の全テキストを読み出す。
    /// </summary>
    /// <param name="self">対象ファイルのFileInfo</param>
    /// <param name="encoding">ファイル内容をデコードするテキストエンコーディング</param>
    /// <returns>ファイルから読みだした全テキスト</returns>
    public static string ReadAllText(this FileInfo self, Encoding encoding)
    {
        if (self == null) throw new ArgumentNullException(nameof(self));
        return File.ReadAllText(self.FullName, encoding);
    }

    /// <summary>
    /// ファイル内容の全テキスト行を読み出す。
    /// </summary>
    /// <param name="self">対象ファイルのFileInfo</param>
    /// <returns>ファイルから読みだした全テキスト行</returns>
    public static string[] ReadAllLines(this FileInfo self)
    {
        if (self == null) throw new ArgumentNullException(nameof(self));
        return File.ReadAllLines(self.FullName);
    }

    /// <summary>
    /// ファイル内容の全テキスト行を読み出す。
    /// </summary>
    /// <param name="self">対象ファイルのFileInfo</param>
    /// <param name="encoding">ファイル内容をデコードするテキストエンコーディング</param>
    /// <returns>ファイルから読みだした全テキスト行</returns>
    public static string[] ReadAllLines(this FileInfo self, Encoding encoding)
    {
        if (self == null) throw new ArgumentNullException(nameof(self));
        return File.ReadAllLines(self.FullName, encoding);
    }

    /// <summary>
    /// ファイル内容のテキスト行を読み出す。
    /// </summary>
    /// <param name="self">対象ファイルのFileInfo</param>
    /// <returns>ファイルから読みだしたテキスト行</returns>
    public static IEnumerable<string> ReadLines(this FileInfo self)
    {
        if (self == null) throw new ArgumentNullException(nameof(self));
        return File.ReadLines(self.FullName);
    }

    /// <summary>
    /// ファイル内容の全テキスト行を読み出す。
    /// </summary>
    /// <param name="self">対象ファイルのFileInfo</param>
    /// <param name="encoding">ファイル内容をデコードするテキストエンコーディング</param>
    /// <returns>ファイルから読みだしたテキスト行</returns>
    public static IEnumerable<string> ReadLines(this FileInfo self, Encoding encoding)
    {
        if (self == null) throw new ArgumentNullException(nameof(self));
        return File.ReadLines(self.FullName, encoding);
    }

#if NETCOREAPP2_0_OR_GREATER
    /// <summary>
    /// ファイル内容の全バイト列を読み出す。
    /// </summary>
    /// <param name="self">対象ファイルのFileInfo</param>
    /// <param name="cancelToken">キャンセルトークン</param>
    /// <returns>ファイルから読みだしたバイト列</returns>
    public static Task<byte[]> ReadAllBytesAsync(this FileInfo self, CancellationToken cancelToken = default)
    {
        if (self == null) throw new ArgumentNullException(nameof(self));
        return File.ReadAllBytesAsync(self.FullName, cancelToken);
    }

    /// <summary>
    /// ファイル内容の全テキストを読み出す。
    /// </summary>
    /// <param name="self">対象ファイルのFileInfo</param>
    /// <param name="cancelToken">キャンセルトークン</param>
    /// <returns>ファイルから読みだした全テキスト</returns>
    public static Task<string> ReadAllTextAsync(this FileInfo self, CancellationToken cancelToken = default)
    {
        if (self == null) throw new ArgumentNullException(nameof(self));
        return File.ReadAllTextAsync(self.FullName, cancelToken);
    }

    /// <summary>
    /// ファイル内容の全テキストを読み出す。
    /// </summary>
    /// <param name="self">対象ファイルのFileInfo</param>
    /// <param name="encoding">ファイル内容をデコードするテキストエンコーディング</param>
    /// <param name="cancelToken">キャンセルトークン</param>
    /// <returns>ファイルから読みだした全テキスト</returns>
    public static Task<string> ReadAllTextAsync(this FileInfo self, Encoding encoding, CancellationToken cancelToken = default)
    {
        if (self == null) throw new ArgumentNullException(nameof(self));
        return File.ReadAllTextAsync(self.FullName, encoding, cancelToken);
    }

    /// <summary>
    /// ファイル内容の全テキスト行を読み出す。
    /// </summary>
    /// <param name="self">対象ファイルのFileInfo</param>
    /// <param name="cancelToken">キャンセルトークン</param>
    /// <returns>ファイルから読みだした全テキスト行</returns>
    public static Task<string[]> ReadAllLinesAsync(this FileInfo self, CancellationToken cancelToken = default)
    {
        if (self == null) throw new ArgumentNullException(nameof(self));
        return File.ReadAllLinesAsync(self.FullName, cancelToken);
    }

    /// <summary>
    /// ファイル内容の全テキスト行を読み出す。
    /// </summary>
    /// <param name="self">対象ファイルのFileInfo</param>
    /// <param name="encoding">ファイル内容をデコードするテキストエンコーディング</param>
    /// <param name="cancelToken">キャンセルトークン</param>
    /// <returns>ファイルから読みだした全テキスト行</returns>
    public static Task<string[]> ReadAllLinesAsync(this FileInfo self, Encoding encoding, CancellationToken cancelToken = default)
    {
        if (self == null) throw new ArgumentNullException(nameof(self));
        return File.ReadAllLinesAsync(self.FullName, encoding, cancelToken);
    }
#endif

    /// <summary>
    /// ファイル内容が指定のバイト列となるように書き込む。
    /// </summary>
    /// <param name="self">対象ファイルのFileInfo</param>
    /// <param name="bytes">書き込むバイト列</param>
    public static void WriteAllBytes(this FileInfo self, byte[] bytes)
    {
        if (self == null) throw new ArgumentNullException(nameof(self));
        File.WriteAllBytes(self.FullName, bytes);
        self.Refresh();
    }

    /// <summary>
    /// ファイル内容が指定のテキストとなるように書き込む。
    /// </summary>
    /// <param name="self">対象ファイルのFileInfo</param>
    /// <param name="contents">書き込むテキスト</param>
    public static void WriteAllText(this FileInfo self, string contents)
    {
        if (self == null) throw new ArgumentNullException(nameof(self));
        File.WriteAllText(self.FullName, contents);
        self.Refresh();
    }

    /// <summary>
    /// ファイル内容が指定のテキストとなるように書き込む。
    /// </summary>
    /// <param name="self">対象ファイルのFileInfo</param>
    /// <param name="contents">書き込むテキスト</param>
    /// <param name="encoding">書き込むテキストをエンコードするテキストエンコーディング</param>
    public static void WriteAllText(this FileInfo self, string contents, Encoding encoding)
    {
        if (self == null) throw new ArgumentNullException(nameof(self));
        File.WriteAllText(self.FullName, contents, encoding);
        self.Refresh();
    }

    /// <summary>
    /// ファイル内容が指定のテキスト行となるように書き込む。
    /// </summary>
    /// <param name="self">対象ファイルのFileInfo</param>
    /// <param name="contents">書き込むテキスト行</param>
    public static void WriteAllLines(this FileInfo self, IEnumerable<string> contents)
    {
        if (self == null) throw new ArgumentNullException(nameof(self));
        File.WriteAllLines(self.FullName, contents);
        self.Refresh();
    }

    /// <summary>
    /// ファイル内容が指定のテキスト行となるように書き込む。
    /// </summary>
    /// <param name="self">対象ファイルのFileInfo</param>
    /// <param name="contents">書き込むテキスト行</param>
    /// <param name="encoding">書き込むテキストをエンコードするテキストエンコーディング</param>
    public static void WriteAllLines(this FileInfo self, IEnumerable<string> contents, Encoding encoding)
    {
        if (self == null) throw new ArgumentNullException(nameof(self));
        File.WriteAllLines(self.FullName, contents, encoding);
        self.Refresh();
    }

#if NETCOREAPP2_0_OR_GREATER
    /// <summary>
    /// ファイル内容が指定のバイト列となるように書き込む。
    /// </summary>
    /// <param name="self">対象ファイルのFileInfo</param>
    /// <param name="bytes">書き込むバイト列</param>
    /// <param name="cancelToken">キャンセルトークン</param>
    public static async ValueTask WriteAllBytesAsync(this FileInfo self, byte[] bytes, CancellationToken cancelToken = default)
    {
        if (self == null) throw new ArgumentNullException(nameof(self));
        await File.WriteAllBytesAsync(self.FullName, bytes, cancelToken).ConfigureAwait(false);
        self.Refresh();
    }

    /// <summary>
    /// ファイル内容が指定のテキストとなるように書き込む。
    /// </summary>
    /// <param name="self">対象ファイルのFileInfo</param>
    /// <param name="contents">書き込むテキスト</param>
    /// <param name="cancelToken">キャンセルトークン</param>
    public static async ValueTask WriteAllTextAsync(this FileInfo self, string contents, CancellationToken cancelToken = default)
    {
        if (self == null) throw new ArgumentNullException(nameof(self));
        await File.WriteAllTextAsync(self.FullName, contents, cancelToken).ConfigureAwait(false);
        self.Refresh();
    }

    /// <summary>
    /// ファイル内容が指定のテキストとなるように書き込む。
    /// </summary>
    /// <param name="self">対象ファイルのFileInfo</param>
    /// <param name="contents">書き込むテキスト</param>
    /// <param name="encoding">書き込むテキストをエンコードするテキストエンコーディング</param>
    /// <param name="cancelToken">キャンセルトークン</param>
    public static async ValueTask WriteAllTextAsync(this FileInfo self, string contents, Encoding encoding, CancellationToken cancelToken = default)
    {
        if (self == null) throw new ArgumentNullException(nameof(self));
        await File.WriteAllTextAsync(self.FullName, contents, encoding, cancelToken).ConfigureAwait(false);
        self.Refresh();
    }

    /// <summary>
    /// ファイル内容が指定のテキスト行となるように書き込む。
    /// </summary>
    /// <param name="self">対象ファイルのFileInfo</param>
    /// <param name="contents">書き込むテキスト行</param>
    /// <param name="cancelToken">キャンセルトークン</param>
    public static async ValueTask WriteAllLinesAsync(this FileInfo self, IEnumerable<string> contents, CancellationToken cancelToken = default)
    {
        if (self == null) throw new ArgumentNullException(nameof(self));
        await File.WriteAllLinesAsync(self.FullName, contents, cancelToken);
        self.Refresh();
    }

    /// <summary>
    /// ファイル内容が指定のテキスト行となるように書き込む。
    /// </summary>
    /// <param name="self">対象ファイルのFileInfo</param>
    /// <param name="contents">書き込むテキスト行</param>
    /// <param name="encoding">書き込むテキストをエンコードするテキストエンコーディング</param>
    /// <param name="cancelToken">キャンセルトークン</param>
    public static async ValueTask WriteAllLinesAsync(this FileInfo self, IEnumerable<string> contents, Encoding encoding, CancellationToken cancelToken = default)
    {
        if (self == null) throw new ArgumentNullException(nameof(self));
        await File.WriteAllLinesAsync(self.FullName, contents, encoding).ConfigureAwait(false);
        self.Refresh();
    }
#endif
}
