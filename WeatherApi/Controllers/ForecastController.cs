using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using WeatherApi.Models;

namespace WeatherApi.Controllers
{
    [Route("api/[controller]")]
    public class ForecastController : Controller
    {
        // GET: api/forecast/city
        /// <summary>
        /// Get forecast for specific city
        /// </summary>
        /// <param name="city">name of city</param>
        [HttpGet("{city}")]
        public IActionResult GetForecastFor5Days(string city)
        {
            string url = Config.BaseUrl + Config.Forecast + city + Config.Celsium + Config.Appid;

            var request = (HttpWebRequest)WebRequest.Create(url);
            HttpWebResponse response = new HttpWebResponse();

            try
            {
                response = (HttpWebResponse)request.GetResponse();

                string forecastWeather = "";
                using (var streamReader = new StreamReader(response.GetResponseStream()))
                {
                    forecastWeather = streamReader.ReadToEnd();
                }

                return StatusCode((int)response.StatusCode, JsonConvert.DeserializeObject<ForecastWeather>(forecastWeather));
            }
            catch (WebException webException)
            {
                int statusCode = (int)((HttpWebResponse)webException.Response).StatusCode;
                return StatusCode(statusCode, $"Ошибка {webException.Status}: {webException.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode(400, "Bad Request");
            }
        }
    }
}
