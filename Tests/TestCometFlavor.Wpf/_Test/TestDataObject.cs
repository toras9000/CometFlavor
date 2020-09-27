using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using Moq;
using Moq.Language.Flow;

namespace TestCometFlavor.Wpf._Test
{
    public class TestDataObject : Mock<IDataObject>
    {
        public void Setup_GetDataPresent(Func<string, bool> stub)
        {
            this.Setup(m => m.GetDataPresent(It.IsAny<string>())).Returns(stub);
        }

        public void Setup_GetDataPresent(string format, Func<bool> stub)
        {
            this.Setup(m => m.GetDataPresent(format)).Returns(stub);
        }

        public void Setup_GetData(Func<string, object> stub)
        {
            this.Setup(m => m.GetData(It.IsAny<string>())).Returns(stub);
        }

        public void Setup_GetData(string format, Func<object> stub)
        {
            this.Setup(m => m.GetData(format)).Returns(stub);
        }
    }
}
