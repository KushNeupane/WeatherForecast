

using API.Models.EntityModels;
using API.Models.ResponseModels;

namespace API.Repository
{
    public interface IWeatherForcastRepository
    {
        Task<WeatherPayloadModel> GetWeatherByCity(string city);
        Task<WeatherPayloadModel> GetWeatherByZipCode(string zipCode);
        //Task AddWeatherHistoryAsync(WeatherResponseModel model);
        List<WeatherHistory> GetWeatherHistory(string city);
    }
}
