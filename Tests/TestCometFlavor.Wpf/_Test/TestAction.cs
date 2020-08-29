using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using Microsoft.Xaml.Behaviors;

namespace TestCometFlavor.Wpf._Test
{
    public class TestAction : TriggerAction<DependencyObject>
    {
        public TestAction()
        {
            this.parameters = new List<object>();
            this.InvokedParameters = this.parameters.AsReadOnly();
        }

        public IReadOnlyList<object> InvokedParameters { get; }

        public void Reset() => this.parameters.Clear();

        protected override void Invoke(object parameter)
        {
            this.parameters.Add(parameter);
        }

        private List<object> parameters;
    }
}
