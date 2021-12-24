using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Data;

namespace CometFlavor.Wpf.Converters;

/// <summary>
/// ドロップイベントパラメータからファイルパス情報に変換する
/// </summary>
[ValueConversion(typeof(DragEventArgs), typeof(Uri))]
[ValueConversion(typeof(DragEventArgs), typeof(string))]
public class DragEventArgsToFilePathConverter : IDragDropTriggerParameterConverter
{
    // 公開プロパティ
    #region 属性情報
    /// <summary>このコンバータの処理対象ドロップデータフォーマット</summary>
    public IReadOnlyList<string> AcceptFormats { get; } = new[] { DataFormats.FileDrop, };
    #endregion

    #region 動作設定
    /// <summary>ファイルパスを <see cref="Uri"/> 型に変換するか否か</summary>
    public bool ConvertToUri { get; set; } = false;
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
    /// <returns>変換結果値。変換できない場合は DependencyProperty.UnsetValue。</returns>
    public object? Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        // 元データがドロップイベント引数であることを確認
        if (value is DragEventArgs args)
        {
            // ファイルドロップデータからファイルパスを取得
            var paths = args.Data.GetData(DataFormats.FileDrop) as string[];
            if (paths == null)
            {
                // ファイルドロップデータがなければ null を返すことにする。(DependencyProperty.UnsetValue ではなく。)
                // ドロップデータを処理したけどデータがなかった、ということを表現するため。
                return null;
            }

            // 変換結果をUri型にするかを判定
            // プロパティで設定されていれば常に、もしくは変換先の型がUriならば。
            var toUri = this.ConvertToUri || targetType == typeof(Uri);
            if (toUri)
            {
                // uriへ変換した結果を返却
                var uris = paths
                    .Select(p => Uri.TryCreate(p, UriKind.Absolute, out var uri) ? uri : null)
                    .Where(u => u != null)
                    .ToArray();
                return uris;
            }

            // 変換しない場合は文字列のまま
            return paths;
        }

        return DependencyProperty.UnsetValue;
    }

    /// <summary>
    /// 値を逆変換する。(非サポート)
    /// </summary>
    /// <param name="value">変換元の値</param>
    /// <param name="targetType">対象の型</param>
    /// <param name="parameter">コンバータパラメータ</param>
    /// <param name="culture">変換時のカルチャ</param>
    /// <returns>常に DependencyProperty.UnsetValue を返却。</returns>
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return DependencyProperty.UnsetValue;
    }
    #endregion
}
