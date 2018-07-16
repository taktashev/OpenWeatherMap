using System.Collections.Generic;

namespace WeatherApi.Models
{
    /// <summary>
    /// Class that contains information about the forecast
    /// </summary>
    public class ForecastWeather
    {
        public CityInfo City { get; set; }
        public List<ForecastInfo> List { get; set; }
    }
}
