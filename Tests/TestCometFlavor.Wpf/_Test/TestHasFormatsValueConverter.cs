using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace TestCometFlavor.Wpf._Test;

public class TestHasFormatsValueConverter : IValueConverter
{
    public IReadOnlyList<string>? AcceptFormats { get; set; }
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture) => DependencyProperty.UnsetValue;
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => DependencyProperty.UnsetValue;
}
