using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows;
using System.Windows.Data;

namespace CometFlavor.Wpf.Converters
{
    /// <summary>
    /// bool値とVisibility列挙子に変換する。
    /// </summary>
    public class BooleanToVisibilityConverter : IValueConverter
    {
        // 公開プロパティ
        #region 動作設定
        /// <summary>true に設定すると非表示時に Visibility.Hidden にする。デフォルトでは Visibility.Collapsed となる。</summary>
        public bool InvisibleToHidden { get; set; }

        /// <summary>trueに設定するとbool論理を逆に解釈する。</summary>
        public bool ReverseLogic { get; set; }
        #endregion

        // 公開メソッド
        #region 変換
        /// <summary>
        /// 値を変換する
        /// </summary>
        /// <param name="value">変換元の値</param>
        /// <param name="targetType">対象の型</param>
        /// <param name="parameter">コンバータパラメータ</param>
        /// <param name="culture">変換時のカルチャ</param>
        /// <returns>変換できた場合は表示列挙子。変換できない場合は DependencyProperty.UnsetValue。</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // 値がboolであるかを判定
            if (value is bool logical)
            {
                // 設定に応じて論理値を解釈
                var visibility = this.ReverseLogic ? !logical : logical;

                // 表示ありの場合は表示値を返却
                if (visibility)
                {
                    return Visibility.Visible;
                }

                // 表示なしであれば設定に応じた非表示状態
                return this.InvisibleToHidden ? Visibility.Hidden : Visibility.Collapsed;
            }

            // 変換できない値の場合は変換不可とする
            return DependencyProperty.UnsetValue;
        }

        /// <summary>
        /// 値を逆変換する
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType">対象の型</param>
        /// <param name="parameter">コンバータパラメータ</param>
        /// <param name="culture"></param>
        /// <returns>変換できた場合はbool値。変換できない場合は DependencyProperty.UnsetValue。</returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // 値がVisibilityであるかを判定
            if (value is Visibility visibility)
            {
                // 値に応じて変換
                switch (visibility)
                {
                    case Visibility.Visible:
                        return this.ReverseLogic ? false : true;

                    case Visibility.Hidden:
                    case Visibility.Collapsed:
                        return this.ReverseLogic ? true : false;

                    default:
                        break;
                }
            }

            // 変換できない値の場合は変換不可とする
            return DependencyProperty.UnsetValue;
        }
        #endregion
    }
}
