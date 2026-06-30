using PowerBICleanup.Core.Models;
using PowerBICleanup.Engine.Readers;
using PowerBICleanup.Engine.Readers.Reports;

namespace PowerBICleanup.Engine.Services;

public sealed class ProjectService
{
    private readonly IProjectReader _projectReader;
    private readonly PbipFileReader _pbipFileReader;
    private readonly ReportReader _reportReader;

    public ProjectService()
    {
        _projectReader = new PbipProjectReader();
        _pbipFileReader = new PbipFileReader();
        _reportReader = new ReportReader();
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
                ? null
                : Path.Combine(project.RootFolder, metadata.SemanticModelFolder);

        return new ModelLensProject
        {
            Name = project.Name,
            RootFolder = project.RootFolder,
            PbipFile = project.PbipFile,

            ReportFolderPath = reportFolderPath,
            SemanticModelFolderPath = semanticModelFolderPath,

            Report = _reportReader.Read(reportFolderPath)
        };
    }
}
