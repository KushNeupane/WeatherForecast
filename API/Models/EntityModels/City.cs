using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models.EntityModels
{
    public class City
    {

        [JsonProperty("Name")]
        public string Name { get; set; }

        [JsonProperty("Country")]
        public string Country { get; set; }
       
    }
}