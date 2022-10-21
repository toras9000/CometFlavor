using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using CometFlavor.Unicode.Materials;

namespace CometFlavor.Unicode.Extensions.Text;

/// <summary>
/// string に対する拡張メソッド
/// </summary>
public static class StringExtensions
{
#if NET5_0_OR_GREATER
    /// <summary>EastAsianWidth による文字列幅の算出</summary>
    /// <remarks>
    /// 算出される文字列幅は表示幅というわけではない事に注意。(表示幅はフォントやレンダリングシステムによって決まるもの。)
    /// 単純にUnicodeの定義情報を利用し、幅の値に評価する。
    /// 例えば U+D200B (ZERO WIDTH SPACE) は通常は表示上で目に見える幅を持たない友われるが、
    /// Unicode 仕様上 EastAsianWidth が Neutral として定義されるコードポイントであるため、
    /// このメソッドでは Neutral の幅値で評価される。
    /// また、このメソッドは書記素クラスタを1文字として幅算出を行う。
    /// (書記素分割を StringInfo.GetTextElementEnumerator() に頼るため、正しく分割できる.NET5以上を対象としている。)
    /// 書記素クラスタの幅評価はクラスタを構成するコードポイントの中で最も幅が大きい値を採用する。
    /// </remarks>
    /// <param name="self">対象文字列</param>
    /// <param name="measure">文字の種別毎の仮想的な幅を指定する。</param>
    /// <returns>文字列全体の幅値</returns>
    public static int EvaluateEaw(this string? self, EawMeasure measure)
    {
        // パラメータチェック
        if (measure == null) throw new ArgumentNullException(nameof(measure));
        if (string.IsNullOrEmpty(self)) return 0;

        // 文字要素を列挙しながら指定要素数まで蓄積
        var totalWidth = 0;
        var elementer = StringInfo.GetTextElementEnumerator(self);
        while (elementer.MoveNext())
        {
            // 文字要素の構成コードポイントから EastAsianWidth を調べる。
            // 最も大きな幅で評価される値を採用する。
            var element = (string)elementer.Current;
            var elemWidth = element.EnumerateRunes()
                .Select(rune => UnicodeEastAsianWidthV14.GetEastAsianWidth(rune.Value))
                .Select(eaw => measure.GetWidth(eaw))
                .Max();

            // 合計幅算出
            totalWidth += elemWidth;
        }

        return totalWidth;
    }

    /// <summary>文字列の先頭から指定された幅の文字要素を切り出す。</summary>
    /// <param name="self">元になる文字列</param>
    /// <param name="width">
    /// 切り出す文字要素の合計幅。
    /// この幅値の意味は仮想的なものであり、measure引数で指定された情報を基準とした値となる。
    /// </param>
    /// <param name="measure">文字の種別毎の仮想的な幅を指定する。</param>
    /// <returns>切り出された文字列</returns>
    public static string? CutLeftEaw(this string? self, int width, EawMeasure measure)
    {
        // パラメータチェック
        if (width < 0) throw new ArgumentException(nameof(width));
        if (measure == null) throw new ArgumentNullException(nameof(measure));
        if (string.IsNullOrEmpty(self)) return self;
        if (width == 0) return string.Empty;

        // 切り出し文字列の構築用
        var builder = new StringBuilder();

        // 文字要素を列挙しながら指定要素数まで蓄積
        var takeWidth = 0;
        var elementer = StringInfo.GetTextElementEnumerator(self);
        while (elementer.MoveNext())
        {
            // 文字要素の構成コードポイントから EastAsianWidth を調べる。
            // 最も大きな幅で評価される値を採用する。
            var element = (string)elementer.Current;
            var elemWidth = element.EnumerateRunes()
                .Select(rune => UnicodeEastAsianWidthV14.GetEastAsianWidth(rune.Value))
                .Select(eaw => measure.GetWidth(eaw))
                .Max();

            // 指定された幅を超える場合はここで打ち切り。
            if (width < (takeWidth + elemWidth))
            {
                break;
            }

            // 切り出し対象として追加
            builder.Append(element);
            takeWidth += elemWidth;
        }

        // 切り出した先頭文字列を返却
        return builder.ToString();
    }

    /// <summary>文字列の末尾にある指定された幅の文字要素を切り出す。</summary>
    /// <param name="self">元になる文字列</param>
    /// <param name="width">
    /// 切り出す文字要素の合計幅。
    /// この幅値の意味は仮想的なものであり、measure引数で指定された情報を基準とした値となる。
    /// </param>
    /// <param name="measure">文字の種別毎の仮想的な幅を指定する。</param>
    /// <returns>切り出された文字列</returns>
    public static string? CutRightEaw(this string? self, int width, EawMeasure measure)
    {
        // パラメータチェック
        if (width < 0) throw new ArgumentException(nameof(width));
        if (measure == null) throw new ArgumentNullException(nameof(measure));
        if (string.IsNullOrEmpty(self)) return self;
        if (width == 0) return string.Empty;

        // 最後の文字を蓄積するバッファ
        var buffer = new Queue<(int width, string elem)>();

        // 文字要素を列挙しながら指定数までの最後の要素を蓄積
        var takeWidth = 0;
        var elementer = StringInfo.GetTextElementEnumerator(self);
        while (elementer.MoveNext())
        {
            // 文字要素の構成コードポイントから EastAsianWidth を調べる。
            // 最も大きな幅で評価される値を採用する。
            var element = (string)elementer.Current;
            var elemWidth = element.EnumerateRunes()
                .Select(rune => UnicodeEastAsianWidthV14.GetEastAsianWidth(rune.Value))
                .Select(eaw => measure.GetWidth(eaw))
                .Max();

            // 文字要素を保持
            takeWidth += elemWidth;
            buffer.Enqueue((elemWidth, element));

            // この要素で指定された幅を超える場合は最古のものを除去
            while (width < takeWidth && 0 < buffer.Count)
            {
                var removed = buffer.Dequeue();
                takeWidth -= removed.width;
            }
        }

        // 蓄積された末尾文字列を返却
        return string.Concat(buffer.Select(q => q.elem));
    }

    /// <summary>文字列を指定の幅に省略する。</summary>
    /// <param name="self">元の文字列</param>
    /// <param name="width">制限する文字列の幅</param>
    /// <param name="measure">文字の種別毎の仮想的な幅を指定する。</param>
    /// <param name="marker">省略時に付与するマーカ文字列</param>
    /// <returns>必要に応じて省略した文字列。</returns>
    public static string EllipsisByWidth(this string self, int width, EawMeasure measure, string? marker = null)
    {
        // パラメータチェック
        if (self == null) throw new ArgumentNullException(nameof(self));
        if (width < 0) throw new ArgumentException(nameof(width));
        if (measure == null) throw new ArgumentNullException(nameof(measure));

        // マーカーが指定の長さを超えている場合は矛盾するのでパラメータ指定が正しくない。
        // 省略されずマーカーが使用されない場合もあり得るが、パラメータ width と marker の関係性が正しくないのであれば揺れなく異常検出できようにしている。
        var markerWidth = marker?.EvaluateEaw(measure) ?? 0;
        if (width < markerWidth) throw new ArgumentException(nameof(marker));

        // 明かな状況を処理
        if (string.IsNullOrEmpty(self)) return self;
        if (width == 0) return string.Empty;

        // 省略文字列として切り出す長さを算出。マーカを付与するのでその分を除いた長さ。
        var ellipsisWidth = width - markerWidth;

        // サロゲートペアを分割しないよう、テキスト要素を認識して切り出す。
        var builder = new StringBuilder();
        var elementer = StringInfo.GetTextElementEnumerator(self);
        var sumWidth = 0;       // 切り出し合計幅
        var ellipsedLen = 0;    // 省略が発生する場合に採用する文字列長さ
        while (elementer.MoveNext())
        {
            // テキスト要素の取得と幅算出
            var element = (string)elementer.Current;
            var elemWidth = element.EvaluateEaw(measure);

            // 追加すると切り詰め長を超えないかをチェック
            if (width < (sumWidth + elemWidth))
            {
                // 超える場合は省略時の長さ＋マーカーの内容で切り出し終了
                builder.Length = ellipsedLen;
                builder.Append(marker);
                break;
            }
            else
            {
                // 切り出し文字列に追加
                builder.Append(element);

                // 省略が発生した場合に使う文字列Lengthを把握する。
                // 合計幅が省略時の許容長を越えない限りはその長さを利用できる。
                sumWidth += elemWidth;
                if (sumWidth <= ellipsisWidth)
                {
                    ellipsedLen = builder.Length;
                }
            }
        }

        // 省略した文字列を返却
        return builder.ToString();
    }
#endif
}
