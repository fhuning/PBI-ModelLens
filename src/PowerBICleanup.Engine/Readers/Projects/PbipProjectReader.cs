using PowerBICleanup.Core.Models;

namespace PowerBICleanup.Engine.Readers;

public sealed class PbipProjectReader : IProjectReader
{
    public ModelLensProject? LoadProject(string folder)
    {
        if (!Directory.Exists(folder))
            return null;

        var pbipFile = Directory
            .EnumerateFiles(folder, "*.pbip")
            .FirstOrDefault();

        if (pbipFile is null)
            return null;

        return new ModelLensProject
        {
            Name = Path.GetFileNameWithoutExtension(pbipFile),
            RootFolder = folder,
            PbipFile = pbipFile
        };
    }
}
