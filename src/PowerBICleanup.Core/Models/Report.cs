namespace PowerBICleanup.Core.Models;

public sealed class Report
{
    public required string Name { get; init; }

    public required string FolderPath { get; init; }

    public List<ReportPage> Pages { get; } = [];
}
