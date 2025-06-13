using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using System;
using System.IO;
using System.Threading.Tasks;

namespace ShareSafelyWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly string _vaultUri = "https://sharesafelyvault2.vault.azure.net/";
        private readonly string _secretName = "BlobStorageConnectionString";
        private readonly string _containerName = "uploads";

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            if (file != null && file.Length > 0)
            {
                // 🔐 Retrieve connection string securely from Key Vault
                var client = new SecretClient(new Uri(_vaultUri), new DefaultAzureCredential());
                KeyVaultSecret secret = await client.GetSecretAsync(_secretName);
                string connectionString = secret.Value;

                // 📦 Connect to Blob container
                var blobContainerClient = new BlobContainerClient(connectionString, _containerName);
                await blobContainerClient.CreateIfNotExistsAsync();

                // 📁 Upload file
                var blobClient = blobContainerClient.GetBlobClient(file.FileName);
                using (var stream = file.OpenReadStream())
                {
                    await blobClient.UploadAsync(stream, overwrite: true);
                }

                // 🔗 Generate SAS link
                var sasUri = blobClient.GenerateSasUri(BlobSasPermissions.Read, DateTimeOffset.UtcNow.AddMinutes(30));
                ViewBag.FileLink = sasUri.ToString();
            }

            return View("Index");
        }
    }
}

