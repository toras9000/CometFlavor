using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;
using Moq;

namespace TestCometFlavor.Wpf._Test
{
    public class TestValueConverter : Mock<IValueConverter>
    {
        public void Setup_Convert(Func<object, Type, object, CultureInfo, object> stub)
        {
            this.Setup(c => c.Convert(It.IsAny<object>(), It.IsAny<Type>(), It.IsAny<object>(), It.IsAny<CultureInfo>()))
                .Returns(stub);
        }
    }
}
