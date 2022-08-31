namespace API.Models.EntityModels
{
    public class WeatherHistory
    {
        public int Id { get; set; }
        public string Date { get; set; }
        public string City { get; set; }
        public int Temperature { get; set; }
        public int Humidity { get; set; }
    }
}
