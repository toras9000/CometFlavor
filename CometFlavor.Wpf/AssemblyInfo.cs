using System.Windows;
using System.Windows.Markup;

[assembly: ThemeInfo(
    ResourceDictionaryLocation.None, //where theme specific resource dictionaries are located
                                     //(used if a resource is not found in the page,
                                     // or application resource dictionaries)
    ResourceDictionaryLocation.SourceAssembly //where the generic resource dictionary is located
                                              //(used if a resource is not found in the page,
                                              // app, or any theme specific resource dictionaries)
)]

// XAML用名前空間定義
[assembly: XmlnsDefinition("http://schemas.cometflavor.toras9000/wpf/interactions", "CometFlavor.Wpf.Interactions")]
[assembly: XmlnsDefinition("http://schemas.cometflavor.toras9000/wpf/converters", "CometFlavor.Wpf.Converters")]
[assembly: XmlnsDefinition("http://schemas.cometflavor.toras9000/wpf/utility", "CometFlavor.Wpf.Utility")]

[assembly: XmlnsDefinition("http://schemas.cometflavor.toras9000/", "CometFlavor.Wpf.Interactions")]
[assembly: XmlnsDefinition("http://schemas.cometflavor.toras9000/", "CometFlavor.Wpf.Converters")]
[assembly: XmlnsDefinition("http://schemas.cometflavor.toras9000/", "CometFlavor.Wpf.Utility")]
