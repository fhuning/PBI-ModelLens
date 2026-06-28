using PowerBICleanup.Core.Models;

namespace PowerBICleanup.Engine.Readers;

public interface IProjectReader
{
    ModelLensProject? LoadProject(string folder);
}
