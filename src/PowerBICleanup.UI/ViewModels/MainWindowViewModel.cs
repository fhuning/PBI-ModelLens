using System.Windows.Input;
using PowerBICleanup.Engine.Services;
using PowerBICleanup.UI.Commands;
using System.Collections.ObjectModel;
using PowerBICleanup.Core.Models;
using PowerBICleanup.UI.Explorer;
using PowerBICleanup.Core.Models;

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
                var pageItem = new ExplorerItem
                {
                    Name = page.Name,
                    ItemType = ExplorerItemType.ReportPage,
                    Tag = page
                };

                foreach (var visual in page.Visuals)
                {
                    pageItem.Children.Add(new ExplorerItem
                    {
                        Name = FormatVisualName(visual),
                        ItemType = ExplorerItemType.Visual,
                        Tag = visual
                    });
                }

                reportItem.Children.Add(pageItem);
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

        var tableCount =
            CurrentProject.SemanticModel?.Tables.Count ?? 0;

        MessageBox.Show(
            $"Tables: {tableCount}");

        PopulateExplorer();
    }
    private static string FormatVisualName(Visual visual)
    {
        var type = visual.VisualType switch
        {
            "cardVisual" => "Card",
            "pivotTable" => "Matrix",
            "slicer" => "Slicer",
            "clusteredColumnChart" => "Column Chart",
            "lineChart" => "Line Chart",
            "actionButton" => "Button",
            "textbox" => "Text Box",
            "shape" => "Shape",
            "image" => "Image",
            _ => visual.VisualType
        };

        return string.IsNullOrWhiteSpace(visual.Title)
            ? type
            : $"{type} - {visual.Title}";
    }
}
