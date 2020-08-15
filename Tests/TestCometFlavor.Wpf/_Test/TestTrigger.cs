using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using Microsoft.Xaml.Behaviors;

namespace TestCometFlavor.Wpf._Test
{
    public class TestTrigger : TriggerBase<DependencyObject>
    {
        public void Invoke(object param)
        {
            this.InvokeActions(param);
        }
    }
}
