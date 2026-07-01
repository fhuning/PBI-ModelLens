using System.Windows.Input;
using PowerBICleanup.Engine.Services;
using PowerBICleanup.UI.Commands;
using System.Collections.ObjectModel;
using PowerBICleanup.Core.Models;
using PowerBICleanup.UI.Explorer;

namespace PowerBICleanup.UI.ViewModels;

public sealed class MainWindowViewModel
{
    private readonly ProjectService _projectService = new();

    public ICommand OpenProjectCommand { get; }

    public ModelLensProject? CurrentProject { get; private set; }

    public ObservableCollection<ExplorerItem> ExplorerItems { get; } = new();

    public MainWindowViewModel()
    {
        OpenProjectCommand = new RelayCommand(OpenProject);
    }

    private void PopulateExplorer()
    {
        ExplorerItems.Clear();

        if (CurrentProject is null)
            return;

        var projectItem = new ExplorerItem
        {
            Name = CurrentProject.Name,
            ItemType = ExplorerItemType.Project,
            Tag = CurrentProject
        };

        var reportItem = new ExplorerItem
        {
            Name = "Report",
            ItemType = ExplorerItemType.Report,
            Tag = CurrentProject.Report
        };

        if (CurrentProject.Report is not null)
        {
            foreach (var page in CurrentProject.Report.Pages)
            {
                reportItem.Children.Add(new ExplorerItem
                {
                    Name = page.Name,
                    ItemType = ExplorerItemType.ReportPage,
                    Tag = page
                });
            }
        }

        var semanticModelItem = new ExplorerItem
        {
            Name = "Semantic Model",
            ItemType = ExplorerItemType.SemanticModel
        };

        projectItem.Children.Add(reportItem);
        projectItem.Children.Add(semanticModelItem);

        ExplorerItems.Add(projectItem);
    }
    private void OpenProject()
    {
        using var dialog = new FolderBrowserDialog();

        dialog.Description = "Select a PBIP project";

        if (dialog.ShowDialog() != DialogResult.OK)
            return;

        var project = _projectService.LoadProject(dialog.SelectedPath);

        if (project is null)
        {
            System.Windows.MessageBox.Show(
                "This folder is not a PBIP project.",
                "PBI ModelLens");

            return;
        }
        CurrentProject = project;
        PopulateExplorer();
    }
}
