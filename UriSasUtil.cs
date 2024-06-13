using Azure.Storage;
using Azure.Storage.Blobs;
using Azure.Storage.Sas;

public static class UriSasUtil
{
    public static string Generate(string containerName, string storageAccountName, string storageAcountKey)
    {
        // Definimos aqui as credenciais e a baseUri baseado na conta de armazenamento
        var baseUri = new Uri($"https://{storageAccountName}.blob.core.windows.net");
        var credentials = new StorageSharedKeyCredential(storageAccountName, storageAcountKey);

        // A instância do BlobServiceClient é definida usando os parâmetros
        var blobServiceClient = new BlobServiceClient(baseUri, credentials);
        var containerClient = blobServiceClient.GetBlobContainerClient(containerName);

        // Aqui definimos a política de compartilhamento da SAS Uri que será gerada
        var sasBuilder = new BlobSasBuilder
        {
            BlobContainerName = containerClient.Name, // Nome do container que será compartilhado
            Resource = "c", // "c" indica que o recurso compartilhado é um container
            StartsOn = DateTimeOffset.UtcNow, // Dada de início da vigência da SAS Uri
            ExpiresOn = DateTimeOffset.UtcNow.AddHours(1), // Data que a SAS Uri expira
        };

        // Aqui definimos quais permissões a SAS Uri terá. No nosso caso, apenas permissão de escrita.
        sasBuilder.SetPermissions(BlobContainerSasPermissions.Write);

        // Aqui geramos e convertemos para string a SAS Uri.
        return containerClient.GenerateSasUri(sasBuilder).ToString();
    }
}