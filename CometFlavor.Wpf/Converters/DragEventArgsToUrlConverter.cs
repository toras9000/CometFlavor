using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Data;

namespace CometFlavor.Wpf.Converters;

/// <summary>
/// ドロップイベントパラメータからURL情報に変換する
/// </summary>
[ValueConversion(typeof(DragEventArgs), typeof(Uri))]
[ValueConversion(typeof(DragEventArgs), typeof(string))]
public class DragEventArgsToUrlConverter : IDragDropTriggerParameterConverter
{
    // 公開プロパティ
    #region 属性情報
    /// <summary>このコンバータの処理対象ドロップデータフォーマット</summary>
    public IReadOnlyList<string> AcceptFormats { get; } = new[] { UnicodeUrlFormat, AnsiUrlFormat, };
    #endregion

    #region 動作設定
    /// <summary>URLを <see cref="Uri"/> 型に変換するか否か</summary>
    public bool ConvertToUri { get; set; } = false;
    #endregion

    // 公開メソッド
    #region 変換
    /// <summary>値を変換する</summary>
    /// <param name="value">変換元の値</param>
    /// <param name="targetType">対象の型</param>
    /// <param name="parameter">コンバータパラメータ</param>
    /// <param name="culture">変換時のカルチャ</param>
    /// <returns>変換結果値。変換できない場合は DependencyProperty.UnsetValue。</returns>
    public object? Convert(object? value, Type? targetType, object? parameter, CultureInfo? culture)
    {
        // 元データがドロップイベント引数であることを確認
        if (value is DragEventArgs args)
        {
            // URLデータの取り出し
            // 取得の容易さと内部データ構造への依存を避ける意味から、テキストへの自動変換を利用する。
            var url = tryGetDropUrl(args, UnicodeUrlFormat, Encoding.Unicode)
                   ?? tryGetDropUrl(args, AnsiUrlFormat, Encoding.ASCII);

            // 変換結果をUri型にするかを判定
            // プロパティで設定されていれば常に、もしくは変換先の型がUriならば。
            var toUri = this.ConvertToUri || targetType == typeof(Uri);
            if (toUri)
            {
                return Uri.TryCreate(url, UriKind.Absolute, out var uri) ? uri : null;
            }

            return url;
        }

        return DependencyProperty.UnsetValue;
    }

    /// <summary>値を逆変換する。(非サポート)</summary>
    /// <param name="value">変換元の値</param>
    /// <param name="targetType">対象の型</param>
    /// <param name="parameter">コンバータパラメータ</param>
    /// <param name="culture">変換時のカルチャ</param>
    /// <returns>常に DependencyProperty.UnsetValue を返却。</returns>
    public object ConvertBack(object? value, Type? targetType, object? parameter, CultureInfo? culture)
    {
        return DependencyProperty.UnsetValue;
    }
    #endregion

    // 非公開フィールド
    #region 定数
    /// <summary>UnicodeエンコーディングテキストのURLデータ名</summary>
    private const string UnicodeUrlFormat = "UniformResourceLocatorW";

    /// <summary>ANSIエンコーディングテキストのURLデータ名</summary>
    private const string AnsiUrlFormat = "UniformResourceLocator";
    #endregion

    // 非公開メソッド
    #region データ処理
    /// <summary>ドロップデータからURLを取得する。</summary>
    /// <param name="args">ドロップ引数</param>
    /// <param name="format">データ書式</param>
    /// <param name="encoding">URLテキストをデコードするエンコーディング</param>
    /// <returns>取得したURLテキスト。取得できなかった場合は null を返却。</returns>
    private string? tryGetDropUrl(DragEventArgs args, string format, Encoding encoding)
    {
        // 指定書式のデータを得る。
        // まずは IDisposable であるかを判別。もしもデータが MemoryStream でなくても、必要があれば破棄できるようにする。
        if (args.Data.GetData(format) is IDisposable resource)
        {
            try
            {
                // IDisposable であればとりあえず抜ける前には破棄できるようにする
                using (resource)
                {
                    // データが MemoryStream であるかを判別
                    if (resource is MemoryStream stream)
                    {
                        // 内容をテキストに変換
                        return encoding.GetString(stream.ToArray());
                    }
                }
            }
            catch { }
        }

        return null;
    }
    #endregion
}
