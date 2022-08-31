using API.Configuration;
using API.Data;
using API.Models.EntityModels;
using API.Models.ResponseModels;
using API.Utility;

namespace API.Repository
{
    public class WeatherForecastRepository : IWeatherForcastRepository
    {
        private readonly IConfiguration _config;
        private readonly HttpClient _client;
        private readonly ILogger<WeatherForecastRepository> _logger;
        private readonly DataContext _context;
        public WeatherForecastRepository(IConfiguration config, ILogger<WeatherForecastRepository> logger, DataContext context)
        {
            _config = config;
            _client = new HttpClient();
            _logger = logger;
            _context = context;
        }

        public async Task<WeatherPayloadModel> GetWeatherByCity(string city)
        {
            var payloadModel = new WeatherPayloadModel();

            var weatherApiConfig = new WeatherApiConfig()
            {
                ApiKey = _config["WeatherApiSettings:ApiKey"],
                ApiUrlCity = _config["WeatherApiSettings:ApiUrlCity"]
            };

            var url = string.Format(weatherApiConfig.ApiUrlCity, city, weatherApiConfig.ApiKey);

            try
            {
                var apiResponse = await _client.GetAsync(url);
                var stringResponse = await apiResponse.Content.ReadAsStringAsync();

                if (apiResponse.IsSuccessStatusCode)
                    payloadModel = Helper.GeneratePayload(stringResponse);
                else
                    throw new HttpRequestException(apiResponse.ReasonPhrase);
            }
            catch (Exception ex) { _logger.LogError(ex, ex.Message); }

            if (payloadModel.WeatherForecast != null)
                await AddWeatherHistoryAsync(payloadModel);

            var weatherHistory = GetWeatherHistory(payloadModel.WeatherForecast[0].City);
            payloadModel.WeatherHistory = weatherHistory;
            return payloadModel;
        }

        public async Task<WeatherPayloadModel> GetWeatherByZipCode(string zipCode)
        {
            var payloadModel = new WeatherPayloadModel();

            var weatherApiConfig = new WeatherApiConfig()
            {
                ApiKey = _config["WeatherApiSettings:ApiKey"],
                ApiUrlZipCode = _config["WeatherApiSettings:ApiUrlZipCode"]
            };

            var url = string.Format(weatherApiConfig.ApiUrlZipCode, zipCode, weatherApiConfig.ApiKey);

            try
            {
                var apiResponse = await _client.GetAsync(url);
                var stringResponse = await apiResponse.Content.ReadAsStringAsync();

                if (apiResponse.IsSuccessStatusCode)
                    payloadModel = Helper.GeneratePayload(stringResponse);
                else
                    throw new HttpRequestException(apiResponse.ReasonPhrase);
            }
            catch (Exception ex) { _logger.LogError(ex, ex.Message); }

            if (payloadModel.WeatherForecast != null)
                await AddWeatherHistoryAsync(payloadModel);

            var weatherHistory = GetWeatherHistory(payloadModel.WeatherForecast[0].City);
            payloadModel.WeatherHistory = weatherHistory;
            return payloadModel;
        }

        public async Task AddWeatherHistoryAsync(WeatherPayloadModel model)
        {
           
            var weatherHistory = new WeatherHistory()
            {
                City = model.WeatherForecast[0].City,
                Date = model.WeatherForecast[0].Date,
                Temperature = model.WeatherForecast[0].CurrentTemperature,
                Humidity = model.WeatherForecast[0].CurrentHumidity
            };
            try
            {
                await _context.Weather.AddAsync(weatherHistory);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex) { _logger.LogError(ex, ex.Message); }
           
        }

        public List<WeatherHistory> GetWeatherHistory(string city)
        {
            var weatherHistory = _context.Weather.Where(x => x.City == city)
                                         .GroupBy(temp => temp.Temperature)
                                         .Select(x => x.First()).ToList();
            weatherHistory.Reverse();
            weatherHistory = weatherHistory.Take(4).ToList();
            return weatherHistory;
        }
    }
}
