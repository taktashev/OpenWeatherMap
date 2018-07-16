using System;
using System.IO;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WeatherApi.Models;

namespace WeatherApi.Controllers
{
    [Route("api/[controller]")]
    public class CurrentWeatherController : Controller
    {
        // GET: api/currentweather/city
        /// <summary>
        /// Get information about current weather in specific city
        /// </summary>
        /// <param name="city">name of city</param>
        [HttpGet("{city}")]
        public IActionResult GetCurrentWeather(string city)
        {
            string url = Config.BaseUrl + Config.Current + city + Config.Celsium + Config.Appid;

            var request = (HttpWebRequest)WebRequest.Create(url);
            HttpWebResponse response = new HttpWebResponse();

            try
            {
                response = (HttpWebResponse)request.GetResponse();

                string currentWeather = "";
                using (var streamReader = new StreamReader(response.GetResponseStream()))
                {
                    currentWeather = streamReader.ReadToEnd();
                }
                
                return StatusCode((int)response.StatusCode, JsonConvert.DeserializeObject<CurrentWeather>(currentWeather));
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
