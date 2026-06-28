using PowerBICleanup.Core.Models;

namespace PowerBICleanup.Engine.Readers;

public sealed class PbipReader
{
    public bool IsPbipProject(string folder)
    {
        if (!Directory.Exists(folder))
            return false;

        return Directory
            .EnumerateFiles(folder, "*.pbip")
            .Any();
    }
}
