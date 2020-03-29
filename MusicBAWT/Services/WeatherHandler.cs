using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using MusicBAWT.Objects;

namespace MusicBAWT.Services
{
    /// <summary>
    /// HTTP request client for weather requests to the OpenWeatherMap API
    /// </summary>
    public class WeatherHandler
    {
        static HttpClient client = new HttpClient();
        private readonly IConfiguration _config;
        private readonly string _APIKey;
        public WeatherHandler()
        {
            // create the configuration
            var _builder = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile(path: "config.json");  

            // Build the config and get the API key
            _config = _builder.Build();
            _APIKey = _config["APIWeatherKey"];
        }
        
        
        /// <summary>
        /// Return weather object of requested location
        /// </summary>
        /// <param name="location">Requested location</param>
        /// <returns>Weather object</returns>
        public async Task<Weather> getWeather(string location)
        {
            Weather weather = null;
            
            string requestURI = "http://api.openweathermap.org/data/2.5/weather?q=" + location + "&appid=" + _APIKey;
            
            HttpResponseMessage response = await client.GetAsync(requestURI);

            string resultJSON = await response.Content.ReadAsStringAsync();
            weather = JsonSerializer.Deserialize<IntermediateWeather>(resultJSON);
            
            return weather;
        }
        
    }
}    