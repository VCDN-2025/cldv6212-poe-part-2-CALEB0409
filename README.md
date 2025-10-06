ABC Retail – Azure Function Integration (CLDV6212 – POE Part 2)
Student Information

Student Name: Caleb Ragaven

Student Number: ST10446908

Module Code: CLDV6212

Assessment: POE Part 2 – Integrating Azure Services

 Project Overview

This project forms Part 2 of the Cloud Development B (CLDV6212) Portfolio of Evidence (POE).
It builds on the Azure Storage Solution developed in Part 1 and introduces Azure Functions and event-driven architecture to improve scalability, automation, and cloud efficiency for ABC Retail.

ABC Retail is a growing e-commerce business migrating to Azure to handle increased transaction volumes, storage needs, and real-time processing.
This solution demonstrates how Azure Functions can be used to integrate and automate various Azure storage services.

Objectives

Integrate Azure Functions to automate core processes.

Connect each Function App to existing Azure Storage services:

Table Storage

Blob Storage

Queue Storage

File Storage

Demonstrate serverless cloud computing for scalability and cost-effectiveness.

Discuss customer experience enhancements using Azure Event Hubs and Azure Service Bus.

Azure Functions Implemented
Function Name	Purpose	Connected Azure Service	Trigger Type
TableFunction	Inserts or updates customer/product data.	Azure Table Storage	HTTP Trigger
BlobFunction	Uploads and retrieves product images.	Azure Blob Storage	HTTP Trigger
QueueFunction	Sends and reads transaction messages.	Azure Queue Storage	Queue Trigger
FileFunction	Uploads and retrieves contract files.	Azure File Storage	HTTP Trigger

Each function was created using the .NET 8 Isolated Worker Model and deployed to a Function App on Azure.

 Configuration & Setup
Prerequisites

Visual Studio 2022 (with Azure Development workload)

.NET 8 SDK

Azure Account with active subscription

Azure Storage Account (with Table, Blob, Queue, and File shares)

Setup Steps


Testing & Validation

Each function was tested using:

Postman for HTTP-triggered functions.

Azure Storage Explorer to verify data entries in Blob, Queue, Table, and File Storage.

Azure Portal Logs for monitoring trigger executions.

Function	Validation Performed	Result
TableFunction	Inserted customer data to Table Storage.	 Success
BlobFunction	Uploaded image blob and verified in container.	 Success
QueueFunction	Sent and received queue message.	 Success
FileFunction	Uploaded test contract file to File Share.	 Success
 Enhancing Customer Experience
Service	Description	Mechanism	Value to Users
Azure Event Hubs	Ingests large volumes of streaming data in real time.	Captures telemetry, order, and clickstream events.	Enables real-time analytics and personalization.
Azure Service Bus	Facilitates asynchronous, reliable message delivery.	Uses queues/topics to manage distributed communication.	Improves reliability, reduces downtime, and enhances response time.
 Technologies Used

Backend: .NET 8 Azure Functions (Isolated Worker)

Cloud Services: Azure Blob, Queue, Table, File Storage

Integration Tools: Azure Event Hub, Azure Service Bus

IDE: Visual Studio 2022

Version Control: GitHub

Deployment: Azure Function App 

 Included Evidence

The submission includes:

Screenshots of each function in Azure Portal.




GitHub repository link.

App Service and Function URLs.

 References

Microsoft. (2024). Develop Azure Functions using Visual Studio. Retrieved from: https://learn.microsoft.com/en-us/azure/azure-functions/functions-develop-vs

Microsoft. (2024). Compare Azure messaging services. Retrieved from: https://learn.microsoft.com/en-us/azure/service-bus-messaging/compare-messaging-services

Softweb Solutions. (2023). Azure Service Bus vs Event Hub: Choosing the Right Messaging Platform. Retrieved from: https://www.softwebsolutions.com/resources/azure-service-bus-vs-event-hub.html

YouTube. (2024). Azure Functions Queue Trigger Example. Retrieved from: https://www.youtube.com/watch?v=IQ3gtODpVYM

Conclusion

This project demonstrates a serverless, event-driven retail application using Azure Functions integrated with Azure Storage Services.
By implementing queue-based messaging, blob handling, and table/file operations, this solution achieves scalability, cost-efficiency, and reliability — key benefits of cloud-native architecture on Microsoft Azure.
