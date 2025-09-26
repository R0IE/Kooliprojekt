using System.Collections.Generic;

namespace KooliProjekt.Models
{
    public class WeatherData
    {
        public CurrentWeather Current { get; set; }
        public HourlyForecast Hourly { get; set; }
    }

    public class CurrentWeather
    {
        public float Temperature2m { get; set; }
        public float WindSpeed10m { get; set; }
    }

    public class HourlyForecast
    {
        public List<float> Temperature2m { get; set; }
        public List<float> RelativeHumidity2m { get; set; }
        public List<float> WindSpeed10m { get; set; }
    }
}
