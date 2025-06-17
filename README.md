🧠 Explaining the Logic

This is a simple weather application that retrieves current weather data from the OpenWeatherMap API and displays it in a clean, readable format using .NET and C#. It demonstrates:

    ✅ Dependency injection

    ✅ Asynchronous programming with async/await

    ✅ JSON parsing using Newtonsoft.Json

    ✅ Clean and readable output formatting

🔧 HttpClient Injection and Constructor Setup

We begin by setting up a private field and constructor in our WeatherLink class:

private readonly HttpClient _client;
```
public WeatherLink(HttpClient client)
{
    _client = client;
}
```
HttpClient is used to send HTTP requests to web APIs.

By injecting it through the constructor, we promote dependency injection, enabling reusability and easier testing.

🌐 Making the API Call – GetWeatherAsync()

We define an asynchronous method to fetch and format weather data:

public async Task<string> GetWeatherAsync()

1. Define the API URL

var weatherURL = $"https://api.openweathermap.org/data/2.5/weather?q=Gurnee,US&units=imperial&appid=...";

    This builds the API endpoint to retrieve weather data for Gurnee, IL using Fahrenheit.

    ⚠️ In production, the API key should be stored in configuration, not hardcoded.

2. Make the HTTP Request

var response = await _client.GetAsync(weatherURL);
response.EnsureSuccessStatusCode();

    GetAsync() sends the GET request asynchronously.

    EnsureSuccessStatusCode() throws an exception if the response status code isn't successful (e.g., 404 or 500).

3. Parse the JSON Response

var jsonString = await response.Content.ReadAsStringAsync();
var data = JObject.Parse(jsonString);

    The JSON response is parsed into a JObject for easy traversal using the Newtonsoft.Json package.

🧾 Extracting and Formatting the Weather Data

We extract relevant fields safely from the parsed JSON:

string city = data["name"]?.ToString() ?? "Unknown";
string description = data["weather"]?[0]?["description"]?.ToString() ?? "N/A";
string temperature = data["main"]?["temp"]?.ToString() ?? "N/A";
string humidity = data["main"]?["humidity"]?.ToString() ?? "N/A";
string windSpeed = data["wind"]?["speed"]?.ToString() ?? "N/A";
string windDirection = data["wind"]?["deg"]?.ToString() ?? "N/A";

Each line includes null checks to prevent runtime errors.

📌 Examples:

    data["weather"][0]["description"] → Weather condition (e.g., clear sky)

    data["main"]["temp"] → Temperature

    data["wind"]["speed"] → Wind speed in mph

🖨 Output Formatting

The extracted values are formatted into a clean multi-line string:

return $"Weather Report for {city}\n" +
       new string('-', 27) + "\n" +
       $"Description : {description}\n" +
       $"Temperature : {temperature}°F\n" +
       $"Humidity    : {humidity}%\n" +
       $"Wind Speed  : {windSpeed} mph\n" +
       $"Wind Dir.   : {windDirection}°\n" +
       new string('-', 27);

    Output is structured and easy to read in the terminal or console.

🏁 Program Entry Point (Program.cs)

The Main() method brings everything together:

public static async Task Main(string[] args)
{
    using var client = new HttpClient();
    var weatherLink = new WeatherLink(client);

    Console.WriteLine("Fetching weather data...");
    string report = await weatherLink.GetWeatherAsync();
    Console.WriteLine(report);
}

    async Task Main allows asynchronous code execution inside the entry point (supported since C# 7.1).

    We create an instance of WeatherLink, call GetWeatherAsync(), and print the report.

🧩 Summary of Application Logic

    Main() creates an HttpClient and WeatherLink instance.

    GetWeatherAsync() is called, which:

        Sends an asynchronous request to OpenWeatherMap.

        Parses the JSON response.

        Extracts key weather metrics.

        Formats the data into a readable report.

    The report is printed to the console.

✅ Features Demonstrated

    ✅ Asynchronous programming with async/await

    ✅ Dependency injection of HttpClient

    ✅ Safe JSON parsing with null-checking and fallbacks

    ✅ Formatted console output for clarity

    ✅ Basic error handling via EnsureSuccessStatusCode()
