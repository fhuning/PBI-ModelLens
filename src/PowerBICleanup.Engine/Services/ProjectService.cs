using PowerBICleanup.Core.Models;
using PowerBICleanup.Engine.Readers;

namespace PowerBICleanup.Engine.Services;

public sealed class ProjectService
{
    private readonly IProjectReader _projectReader;
    private readonly PbipFileReader _pbipFileReader = new();

    public ProjectService()
    {
        _projectReader = new PbipProjectReader();
    }

    public ModelLensProject? LoadProject(string folder)
    {
        var project = _projectReader.LoadProject(folder);

        if (project is null)
            return null;

        var metadata = _pbipFileReader.Read(project.PbipFile);

        return new ModelLensProject
        {
            Name = project.Name,
            RootFolder = project.RootFolder,
            PbipFile = project.PbipFile,
            ReportFolder = metadata.ReportFolder is null
                ? null
                : Path.Combine(project.RootFolder, metadata.ReportFolder),
            SemanticModelFolder = metadata.SemanticModelFolder is null
                ? null
                : Path.Combine(project.RootFolder, metadata.SemanticModelFolder)
        };
    }
}
