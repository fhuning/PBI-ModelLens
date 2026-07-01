using PowerBICleanup.Core.Models;
using PowerBICleanup.Engine.Readers.Reports;

namespace PowerBICleanup.Tests;

public sealed class PbipPageReaderTests
{
    [Fact]
    public void Read_AddsPagesToReport()
    {
        var report = new Report
        {
            Name = "Test Report",
            FolderPath = @"C:\VS-projects\PBI-ModelLens\samples\HRM003 - Sickness.Report"
        };

        var reader = new PageReader();

        reader.Read(report);

        Assert.NotEmpty(report.Pages);
    }
}
