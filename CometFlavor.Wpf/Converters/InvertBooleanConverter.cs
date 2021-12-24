using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace CometFlavor.Wpf.Converters;

/// <summary>
/// bool値の論理を反転する。
/// </summary>
[ValueConversion(typeof(bool), typeof(bool))]
public class InvertBooleanConverter : IValueConverter
{
    // 公開メソッド
    #region 変換
    /// <summary>
    /// 値を変換する
    /// </summary>
    /// <param name="value">変換元の値</param>
    /// <param name="targetType">対象の型</param>
    /// <param name="parameter">コンバータパラメータ</param>
    /// <param name="culture">変換時のカルチャ</param>
    /// <returns>変換できた場合は結果のbool値。変換できない場合は DependencyProperty.UnsetValue。</returns>
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is bool b)
        {
            return !b;
        }
        return DependencyProperty.UnsetValue;
    }

    /// <summary>
    /// 値を逆変換する
    /// </summary>
    /// <param name="value"></param>
    /// <param name="targetType">対象の型</param>
    /// <param name="parameter">コンバータパラメータ</param>
    /// <param name="culture"></param>
    /// <returns>変換できた場合は結果のbool値。変換できない場合は DependencyProperty.UnsetValue。</returns>
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is bool b)
        {
            return !b;
        }
        return DependencyProperty.UnsetValue;
    }
    #endregion
}
