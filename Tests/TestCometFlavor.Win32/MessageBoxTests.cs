using System.Diagnostics;
using AwesomeAssertions;
using CometFlavor.Win32.Interaction;

namespace TestCometFlavor.Win32;

[TestClass]
public sealed class MessageBoxTests
{
    [TestMethod]
    public void Show()
    {
        if (!Debugger.IsAttached) Assert.Inconclusive("For debugging only");

        var settings = new MessageBox.Settings(
            Button: MessageBox.Button.YesNo,
            Icon: MessageBox.Icon.Exclamation,
            DefaultButton: MessageBox.DefaultButton.Button2,
            Modality: MessageBox.Modality.System,
            Options: MessageBox.Options.Foreground
        );

        var result = MessageBox.Show("message", "caption", settings);
        result.Should().BeOneOf(MessageBox.Result.Yes, MessageBox.Result.No);
    }
}
