using API.Models.EntityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models.ResponseModels
{
    public class WeatherPayloadModel
    {
        public WeatherPayloadModel()
        {
            WeatherForecast = new List<WeatherResponseModel>();
        }
        public string City { get; set; }
        public List<WeatherResponseModel> WeatherForecast { get; set; }
        public List<WeatherHistory> WeatherHistory { get; set; }
    }
}