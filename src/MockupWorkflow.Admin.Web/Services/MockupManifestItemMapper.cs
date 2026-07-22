using MockupWorkflow.Shared.Manifests;
using MockupWorkflow.Shared.Models;

namespace MockupWorkflow.Admin.Web.Services;

public sealed class MockupManifestItemMapper
{
    public PodItem Map(
        MockupManifestRecord record,
        string batchId)
    {
        ArgumentNullException.ThrowIfNull(record);

        if (string.IsNullOrWhiteSpace(batchId))
        {
            throw new ArgumentException(
                "Batch ID is required.",
                nameof(batchId));
        }

        var normalizedSourcePath =
            record.SourceImageFile.Replace('\\', '/');

        var filename =
            normalizedSourcePath.Split('/').Last();

        var folderName =
            CreateFolderName(record.ArtworkId);

        return new PodItem
        {
            BatchId = batchId,
            ProductType = record.ProductType,
            Phrase = record.ArtworkId,
            FolderName = folderName,
            ExpectedFolderName = folderName,
            Filename = filename,
            MockupRoot = "/data/builds",
            SourceKey =
                $"{batchId}:{record.ProductType}:{folderName}"
        };
    }

    private static string CreateFolderName(
        string artworkId)
    {
        if (string.IsNullOrWhiteSpace(artworkId))
        {
            throw new ArgumentException(
                "Artwork ID is required.",
                nameof(artworkId));
        }

        return artworkId.Replace('.', '-');
    }
}