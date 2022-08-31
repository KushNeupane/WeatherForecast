using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Configuration
{
    public class WeatherApiConfig
    {
        public string ApiKey { get; set; }
        public string ApiUrlCity { get; set; }
        public string ApiUrlZipCode { get; set; }
    }
}