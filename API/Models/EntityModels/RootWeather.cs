using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace API.Models.EntityModels
{
    public class RootWeather
    {
        public RootWeather()
        {
            Weathers = new List<Weathers>();
            City =  new City();
        }
        public int Cnt { get; set; }
        [JsonProperty("List")]
        public List<Weathers> Weathers { get; set; }
        
        [JsonProperty("City")]
        public City City { get; set; }
    }
}