using System.Diagnostics;
using AwesomeAssertions;
using CometFlavor.Win32.Dialogs;
using Lestaly;

namespace TestCometFlavor.Win32;

[TestClass]
public sealed class ShellFileDialogTests
{
    [TestMethod]
    public void ShellOpenFileDialog()
    {
        if (!Debugger.IsAttached) Assert.Inconclusive("For debugging only");

        var thisDir = ThisSource.Directory();

        var parameters = new ShellOpenFileDialogParameter();
        parameters.Title = "テストタイトル";
        parameters.AcceptButtonLabel = "あくせぷと";
        parameters.FileNameLabel = "ふぁいるのなまえ";
        parameters.AdditionalPlaces.Add(new(thisDir.FullName, ShellFileDialogPlaceOrder.Top));
        parameters.Directory = thisDir.FullName;
        parameters.InitialFileName = "TestDefaultFileName";
        parameters.DefaultExtension = "test";
        parameters.Filters.Add(new("Test files", "*.test"));
        parameters.Filters.Add(new("All files", "*"));
        parameters.StrictFileTypes = false;
        parameters.NoChangeDirectory = true;
        parameters.PickFolders = false;
        parameters.ForceFileSystem = false;
        parameters.AllNonStorageItems = false;
        parameters.NoValidate = false;
        parameters.AllowMultiSelect = true;
        parameters.PathMustExist = true;
        parameters.FileMustExist = true;
        parameters.CreatePrompt = false;
        parameters.ShareAware = false;
        parameters.NoReadOnlyReturn = false;
        parameters.NoTestFileCreate = true;
        parameters.HidePinnedPlaces = false;
        parameters.NoDereferenceLinks = false;
        parameters.OkButtonNeedsInteraction = false;
        parameters.DontAddToRecent = true;
        parameters.ForceShowHidden = false;
        parameters.ForcePreviewPaneOn = false;

        var dialog = new ShellOpenFileDialog();
        var result = dialog.ShowDialog(parameters);
        var noneOrFile = result.Item.IsWhiteSpace() || result.Item?.AsFileInfo().Exists == true;
        noneOrFile.Should().BeTrue();
    }

    [TestMethod]
    public void ShellSaveFileDialog()
    {
        if (!Debugger.IsAttached) Assert.Inconclusive("For debugging only");

        var thisDir = ThisSource.Directory();

        var parameters = new ShellSaveFileDialogParameter();
        parameters.Title = "テストタイトル";
        parameters.AcceptButtonLabel = "あくせぷと";
        parameters.FileNameLabel = "ふぁいるのなまえ";
        parameters.AdditionalPlaces.Add(new(thisDir.FullName, ShellFileDialogPlaceOrder.Top));
        parameters.Directory = thisDir.FullName;
        parameters.InitialFileName = "TestDefaultFileName";
        parameters.DefaultExtension = "test";
        parameters.Filters.Add(new("Test files", "*.test"));
        parameters.Filters.Add(new("All files", "*"));
        parameters.StrictFileTypes = false;
        parameters.NoChangeDirectory = true;
        parameters.ForceFileSystem = false;
        parameters.AllNonStorageItems = false;
        parameters.NoValidate = false;
        parameters.PathMustExist = false;
        parameters.FileMustExist = false;
        parameters.CreatePrompt = false;
        parameters.ShareAware = false;
        parameters.NoReadOnlyReturn = false;
        parameters.NoTestFileCreate = true;
        parameters.HidePinnedPlaces = false;
        parameters.NoDereferenceLinks = false;
        parameters.OkButtonNeedsInteraction = false;
        parameters.DontAddToRecent = true;
        parameters.ForceShowHidden = false;

        var dialog = new ShellSaveFileDialog();
        dialog.ShowDialog(parameters);
    }
}
