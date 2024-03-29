﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace CometFlavor.Extensions.Linq;

/// <summary>
/// IEnumerable{T} に対する拡張メソッド
/// </summary>
public static class EnumerableExtensions
{
    /// <summary>シーケンスのフィルタと条件に適合しない場合のアクションを指定するオペレータ。</summary>
    /// <typeparam name="TSource">シーケンスの要素型</typeparam>
    /// <param name="self">対象シーケンス</param>
    /// <param name="predicate">要素の通過判定処理</param>
    /// <param name="skipped">要素が通過しない場合に呼び出される処理</param>
    /// <returns>フィルタされたシーケンス</returns>
    public static IEnumerable<TSource> WhereElse<TSource>(this IEnumerable<TSource> self, Func<TSource, bool> predicate, Action<TSource> skipped)
    {
        foreach (var item in self)
        {
            if (predicate(item))
            {
                yield return item;
            }
            else
            {
                skipped(item);
            }
        }
    }

    /// <summary>シーケンスインスタンスがnullならば空のシーケンスを代替とする</summary>
    /// <typeparam name="TSource">シーケンスの要素型</typeparam>
    /// <param name="self">対象シーケンス</param>
    /// <returns>元のシーケンスまたは空のシーケンス</returns>
    public static IEnumerable<TSource> CoalesceEmpty<TSource>(this IEnumerable<TSource>? self)
    {
        return self ?? Enumerable.Empty<TSource>();
    }
}
