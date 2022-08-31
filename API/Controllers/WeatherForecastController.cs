using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Models.ResponseModels;
using API.Repository;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class WeatherForecastController : BaseApiController
    {
        private readonly IWeatherForcastRepository _weatherRepository;
        public WeatherForecastController(IWeatherForcastRepository weatherRepository)
        {
            _weatherRepository = weatherRepository;
        }
        
        [HttpGet("city/{city}", Name ="GetWeatherByCity")]        
        public async Task<WeatherPayloadModel> GetWeatherByCity(string city)
        {
            var result = await _weatherRepository.GetWeatherByCity(city);
            return result;
        }

        [HttpGet("zipcode/{zipCode}", Name ="GetWeatherByZipCode")]
        public async Task<WeatherPayloadModel> GetWeatherByZipCode(string zipCode)
        {
            var result = await _weatherRepository.GetWeatherByZipCode(zipCode);
            return result;
        }
    }
}