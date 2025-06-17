Explaining the Logic

This is a simple weather application that retrieves current weather data from the OpenWeatherMap API and displays it in a clean, readable format using .NET and C#. It demonstrates fundamental concepts like dependency injection, asynchronous programming, JSON parsing, and clean output formatting.

HttpClient Injection and Constructor Setup

We start by setting up a private field and a constructor in our WeatherLink class:

private readonly HttpClient _client;

public WeatherLink(HttpClient client)
{
    _client = client;
}

HttpClient is used to send HTTP requests to web APIs.

We're injecting an instance of HttpClient via the constructor so it can be reused throughout the class.

This follows best practices for dependency injection and promotes testability and reusability.

Making the API Call – GetWeatherAsync()

We define an asynchronous method that fetches and formats the weather data:

public async Task<string> GetWeatherAsync()

Within this method:
1. Define the API URL
var weatherURL = $"https://api.openweathermap.org/data/2.5/weather?q=Gurnee,US&units=imperial&appid=...";

This constructs the full API URL to fetch weather data for Gurnee, IL in Fahrenheit.

The API key is stored as a class-level constant (you should move it to a config file or environment variable for production).

2. Make the Request
var response = await _client.GetAsync(weatherURL);
response.EnsureSuccessStatusCode();

await _client.GetAsync(...) sends an HTTP GET request asynchronously and waits for a response.

EnsureSuccessStatusCode() throws an exception if the response indicates a failure (e.g. 404 or 500).
3. Parse the JSON Response
var jsonString = await response.Content.ReadAsStringAsync();
var data = JObject.Parse(jsonString);

We read the body of the response and parse it using JObject from the Newtonsoft.Json package.

This gives us structured access to the weather data.

Extracting and Formatting Data

We then extract key fields from the parsed JSON object:

string city = data["name"]?.ToString() ?? "Unknown";
string description = data["weather"]?[0]?["description"]?.ToString() ?? "N/A";
string temperature = data["main"]?["temp"]?.ToString() ?? "N/A";
string humidity = data["main"]?["humidity"]?.ToString() ?? "N/A";
string windSpeed = data["wind"]?["speed"]?.ToString() ?? "N/A";
string windDirection = data["wind"]?["deg"]?.ToString() ?? "N/A";

Each line safely accesses a field from the JSON and provides a fallback value if the field is missing ("N/A" or "Unknown").

For example:

    data["weather"][0]["description"] gives the current weather condition (e.g. "clear sky").

    data["main"]["temp"] returns the temperature.

    data["wind"]["speed"] gives the wind speed in mph.

Output Formatting

Once the data is extracted, it's arranged into a formatted string:

return $"Weather Report for {city}\n" +
       new string('-', 27) + "\n" +
       $"Description : {description}\n" +
       $"Temperature : {temperature}°F\n" +
       $"Humidity    : {humidity}%\n" +
       $"Wind Speed  : {windSpeed} mph\n" +
       $"Wind Dir.   : {windDirection}°\n" +
       new string('-', 27);

This produces clean, easy-to-read output in the console, ideal for command-line users.

Program Entry Point (Program.cs)

The Main() method ties everything together:

public static async Task Main(string[] args)
{
    using var client = new HttpClient();
    var weatherLink = new WeatherLink(client);
    Console.WriteLine("Fetching weather data...");
    string report = await weatherLink.GetWeatherAsync();
    Console.WriteLine(report);
}

async Task Main allows for await usage in the Main() method (available since C# 7.1).

We instantiate HttpClient, pass it into WeatherLink, call the GetWeatherAsync() method, and print the result.

Summary of Application Logic

    Main() creates an HttpClient and an instance of WeatherLink.

    It calls GetWeatherAsync() which:

        Sends an asynchronous request to the OpenWeatherMap API.

        Parses the JSON response using JObject.

        Extracts key weather metrics (temperature, humidity, wind, etc.).

        Formats the data into a human-readable report.

    The report is printed to the console.
