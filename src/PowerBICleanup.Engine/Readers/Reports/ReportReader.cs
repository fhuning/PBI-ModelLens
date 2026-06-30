namespace PowerBICleanup.Engine.Readers.Reports;

using PowerBICleanup.Core.Models;

public sealed class ReportReader
{
    public Report? Read(string? reportFolderPath)
    {
        if (string.IsNullOrWhiteSpace(reportFolderPath))
            return null;

        if (!Directory.Exists(reportFolderPath))
            return null;

        return new Report
        {
            Name = Path.GetFileNameWithoutExtension(reportFolderPath),
            FolderPath = reportFolderPath
        };
    }
}
