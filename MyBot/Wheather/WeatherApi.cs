using System;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RestSharp;

[assembly: InternalsVisibleTo("MyBot.xUnit.Tests")]

namespace MyBot.Wheather
{
    public class WeatherApi
    {
        private const string API_URL = "http://api.apixu.com/v1/current.json";
        private const string API_KEY = "apixu.com";
        internal const string FINAL_URL = API_URL + "?key=" + API_KEY + "&lang=ru&q=";
        private readonly RestClient _restClient = new RestClient();

        internal async Task<string> GetWeatherInCityAsync(string city)
        {
            try
            {
                city = city.Substring(0, 1).ToUpper() + (city.Length > 1 ? city.Substring(1) : "");
                var url = FINAL_URL + city;

                var request = new RestRequest(url);
                var response = await _restClient.ExecuteTaskAsync(request, new CancellationToken());

                var data = JsonConvert.DeserializeObject<WeatherData>(response.Content);
                string condition = data?.Current?.Condition?.Text.ToLowerInvariant();
                int temp = (int) data?.Current?.TempC;

                return $"В городе {city} сейчас {condition}, {temp} градусов";
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}