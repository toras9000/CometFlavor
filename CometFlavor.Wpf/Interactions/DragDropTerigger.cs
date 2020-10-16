using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using CometFlavor.Wpf.Converters;
using Microsoft.Xaml.Behaviors;

namespace CometFlavor.Wpf.Interactions
{
    /// <summary>
    /// ドラッグ＆ドロップを契機とするトリガ
    /// </summary>
    public class DragDropTerigger : TriggerBase<UIElement>
    {
        // 公開プロパティ
        #region 動作設定
        /// <summary>ドロップ受け入れを自動的に有効にするか否か</summary>
        /// <remarks>
        /// このプロパティを true にると要素にアタッチした際に自動的に <see cref="UIElement.AllowDrop"/> を true とする。
        /// また、デタッチ時には自動設定前の値に再設定する。
        /// </remarks>
        public bool AutoAllowDrop
        {
            get { return (bool)GetValue(AutoAllowDropProperty); }
            set { SetValue(AutoAllowDropProperty, value); }
        }

        /// <summary>ドロップを受け入れるデータフォーマット名の配列</summary>
        /// <remarks>
        /// 有効な名称配列を指定した場合、そのフォーマット名のデータを含む場合のみドロップを受け入れる。
        /// このプロパティがnullや空の配列である場合、全てのドロップを受け入れる。
        /// デフォルト値は null となる。
        /// </remarks>
        public IReadOnlyList<string> AcceptDropFormats
        {
            get { return (string[])GetValue(AcceptDropFormatsProperty); }
            set { SetValue(AcceptDropFormatsProperty, value); }
        }

        /// <summary>受け入れエフェクト</summary>
        /// <remarks>
        /// 要素上にドラッグされた際の受け入れエフェクト(ポインタアイコン)を指定する。
        /// ただし、この受け入れエフェクトはドラッグソース(ドラッグ開始側)が指定する許可エフェクトに含まれる必要があり、
        /// 指定された受け入れエフェクトと許可エフェクトと論理積が実際に利用される。
        /// デフォルト値は DragDropEffects.All となる。(これは許可エフェクト自体を受け入れエフェクトとする意味を持つ)
        /// </remarks>
        public DragDropEffects AcceptDropEffect
        {
            get { return (DragDropEffects)GetValue(AcceptDropEffectProperty); }
            set { SetValue(AcceptDropEffectProperty, value); }
        }

        /// <summary>アクションパラメータに対するコンバータ</summary>
        /// <remarks>
        /// ドロップイベント発生時にアクションに渡すパラメータを変換するコンバータ。
        /// コンバータを指定しない場合、アクションパラメータにはドロップイベント引数が渡される。
        /// コンバータを指定した場合、ドロップイベント引数をコンバータで変換した結果がアクションパラメータとして渡される。
        /// デフォルト値は null となる。
        /// </remarks>
        public IValueConverter ParameterConverter
        {
            get { return (IValueConverter)GetValue(ParameterConverterProperty); }
            set { SetValue(ParameterConverterProperty, value); }
        }

        #endregion

        #region 依存プロパティ
        /// <summary><see cref="AutoAllowDrop"/> の依存プロパティ</summary>
        public static readonly DependencyProperty AutoAllowDropProperty = DependencyProperty.Register(nameof(AutoAllowDrop), typeof(bool), typeof(DragDropTerigger), new PropertyMetadata(true));

        /// <summary><see cref="AcceptDropFormats"/> の依存プロパティ</summary>
        public static readonly DependencyProperty AcceptDropFormatsProperty = DependencyProperty.Register(nameof(AcceptDropFormats), typeof(string[]), typeof(DragDropTerigger), new PropertyMetadata(null, onAcceptDropFormatsChanged));

        /// <summary><see cref="AcceptDropEffect"/> の依存プロパティ</summary>
        public static readonly DependencyProperty AcceptDropEffectProperty = DependencyProperty.Register(nameof(AcceptDropEffect), typeof(DragDropEffects), typeof(DragDropTerigger), new PropertyMetadata(DragDropEffects.All));

        /// <summary><see cref="ParameterConverter"/> の依存プロパティ</summary>
        public static readonly DependencyProperty ParameterConverterProperty = DependencyProperty.Register(nameof(ParameterConverter), typeof(IValueConverter), typeof(DragDropTerigger), new PropertyMetadata(null, onParameterConverterChanged));
        #endregion

        // 保護メソッド
        #region 配置
        /// <summary>
        /// トリガを要素にアタッチした際の処理
        /// </summary>
        protected override void OnAttached()
        {
            // 基本クラス処理
            base.OnAttached();

            // エイリアス
            var assosiated = this.AssociatedObject;

            // 自動設定がONであればドロップ受け入れを有効化
            if (this.AutoAllowDrop)
            {
                this.orgAllowDrop = assosiated.AllowDrop;
                assosiated.AllowDrop = true;
            }

            // ドロップイベントをハンドル
            assosiated.DragOver -= AssociatedObject_DragOver;
            assosiated.DragOver += AssociatedObject_DragOver;
            assosiated.Drop -= AssociatedObject_Drop;
            assosiated.Drop += AssociatedObject_Drop;
        }

        /// <summary>
        /// トリガを要素からデタッチした際の処理
        /// </summary>
        protected override void OnDetaching()
        {
            // エイリアス
            var assosiated = this.AssociatedObject;

            // ドロップイベントをアンハンドル
            assosiated.Drop -= AssociatedObject_Drop;
            assosiated.DragOver -= AssociatedObject_DragOver;

            // ドロップ受け入れを自動設定した場合は元にもどす
            if (this.orgAllowDrop.HasValue)
            {
                assosiated.AllowDrop = this.orgAllowDrop.Value;
                this.orgAllowDrop = null;
            }

            // 基本クラス処理
            base.OnDetaching();
        }
        #endregion

        // 非公開フィールド
        #region 保存
        /// <summary>バックアップされたアタッチ要素のAllowDrop設定値</summary>
        private bool? orgAllowDrop;

        /// <summary>直接のプロパティで指定された受け入れ書式</summary>
        private IReadOnlyList<string> explicitAcceptFormats;

        /// <summary>パラメータコンバータから指定された受け入れ書式</summary>
        private IReadOnlyList<string> converterAcceptFormats;
        #endregion

        // 非公開メソッド
        #region 依存プロパティ変更ハンドラ
        /// <summary>
        /// <see cref="AcceptDropFormats"/> 依存プロパティの変更ハンドラ
        /// </summary>
        private static void onAcceptDropFormatsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is DragDropTerigger self)
            {
                // 明示的なプロパティ変更が行われた場合、必ずその値を使用するためにnullでない値(同じ意味となる空配列)で保持する。
                self.explicitAcceptFormats = (e.NewValue as IReadOnlyList<string>) ?? Array.Empty<string>();
            }
        }

        /// <summary>
        /// <see cref="ParameterConverter"/> 依存プロパティの変更ハンドラ
        /// </summary>
        private static void onParameterConverterChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is DragDropTerigger self)
            {
                // パラメータコンバータが専用のインターフェースを実装している場合、インターフェースから受け入れ書式を取得
                // (専用インターフェースを実装していなければコンバータでの書式指定値はクリアする)
                self.converterAcceptFormats = (e.NewValue as IDragDropTriggerParameterConverter)?.AcceptFormats;
            }
        }
        #endregion

        #region イベントハンドラ
        /// <summary>
        /// 要素上へのドラッグオーバーイベントハンドラ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AssociatedObject_DragOver(object sender, DragEventArgs e)
        {
            // 受け入れ書式ソースを選択
            // このコンバータ自体のプロパティによる明示的な設定値があればそれを優先。
            var acceptFormats = this.explicitAcceptFormats ?? this.converterAcceptFormats;

            // ドロップを受け入れるかを判定
            if (acceptFormats == null || acceptFormats.Count <= 0
             || acceptFormats.Any(format => e.Data.GetDataPresent(format)))
            {
                // データ形式が特に指定されていない場合、
                // もしくは指定された形式のいずれかを含む場合は、受け入れ可能。
                e.Effects = e.AllowedEffects & this.AcceptDropEffect;
            }
            else
            {
                // 適合しない場合はドロップ受け入れ不可
                e.Effects = DragDropEffects.None;
            }

            // イベントをハンドルした
            e.Handled = true;
        }

        /// <summary>
        /// 要素上へのドロップイベントハンドラ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AssociatedObject_Drop(object sender, DragEventArgs e)
        {
            // ドロップパラメータ
            var parameter = (object)e;

            // パラメータコンバータが設定されているか
            var converter = this.ParameterConverter;
            if (converter != null)
            {
                // 設定されているのであればパラメータの変換を行う
                var converted = converter.Convert(parameter, null, null, CultureInfo.CurrentCulture);
                if (converted != DependencyProperty.UnsetValue)
                {
                    // 変換結果が無しでなければ、変換された値をパラメータとする。
                    parameter = converted;
                }
            }

            // アクション呼び出し
            this.InvokeActions(parameter);

            // イベントをハンドルした
            e.Handled = true;
        }
        #endregion
    }
}
