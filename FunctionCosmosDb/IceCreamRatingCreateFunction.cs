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

namespace FunctionCosmosDb
{
    public static class IceCreamRatingCreateFunction
    {
     
        [FunctionName("CreateRating")]
        public static async Task<IActionResult> Run(
          [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "CreateRating")] HttpRequest req,
           [CosmosDB(databaseName: "icecreamdb", collectionName: "icecreamcontainer", ConnectionStringSetting = "cosmosDbConn")]IAsyncCollector<dynamic> documentsOut,
          ILogger log)
        {
            log.LogInformation("Adding a rating.");
            string requestData = await new StreamReader(req.Body).ReadToEndAsync();

            var data = JsonConvert.DeserializeObject<IceCreamRating>(requestData);

            var item = new IceCreamRating
            {
                Id = Guid.NewGuid().ToString(),
                ProductId = data.ProductId,
                UserId = data.UserId,
                UserNotes = data.UserNotes,
                Rating = data.Rating,
                LocationName = data.LocationName,
                TimeStamp = DateTime.Now
            };

            await documentsOut.AddAsync(item);

            return new OkObjectResult(item);

        }

      
    
    }
}
