﻿using System;
using System.Text;

namespace CometFlavor.Extensions.Text;

/// <summary>
/// StringBuilder に対する拡張メソッド
/// </summary>
public static class StringBuilderExtensions
{
    /// <summary>文字列がnullや空であるかを判定する。</summary>
    /// <param name="self">対象文字列</param>
    /// <returns>nullや空ならば true</returns>
    public static bool IsEmpty(this StringBuilder self) => self.Length == 0;

    /// <summary>文字列がnullや空以外であるかを判定する。</summary>
    /// <param name="self">対象文字列</param>
    /// <returns>nullや空以外であれば true</returns>
    public static bool IsNotEmpty(this StringBuilder self) => !self.IsEmpty();

#if NETCOREAPP2_1_OR_GREATER
    /// <summary>文字列がnullや空白文字であるかを判定する</summary>
    /// <param name="self">対象文字列</param>
    /// <returns>nullや空白文字ならば true</returns>
    public static bool IsWhite(this StringBuilder self)
    {
        if (self == null) throw new ArgumentNullException(nameof(self));

        foreach (var chunk in self.GetChunks())
        {
            if (!chunk.Span.IsWhiteSpace()) return false;
        }

        return true;
    }

    /// <summary>文字列がnullや空白文字以外であるかを判定する</summary>
    /// <param name="self">対象文字列</param>
    /// <returns>nullや空白文字以外ならば true</returns>
    public static bool IsNotWhite(this StringBuilder self) => !self.IsWhite();

    /// <summary>文字列が指定の文字列で始まるかを判定する。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="value">一致判定文字列</param>
    /// <returns>一致するか否か</returns>
    public static bool StartsWith(this StringBuilder self, string value)
    {
        // パラメータの検証 
        if (self == null) throw new ArgumentNullException(nameof(self));
        if (value == null) throw new ArgumentNullException(nameof(value));
        if (value.Length <= 0) throw new ArgumentException($"Invalid {nameof(value)}");

        // 一致判定対象。スパンでずらしていく。
        var target = value.AsSpan();

        // 各チャンクを順に参照し先頭から比較対象に一致するか検証していく、
        foreach (var chunk in self.GetChunks())
        {
            // 一致判定
            if (chunk.Length < target.Length)
            {
                if (!chunk.Span.SequenceEqual(target[..chunk.Length])) return false;
                target = target[chunk.Length..];
            }
            else
            {
                if (!chunk.Span[..target.Length].SequenceEqual(target)) return false;
                target = target[target.Length..];
            }
            // 不一致なく判定文字列の全長を比較し終わったら終了
            if (target.IsEmpty) return true;
        }

        return false;
    }

    /// <summary>文字列が指定の文字列で始まるかを判定する。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="value">一致判定文字列</param>
    /// <param name="comparison">文字列比較方法</param>
    /// <returns>一致するか否か</returns>
    public static bool StartsWith(this StringBuilder self, string value, StringComparison comparison)
    {
        // パラメータの検証 
        if (self == null) throw new ArgumentNullException(nameof(self));
        if (value == null) throw new ArgumentNullException(nameof(value));
        if (value.Length <= 0) throw new ArgumentException($"Invalid {nameof(value)}");

        // 一致判定対象。スパンでずらしていく。
        var target = value.AsSpan();

        // 各チャンクを順に参照し先頭から比較対象に一致するか検証していく、
        foreach (var chunk in self.GetChunks())
        {
            // 一致判定
            if (chunk.Length < target.Length)
            {
                if (!chunk.Span.Equals(target[..chunk.Length], comparison)) return false;
                target = target[chunk.Length..];
            }
            else
            {
                if (!chunk.Span[..target.Length].Equals(target, comparison)) return false;
                target = target[target.Length..];
            }
            // 不一致なく判定文字列の全長を比較し終わったら終了
            if (target.IsEmpty) return true;
        }

        return false;
    }

    /// <summary>文字列が指定の文字列で終端するかを判定する。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="value">一致判定文字列</param>
    /// <returns>一致するか否か</returns>
    public static bool EndsWith(this StringBuilder self, string value)
    {
        // パラメータの検証 
        if (self == null) throw new ArgumentNullException(nameof(self));
        if (value == null) throw new ArgumentNullException(nameof(value));
        if (value.Length <= 0) throw new ArgumentException($"Invalid {nameof(value)}");

        // 比較文字列より小さい場合は不一致に確定
        if (self.Length < value.Length) return false;

        // 比較対象の長さによって比較方法を選択
        if (value.Length <= 100)
        {
            // 比較文字列が小さければスタック上で比較
            Span<char> tail = stackalloc char[value.Length];
            self.CopyTo(self.Length - tail.Length, tail, tail.Length);
            return tail.SequenceEqual(value.AsSpan());
        }
        else
        {
            // それ以外は文字列に切り出して比較
            var tail = self.ToString(self.Length - value.Length, value.Length);
            return tail == value;
        }
    }

    /// <summary>文字列が指定の文字列で終端するかを判定する。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="value">一致判定文字列</param>
    /// <param name="comparison">文字列比較方法</param>
    /// <returns>一致するか否か</returns>
    public static bool EndsWith(this StringBuilder self, string value, StringComparison comparison)
    {
        // パラメータの検証 
        if (self == null) throw new ArgumentNullException(nameof(self));
        if (value == null) throw new ArgumentNullException(nameof(value));
        if (value.Length <= 0) throw new ArgumentException($"Invalid {nameof(value)}");

        // 比較文字列より小さい場合は不一致に確定
        if (self.Length < value.Length) return false;

        // 比較対象の長さによって比較方法を選択
        if (value.Length <= 100)
        {
            // 比較文字列が小さければスタック上で比較
            Span<char> tail = stackalloc char[value.Length];
            self.CopyTo(self.Length - tail.Length, tail, tail.Length);
            return ((ReadOnlySpan<char>)tail).Equals(value, comparison);
        }
        else
        {
            // それ以外は文字列に切り出して比較
            var tail = self.ToString(self.Length - value.Length, value.Length);
            return tail.Equals(value, comparison);
        }
    }
#endif

    /// <summary>文字列の先頭に指定の幅まで文字を埋める</summary>
    /// <param name="self">対象の文字列ビルダ</param>
    /// <param name="totalWidth">埋め込み後の最大文字数。対象がこの文字数以上であれば埋め込みは行われない。</param>
    /// <param name="paddingChar">埋め込み文字</param>
    /// <returns>対象インスタンスをそのまま返却する</returns>
    public static StringBuilder PadLeft(this StringBuilder self, int totalWidth, char paddingChar = ' ')
    {
        // パラメータの検証 
        if (self == null) throw new ArgumentNullException(nameof(self));
        if (totalWidth < 0) throw new ArgumentException($"Invalid {nameof(totalWidth)}");

        // 元々指定サイズ以上の長さであれば追加しない
        if (totalWidth <= self.Length) return self;

        // 必要なサイズを算出
        var padCount = totalWidth - self.Length;
#if NETCOREAPP2_1_OR_GREATER
        if (padCount < 128)
        {
            // 必要数が小さい場合はスタック上で。
            var padding = (stackalloc char[padCount]);
            padding.Fill(paddingChar);
            return self.Insert(0, padding);
        }
        else
#endif
        {
            // 一定以上の長さが必要ならば普通に。
            return self.Insert(0, new string(paddingChar, padCount));
        }
    }

    /// <summary>文字列の末尾に指定の幅まで文字を埋める</summary>
    /// <param name="self">対象の文字列ビルダ</param>
    /// <param name="totalWidth">埋め込み後の最大文字数。対象がこの文字数以上であれば埋め込みは行われない。</param>
    /// <param name="paddingChar">埋め込み文字</param>
    /// <returns>対象インスタンスをそのまま返却する</returns>
    public static StringBuilder PadRight(this StringBuilder self, int totalWidth, char paddingChar = ' ')
    {
        // パラメータの検証 
        if (self == null) throw new ArgumentNullException(nameof(self));
        if (totalWidth < 0) throw new ArgumentException($"Invalid {nameof(totalWidth)}");

        // 元々指定サイズ以上の長さであれば追加しない
        if (totalWidth < self.Length) return self;

        // 必要なサイズ分のキャラクタを追加
        var padCount = totalWidth - self.Length;
        return self.Append(paddingChar, padCount);
    }

}

