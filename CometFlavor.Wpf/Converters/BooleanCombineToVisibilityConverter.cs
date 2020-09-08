using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace CometFlavor.Wpf.Converters
{
    /// <summary>
    /// 複数のbool値を結合して表示列挙子に変化する。
    /// </summary>
    public class BooleanCombineToVisibilityConverter : IMultiValueConverter
    {
        // 公開型
        #region 種別定義
        /// <summary>
        /// 結合モード種別
        /// </summary>
        public enum CombineMode
        {
            /// <summary>全て true の場合に結果を Visible とする。それ以外は 非表示値(Collapsed/Hidden) とする。</summary>
            AllTrue,
            /// <summary>全て false の場合に結果を Visible とする。それ以外は 非表示値(Collapsed/Hidden) とする。</summary>
            AllFalse,
            /// <summary>1つでも true があれば結果を Visible とする。それ以外は 非表示値(Collapsed/Hidden) とする。</summary>
            AnyTrue,
            /// <summary>1つでも false があれば結果を Visible とする。それ以外は 非表示値(Collapsed/Hidden) とする。</summary>
            AnyFalse,
        }
        #endregion

        // 公開プロパティ
        #region 動作設定
        /// <summary>bool値の結合モード。デフォルト設定値は CombineMode.AllTrue となる。</summary>
        public CombineMode Mode { get; set; } = CombineMode.AllTrue;

        /// <summary>bool型以外の変換元値を無視するか否か。デフォルト設定値は true となる。</summary>
        /// <remarks>false に設定した場合、非bool型が含まれると変換結果は DependencyProperty.UnsetValue となる。</remarks>
        public bool IgnoreNotBool { get; set; } = false;

        /// <summary>非表示判定時に Visibility.Hidden へ変換するか否か。デフォルト設定値は false となる。</summary>
        /// <remarks>true に設定した場合非表示時に Visibility.Hidden とする。false に設定した場合は Visibility.Collapsed とする。</remarks>
        public bool InvisibleToHidden { get; set; } = false;
        #endregion

        // 公開メソッド
        #region 変換
        /// <summary>
        /// 値を変換する
        /// </summary>
        /// <param name="values">変換元の値</param>
        /// <param name="targetType">対象の型</param>
        /// <param name="parameter">コンバータパラメータ</param>
        /// <param name="culture">変換時のカルチャ</param>
        /// <returns>変換できた場合は結果の表示列挙子。変換できない場合は DependencyProperty.UnsetValue。</returns>
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            // 値がない場合は変換不可
            if (values == null)
            {
                return DependencyProperty.UnsetValue;
            }

            // モード別の判定を行う。
            switch (this.Mode)
            {
                case CombineMode.AllTrue:
                    // すべて true であるかを判定
                    return valuesAll(values, true);

                case CombineMode.AllFalse:
                    // すべて false であるかを判定
                    return valuesAll(values, false);

                case CombineMode.AnyTrue:
                    // 1つでも true があるかを判定
                    return valuesAny(values, true);

                case CombineMode.AnyFalse:
                    // 1つでも false があるかを判定
                    return valuesAny(values, false);

                default:
                    break;
            }

            return DependencyProperty.UnsetValue;
        }

        /// <summary>
        /// 値を逆変換する
        /// </summary>
        /// <param name="value">変換元の値</param>
        /// <param name="targetTypes">対象の型</param>
        /// <param name="parameter">コンバータパラメータ</param>
        /// <param name="culture">変換時のカルチャ</param>
        /// <returns>常に null を返却。</returns>
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            return null;
        }
        #endregion

        // 非公開メソッド
        #region 判定処理
        /// <summary>
        /// 配列要素が全て条件を満たすかを判定する。
        /// </summary>
        /// <param name="values">判定対象配列</param>
        /// <param name="expect">期待する値</param>
        /// <returns>判定結果。全て期待値であれば 表示, 期待値と論理が合わないものがあれば 非表示, 判定不可ならば DependencyProperty.UnsetValue</returns>
        private object valuesAll(object[] values, bool expect)
        {
            var condition = true;
            var exists = false;

            for (var i = 0; i < values.Length; i++)
            {
                // 型判別
                if (values[i] is bool b)
                {
                    // 全て条件に合うかを判定
                    // 非boolを見つけて変換不可とする場合があるので条件不適合でもショートサーキットしずらい
                    condition &= (b == expect);

                    // 有効なbool値を見つけたらフラグを立てる
                    exists = true;
                }
                else if (!this.IgnoreNotBool)
                {
                    // 非boolを無視しない設定ならば変換失敗
                    return DependencyProperty.UnsetValue;
                }
            }

            // 有効な値を見つけている場合は判定結果を返却
            if (exists)
            {
                return condition ? Visibility.Visible : (this.InvisibleToHidden ? Visibility.Hidden : Visibility.Collapsed);
            }

            // 値が無かった(あるいは全て無視した)のならば条件に合わないので変換不可
            return DependencyProperty.UnsetValue;
        }

        /// <summary>
        /// 配列要素に条件を満たすものが含まれるを判定する。
        /// </summary>
        /// <param name="values">判定対象配列</param>
        /// <param name="expect">期待する値</param>
        /// <returns>判定結果。期待値と一致するものがあれば 表示, なければ 非表示, 判定不可ならば DependencyProperty.UnsetValue</returns>
        private object valuesAny(object[] values, bool expect)
        {
            var condition = false;
            var exists = false;

            for (var i = 0; i < values.Length; i++)
            {
                // 型判別
                if (values[i] is bool b)
                {
                    // 条件に合うものがあるかを判定
                    // 非boolを見つけて変換不可とする場合があるので条件適合でもショートサーキットしずらい
                    condition |= (b == expect);

                    // 有効なbool値を見つけたらフラグを立てる
                    exists = true;
                }
                else if (!this.IgnoreNotBool)
                {
                    // 非boolを無視しない設定ならば変換失敗
                    return DependencyProperty.UnsetValue;
                }
            }

            // 有効な値を見つけている場合は判定結果を返却
            if (exists)
            {
                return condition ? Visibility.Visible : (this.InvisibleToHidden ? Visibility.Hidden : Visibility.Collapsed);
            }

            // 値が無かった(あるいは全て無視した)のならば条件に合わないので変換不可
            return DependencyProperty.UnsetValue;

        }
        #endregion
    }
}
