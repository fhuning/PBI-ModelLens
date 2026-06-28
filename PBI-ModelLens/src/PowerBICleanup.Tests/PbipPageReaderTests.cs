using PowerBICleanup.Engine.Readers;

namespace PowerBICleanup.Tests;

public sealed class PbipPageReaderTests
{
    [Fact]
    public void ReadPages_ReturnsPageWithDisplayName()
    {
        var rootFolder = CreateTempFolder();

        try
        {
            var pageFolder = Path.Combine(rootFolder, "Report", "pages", "abc123");
            Directory.CreateDirectory(pageFolder);

            var pageJson = """
            {
              "name": "abc123",
              "displayName": "Dashboard"
            }
            """;

            File.WriteAllText(Path.Combine(pageFolder, "page.json"), pageJson);

            var reader = new PbipPageReader();

            var pages = reader.ReadPages(rootFolder);

            Assert.Single(pages);
            Assert.Equal("abc123", pages[0].Id);
            Assert.Equal("Dashboard", pages[0].DisplayName);
            Assert.Equal(pageFolder, pages[0].FolderPath);
        }
        finally
        {
            Directory.Delete(rootFolder, recursive: true);
        }
    }

    private static string CreateTempFolder()
    {
        var path = Path.Combine(Path.GetTempPath(), "PowerBICleanupTests", Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(path);
        return path;
    }
}
