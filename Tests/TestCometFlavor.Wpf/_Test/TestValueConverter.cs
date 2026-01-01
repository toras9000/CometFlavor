using System.Globalization;
using System.Windows.Data;
using Moq;

namespace TestCometFlavor.Wpf._Test;

public class TestValueConverter : Mock<IValueConverter>
{
    public void Setup_Convert(Func<object, Type, object, CultureInfo, object> stub)
    {
        this.Setup(c => c.Convert(It.IsAny<object>(), It.IsAny<Type>(), It.IsAny<object>(), It.IsAny<CultureInfo>()))
            .Returns(stub);
    }
    public void Setup_Convert_Throws(Exception exception)
    {
        this.Setup(c => c.Convert(It.IsAny<object>(), It.IsAny<Type>(), It.IsAny<object>(), It.IsAny<CultureInfo>()))
            .Throws(exception);
    }
}
