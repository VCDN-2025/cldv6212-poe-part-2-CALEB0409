using System;
using System.IO;
using System.Threading.Tasks;
using Azure.Storage.Blobs;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

namespace AzureFunctionAppRetail
{
    public class BlobStorageFunction
    {
        // Azure Blob container client used to interact with the blob storage container
        private readonly BlobContainerClient _containerClient;

        // Constructor: Initialize Blob service and container
        public BlobStorageFunction()
        {
            // Get Azure Storage connection string from environment variables
            string connectionString = Environment.GetEnvironmentVariable("AzureWebJobsStorage");

            // Create a BlobServiceClient to interact with Blob Storage
            var blobServiceClient = new BlobServiceClient(connectionString);

            // Get a reference to the container named "uploads"
            _containerClient = blobServiceClient.GetBlobContainerClient("uploads");

            // Create the container if it does not exist
            _containerClient.CreateIfNotExists();
        }

        // Azure Function definition for uploading a blob
        [Function("UploadBlob")]
        public async Task<HttpResponseData> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post")] HttpRequestData req, // HTTP trigger for POST requests
            FunctionContext context) // Function execution context for logging
        {
            // Get a logger to log information or errors
            var logger = context.GetLogger("UploadBlob");

            // Generate a unique blob name using a GUID
            string blobName = $"file-{Guid.NewGuid()}.txt";

            // Get a reference to the blob inside the container
            var blobClient = _containerClient.GetBlobClient(blobName);

            // Copy the request body (file content) into a memory stream
            using var stream = new MemoryStream();
            await req.Body.CopyToAsync(stream);
            stream.Position = 0; // Reset stream position before upload

            // Upload the file content to the blob storage
            await blobClient.UploadAsync(stream, overwrite: true);

            // Log that the blob was successfully uploaded
            logger.LogInformation($"Blob {blobName} uploaded.");

            // Create an HTTP response with status 200 OK
            var response = req.CreateResponse(System.Net.HttpStatusCode.OK);

            // Return the uploaded blob name in the response
            await response.WriteStringAsync($"Blob uploaded: {blobName}");

            return response;
        }
    }
}
