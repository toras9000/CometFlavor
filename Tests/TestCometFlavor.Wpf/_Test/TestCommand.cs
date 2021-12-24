using System;
using System.Windows.Input;
using Moq;

namespace TestCometFlavor.Wpf._Test;

public class TestCommand : Mock<ICommand>
{
    public void Setup_CanExecute(Func<bool> stub)
    {
        this.Setup(c => c.CanExecute(It.IsAny<object>())).Returns(stub);
    }

    public void Setup_CanExecute(bool state)
    {
        this.Setup(c => c.CanExecute(It.IsAny<object>())).Returns(state);
    }

    public void Raise_CanExecuteChanged()
    {
        this.Raise(c => c.CanExecuteChanged += It.IsAny<EventHandler>(), EventArgs.Empty);
    }
}
