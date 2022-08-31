using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace API.Models.EntityModels
{
    public class Weathers
    {
        public Weathers()
       {
            Main = new Main();
            Wind = new Wind();
        }
        [JsonProperty("Dt")]
        public int Dt { get; set; }

        [JsonProperty("Main")]
        public Main Main { get; set; }

        [JsonProperty("Wind")]
        public Wind Wind { get; set; }

        [JsonProperty("Dt_txt")]
        public string Dt_txt { get; set; }
        
    }
}