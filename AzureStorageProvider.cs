using Azure.Storage.Blobs;

public class AzureStorageProvider
{
    private readonly BlobContainerClient _containerClient;

    public AzureStorageProvider(string urisas, string containerName)
    {
        var blobServiceClient = new BlobServiceClient(new Uri(urisas));
        _containerClient = blobServiceClient.GetBlobContainerClient(containerName);
    }

    public async Task<IList<string>> GetBlobNameList()
    {
        var list = new List<string>();

        await foreach (var blobItem in _containerClient.GetBlobsAsync())
        {
            list.Add(blobItem.Name);
        }

        return list;
    }

    public async Task UploadFileByPath(string filePath, string destination)
    {
        await _containerClient.GetBlobClient(destination).UploadAsync(filePath, true);
    }
}
