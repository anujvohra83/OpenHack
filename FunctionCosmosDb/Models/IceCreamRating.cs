using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace FunctionCosmosDb.Models
{
    public class IceCreamRating
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        public string UserId { get; set; }
        public string ProductId { get; set; }
        public string LocationName { get; set; }
        public DateTime TimeStamp { get; set; } 
        public int Rating { get; set; }
        public string UserNotes { get; set; }
    }
}
