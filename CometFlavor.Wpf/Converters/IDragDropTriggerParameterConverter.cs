using System.Collections.Generic;
using System.Windows.Data;

namespace CometFlavor.Wpf.Converters
{
    /// <summary>
    /// DragDropTrigger用のデータ提供I/Fを備えたパラメータコンバータ
    /// </summary>
    public interface IDragDropTriggerParameterConverter : IValueConverter
    {
        /// <summary>コンバータが処理可能なドロップデータ書式</summary>
        IReadOnlyList<string> AcceptFormats { get; }
    }
}
