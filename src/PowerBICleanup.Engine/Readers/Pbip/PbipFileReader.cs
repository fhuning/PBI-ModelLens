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

        var reportFolder = TryGetArtifactPath(root, "report");
        var semanticModelFolder = TryGetArtifactPath(root, "semanticModel");

        return new PbipMetadata
        {
            ReportFolder = reportFolder,
            SemanticModelFolder = semanticModelFolder
        };
    }

    private static string? TryGetArtifactPath(
        JsonElement root,
        string artifactName)
    {
        if (!root.TryGetProperty("artifacts", out var artifactsElement))
            return null;

        foreach (var artifact in artifactsElement.EnumerateArray())
        {
            if (!artifact.TryGetProperty(artifactName, out var artifactElement))
                continue;

            if (!artifactElement.TryGetProperty("path", out var pathElement))
                return null;

            return pathElement.GetString();
        }

        return null;
    }
}
