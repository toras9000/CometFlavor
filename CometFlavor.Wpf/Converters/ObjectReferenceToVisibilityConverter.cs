using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace CometFlavor.Wpf.Converters;

/// <summary>
/// オブジェクトの有無(nullか否か)をVisibility列挙子に変換する。
/// </summary>
[ValueConversion(typeof(object), typeof(Visibility))]
public class ObjectReferenceToVisibilityConverter : IValueConverter
{
    // 公開プロパティ
    #region 動作設定
    /// <summary>true に設定すると非表示時に Visibility.Hidden にする。デフォルトでは Visibility.Collapsed となる。</summary>
    public bool InvisibleToHidden { get; set; }

    /// <summary>trueに設定するとオブジェクト有無の解釈を逆にする。</summary>
    public bool ReverseLogic { get; set; }
    #endregion

    // 公開メソッド
    #region 変換
    /// <summary>値を変換する</summary>
    /// <param name="value">変換元の値</param>
    /// <param name="targetType">対象の型</param>
    /// <param name="parameter">コンバータパラメータ</param>
    /// <param name="culture">変換時のカルチャ</param>
    /// <returns>変換できた場合は表示列挙子。変換できない場合は DependencyProperty.UnsetValue。</returns>
    public object Convert(object? value, Type? targetType, object? parameter, CultureInfo? culture)
    {
        // オブジェクトの有無
        var existance = (value != null);

        // 表示状態
        var visibility = this.ReverseLogic ? !existance : existance;

        // 表示ありの場合は表示値を返却
        if (visibility)
        {
            return Visibility.Visible;
        }

        // 表示なしであれば設定に応じた非表示状態
        return this.InvisibleToHidden ? Visibility.Hidden : Visibility.Collapsed;
    }

    /// <summary>値を逆変換する。(非サポート)</summary>
    /// <param name="value"></param>
    /// <param name="targetType">対象の型</param>
    /// <param name="parameter">コンバータパラメータ</param>
    /// <param name="culture"></param>
    /// <returns>常に DependencyProperty.UnsetValue。</returns>
    public object ConvertBack(object? value, Type? targetType, object? parameter, CultureInfo? culture)
    {
        // 対称性のある逆変換は出来ない。
        return DependencyProperty.UnsetValue;
    }
    #endregion
}
