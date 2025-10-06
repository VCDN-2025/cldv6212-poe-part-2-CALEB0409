using Azure.Data.Tables;
using AzureFunctionAppRetail.Models;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using System.Web;

namespace AzureFunctionAppRetail
{
    public class TableStorageFunction
    {
        private readonly ILogger _logger;

        public TableStorageFunction(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<TableStorageFunction>();
        }

     

        [Function("AddCustomers")]
        public async Task<HttpResponseData> AddCustomer(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "AddCustomers")] HttpRequestData req)
        {
            try
            {
                var body = await new StreamReader(req.Body).ReadToEndAsync();

                if (string.IsNullOrEmpty(body))
                {
                    var errorResponse = req.CreateResponse(HttpStatusCode.BadRequest);
                    await errorResponse.WriteStringAsync("Request body is empty");
                    return errorResponse;
                }

                var customer = JsonSerializer.Deserialize<Customer>(body);

                if (customer == null)
                {
                    var errorResponse = req.CreateResponse(HttpStatusCode.BadRequest);
                    await errorResponse.WriteStringAsync("Invalid customer data");
                    return errorResponse;
                }

                // Create TableServiceClient directly
                var connectionString = Environment.GetEnvironmentVariable("AzureWebJobsStorage") ?? "UseDevelopmentStorage=true";
                var tableServiceClient = new TableServiceClient(connectionString);

                var tableClient = tableServiceClient.GetTableClient("Customers");
                await tableClient.CreateIfNotExistsAsync();
                await tableClient.AddEntityAsync(customer);

                _logger.LogInformation("Added new customer: {FirstName} {LastName}", customer.FirstName, customer.LastName);

                var response = req.CreateResponse(HttpStatusCode.OK);
                response.Headers.Add("Content-Type", "application/json");
                await response.WriteStringAsync(JsonSerializer.Serialize(new
                {
                    message = "Customer added successfully",
                    customerId = customer.RowKey
                }));
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error adding customer: {ex.Message}");
                var errorResponse = req.CreateResponse(HttpStatusCode.InternalServerError);
                await errorResponse.WriteStringAsync($"Error: {ex.Message}");
                return errorResponse;
            }
        }

        [Function("GetCustomers")]
        public async Task<HttpResponseData> GetCustomers(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "GetCustomers")] HttpRequestData req)
        {
            try
            {
                // Create TableServiceClient directly
                var connectionString = Environment.GetEnvironmentVariable("AzureWebJobsStorage") ?? "UseDevelopmentStorage=true";
                var tableServiceClient = new TableServiceClient(connectionString);

                var tableClient = tableServiceClient.GetTableClient("Customers");
                var Customers = new List<Customer>();

                await foreach (var customer in tableClient.QueryAsync<Customer>())
                {
                    Customers.Add(customer);
                }

                var response = req.CreateResponse(HttpStatusCode.OK);
                response.Headers.Add("Content-Type", "application/json");
                await response.WriteStringAsync(JsonSerializer.Serialize(Customers));
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error getting customers: {ex.Message}");
                var errorResponse = req.CreateResponse(HttpStatusCode.InternalServerError);
                await errorResponse.WriteStringAsync($"Error: {ex.Message}");
                return errorResponse;
            }
        }

      
    }
}