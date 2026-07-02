using PowerBICleanup.Core.Models;

namespace PowerBICleanup.Engine.Readers.SemanticModel;

public sealed class SemanticModelReader
{
    private readonly TableReader _tableReader = new();

    public PowerBICleanup.Core.Models.SemanticModel Read(string? folder)
    {
        var model = new PowerBICleanup.Core.Models.SemanticModel();

        if (string.IsNullOrWhiteSpace(folder))
            return model;

        _tableReader.Read(model, folder);

        return model;
    }
}
