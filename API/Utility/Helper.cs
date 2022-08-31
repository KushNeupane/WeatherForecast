using API.Models.EntityModels;
using API.Models.ResponseModels;
using Newtonsoft.Json;
using System.Globalization;

namespace API.Utility
{
    public static class Helper
    {
        public static WeatherPayloadModel GeneratePayload(string serializedResponse)
        {
            var model = new RootWeather();
            var payloadModel = new WeatherPayloadModel();

            model = JsonConvert.DeserializeObject<RootWeather>(serializedResponse);

            // Grouping the collection of data to arrange further
            var weatherGroup = model.Weathers
                .GroupBy(x => DateTime.Parse(x.Dt_txt).Date,
                            x => new { x.Main, x.Wind },
                            (key, g) => new { day = key, weathers = g.ToList() });

            // Arranging the data in response model
            for (int i = 0; i < weatherGroup.Count(); i++)
            {
                int avgTemperature = 0;
                int avgHumidity = 0;
                double avgWindSpeed = 0;
                var weatherResponseList = new WeatherResponseModel();
                var forecasts = weatherGroup.ElementAt(i);
               
                foreach (var dayWeather in forecasts.weathers)
                {
                    avgTemperature += ((int)dayWeather.Main.Temp - 273) / forecasts.weathers.Count();
                    avgHumidity += dayWeather.Main.Humidity / forecasts.weathers.Count();
                    avgWindSpeed += Math.Round((dayWeather.Wind.Speed) / forecasts.weathers.Count(), 2, MidpointRounding.AwayFromZero);
                }

                weatherResponseList.Date = forecasts.day.Day.ToString() + ", " + forecasts.day.ToString("MMMM", CultureInfo.InvariantCulture);
                weatherResponseList.City = model.City.Name;

                if(i < forecasts.weathers.Count())
                {
                    weatherResponseList.CurrentHumidity = forecasts.weathers[i].Main.Humidity;
                    weatherResponseList.CurrentTemperature = (int)forecasts.weathers[i].Main.Temp - 273;
                }                
                weatherResponseList.AverageTemp = avgTemperature;
                weatherResponseList.AverageHumidity = avgHumidity;
                weatherResponseList.AverageWindSpeed =Math.Round(avgWindSpeed, 2);
                payloadModel.WeatherForecast.Add(weatherResponseList);
            }
            return payloadModel;
        }
    }
}
