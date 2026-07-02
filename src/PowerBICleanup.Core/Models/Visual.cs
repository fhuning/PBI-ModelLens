namespace PowerBICleanup.Core.Models;

public sealed class Visual
{
    public required string VisualId { get; init; }

    public required string VisualType { get; init; }

    public string? Title { get; init; }

    public required string FolderPath { get; init; }
}
