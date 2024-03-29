﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CometFlavor.Extensions.IO;

/// <summary>
/// FileInfo に対する拡張メソッド
/// </summary>
public static class FileInfoExtensions
{
    #region Name
    /// <summary>拡張子を除いたファイル名を取得する。</summary>
    /// <param name="self">対象ファイルのFileInfo</param>
    /// <returns>拡張子を除いたファイル名</returns>
    public static string GetNameWithoutExtension(this FileInfo self)
    {
        if (self == null) throw new ArgumentNullException(nameof(self));
        return Path.GetFileNameWithoutExtension(self.Name);
    }

    /// <summary>拡張子を取得する。</summary>
    /// <param name="self">対象ファイルのFileInfo</param>
    /// <returns>拡張子</returns>
    public static string GetExtension(this FileInfo self)
    {
        if (self == null) throw new ArgumentNullException(nameof(self));
        return Path.GetExtension(self.Name);
    }

    /// <summary>指定した拡張子ファイルを示すFileInfoを取得する。</summary>
    /// <param name="self">対象ファイルのFileInfo</param>
    /// <param name="extension">拡張子</param>
    /// <returns> 指定した拡張子ファイルを示すFileInfo</returns>
    public static FileInfo GetAnotherExtension(this FileInfo self, string extension)
    {
        if (self == null) throw new ArgumentNullException(nameof(self));
        var path = Path.ChangeExtension(self.FullName, extension);
        return new FileInfo(path);
    }
    #endregion

    #region Read
    /// <summary>ファイル内容の全バイト列を読み出す。</summary>
    /// <param name="self">対象ファイルのFileInfo</param>
    /// <returns>ファイルから読みだしたバイト列</returns>
    public static byte[] ReadAllBytes(this FileInfo self)
    {
        if (self == null) throw new ArgumentNullException(nameof(self));
        return File.ReadAllBytes(self.FullName);
    }

    /// <summary>ファイル内容の全テキストを読み出す。</summary>
    /// <param name="self">対象ファイルのFileInfo</param>
    /// <returns>ファイルから読みだした全テキスト</returns>
    public static string ReadAllText(this FileInfo self)
    {
        if (self == null) throw new ArgumentNullException(nameof(self));
        return File.ReadAllText(self.FullName);
    }

    /// <summary>ファイル内容の全テキストを読み出す。</summary>
    /// <param name="self">対象ファイルのFileInfo</param>
    /// <param name="encoding">ファイル内容をデコードするテキストエンコーディング</param>
    /// <returns>ファイルから読みだした全テキスト</returns>
    public static string ReadAllText(this FileInfo self, Encoding encoding)
    {
        if (self == null) throw new ArgumentNullException(nameof(self));
        return File.ReadAllText(self.FullName, encoding);
    }

    /// <summary>ファイル内容の全テキスト行を読み出す。</summary>
    /// <param name="self">対象ファイルのFileInfo</param>
    /// <returns>ファイルから読みだした全テキスト行</returns>
    public static string[] ReadAllLines(this FileInfo self)
    {
        if (self == null) throw new ArgumentNullException(nameof(self));
        return File.ReadAllLines(self.FullName);
    }

    /// <summary>ファイル内容の全テキスト行を読み出す。</summary>
    /// <param name="self">対象ファイルのFileInfo</param>
    /// <param name="encoding">ファイル内容をデコードするテキストエンコーディング</param>
    /// <returns>ファイルから読みだした全テキスト行</returns>
    public static string[] ReadAllLines(this FileInfo self, Encoding encoding)
    {
        if (self == null) throw new ArgumentNullException(nameof(self));
        return File.ReadAllLines(self.FullName, encoding);
    }

    /// <summary>ファイル内容のテキスト行を読み出す。</summary>
    /// <param name="self">対象ファイルのFileInfo</param>
    /// <returns>ファイルから読みだしたテキスト行</returns>
    public static IEnumerable<string> ReadLines(this FileInfo self)
    {
        if (self == null) throw new ArgumentNullException(nameof(self));
        return File.ReadLines(self.FullName);
    }

    /// <summary>ファイル内容の全テキスト行を読み出す。</summary>
    /// <param name="self">対象ファイルのFileInfo</param>
    /// <param name="encoding">ファイル内容をデコードするテキストエンコーディング</param>
    /// <returns>ファイルから読みだしたテキスト行</returns>
    public static IEnumerable<string> ReadLines(this FileInfo self, Encoding encoding)
    {
        if (self == null) throw new ArgumentNullException(nameof(self));
        return File.ReadLines(self.FullName, encoding);
    }

#if NETCOREAPP2_0_OR_GREATER
    /// <summary>ファイル内容の全バイト列を読み出す。</summary>
    /// <param name="self">対象ファイルのFileInfo</param>
    /// <param name="cancelToken">キャンセルトークン</param>
    /// <returns>ファイルから読みだしたバイト列</returns>
    public static Task<byte[]> ReadAllBytesAsync(this FileInfo self, CancellationToken cancelToken = default)
    {
        if (self == null) throw new ArgumentNullException(nameof(self));
        return File.ReadAllBytesAsync(self.FullName, cancelToken);
    }

    /// <summary>ファイル内容の全テキストを読み出す。</summary>
    /// <param name="self">対象ファイルのFileInfo</param>
    /// <param name="cancelToken">キャンセルトークン</param>
    /// <returns>ファイルから読みだした全テキスト</returns>
    public static Task<string> ReadAllTextAsync(this FileInfo self, CancellationToken cancelToken = default)
    {
        if (self == null) throw new ArgumentNullException(nameof(self));
        return File.ReadAllTextAsync(self.FullName, cancelToken);
    }

    /// <summary>ファイル内容の全テキストを読み出す。</summary>
    /// <param name="self">対象ファイルのFileInfo</param>
    /// <param name="encoding">ファイル内容をデコードするテキストエンコーディング</param>
    /// <param name="cancelToken">キャンセルトークン</param>
    /// <returns>ファイルから読みだした全テキスト</returns>
    public static Task<string> ReadAllTextAsync(this FileInfo self, Encoding encoding, CancellationToken cancelToken = default)
    {
        if (self == null) throw new ArgumentNullException(nameof(self));
        return File.ReadAllTextAsync(self.FullName, encoding, cancelToken);
    }

    /// <summary>ファイル内容の全テキスト行を読み出す。</summary>
    /// <param name="self">対象ファイルのFileInfo</param>
    /// <param name="cancelToken">キャンセルトークン</param>
    /// <returns>ファイルから読みだした全テキスト行</returns>
    public static Task<string[]> ReadAllLinesAsync(this FileInfo self, CancellationToken cancelToken = default)
    {
        if (self == null) throw new ArgumentNullException(nameof(self));
        return File.ReadAllLinesAsync(self.FullName, cancelToken);
    }

    /// <summary>ファイル内容の全テキスト行を読み出す。</summary>
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

#if NET6_0_OR_GREATER
    /// <summary>ファイル内容をテキストで読み取るリーダーを生成する。</summary>
    /// <param name="self">対象ファイルのFileInfo</param>
    /// <param name="encoding">ファイル内容をデコードするテキストエンコーディング</param>
    /// <param name="detectBom">BOMを検出してエンコーディングを決定するか否か</param>
    /// <param name="options">元になるファイルストリームを開くオプション</param>
    /// <returns>ストリームリーダー</returns>
    public static StreamReader CreateTextReader(this FileInfo self, bool detectBom = true, FileStreamOptions? options = null, Encoding? encoding = null)
    {
        return (options == null) ? new StreamReader(self.FullName, encoding ?? Encoding.UTF8, detectBom)
                                 : new StreamReader(self.FullName, encoding ?? Encoding.UTF8, detectBom, options);
    }
#endif
    #endregion

    #region Write
    /// <summary>ファイル内容が指定のバイト列となるように書き込む。</summary>
    /// <param name="self">対象ファイルのFileInfo</param>
    /// <param name="bytes">書き込むバイト列</param>
    public static void WriteAllBytes(this FileInfo self, byte[] bytes)
    {
        if (self == null) throw new ArgumentNullException(nameof(self));
        File.WriteAllBytes(self.FullName, bytes);
        self.Refresh();
    }

    /// <summary>ファイル内容が指定のテキストとなるように書き込む。</summary>
    /// <param name="self">対象ファイルのFileInfo</param>
    /// <param name="contents">書き込むテキスト</param>
    public static void WriteAllText(this FileInfo self, string contents)
    {
        if (self == null) throw new ArgumentNullException(nameof(self));
        File.WriteAllText(self.FullName, contents);
        self.Refresh();
    }

    /// <summary>ファイル内容が指定のテキストとなるように書き込む。</summary>
    /// <param name="self">対象ファイルのFileInfo</param>
    /// <param name="contents">書き込むテキスト</param>
    /// <param name="encoding">書き込むテキストをエンコードするテキストエンコーディング</param>
    public static void WriteAllText(this FileInfo self, string contents, Encoding encoding)
    {
        if (self == null) throw new ArgumentNullException(nameof(self));
        File.WriteAllText(self.FullName, contents, encoding);
        self.Refresh();
    }

    /// <summary>ファイル内容が指定のテキスト行となるように書き込む。</summary>
    /// <param name="self">対象ファイルのFileInfo</param>
    /// <param name="contents">書き込むテキスト行</param>
    public static void WriteAllLines(this FileInfo self, IEnumerable<string> contents)
    {
        if (self == null) throw new ArgumentNullException(nameof(self));
        File.WriteAllLines(self.FullName, contents);
        self.Refresh();
    }

    /// <summary>ファイル内容が指定のテキスト行となるように書き込む。</summary>
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
    /// <summary>ファイル内容が指定のバイト列となるように書き込む。</summary>
    /// <param name="self">対象ファイルのFileInfo</param>
    /// <param name="bytes">書き込むバイト列</param>
    /// <param name="cancelToken">キャンセルトークン</param>
    public static async ValueTask WriteAllBytesAsync(this FileInfo self, byte[] bytes, CancellationToken cancelToken = default)
    {
        if (self == null) throw new ArgumentNullException(nameof(self));
        await File.WriteAllBytesAsync(self.FullName, bytes, cancelToken).ConfigureAwait(false);
        self.Refresh();
    }

    /// <summary>ファイル内容が指定のテキストとなるように書き込む。</summary>
    /// <param name="self">対象ファイルのFileInfo</param>
    /// <param name="contents">書き込むテキスト</param>
    /// <param name="cancelToken">キャンセルトークン</param>
    public static async ValueTask WriteAllTextAsync(this FileInfo self, string contents, CancellationToken cancelToken = default)
    {
        if (self == null) throw new ArgumentNullException(nameof(self));
        await File.WriteAllTextAsync(self.FullName, contents, cancelToken).ConfigureAwait(false);
        self.Refresh();
    }

    /// <summary>ファイル内容が指定のテキストとなるように書き込む。</summary>
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

    /// <summary>ファイル内容が指定のテキスト行となるように書き込む。</summary>
    /// <param name="self">対象ファイルのFileInfo</param>
    /// <param name="contents">書き込むテキスト行</param>
    /// <param name="cancelToken">キャンセルトークン</param>
    public static async ValueTask WriteAllLinesAsync(this FileInfo self, IEnumerable<string> contents, CancellationToken cancelToken = default)
    {
        if (self == null) throw new ArgumentNullException(nameof(self));
        await File.WriteAllLinesAsync(self.FullName, contents, cancelToken);
        self.Refresh();
    }

    /// <summary>ファイル内容が指定のテキスト行となるように書き込む。</summary>
    /// <param name="self">対象ファイルのFileInfo</param>
    /// <param name="contents">書き込むテキスト行</param>
    /// <param name="encoding">書き込むテキストをエンコードするテキストエンコーディング</param>
    /// <param name="cancelToken">キャンセルトークン</param>
    public static async ValueTask WriteAllLinesAsync(this FileInfo self, IEnumerable<string> contents, Encoding encoding, CancellationToken cancelToken = default)
    {
        if (self == null) throw new ArgumentNullException(nameof(self));
        await File.WriteAllLinesAsync(self.FullName, contents, encoding, cancelToken).ConfigureAwait(false);
        self.Refresh();
    }
#endif

#if NET6_0_OR_GREATER
    /// <summary>ファイル内容が指定のバイト列となるように書き込む。</summary>
    /// <param name="self">対象ファイルのFileInfo</param>
    /// <param name="bytes">書き込むバイト列</param>
    /// <param name="options">ファイルストリームを開くオプション。Access プロパティは無視する。</param>
    public static void WriteAllBytes(this FileInfo self, ReadOnlySpan<byte> bytes, FileStreamOptions? options = null)
    {
        if (self == null) throw new ArgumentNullException(nameof(self));
        using var stream = new FileStream(self.FullName, createStreamWriteOptions(options));
        stream.Write(bytes);
        self.Refresh();
    }

    /// <summary>ファイル内容が指定のバイト列となるように書き込む。</summary>
    /// <param name="self">対象ファイルのFileInfo</param>
    /// <param name="bytes">書き込むバイト列</param>
    /// <param name="options">ファイルストリームを開くオプション。Access プロパティは無視する。</param>
    /// <param name="cancelToken">キャンセルトークン</param>
    public static async ValueTask WriteAllBytesAsync(this FileInfo self, ReadOnlyMemory<byte> bytes, FileStreamOptions? options = null, CancellationToken cancelToken = default)
    {
        if (self == null) throw new ArgumentNullException(nameof(self));
        using var stream = new FileStream(self.FullName, createStreamWriteOptions(options));
        await stream.WriteAsync(bytes, cancelToken);
        self.Refresh();
    }

    /// <summary>ファイル内容が指定のテキストとなるように書き込む。</summary>
    /// <param name="self">対象ファイルのFileInfo</param>
    /// <param name="contents">書き込むテキスト</param>
    /// <param name="options">ファイルストリームを開くオプション。Access プロパティは無視する。</param>
    /// <param name="encoding">書き込むテキストをエンコードするテキストエンコーディング</param>
    public static void WriteAllText(this FileInfo self, ReadOnlySpan<char> contents, FileStreamOptions? options = null, Encoding? encoding = null)
    {
        if (self == null) throw new ArgumentNullException(nameof(self));
        using var writer = self.CreateTextWriter(createStreamWriteOptions(options), encoding);
        writer.Write(contents);
        self.Refresh();
    }

    /// <summary>ファイル内容が指定のバイト列となるように書き込む。</summary>
    /// <param name="self">対象ファイルのFileInfo</param>
    /// <param name="contents">書き込むテキスト</param>
    /// <param name="options">ファイルストリームを開くオプション。Access プロパティは無視する。</param>
    /// <param name="encoding">書き込むテキストをエンコードするテキストエンコーディング</param>
    /// <param name="cancelToken">キャンセルトークン</param>
    public static async ValueTask WriteAllTextAsync(this FileInfo self, ReadOnlyMemory<char> contents, FileStreamOptions? options = null, Encoding? encoding = null, CancellationToken cancelToken = default)
    {
        if (self == null) throw new ArgumentNullException(nameof(self));
        using var writer = self.CreateTextWriter(createStreamWriteOptions(options), encoding);
        await writer.WriteAsync(contents, cancelToken);
        self.Refresh();
    }

    /// <summary>ファイル内容をテキストで読み取るリーダーを生成する。</summary>
    /// <param name="self">対象ファイルのFileInfo</param>
    /// <param name="encoding">ファイル内容をデコードするテキストエンコーディング</param>
    /// <param name="append">追記するか否か</param>
    /// <returns>ストリームリーダー</returns>
    public static StreamWriter CreateTextWriter(this FileInfo self, bool append = false, Encoding? encoding = null)
    {
        return new StreamWriter(self.FullName, append, encoding ?? Encoding.UTF8);
    }

    /// <summary>ファイル内容をテキストで読み取るリーダーを生成する。</summary>
    /// <param name="self">対象ファイルのFileInfo</param>
    /// <param name="encoding">ファイル内容をデコードするテキストエンコーディング</param>
    /// <param name="options">元になるファイルストリームを開くオプション</param>
    /// <returns>ストリームリーダー</returns>
    public static StreamWriter CreateTextWriter(this FileInfo self, FileStreamOptions options, Encoding? encoding = null)
    {
        return new StreamWriter(self.FullName, encoding ?? Encoding.UTF8, options);
    }
#endif
    #endregion

    #region FileSystem
    /// <summary>ファイルまでのディレクトリを作成する。</summary>
    /// <param name="self">対象ファイル情報</param>
    /// <returns>元のファイル情報</returns>
    public static FileInfo WithDirectoryCreate(this FileInfo self)
    {
        if (self == null) throw new ArgumentNullException(nameof(self));
        self.Directory?.WithCreate();
        return self;
    }
    #endregion

    #region Path
    /// <summary>ファイルパスの構成セグメントを取得する。</summary>
    /// <param name="self">対象ファイルのFileInfo</param>
    /// <returns>パス構成セグメントのリスト</returns>
    public static IList<string> GetPathSegments(this FileInfo self)
    {
        if (self == null) throw new ArgumentNullException(nameof(self));

        // まずファイル名を処理
        var segments = new List<string>(10);
        segments.Add(self.Name);

        // 構成ディレクトリを取得
        var part = self.Directory;
        while (part != null)
        {
            segments.Add(part.Name);
            part = part.Parent;
        }

        // リスト内容を逆順にする
        segments.Reverse();

        return segments;
    }

    /// <summary>ファイルが指定のディレクトリの子孫であるかを判定する。</summary>
    /// <remarks></remarks>
    /// <param name="self">対象ファイル</param>
    /// <param name="other">比較するディレクトリ</param>
    /// <returns>指定ディレクトリの子孫であるか否か</returns>
    public static bool IsDescendantOf(this FileInfo self, DirectoryInfo other)
    {
        if (self == null) throw new ArgumentNullException(nameof(self));
        if (other == null) throw new ArgumentNullException(nameof(other));
        if (self.Directory == null) throw new ArgumentException($"{nameof(self)}.{nameof(self.Directory)}");

        // ファイル格納ディレクトリと比較対象のパス階層を取得
        var selfDirSegs = self.Directory.GetPathSegments();
        var otherSegs = other.GetPathSegments();

        // 比較対象のほうが階層が深い場合はその子孫ではありえない。
        if (selfDirSegs.Count < otherSegs.Count) return false;

        // 比較対象が対象ファイルのディレクトリを全て含んでいるかを判定
        for (var i = 0; i < otherSegs.Count; i++)
        {
            if (!string.Equals(selfDirSegs[i], otherSegs[i], StringComparison.OrdinalIgnoreCase)) return false;
        }

        return true;
    }

    /// <summary>指定のディレクトリを起点としたファイルの相対パスを取得する。</summary>
    /// <remarks>
    /// 単純なパス文字列処理であり、リパースポイントなどを解釈することはない。
    /// </remarks>
    /// <param name="self">対象ファイルのFileInfo</param>
    /// <param name="baseDir">基準ディレクトリのDirectoryInfo</param>
    /// <param name="ignoreCase">大文字と小文字を同一視するか否か</param>
    /// <returns>相対パス</returns>
    public static string RelativePathFrom(this FileInfo self, DirectoryInfo baseDir, bool ignoreCase)
    {
        // パラメータチェック
        if (self == null) throw new ArgumentNullException(nameof(self));
        if (baseDir == null) throw new ArgumentNullException(nameof(baseDir));

        return DirectoryInfoExtensions.SegmentsToReletivePath(self.GetPathSegments(), baseDir, ignoreCase);
    }
    #endregion

    // 非公開メソッド
    #region Helper
#if NET6_0_OR_GREATER
    /// <summary>Write向けのストリームオプションを生成する。</summary>
    /// <param name="options">元にするオプション。nullの場合はデフォルトの値とする。</param>
    /// <returns>生成したオプション</returns>
    private static FileStreamOptions createStreamWriteOptions(FileStreamOptions? options = null)
    {
        var writeOpt = new FileStreamOptions();
        writeOpt.Mode = FileMode.Create;
        writeOpt.Access = FileAccess.Write;
        writeOpt.Share = FileShare.Read;
        if (options != null)
        {
            writeOpt.Mode = options.Mode;
            writeOpt.Share = options.Share;
            writeOpt.Options = options.Options;
            writeOpt.PreallocationSize = options.PreallocationSize;
            writeOpt.BufferSize = options.BufferSize;
#if NET7_0_OR_GREATER
            if (!RuntimeInformation.IsOSPlatform(OSPlatform.Windows)) writeOpt.UnixCreateMode = options.UnixCreateMode;
#endif
        }

        return writeOpt;
    }
#endif
    #endregion
}
