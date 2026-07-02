using PowerBICleanup.Core.Models;
using PowerBICleanup.Engine.Readers;
using PowerBICleanup.Engine.Readers.Reports;
using PowerBICleanup.Engine.Readers.SemanticModel;

namespace PowerBICleanup.Engine.Services;

public sealed class ProjectService
{
    private readonly IProjectReader _projectReader;
    private readonly PbipFileReader _pbipFileReader;
    private readonly ReportReader _reportReader;
    private readonly PageReader _pageReader;
    private readonly VisualReader _visualReader;
    private readonly SemanticModelReader _semanticModelReader;

    public ProjectService()
    {
        _projectReader = new PbipProjectReader();
        _pbipFileReader = new PbipFileReader();
        _reportReader = new ReportReader();
        _pageReader = new PageReader();
        _visualReader = new VisualReader();
        _semanticModelReader = new SemanticModelReader();
    }

    public ModelLensProject? LoadProject(string folder)
    {
        var project = _projectReader.LoadProject(folder);

        if (project is null)
            return null;

        var metadata = _pbipFileReader.Read(project.PbipFile);

        var reportFolderPath =
            metadata.ReportFolder is null
                ? null
                : Path.Combine(project.RootFolder, metadata.ReportFolder);

        var semanticModelFolderPath =
            metadata.SemanticModelFolder is null
                ? Directory
                    .EnumerateDirectories(project.RootFolder, "*.SemanticModel")
                    .FirstOrDefault()
                : Path.Combine(project.RootFolder, metadata.SemanticModelFolder);

        var semanticModel =
            _semanticModelReader.Read(
                semanticModelFolderPath);

        var report = _reportReader.Read(reportFolderPath);

        if (report is not null)
        {
            _pageReader.Read(report);
            foreach (var page in report.Pages)
            {
                _visualReader.Read(page);
            }
        }

        return new ModelLensProject
        {
            Name = project.Name,
            RootFolder = project.RootFolder,
            PbipFile = project.PbipFile,
            ReportFolderPath = reportFolderPath,
            SemanticModelFolderPath = semanticModelFolderPath,
            Report = report,
            SemanticModel = semanticModel
        };
    }
}
