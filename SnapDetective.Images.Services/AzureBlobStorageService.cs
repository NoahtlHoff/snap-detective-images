using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using SnapDetective.Images.Interfaces;

namespace SnapDetective.Images.Services;

public class AzureBlobStorageService(BlobServiceClient blobServiceClient) : IBlobStorageService
{
    private const string ContainerName = "images";

    public async Task<string> UploadAsync(Stream fileStream, string fileName, string contentType)
    {
        var container = blobServiceClient.GetBlobContainerClient(ContainerName);
        await container.CreateIfNotExistsAsync(PublicAccessType.Blob);

        var uniqueFileName = $"{Guid.NewGuid()}{Path.GetExtension(fileName)}";
        var blob = container.GetBlobClient(uniqueFileName);
        await blob.UploadAsync(fileStream, new BlobHttpHeaders { ContentType = contentType });

        return blob.Uri.ToString();
    }

    public async Task DeleteAsync(string fileName)
    {
        var container = blobServiceClient.GetBlobContainerClient(ContainerName);
        var blob = container.GetBlobClient(fileName);
        await blob.DeleteIfExistsAsync();
    }
}