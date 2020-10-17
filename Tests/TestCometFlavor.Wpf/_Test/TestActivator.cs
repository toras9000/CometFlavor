using System.Reflection;
using System.Windows;

namespace TestCometFlavor.Wpf._Test
{
    public static class TestActivator
    {
        public static DragEventArgs CreateDragEventArgs(IDataObject data, DragDropEffects allowedEffects = DragDropEffects.All, DragDropKeyStates dragDropKeyStates = DragDropKeyStates.None, DependencyObject target = null, Point point = default)
        {
            var argType = typeof(DragEventArgs);
            var argConstructor = argType.GetConstructor(
                BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic,
                null,
                new[] { typeof(IDataObject), typeof(DragDropKeyStates), typeof(DragDropEffects), typeof(DependencyObject), typeof(Point), },
                null
            );

            return (DragEventArgs)argConstructor.Invoke(new object[] { data, dragDropKeyStates, allowedEffects, target, point });
        }
    }
}
