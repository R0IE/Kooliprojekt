namespace KooliProjekt.ViewModels
{
    public class WeatherViewModel
    {
        public float CurrentTemperature { get; set; }
        public float CurrentWindSpeed { get; set; }
        public List<float> HourlyTemperatures { get; set; }
        public List<float> HourlyHumidity { get; set; }
        public List<float> HourlyWindSpeed { get; set; }
    }
}
