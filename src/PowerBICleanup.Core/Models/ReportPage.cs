namespace PowerBICleanup.Core.Models;

public sealed class ReportPage
{
    public required string PageId { get; init; }

    public required string Name { get; init; }

    public required string FolderPath { get; init; }
}
