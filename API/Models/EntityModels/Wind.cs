using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models.EntityModels
{
    public class Wind
    {
        [JsonProperty("Speed")]
        public double Speed { get; set; }
    }
}