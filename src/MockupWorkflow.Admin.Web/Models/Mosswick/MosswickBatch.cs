using MockupWorkflow.Shared.Manifests;

public sealed class MosswickBatch
{
    public required string BatchFolder { get; init; }

    public required string ManifestPath { get; init; }

    public required IReadOnlyList<MockupManifestRecord> Records { get; init; }
}