using System.Text.Json;

namespace PowerBICleanup.Engine.Readers;

public sealed class PbipFileReader
{
    public PbipMetadata Read(string pbipFile)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(pbipFile);

        var json = File.ReadAllText(pbipFile);

        using var document = JsonDocument.Parse(json);

        var root = document.RootElement;

        var reportFolder = TryGetPath(root, "report");
        var semanticModelFolder = TryGetPath(root, "semanticModel");

        return new PbipMetadata
        {
            ReportFolder = reportFolder,
            SemanticModelFolder = semanticModelFolder
        };
    }

    private static string? TryGetPath(JsonElement root, string propertyName)
    {
        if (!root.TryGetProperty(propertyName, out var element))
            return null;

        if (!element.TryGetProperty("path", out var pathElement))
            return null;

        return pathElement.GetString();
    }
}
