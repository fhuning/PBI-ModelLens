using System.Text.Json;
using PowerBICleanup.Core.Models;

namespace PowerBICleanup.Engine.Readers;

public sealed class PageReader
{
    public IReadOnlyList<PbipPage> ReadPages(string pbipRootFolder)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(pbipRootFolder);

        if (!Directory.Exists(pbipRootFolder))
        {
            throw new DirectoryNotFoundException(pbipRootFolder);
        }

        var pageFiles = Directory
            .GetFiles(pbipRootFolder, "page.json", SearchOption.AllDirectories)
            .OrderBy(file => file)
            .ToList();

        var pages = new List<PbipPage>();

        foreach (var pageFile in pageFiles)
        {
            using var document = JsonDocument.Parse(File.ReadAllText(pageFile));
            var root = document.RootElement;

            var folder = Path.GetDirectoryName(pageFile) ?? "";
            var id = Path.GetFileName(folder);

            pages.Add(new PbipPage
            {
                Id = id,
                DisplayName = GetOptionalString(root, "displayName")
                    ?? GetOptionalString(root, "name")
                    ?? id,
                FolderPath = folder
            });
        }

        return pages;
    }

    private static string? GetOptionalString(JsonElement element, string propertyName)
    {
        return element.TryGetProperty(propertyName, out var property)
            && property.ValueKind == JsonValueKind.String
                ? property.GetString()
                : null;
    }
}
