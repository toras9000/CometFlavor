using System;

namespace CometFlavor.Unicode.Extensions.Text;

#if NET5_0_OR_GREATER

/// <summary>
/// EastAsianWidthによる幅計算情報
/// </summary>
/// <remarks>
/// このクラスはUnicode EastAsianWidth プロパティ情報に沿って文字幅の評価を行う場合の情報を表す。
/// EastAsianWidth については以下を参照。
/// ・Unicode Standard Annex #11 - East Asian Width  (http://www.unicode.org/reports/tr11/)
/// 
/// このクラスが示す文字幅情報は仮想的な値であり、いかなる単位系とも関連性を持たない。
/// 用途としては、例えば日本語で関連的に扱われている全角と半角に対して2:1のサイズ関係性で幅(長さ)を求めたい、などを想定する。
/// </remarks>
public class EawMeasure : IEquatable<EawMeasure>
{
    // 構築
    #region コンストラクタ
    /// <summary>文字幅を評価する値を指定するコンストラクタ</summary>
    /// <param name="normal">Narrow，Halfwidth，Neutral キャラクタの仮想幅値</param>
    /// <param name="wide">Wide，Fullwidth キャラクタの仮想幅値</param>
    /// <param name="ambiguous">Ambiguous キャラクタの仮想幅値</param>
    public EawMeasure(int normal, int wide, int ambiguous)
    {
        if (normal < 0) throw new ArgumentException(nameof(normal));
        if (wide < 0) throw new ArgumentException(nameof(wide));
        if (ambiguous < 0) throw new ArgumentException(nameof(ambiguous));

        this.Narrow = normal;
        this.Halfwidth = normal;
        this.Wide = wide;
        this.Fullwidth = wide;
        this.Neutral = normal;
        this.Ambiguous = ambiguous;
    }

    /// <summary>文字幅を評価する値を指定するコンストラクタ</summary>
    /// <param name="narrow">Narrow，Halfwidth キャラクタの仮想幅値</param>
    /// <param name="wide">Wide，Fullwidth キャラクタの仮想幅値</param>
    /// <param name="neutral">Neutral キャラクタの仮想幅値</param>
    /// <param name="ambiguous">Ambiguous キャラクタの仮想幅値</param>
    public EawMeasure(int narrow, int wide, int neutral, int ambiguous)
    {
        if (narrow < 0) throw new ArgumentException(nameof(narrow));
        if (wide < 0) throw new ArgumentException(nameof(wide));
        if (neutral < 0) throw new ArgumentException(nameof(neutral));
        if (ambiguous < 0) throw new ArgumentException(nameof(ambiguous));

        this.Narrow = narrow;
        this.Halfwidth = narrow;
        this.Wide = wide;
        this.Fullwidth = wide;
        this.Neutral = neutral;
        this.Ambiguous = ambiguous;
    }

    /// <summary>文字幅を評価する値を指定するコンストラクタ</summary>
    /// <param name="narrow">Narrow キャラクタの仮想幅値</param>
    /// <param name="wide">Wide キャラクタの仮想幅値</param>
    /// <param name="half">Halfwidth キャラクタの仮想幅値</param>
    /// <param name="full">Fullwidth キャラクタの仮想幅値</param>
    /// <param name="neutral">Neutral キャラクタの仮想幅値</param>
    /// <param name="ambiguous">Ambiguous キャラクタの仮想幅値</param>
    public EawMeasure(int narrow, int wide, int half, int full, int neutral, int ambiguous)
    {
        if (narrow < 0) throw new ArgumentException(nameof(narrow));
        if (wide < 0) throw new ArgumentException(nameof(wide));
        if (half < 0) throw new ArgumentException(nameof(half));
        if (full < 0) throw new ArgumentException(nameof(full));
        if (neutral < 0) throw new ArgumentException(nameof(neutral));
        if (ambiguous < 0) throw new ArgumentException(nameof(ambiguous));

        this.Narrow = narrow;
        this.Wide = wide;
        this.Halfwidth = half;
        this.Fullwidth = full;
        this.Neutral = neutral;
        this.Ambiguous = ambiguous;
    }
    #endregion

    // 公開プロパティ
    #region 幅情報
    /// <summary>Narrow キャラクタの仮想幅値</summary>
    public int Narrow { get; }

    /// <summary>Wide キャラクタの仮想幅値</summary>
    public int Wide { get; }

    /// <summary>Halfwidth キャラクタの仮想幅値</summary>
    public int Halfwidth { get; }

    /// <summary>Fullwidth キャラクタの仮想幅値</summary>
    public int Fullwidth { get; }

    /// <summary>Neutral キャラクタの仮想幅値</summary>
    public int Neutral { get; }

    /// <summary>Ambiguous キャラクタの仮想幅値</summary>
    public int Ambiguous { get; }
    #endregion

    // 公開メソッド
    #region 計算
    /// <summary>EastAsianWidth に対する幅値を取得する。</summary>
    /// <param name="eaw">EastAsianWidth 種別</param>
    /// <returns>
    /// EastAsianWidth に対する幅値。
    /// 無効な種別の場合は Neutral として評価する。
    /// </returns>
    public int GetWidth(EastAsianWidth eaw) => eaw switch
    {
        EastAsianWidth.Full => this.Fullwidth,
        EastAsianWidth.Wide => this.Wide,
        EastAsianWidth.Half => this.Halfwidth,
        EastAsianWidth.Narrow => this.Narrow,
        EastAsianWidth.Ambiguous => this.Ambiguous,
        EastAsianWidth.Neutral => this.Neutral,
        _ => this.Neutral,
    };
    #endregion

    #region IEquatable インターフェース
    /// <inheritdoc />
    public bool Equals(EawMeasure? other)
    {
        if (other == null) return false;

        return this.Narrow == other.Narrow
            && this.Wide == other.Wide
            && this.Halfwidth == other.Halfwidth
            && this.Fullwidth == other.Fullwidth
            && this.Neutral == other.Neutral
            && this.Ambiguous == other.Ambiguous;
    }
    #endregion

    #region インフラストラクチャ
    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
        return Equals(obj as EawMeasure);
    }

    /// <inheritdoc />
    public override int GetHashCode()
    {
        return HashCode.Combine(this.Narrow, this.Wide, this.Halfwidth, this.Fullwidth, this.Neutral, this.Ambiguous);
    }
    #endregion
}
#endif
