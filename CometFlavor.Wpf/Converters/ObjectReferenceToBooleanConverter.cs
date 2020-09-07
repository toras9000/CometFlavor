using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows;
using System.Windows.Data;

namespace CometFlavor.Wpf.Converters
{
    /// <summary>
    /// オブジェクトの有無をbool値に変換する。
    /// </summary>
    public class ObjectReferenceToBooleanConverter : IValueConverter
    {
        // 公開プロパティ
        #region 動作設定
        /// <summary>trueに設定するとオブジェクト有無の解釈を逆にする。</summary>
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
            // オブジェクトの有無
            var existance = (value != null);

            // 論理解釈の設定値に応じた値を返却
            return this.ReverseLogic ? !existance : existance;
        }

        /// <summary>
        /// 値を逆変換する。(非サポート)
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType">対象の型</param>
        /// <param name="parameter">コンバータパラメータ</param>
        /// <param name="culture"></param>
        /// <returns>変換できた場合はbool値。変換できない場合は DependencyProperty.UnsetValue。</returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // 対称性のある逆変換は出来ない。
            return DependencyProperty.UnsetValue;
        }
        #endregion
    }
}
