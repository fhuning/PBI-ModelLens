using PowerBICleanup.Core.Models;

namespace PowerBICleanup.Engine.Readers.SemanticModel;

public sealed class TableReader
{
    public void Read(
        Core.Models.SemanticModel semanticModel,
        string semanticModelFolder)
    {
        var tablesFolder = Path.Combine(
            semanticModelFolder,
            "definition",
            "tables");

        if (!Directory.Exists(tablesFolder))
            return;

        foreach (var file in Directory.EnumerateFiles(
                     tablesFolder,
                     "*.tmdl"))
        {
            semanticModel.Tables.Add(new Table
            {
                Name = Path.GetFileNameWithoutExtension(file),
                FilePath = file
            });
        }
    }
}
