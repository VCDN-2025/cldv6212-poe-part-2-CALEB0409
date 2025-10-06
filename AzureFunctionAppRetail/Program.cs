using Azure.Data.Tables;
using Azure.Storage.Blobs;
using Azure.Storage.Files.Shares;
using Azure.Storage.Queues;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

// References / Tutorials that helped with this setup:
// 1. Azure Functions with Blob, Queue, Table, File Storage: https://youtu.be/l7s5u-QzYe8?si=KLrVrYtDgl6VvcaZ
// 2. Dependency Injection in Azure Functions: https://youtu.be/zP4umzRCsTM?si=5U9tBorvbXiBzLc3
// 3. Azure Functions File Upload Example: https://youtu.be/r-VksPFfFpE?si=OrdAJsfNcu1GuJvR
// 4. I used GITHUB Copilot in Visual Studio to fix certain errors and and for missing or outstandig code

var host = new HostBuilder()
    .ConfigureFunctionsWebApplication() // Set up Azure Functions worker runtime
    .ConfigureServices(services =>
    {
        // Get your connection string from local.settings.json or environment variables
        var connectionString = Environment.GetEnvironmentVariable("AzureWebJobsStorage");

        if (string.IsNullOrEmpty(connectionString))
            throw new InvalidOperationException("AzureWebJobsStorage connection string is not set.");

        // Register Azure clients for dependency injection
        services.AddSingleton(new TableServiceClient(connectionString));
        services.AddSingleton(new BlobServiceClient(connectionString));
        services.AddSingleton(new ShareServiceClient(connectionString));

        // Register your FileFunction class
        services.AddScoped<AzureFunctionAppRetail.FileFunction>();

        // Add logging and monitoring
        services.AddApplicationInsightsTelemetryWorkerService();
        services.ConfigureFunctionsApplicationInsights();
    })
    .Build();

// Run the host
await host.RunAsync();
