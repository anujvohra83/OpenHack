using System;
using System.Collections.Generic;
using Microsoft.Azure.Documents;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using FunctionCosmosDb.Models;

namespace FunctionCosmosDb
{
    public static class Function1
    {
        [FunctionName("Function1")]
        public static void Run([CosmosDBTrigger(
            databaseName: "icecreamdb",
            collectionName: "icecreamcontainer",
            ConnectionStringSetting = "cosmosDbConn",
            LeaseCollectionName = "leases",
            CreateLeaseCollectionIfNotExists= true)]IReadOnlyList<Document> input,
            ILogger log)
        {
            if (input != null && input.Count > 0)
            {
                log.LogInformation("Documents modified " + input.Count);
                log.LogInformation("First document Id " + input[0].Id);
                log.LogInformation("Document " + input[0].ToString());
            }
        }
    }
}
