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

            foreach (var weather in weatherGroup)
            {               
                var weatherResponseList = new WeatherResponseModel();
                
                weatherResponseList.AverageTemp = (int)weather.weathers.Average(x => x.Main.Temp - 273);
                weatherResponseList.AverageHumidity = (int)weather.weathers.Average(x => x.Main.Humidity);
                weatherResponseList.AverageWindSpeed = Math.Round(weather.weathers.Average(x => x.Wind.Speed), 2);
                weatherResponseList.Date = weather.day.Day.ToString() + ", " + weather.day.ToString("MMMM", CultureInfo.InvariantCulture);
                weatherResponseList.CurrentTemperature = (int)weather.weathers[0].Main.Temp - 273;
                weatherResponseList.CurrentHumidity = (int)weather.weathers[0].Main.Humidity;

                payloadModel.WeatherForecast.Add(weatherResponseList);
            }
            payloadModel.City = model.City.Name;
            return payloadModel;
        }
    }
}
