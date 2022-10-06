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
    public static class IceCreamRatingFunction
    {

        [FunctionName("GetRating")]
        public static IActionResult Run(
          [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "GetRating")] HttpRequest req,
          [CosmosDB(databaseName: "icecreamdb", collectionName: "icecreamcontainer", ConnectionStringSetting = "cosmosDbConn", SqlQuery = "SELECT * FROM c")] IEnumerable<IceCreamRating> iceCreamRatings,
          ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");
            string id = req.Query["id"];

            if (string.IsNullOrEmpty(id))
            {
                return new BadRequestResult();
            }
            if (iceCreamRatings is null)
            {
                return new NotFoundResult();
            }

            var result = iceCreamRatings.Where(r => r.Id == id);

            return new OkObjectResult(result);


        }


    }
}
