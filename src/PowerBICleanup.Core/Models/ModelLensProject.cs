namespace PowerBICleanup.Core.Models;

public sealed class ModelLensProject
{
    public required string Name { get; init; }

    public required string RootFolder { get; init; }

    public required string PbipFile { get; init; }
}
