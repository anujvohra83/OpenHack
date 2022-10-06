using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using FunctionCosmosDb.Models;
using System.Collections.Generic;
using System.Linq;

namespace FunctionCosmosDb
{
    public static class IceCreamRatingGetAllFunction
    {

        [FunctionName("GetAllRatings")]
        public static IActionResult Run(
          [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "GetAllRatings")] HttpRequest req,
          [CosmosDB(databaseName: "icecreamdb", collectionName: "icecreamcontainer", ConnectionStringSetting = "cosmosDbConn", SqlQuery = "SELECT * FROM c")] IEnumerable<IceCreamRating> iceCreamRatings,
          ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");
           
            if (iceCreamRatings is null)
            {
                return new NotFoundResult();
            }

            return new OkObjectResult(iceCreamRatings);
        }


    }
}
