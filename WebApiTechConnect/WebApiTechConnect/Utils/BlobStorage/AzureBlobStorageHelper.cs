using Azure.Storage.Blobs;
using WebApiTechConnect.Domains;

namespace WebApiTechConnect.Utils.BlobStorage
{
    public class AzureBlobStorageHelper
    {

        private static readonly string ContainerBlobStorageName = Environment.GetEnvironmentVariable("ContainerBlobStorageName")!;

        private static readonly string ConnectionStringBlobStorage = Environment.GetEnvironmentVariable("ConnectionStringBlobStorage")!;

        public static async Task<Midia> UploadImageBlobAsync(IFormFile arquivo)
        {
            try
            {
                Midia novaMidia = new();
                if (arquivo != null)
                {
                    //Path.GetExtension(arquivo.FileName): pega o nome do arquivo e obtém a extensao dele. Ex: A754E556CFD4457D908D309849E44475.png

                    //gera um nome unico + extensao do arquivo
                    var blobName = "Picture" + Guid.NewGuid().ToString().Replace("-", "") + Path.GetExtension(arquivo.FileName);


                    //cria uma instancia do cliente blob service e passa a string de conexao
                    var blobServiceClient = new BlobServiceClient(ConnectionStringBlobStorage);

                    //obtem um containerclient usando o nome do container dp blob
                    var blobContainerClient = blobServiceClient.GetBlobContainerClient(ContainerBlobStorageName);

                    //obtem um blob client usando o blob name
                    var blobClient = blobContainerClient.GetBlobClient(blobName);

                    //abre o fluxo de entrada do arquivo(foto)
                    using (var stream = arquivo.OpenReadStream())
                    {

                        //carrega o arquivo para o blob storage de forma assincrona
                        await blobClient.UploadAsync(stream, true);
                    }


                    novaMidia.UrlMidia = blobClient.Uri.ToString();

                    novaMidia.BlobStorageName = blobName;


                    //retorna o exame com algumas propriedades preenchidas
                    return novaMidia;
                }
                else
                {
                    novaMidia.UrlMidia = "https://blobvitalhubfilipegoisg2.blob.core.windows.net/containervitalhubfilipegoisg2/defaultImage.png";
                    //retorna a uri de uma imagem padrao caso nenhum arquivo seja enviado
                    return novaMidia;
                }
            }
            catch (Exception e)
            {

                throw;
            }
        }
        public static async Task DeleteBlobAsync(string blobName)
        {
            try
            {
                // Cria uma instância do cliente BlobService usando a string de conexão
                var blobServiceClient = new BlobServiceClient(ConnectionStringBlobStorage);

                // Obtém um BlobContainerClient para o container onde o blob está localizado
                var blobContainerClient = blobServiceClient.GetBlobContainerClient(ContainerBlobStorageName);

                // Obtém um BlobClient para o blob que deseja deletar
                var blobClient = blobContainerClient.GetBlobClient(blobName);

                // Deleta o blob de forma assíncrona
                await blobClient.DeleteIfExistsAsync();
            }
            catch (Exception e)
            {

                throw;
            }
        }
    }
}
