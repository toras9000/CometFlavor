﻿using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Text;

namespace CometFlavor.Extensions.Text;

/// <summary>
/// string に対する拡張メソッド
/// </summary>
public static class StringExtensions
{
    /// <summary>文字列がnullや空であるかを判定する。</summary>
    /// <param name="self">対象文字列</param>
    /// <returns>nullや空ならば true</returns>
    public static bool IsEmpty([NotNullWhen(false)] this string? self)
        => string.IsNullOrEmpty(self);

    /// <summary>文字列がnullや空以外であるかを判定する。</summary>
    /// <param name="self">対象文字列</param>
    /// <returns>nullや空以外であれば true</returns>
    public static bool IsNotEmpty([NotNullWhen(true)] this string? self)
        => !string.IsNullOrEmpty(self);

    /// <summary>文字列がnullや空白文字であるかを判定する</summary>
    /// <param name="self">対象文字列</param>
    /// <returns>nullや空白文字ならば true</returns>
    public static bool IsWhite([NotNullWhen(false)] this string? self)
        => string.IsNullOrWhiteSpace(self);

    /// <summary>文字列がnullや空白文字以外であるかを判定する</summary>
    /// <param name="self">対象文字列</param>
    /// <returns>nullや空白文字以外ならば true</returns>
    public static bool IsNotWhite([NotNullWhen(true)] this string? self)
        => !string.IsNullOrWhiteSpace(self);

    /// <summary>文字列がnullや空であれば代替文字列を返却する。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="alt">代替文字列</param>
    /// <returns>nullや空ならば代替文字列、それ以外ならば元の文字列</returns>
    public static string WhenEmpty(this string? self, string alt)
        => string.IsNullOrEmpty(self) ? alt : self!;

    /// <summary>文字列がnullや空であれば代替文字列を返却する。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="alt">代替文字列取得デリゲート</param>
    /// <returns>nullや空ならば代替文字列、それ以外ならば元の文字列</returns>
    public static string WhenEmpty(this string? self, Func<string> alt)
        => string.IsNullOrEmpty(self) ? alt() : self!;

    /// <summary>文字列がnullや空白文字であれば代替文字列を返却する。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="alt">代替文字列</param>
    /// <returns>nullや空白文字ならば代替文字列、それ以外ならば元の文字列</returns>
    public static string WhenWhite(this string? self, string alt)
        => string.IsNullOrWhiteSpace(self) ? alt : self!;

    /// <summary>文字列がnullや空白文字であれば代替文字列を返却する。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="alt">代替文字列取得デリゲート</param>
    /// <returns>nullや空白文字ならば代替文字列、それ以外ならば元の文字列</returns>
    public static string WhenWhite(this string? self, Func<string> alt)
        => string.IsNullOrWhiteSpace(self) ? alt() : self!;

    /// <summary>文字列の最初の行を取得する。</summary>
    /// <param name="self">対象文字列</param>
    /// <returns>最初の行文字列</returns>
    [return: NotNullIfNotNull(nameof(self))]
    public static string? FirstLine(this string? self)
    {
        if (string.IsNullOrEmpty(self)) return self;

        // 改行位置を検索
        var breakIdx = self.IndexOfAny(LineBreakChars);
        if (breakIdx < 0)
        {
            // 改行がない場合はそのままを返却
            return self;
        }

        // 改行前までを切り出し
        return self.Substring(0, breakIdx);
    }

    /// <summary>文字列の最後の行を取得する。</summary>
    /// <param name="self">対象文字列</param>
    /// <returns>最後の行文字列</returns>
    [return: NotNullIfNotNull(nameof(self))]
    public static string? LastLine(this string? self)
    {
        if (string.IsNullOrEmpty(self)) return self;

        // 最終改行位置を検索
        var breakIdx = self.LastIndexOfAny(LineBreakChars);
        if (breakIdx < 0)
        {
            // 改行がない場合はそのままを返却
            return self;
        }

        // 最終改行の後ろを返却
        return self.Substring(breakIdx + 1);
    }

    /// <summary>文字列の行を列挙する。</summary>
    /// <param name="self">対象文字列</param>
    /// <returns>テキスト行シーケンス</returns>
    public static IEnumerable<string> AsTextLines(this string self)
    {
        // パラメータチェック
        if (self == null) throw new ArgumentNullException(nameof(self));

        // テキスト行を列挙
        var idx = 0;
        while (idx < self.Length)
        {
            // 改行を検索
            var pos = self.IndexOfAny(LineBreakChars, idx);
            if (pos < 0) break;

            // 行を列挙
            yield return self.Substring(idx, pos - idx);

            // 次の位置へ。CRLFの場合は1つの改行として扱う
            idx = pos + 1;
            if (idx < self.Length && self[idx - 1] == '\r' && self[idx] == '\n')
            {
                idx++;
            }
        }

        // 最期の部分を列挙
        yield return self.Substring(idx);
    }

    /// <summary>特定文字の前部分文字列を取得する</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="marker">検索文字</param>
    /// <param name="defaultEmpty">検索文字が見つからない場合に空を返すか否か</param>
    /// <returns>
    /// 検索文字が存在する場合はそれより前の文字列。
    /// 見つからない場合はパラメータ指定により文字列全体または空文字列。
    /// </returns>
    [return: NotNullIfNotNull(nameof(self))]
    public static string? BeforeAt(this string? self, char marker, bool defaultEmpty = false)
    {
        if (self != null)
        {
            var idx = self.IndexOf(marker);
            if (0 <= idx) return self.Substring(0, idx);
        }
        return defaultEmpty ? "" : self;
    }

    /// <summary>特定文字列の前部分文字列を取得する</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="marker">検索文字列</param>
    /// <param name="defaultEmpty">検索文字列が見つからない場合に空を返すか否か</param>
    /// <returns>
    /// 検索文字列が存在する場合はそれより前の文字列。
    /// 見つからない場合はパラメータ指定により文字列全体または空文字列。
    /// </returns>
    [return: NotNullIfNotNull(nameof(self))]
    public static string? BeforeAt(this string? self, string marker, bool defaultEmpty = false)
    {
        if (self != null)
        {
            var idx = self.IndexOf(marker);
            if (0 <= idx) return self.Substring(0, idx);
        }
        return defaultEmpty ? "" : self;
    }

    /// <summary>特定文字の後部分文字列を取得する</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="marker">検索文字</param>
    /// <param name="defaultEmpty">検索文字が見つからない場合に空を返すか否か</param>
    /// <returns>
    /// 検索文字が存在する場合はそれより後の文字列。
    /// 見つからない場合はパラメータ指定により文字列全体または空文字列。
    /// </returns>
    [return: NotNullIfNotNull(nameof(self))]
    public static string? AfterAt(this string? self, char marker, bool defaultEmpty = false)
    {
        if (self != null)
        {
            var idx = self.IndexOf(marker);
            if (0 <= idx) return self.Substring(idx + 1);
        }
        return defaultEmpty ? "" : self;
    }

    /// <summary>特定文字列の後部分文字列を取得する</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="marker">検索文字列</param>
    /// <param name="defaultEmpty">検索文字列が見つからない場合に空を返すか否か</param>
    /// <returns>
    /// 検索文字列が存在する場合はそれより後の文字列。
    /// 見つからない場合はパラメータ指定により文字列全体または空文字列。
    /// </returns>
    [return: NotNullIfNotNull(nameof(self))]
    public static string? AfterAt(this string? self, string marker, bool defaultEmpty = false)
    {
        if (self != null)
        {
            var idx = self.IndexOf(marker);
            if (0 <= idx) return self.Substring(idx + marker.Length);
        }
        return defaultEmpty ? "" : self;
    }

    /// <summary>特定文字まで部分文字列を取得する</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="marker">検索文字</param>
    /// <param name="defaultEmpty">検索文字が見つからない場合に空を返すか否か</param>
    /// <returns>
    /// 検索文字が存在する場合は検索文字までの文字列。
    /// 見つからない場合はパラメータ指定により文字列全体または空文字列。
    /// </returns>
    [return: NotNullIfNotNull(nameof(self))]
    public static string? TakeTo(this string? self, char marker, bool defaultEmpty = false)
    {
        if (self != null)
        {
            var idx = self.IndexOf(marker);
            if (0 <= idx) return self.Substring(0, idx + 1);
        }
        return defaultEmpty ? "" : self;
    }

    /// <summary>特定文字列までの部分文字列を取得する</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="marker">検索文字列</param>
    /// <param name="defaultEmpty">検索文字列が見つからない場合に空を返すか否か</param>
    /// <returns>
    /// 検索文字列が存在する場合は検索文字列までの文字列。
    /// 見つからない場合はパラメータ指定により文字列全体または空文字列。
    /// </returns>
    [return: NotNullIfNotNull(nameof(self))]
    public static string? TakeTo(this string? self, string marker, bool defaultEmpty = false)
    {
        if (self != null)
        {
            var idx = self.IndexOf(marker);
            if (0 <= idx) return self.Substring(0, idx + marker.Length);
        }
        return defaultEmpty ? "" : self;
    }

    /// <summary>特定文字からの部分文字列を取得する</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="marker">検索文字</param>
    /// <param name="defaultEmpty">検索文字が見つからない場合に空を返すか否か</param>
    /// <returns>
    /// 検索文字が存在する場合は検索文字からの文字列。
    /// 見つからない場合はパラメータ指定により文字列全体または空文字列。
    /// </returns>
    [return: NotNullIfNotNull(nameof(self))]
    public static string? TakeFrom(this string? self, char marker, bool defaultEmpty = false)
    {
        if (self != null)
        {
            var idx = self.IndexOf(marker);
            if (0 <= idx) return self.Substring(idx);
        }
        return defaultEmpty ? "" : self;
    }

    /// <summary>特定文字列からの部分文字列を取得する</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="marker">検索文字列</param>
    /// <param name="defaultEmpty">検索文字列が見つからない場合に空を返すか否か</param>
    /// <returns>
    /// 検索文字列が存在する場合は検索文字列からの文字列。
    /// 見つからない場合はパラメータ指定により文字列全体または空文字列。
    /// </returns>
    [return: NotNullIfNotNull(nameof(self))]
    public static string? TakeFrom(this string? self, string marker, bool defaultEmpty = false)
    {
        if (self != null)
        {
            var idx = self.IndexOf(marker);
            if (0 <= idx) return self.Substring(idx);
        }
        return defaultEmpty ? "" : self;
    }

    /// <summary>文字列のシーケンスからnull/空を取り除く。</summary>
    /// <param name="self">文字列のシーケンス</param>
    /// <returns>null/空以外のシーケンス</returns>
    public static IEnumerable<string> DropEmpty(this IEnumerable<string?> self)
        => self.Where(s => !string.IsNullOrEmpty(s))!;

    /// <summary>文字列のシーケンスからnull/空白文字列を取り除く。</summary>
    /// <param name="self">文字列のシーケンス</param>
    /// <returns>null/空白文字列以外のシーケンス</returns>
    public static IEnumerable<string> DropWhite(this IEnumerable<string?> self)
        => self.Where(s => !string.IsNullOrWhiteSpace(s))!;

    /// <summary>文字列を連結する。</summary>
    /// <param name="self">文字列のシーケンス</param>
    /// <param name="separator">連結する文字間に差し込む文字列</param>
    /// <returns>連結された文字列</returns>
    public static string JoinString(this IEnumerable<string?> self, string? separator = null)
        => string.Join(separator, self);

    /// <summary>指定の文字列がnull/空でない場合に装飾を付与する。</summary>
    /// <param name="self">元になる文字列</param>
    /// <param name="format">文字列を装飾する書式。埋め込み位置0のプレースホルダ({{0}})が含まれる必要がある。</param>
    /// <returns>装飾された文字列。元がnullまたは空の場合はそのまま返却。</returns>
    [return: NotNullIfNotNull(nameof(self))]
    public static string? Decorate(this string? self, string format)
        => string.IsNullOrEmpty(self) ? self : string.Format(format, self);

    /// <summary>指定の文字列がnull/空でない場合に装飾を付与する。</summary>
    /// <param name="self">元になる文字列</param>
    /// <param name="decorator">文字列を装飾するデリゲート</param>
    /// <returns>装飾された文字列。元がnullまたは空の場合はそのまま返却。</returns>
    [return: NotNullIfNotNull(nameof(self))]
    public static string? Decorate(this string? self, Func<string, string> decorator)
        => string.IsNullOrEmpty(self) ? self : decorator == null ? self : decorator.Invoke(self);

    /// <summary>文字列をクォートする。</summary>
    /// <param name="text">対象文字列。nullの場合は空文字列と同じ扱いとする。</param>
    /// <param name="quote">クォートキャラクタ</param>
    /// <param name="escape">対象文字列中のクォートキャラクタをエスケープするキャラクタ</param>
    /// <returns>クォートされたキャラクタ</returns>
    public static string Quote(this string? text, char quote = '"', char? escape = null)
    {
        if (string.IsNullOrEmpty(text)) return new string(quote, 2);
        var esc = escape ?? quote;
        var buffer = new StringBuilder(text.Length + 10);
        buffer.Append(quote);
        foreach (var c in text)
        {
            if (c == quote) buffer.Append(esc);
            buffer.Append(c);
        }
        buffer.Append(quote);

        return buffer.ToString();
    }

#if NET5_0_OR_GREATER
    /// <summary>文字列をアンクォートする。</summary>
    /// <param name="self">対象文字列。</param>
    /// <param name="quotes">クォートキャラクタ候補。空の場合はダブル/シングルクォートキャラクタを候補とする。</param>
    /// <param name="escape">クォートキャラクタをエスケープしているキャラクタ。指定がない場合はクォートキャラクタ2つで</param>
    /// <returns>アンクォートされた文字列</returns>
    [return: NotNullIfNotNull(nameof(self))]
    public static string? Unquote(this string self, ReadOnlySpan<char> quotes = default, char? escape = null)
    {
        // nullやクォート分の幅がなければそのまま返す。
        if (self == null || self.Length < 2) return self;

        // クォートキャラクタ候補。指定があればそれを、無ければダブル・シングルクォートキャラクタを候補とする。
        var candidates = (quotes.Length == 0) ? stackalloc char[] { '"', '\'', } : quotes;

        // 最初の文字がクォート文字候補のいずれかであるかを判別
        // クォート文字で始まらない場合は元の文字列を返す
        if (candidates.IndexOf(self[0]) < 0) return self;

        // 両端がクォートキャラクタであるかを判定。
        var quoteChar = self[0];
        if (quoteChar != self[^1]) return self;

        // 前後クォートを除去した部分を取得
        var core = self.AsSpan(1, self.Length - 2);

        // クォートキャラクタのエスケープ表現
        var escaped = (stackalloc char[] { escape ?? quoteChar, quoteChar, });

        // エスケープ表見があるか検索
        var escIdx = core.IndexOf(escaped);
        if (escIdx < 0) return core.ToString();

        // エスケープ解除した文字列を作成
        var buffer = new StringBuilder(core.Length);
        var scan = core;
        do
        {
            // エスケープ文字位置前までとエスケープ解除文字を追加
            buffer.Append(scan[0..escIdx]);
            buffer.Append(quoteChar);

            // 次の位置からエスケープ表現を検索
            scan = scan[(escIdx + 2)..];
            escIdx = scan.IndexOf(escaped);
        }
        while (0 <= escIdx);

        // 残りの部分を追加
        buffer.Append(scan);

        return buffer.ToString();
    }

    /// <summary>文字列のテキスト要素を列挙する。</summary>
    /// <param name="self">対象文字列</param>
    /// <returns>テキスト要素シーケンス</returns>
    public static IEnumerable<string> AsTextElements(this string self)
    {
        // パラメータチェック
        if (self == null) throw new ArgumentNullException(nameof(self));

        // テキスト要素を列挙
        var elementer = StringInfo.GetTextElementEnumerator(self);
        while (elementer.MoveNext())
        {
            yield return (string)elementer.Current;
        }
    }

    /// <summary>文字列のテキスト要素数を取得する。</summary>
    /// <param name="self">対象文字列</param>
    /// <returns>テキスト要素数</returns>
    public static int TextElementCount(this string self)
    {
        // パラメータチェック
        if (self == null) throw new ArgumentNullException(nameof(self));

        return StringInfo.ParseCombiningCharacters(self).Length;
    }

    /// <summary>文字列の先頭から指定された長さの文字要素を切り出す。</summary>
    /// <param name="self">元になる文字列。nullまたは空の場合は元のインスタンスをそのまま返却する。</param>
    /// <param name="count">切り出す文字要素の長さ。</param>
    /// <returns>切り出された文字列</returns>
    [return: NotNullIfNotNull(nameof(self))]
    public static string? CutLeftElements(this string? self, int count)
    {
        // パラメータチェック
        if (count < 0) throw new ArgumentException(nameof(count));
        if (string.IsNullOrEmpty(self)) return self;
        if (count == 0) return string.Empty;

        // 切り出し文字列の構築用
        var builder = new StringBuilder(capacity: count);

        // 文字要素を列挙しながら指定要素数まで蓄積
        var taked = 0;
        var elementer = StringInfo.GetTextElementEnumerator(self);
        while (taked < count && elementer.MoveNext())
        {
            builder.Append(elementer.Current);
            taked++;
        }

        // 切り出した先頭文字列を返却
        return builder.ToString();
    }

    /// <summary>文字列の末尾にある指定された長さの文字要素を切り出す。</summary>
    /// <param name="self">元になる文字列。nullまたは空の場合は元のインスタンスをそのまま返却する。</param>
    /// <param name="count">切り出す文字要素の長さ。</param>
    /// <returns>切り出された文字列</returns>
    [return: NotNullIfNotNull(nameof(self))]
    public static string? CutRightElements(this string? self, int count)
    {
        // パラメータチェック
        if (count < 0) throw new ArgumentException(nameof(count));
        if (string.IsNullOrEmpty(self)) return self;
        if (count == 0) return string.Empty;

        // 最後の文字を蓄積するバッファ
        var buffer = new Queue<object>(capacity: count);

        // 文字要素を列挙しながら指定数までの最後の要素を蓄積
        var elementer = StringInfo.GetTextElementEnumerator(self);
        while (elementer.MoveNext())
        {
            // 既に切り出し要素数蓄積済みならば1つ除去
            if (count <= buffer.Count)
            {
                buffer.Dequeue();
            }

            // 後方の要素を蓄積
            buffer.Enqueue(elementer.Current);
        }

        // 蓄積された末尾文字列を返却
        return string.Concat(buffer);
    }

    /// <summary>
    /// 文字列を指定の長さに省略する。
    /// このメソッドでは string の Length 基準での長さ制限となる。
    /// </summary>
    /// <param name="self">元の文字列</param>
    /// <param name="length">制限する文字列の長さ</param>
    /// <param name="marker">省略時に付与するマーカ文字列</param>
    /// <returns>必要に応じて省略した文字列。</returns>
    public static string EllipsisByLength(this string self, int length, string? marker = null)
    {
        // パラメータチェック
        if (self == null) throw new ArgumentNullException(nameof(self));
        if (length < 0) throw new ArgumentException(nameof(length));

        // マーカーが指定の長さを超えている場合は矛盾するのでパラメータ指定が正しくない。
        // 省略されずマーカーが使用されない場合もあり得るが、パラメータ length と marker の関係性が正しくないのであれば揺れなく異常検出できようにしている。
        var markerLen = marker?.Length ?? 0;
        if (length < markerLen) throw new ArgumentException();

        // 元の文字列が指定の長さに収まる場合はそのまま返却
        if (self.Length <= length)
        {
            return self;
        }

        // 省略文字列として切り出す長さを算出。マーカを付与するのでその分を除いた長さ。
        var takeLen = length - markerLen;

        // 書記素クラスタを分割しないよう、テキスト要素を認識して切り出す。
        var builder = new StringBuilder();
        var elementer = StringInfo.GetTextElementEnumerator(self);
        while (0 < takeLen && elementer.MoveNext())
        {
            // テキスト要素を追加すると切り詰め長を超えてしまうようであればここで終わり
            var element = (string)elementer.Current;
            if (takeLen < element.Length)
            {
                break;
            }
            // 切り出し文字列に追加
            builder.Append(element);
            takeLen -= element.Length;
        }

        // マーカーを追加
        builder.Append(marker);

        // 省略した文字列を返却
        return builder.ToString();
    }

    /// <summary>
    /// 文字列を指定の長さに省略する。
    /// このメソッドでは string の 文字要素 基準での長さ制限となる。
    /// </summary>
    /// <param name="self">元の文字列</param>
    /// <param name="count">制限する文字列の文字要素数</param>
    /// <param name="marker">省略時に付与するマーカ文字列</param>
    /// <returns>必要に応じて省略した文字列。</returns>
    public static string EllipsisByElements(this string self, int count, string? marker = null)
    {
        // パラメータチェック
        if (self == null) throw new ArgumentNullException(nameof(self));
        if (count < 0) throw new ArgumentException(nameof(count));

        // マーカーが指定の長さを超えている場合は矛盾するのでパラメータ指定が正しくない。
        // 省略されずマーカーが使用されない場合もあり得るが、パラメータ width と marker の関係性が正しくないのであれば揺れなく異常検出できようにしている。
        var markerCount = marker?.TextElementCount() ?? 0;
        if (count < markerCount) throw new ArgumentException(nameof(marker));

        // 明かな状況を処理
        if (string.IsNullOrEmpty(self)) return self;
        if (count == 0) return string.Empty;

        // 省略文字列として切り出す長さを算出。マーカを付与するのでその分を除いた長さ。
        var ellipsisCount = count - markerCount;

        // 書記素クラスタペアを分割しないよう、テキスト要素を認識して切り出す。
        var builder = new StringBuilder();
        var elementer = StringInfo.GetTextElementEnumerator(self);
        var sumCount = 0;       // 切り出し合計幅
        var ellipsedLen = 0;    // 省略が発生する場合に採用する文字列長さ
        while (elementer.MoveNext())
        {
            // 追加すると切り詰め長を超えないかをチェック
            if (count < (sumCount + 1))
            {
                // 超える場合は省略時の長さ＋マーカーの内容で切り出し終了
                builder.Length = ellipsedLen;
                builder.Append(marker);
                break;
            }
            else
            {
                // 切り出し文字列に追加
                builder.Append(elementer.Current);

                // 省略が発生した場合に使う文字列Lengthを把握する。
                // 合計幅が省略時の許容長を越えない限りはその長さを利用できる。
                sumCount++;
                if (sumCount <= ellipsisCount)
                {
                    ellipsedLen = builder.Length;
                }
            }
        }

        // 省略した文字列を返却
        return builder.ToString();
    }
#endif

    /// <summary>改行キャラクタ配列</summary>
    private static readonly char[] LineBreakChars = new[] { '\r', '\n', };
}
