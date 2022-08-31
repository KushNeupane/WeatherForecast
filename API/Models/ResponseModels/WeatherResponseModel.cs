using API.Models.EntityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models.ResponseModels
{
    public class WeatherResponseModel
    {
        
        public string City { get; set; }
        public int CurrentTemperature { get; set; }
        public int CurrentHumidity { get; set; }
        public string Date { get; set; }
        public int AverageTemp { get; set; }
        public int AverageHumidity { get; set; }
        public double AverageWindSpeed { get; set; }
       
    }
}