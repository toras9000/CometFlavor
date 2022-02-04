using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace CometFlavor.Extensions.Text;

/// <summary>
/// string に対する拡張メソッド
/// </summary>
public static class StringExtensions
{
    /// <summary>
    /// 文字列の最初の行を取得する。
    /// </summary>
    /// <param name="self">対象文字列</param>
    /// <returns>最初の行文字列</returns>
    public static string FirstLine(this string self)
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

    /// <summary>
    /// 文字列を連結する。
    /// </summary>
    /// <param name="self">文字列のシーケンス</param>
    /// <param name="separator">連結する文字間に差し込む文字列</param>
    /// <returns></returns>
    public static string JoinString(this IEnumerable<string> self, string? separator = null)
    {
        return string.Join(separator, self);
    }

    /// <summary>
    /// 文字列を装飾する。
    /// 元の文字列が null または 空の場合はなにもしない。
    /// </summary>
    /// <param name="self">元になる文字列</param>
    /// <param name="format">文字列を装飾する書式。埋め込み位置0のプレースホルダ({{0}})が含まれる必要がある。</param>
    /// <returns>装飾された文字列。元がnullまたは空の場合はそのまま返却。</returns>
    public static string Decorate(this string self, string format)
    {
        if (string.IsNullOrEmpty(self)) return self;
        return string.Format(format, self);
    }

    /// <summary>
    /// 文字列を装飾する。
    /// 元の文字列が null または 空の場合はなにもしない。
    /// </summary>
    /// <param name="self">元になる文字列</param>
    /// <param name="decorator">文字列を装飾するデリゲート</param>
    /// <returns>装飾された文字列。元がnullまたは空の場合はそのまま返却。</returns>
    public static string Decorate(this string self, Func<string, string> decorator)
    {
        if (string.IsNullOrEmpty(self)) return self;
        if (decorator == null) return self;
        return decorator(self);
    }

#if NET5_0_OR_GREATER
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

        // 元の文字列が指定の長さに収まる場合はそのまま返却
        if (self.Length <= length)
        {
            return self;
        }

        // マーカーが指定の長さを超えている場合は矛盾するのでパラメータ指定が正しくない。
        var markerLen = marker?.Length ?? 0;
        if (length < markerLen) throw new ArgumentException();

        // 省略文字列として切り出す長さを算出。マーカを付与するのでその分を除いた長さ。
        var takeLen = length - markerLen;

        // サロゲートペアを分割しないよう、テキスト要素を認識して切り出す。
        var builder = new StringBuilder();
        var elementer = StringInfo.GetTextElementEnumerator(self);
        while (elementer.MoveNext())
        {
            // テキスト要素を追加すると切り詰め長を超えてしまうようであればここで終わり
            var element = (string)elementer.Current;
            if (takeLen < (builder.Length + element.Length))
            {
                break;
            }
            // 切り出し文字列に追加
            builder.Append(element);
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

        // マーカーの長さを取得。
        var markerLen = 0;
        if (marker != null)
        {
            var checker = StringInfo.GetTextElementEnumerator(marker);
            while (checker.MoveNext())
            {
                markerLen++;
            }
        }

        // マーカーが指定の長さを超えている場合は矛盾するのでパラメータ指定が正しくない。
        if (count < markerLen) throw new ArgumentException();

        // 省略文字列として切り出す長さを算出。マーカを付与するのでその分を除いた長さ。
        var elipsedSrcCount = count - markerLen;

        // サロゲートペアを分割しないよう、テキスト要素を認識して切り出す。
        var elipsisTextLen = default(int?);
        var takeCount = 0;
        var builder = new StringBuilder();
        var elementer = StringInfo.GetTextElementEnumerator(self);
        while (elementer.MoveNext())
        {
            // 制限長を超える長さになるかをチェック
            if (count < (takeCount + 1))
            {
                // 省略時のソース長さを保持している場合はその長さに切り詰め
                if (elipsisTextLen.HasValue)
                {
                    builder.Length = elipsisTextLen.Value;
                }
                // マーカ追加して切り出しを打ち切り。
                builder.Append(marker);
                break;
            }

            // 「省略を行う場合にソーステキストから取り出す長さ」になった場合の文字列Lengthを取得しておく。
            // 続く文字要素を追加して制限に収まるかもしれないし、収まらずに省略するかもしれないので。
            if (!elipsisTextLen.HasValue && elipsedSrcCount <= takeCount)
            {
                elipsisTextLen = builder.Length;
            }

            // 切り出し文字列に追加
            builder.Append(elementer.Current);
            takeCount++;
        }

        // 省略した文字列を返却
        return builder.ToString();
    }
#endif

    /// <summary>改行キャラクタ配列</summary>
    private static readonly char[] LineBreakChars = new[] { '\r', '\n', };
}
