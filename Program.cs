// Informações necessárias para a construção da SAS Uri
var containerName = "<<nome-do-container>>";
var storageAccountName = "<<nome-da-conta>>";
var storageAcountKey = "<<chave-de-acesso-da-conta>>";

// Aqui geramos a SAS Uri
var urisas = UriSasUtil.Generate(containerName, storageAccountName, storageAcountKey);
// E definimos uma instância do provedor de armazenamento passando a SAS Uri como argumento
var provider = new AzureStorageProvider(urisas, containerName);

try
{
    /** 
    * O código abaixo deve funcionar normalmente, uma vez que
    * a SAS Uri possui permissão de escrita no container.  
    */
    var filePath = "<<caminho-físico-do-arquivo>>";
    var destination = "<<caminho-de-destino-do-arquivo-no-storage>>";

    await provider.UploadFileByPath(filePath, destination);

    Console.WriteLine("Upload Successful!");
}
catch (Exception ex)
{
    Console.WriteLine("Fail to upload.");
    Console.WriteLine(ex.Message);
}

try
{
    /**
    * O Código abaixo deve lançar uma exceção, uma vez que
    * a SAS Uri não possui permissão de leitura no container.
    */
    var list = await provider.GetBlobNameList();

    foreach (var item in list)
    {
        Console.WriteLine(item);
    }
}
catch (Exception ex)
{
    Console.WriteLine("Fail to read data.");
    Console.WriteLine(ex.Message);
}
