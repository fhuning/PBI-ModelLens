using System.Windows.Forms;
using System.Windows.Input;
using PowerBICleanup.Engine.Services;
using PowerBICleanup.UI.Commands;

namespace PowerBICleanup.UI.ViewModels;

public sealed class MainWindowViewModel
{
    private readonly ProjectService _projectService = new();

    public ICommand OpenProjectCommand { get; }

    public MainWindowViewModel()
    {
        OpenProjectCommand = new RelayCommand(OpenProject);
    }

    private void OpenProject()
    {
        using var dialog = new FolderBrowserDialog
        {
            Description = "Select a PBIP project"
        };

        if (dialog.ShowDialog() != DialogResult.OK)
            return;

        var project = _projectService.LoadProject(dialog.SelectedPath);

        if (project is not null)
        {
            System.Windows.MessageBox.Show(
                $"PBIP project detected: {project.Name}",
                "PBI ModelLens");
        }
        else
        {
            System.Windows.MessageBox.Show(
                "This folder is not a PBIP project.",
                "PBI ModelLens");
        }
    }
}
