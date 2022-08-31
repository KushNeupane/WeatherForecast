using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models.EntityModels
{
    public class Main
    {
        [JsonProperty("Temp")]
        public double Temp { get; set; }

        [JsonProperty("Humidity")]
        public int Humidity { get; set; }
       
    }
}