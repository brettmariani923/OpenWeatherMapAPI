using System;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json.Linq;
using OpenWeatherMap_API;

namespace OpenWeatherMapAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var client = new HttpClient();
            var weatherLink = new WeatherLink(client);

            Console.WriteLine("Fetching weather data...");
            Console.WriteLine(weatherLink.Weather());
        }
    }
}
