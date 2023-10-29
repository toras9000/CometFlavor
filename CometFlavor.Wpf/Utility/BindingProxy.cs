using System.Windows;

namespace CometFlavor.Wpf.Utility;

/// <summary>
/// 論理/ビジュアルツリー外の要素に対してバインドデータを仲介するプロキシ
/// </summary>
public class BindingProxy : Freezable
{
    /// <summary>任意のデータ</summary>
    [Localizability(LocalizationCategory.NeverLocalize)]
    public object? DataContext
    {
        get { return (object?)GetValue(DataContextProperty); }
        set { SetValue(DataContextProperty, value); }
    }

    /// <summary>DataContext 依存プロパティ</summary>
    public static readonly DependencyProperty DataContextProperty = DependencyProperty.Register(nameof(DataContext), typeof(object), typeof(BindingProxy), new PropertyMetadata(null));

    /// <iheritdoc />
    protected override Freezable CreateInstanceCore()
        => new BindingProxy();
}
