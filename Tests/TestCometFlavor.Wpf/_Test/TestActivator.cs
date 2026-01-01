using System.Reflection;
using System.Windows;

namespace TestCometFlavor.Wpf._Test;

public static class TestActivator
{
    public static DragEventArgs CreateDragEventArgs(IDataObject data, DragDropEffects allowedEffects = DragDropEffects.All, DragDropKeyStates dragDropKeyStates = DragDropKeyStates.None, DependencyObject? target = null, Point point = default)
    {
        var argType = typeof(DragEventArgs);
        var argConstructor = argType.GetConstructor(
            BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic,
            null,
            [typeof(IDataObject), typeof(DragDropKeyStates), typeof(DragDropEffects), typeof(DependencyObject), typeof(Point)],
            null
        ) ?? throw new Exception();

        return (DragEventArgs)argConstructor.Invoke([data, dragDropKeyStates, allowedEffects, target, point]);
    }
}
