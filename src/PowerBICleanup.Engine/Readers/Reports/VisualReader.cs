using System.Text.Json;
using System.Text.Json.Serialization;
using PowerBICleanup.Core.Models;

namespace PowerBICleanup.Engine.Readers.Reports;

public sealed class VisualReader
{
    public void Read(ReportPage page)
    {
        var visualsFolder = Path.Combine(page.FolderPath, "visuals");

        if (!Directory.Exists(visualsFolder))
            return;

        foreach (var visualFolder in Directory.EnumerateDirectories(visualsFolder))
        {
            var visualJsonFile = Path.Combine(visualFolder, "visual.json");

            if (!File.Exists(visualJsonFile))
                continue;

            var json = File.ReadAllText(visualJsonFile);
            var definition = JsonSerializer.Deserialize<VisualDefinition>(json);
            var title = ReadTitle(json);

            if (definition?.Name is null)
                continue;

            if (definition.Visual?.VisualType is null)
                continue;

            page.Visuals.Add(new Visual
            {
                VisualId = definition.Name,
                VisualType = definition.Visual.VisualType,
                Title = title,
                FolderPath = visualFolder
            });
            if (definition?.Name is null)
                continue;

            if (definition.Visual?.VisualType is null)
                continue;

            page.Visuals.Add(new Visual
            {
                VisualId = definition.Name,
                VisualType = definition.Visual.VisualType,
                FolderPath = visualFolder
            });
        }
    }

private static string? ReadTitle(string json)
{
    using var document = JsonDocument.Parse(json);

    var root = document.RootElement;

    if (!root.TryGetProperty("visualContainerObjects", out var container))
        return null;

    if (!container.TryGetProperty("title", out var titles))
        return null;

    if (titles.GetArrayLength() == 0)
        return null;

    var first = titles[0];

    if (!first.TryGetProperty("properties", out var properties))
        return null;

    if (!properties.TryGetProperty("text", out var text))
        return null;

    if (!text.TryGetProperty("expr", out var expr))
        return null;

    if (!expr.TryGetProperty("Literal", out var literal))
        return null;

    if (!literal.TryGetProperty("Value", out var value))
        return null;

    var title = value.GetString();

    if (string.IsNullOrWhiteSpace(title))
        return null;

    return title.Trim('\'');
}

    private sealed class VisualDefinition
    {
        [JsonPropertyName("name")]
        public string? Name { get; init; }

        [JsonPropertyName("visual")]
        public VisualMetadata? Visual { get; init; }
    }

    private sealed class VisualMetadata
    {
        [JsonPropertyName("visualType")]
        public string? VisualType { get; init; }
    }
}
