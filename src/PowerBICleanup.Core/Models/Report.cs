namespace PowerBICleanup.Core.Models;

public sealed class Report
{
    public required string Name { get; init; }

    public IList<ReportPage> Pages { get; } = new List<ReportPage>();
}
