using System;
using System.Collections.Generic;

namespace WeatherApi.Models
{
    /// <summary>
    /// Class that contains information about current weather
    /// </summary>
    public class CurrentWeather
    {
        public string Name { get; set; }
        public DateTime ServerDateTime { get; set; }
        public TemperatureInfo Main { get; set; }
        public WindInfo Wind { get; set; }
        public CloudsInfo Clouds { get; set; }
        public List<WeatherInfo> Weather { get; set; }

        private int dt;
        public int Dt
        {
            get { return dt; }
            set
            {
                dt = value;
                ServerDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0).AddSeconds(dt).ToLocalTime();
            }
        }
    }
}
