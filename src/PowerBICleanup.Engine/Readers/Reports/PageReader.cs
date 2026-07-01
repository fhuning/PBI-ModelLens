using System.Text.Json;
using System.Text.Json.Serialization;
using PowerBICleanup.Core.Models;

namespace PowerBICleanup.Engine.Readers.Reports;

public sealed class PageReader
{
    public void Read(Report report)
    {
        var pagesFolder = Path.Combine(
            report.FolderPath,
            "definition",
            "pages");

        if (!Directory.Exists(pagesFolder))
            return;

        foreach (var pageId in ReadPageOrder(pagesFolder))
        {
            var pageFolder = Path.Combine(pagesFolder, pageId);
            var pageJsonFile = Path.Combine(pageFolder, "page.json");

            if (!File.Exists(pageJsonFile))
                continue;

            var json = File.ReadAllText(pageJsonFile);
            var pageDefinition = JsonSerializer.Deserialize<PageDefinition>(json);

            if (pageDefinition is null)
                continue;

            report.Pages.Add(new ReportPage
            {
                PageId = pageDefinition.Name,
                Name = pageDefinition.DisplayName,
                FolderPath = pageFolder
            });
        }
    }

    private static IReadOnlyList<string> ReadPageOrder(string pagesFolder)
    {
        var pagesJsonFile = Path.Combine(pagesFolder, "pages.json");

        if (!File.Exists(pagesJsonFile))
            return Directory
                .EnumerateDirectories(pagesFolder)
                .Select(Path.GetFileName)
                .Where(name => !string.IsNullOrWhiteSpace(name))
                .Cast<string>()
                .ToList();

        var json = File.ReadAllText(pagesJsonFile);
        var metadata = JsonSerializer.Deserialize<PagesMetadata>(json);

        return metadata?.PageOrder ?? [];
    }

    private sealed class PagesMetadata
    {
        [JsonPropertyName("pageOrder")]
        public List<string> PageOrder { get; init; } = [];
    }

    private sealed class PageDefinition
    {
        [JsonPropertyName("name")]
        public required string Name { get; init; }

        [JsonPropertyName("displayName")]
        public required string DisplayName { get; init; }
    }
}
