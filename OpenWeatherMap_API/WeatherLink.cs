using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenWeatherMap_API
{
    public class WeatherLink
    {
        private HttpClient _client;

        public WeatherLink(HttpClient client)
        {
            _client = client;
        }

        public string Weather()
        {
            var weatherURL = "https://api.openweathermap.org/data/2.5/weather?q=Gurnee,US&units=imperial&appid=d6932d83f438050cdbfaffdafef0b5db";

            var getWeather = _client.GetAsync(weatherURL).Result;

            var data = JObject.Parse(getWeather.Content.ReadAsStringAsync().Result);

            string city = data["name"].ToString();
            string description = data["weather"][0]["description"].ToString();
            string temperature = data["main"]["temp"].ToString();
            string humidity = data["main"]["humidity"].ToString();
            string windSpeed = data["wind"]["speed"].ToString();
            string windDirection = data["wind"]["deg"].ToString();

            string result = $"Weather Report for {city}\n" +
                   new string('-', 27) + "\n" +
                   $"Description : {description}\n" +
                   $"Temperature : {temperature}°F\n" +
                   $"Humidity    : {humidity}%\n" +
                   $"Wind Speed  : {windSpeed} mph\n" +
                   $"Wind Dir.   : {windDirection}°\n" +
                   new string('-', 27);

            return result;

        }
    }
}
