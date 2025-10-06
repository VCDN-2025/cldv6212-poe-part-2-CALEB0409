using System;
using Azure.Storage.Queues.Models;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace AzureFunctionAppRetail;

public class QueueFunction
{
    private readonly ILogger<QueueFunction> _logger;

    public QueueFunction(ILogger<QueueFunction> logger)
    {
        _logger = logger;
    }

    [Function(nameof(QueueFunction))] // Linking function to Azure
    public void Run([QueueTrigger("orders", Connection = "AzureWebJobsStorage")] QueueMessage message)
    {
        _logger.LogInformation($"Queue trigger function processed message: {message.MessageText}");
        
        // Log additional message properties if needed
        _logger.LogInformation($"Message ID: {message.MessageId}");
        _logger.LogInformation($"Insertion Time: {message.InsertedOn}");
    }
}