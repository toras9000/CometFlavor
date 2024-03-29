﻿using System;
using System.Collections.ObjectModel;

namespace CometFlavor.Extensions.Collection;

/// <summary>
/// 配列 に対する拡張メソッド
/// </summary>
public static class ArrayExtensions
{
    /// <summary>配列のラッパー読み取り専用コレクションを作成する。</summary>
    /// <typeparam name="T">要素の型</typeparam>
    /// <param name="self">対象配列</param>
    /// <returns>読み取り専用コレクション</returns>
    public static ReadOnlyCollection<T> AsReadOnly<T>(this T[] self)
    {
        return Array.AsReadOnly(self);
    }
}
