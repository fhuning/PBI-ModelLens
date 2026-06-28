using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using PowerBICleanup.Engine.Readers;
using PowerBICleanup.UI.Commands;

namespace PowerBICleanup.UI.ViewModels;

public sealed class MainWindowViewModel
{
    public ICommand OpenProjectCommand { get; }

    public MainWindowViewModel()
    {
        OpenProjectCommand =
            new RelayCommand(OpenProject);
    }

    private void OpenProject()
    {
        using var dialog = new FolderBrowserDialog();

        dialog.Description = "Select a PBIP project";

        if (dialog.ShowDialog() != DialogResult.OK)
            return;

        var reader = new PbipReader();

        if (reader.IsPbipProject(dialog.SelectedPath))
        {
            MessageBox.Show(
                "PBIP project detected.",
                "PBI ModelLens");
        }
        else
        {
            MessageBox.Show(
                "This folder is not a PBIP project.",
                "PBI ModelLens");
        }
    }
}
