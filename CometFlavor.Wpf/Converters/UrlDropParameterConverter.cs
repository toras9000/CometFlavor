using System;
using System.Globalization;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Data;

namespace CometFlavor.Wpf.Converters
{
    /// <summary>
    /// ドロップイベントパラメータからURL情報に変換する
    /// </summary>
    [ValueConversion(typeof(DragEventArgs), typeof(Uri))]
    [ValueConversion(typeof(DragEventArgs), typeof(string))]
    public class UrlDropParameterConverter : IValueConverter
    {
        // 公開プロパティ
        #region 属性情報
        /// <summary>このコンバータの処理対象ドロップデータフォーマット</summary>
        public string[] AcceptFormats { get; } = new[] { "UniformResourceLocatorW", "UniformResourceLocator", };
        #endregion

        #region 動作設定
        /// <summary>URLを <see cref="Uri"/> 型に変換するか否か</summary>
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
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // 元データがドロップイベント引数であることを確認
            if (value is DragEventArgs args)
            {
                var url = tryGetDropDataUrl(args, "UniformResourceLocatorW", Encoding.Unicode)
                       ?? tryGetDropDataUrl(args, "UniformResourceLocator", Encoding.Default);

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

        private string tryGetDropDataUrl(DragEventArgs args, string format, Encoding encoding)
        {
            var url = default(string);
            try
            {
                var data = args.Data.GetData(format);
                if (data is MemoryStream stream)
                {
                    using (stream)
                    {
                        url = encoding.GetString(stream.ToArray());
                    }
                }
                else if (data is IDisposable res)
                {
                    res.Dispose();
                }
            }
            catch
            { }

            return url;
        }
    }
}
