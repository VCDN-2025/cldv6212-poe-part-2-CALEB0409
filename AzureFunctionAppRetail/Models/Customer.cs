using Azure;
using Azure.Data.Tables;
using AzureFunctionAppRetail.Models;

namespace AzureFunctionAppRetail.Models
{
    public class Customer : ITableEntity
    {
        // Azure Table Storage entity (CLDV6212 Guide – Step A: Azure Tables)
        // Store customer profiles and product information
        // Timestamp & ETag are required for concurrency control (Guide, Step E)

        // Azure Table Storage properties
        public string PartitionKey { get; set; } = "CUSTOMER";   // Groups related rows (Guide Step E)
        public string RowKey { get; set; } = Guid.NewGuid().ToString(); // Unique identifier in partition (Guide Step E)
        public DateTimeOffset? Timestamp { get; set; }           // Auto-managed by Azure (Guide Step E)
        public ETag ETag { get; set; }                           // Optimistic concurrency check (Guide Step E)

        // Custom properties for Customer Profile (CLDV6212 Guide – Step A requirement: profiles stored in Azure Tables)
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
    }
}

