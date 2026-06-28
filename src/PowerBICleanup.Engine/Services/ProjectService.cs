using PowerBICleanup.Core.Models;
using PowerBICleanup.Engine.Readers;

namespace PowerBICleanup.Engine.Services;

public sealed class ProjectService
{
    private readonly IProjectReader _reader = new PbipProjectReader();

    public ModelLensProject? LoadProject(string folder)
    {
        return _reader.LoadProject(folder);
    }
}
