using System;
using System.Net;
using System.Threading.Tasks;
using MyBot.Wheather;
using Newtonsoft.Json;
using RestSharp;
using Xunit;

namespace MyBot.xUnit.Tests
{
    public class WheatherApiTests
    {
        private readonly WeatherApi _api = new WeatherApi();

        [Fact]
        public async Task GetWeatherInCity_Exception_TestAsync()
        {
            await Assert.ThrowsAsync<InvalidOperationException>(() => _api.GetWeatherInCityAsync("hjghjg"));
        }

        [Fact]
        public void GetWeatherInCity_Positive_Test()
        {
            Task<string> actual = _api.GetWeatherInCityAsync("Москва");
            Assert.StartsWith("В городе Москва", actual.Result);
            Assert.NotEmpty(actual.Result);
            Assert.IsType<string>(actual.Result);
            Assert.NotNull(actual);
        }

        [Fact]
        public void GetWeatherInCity_Negative_Test()
        {
            Task<string> actual = _api.GetWeatherInCityAsync(null);
            Assert.True(actual.IsFaulted);
        }

        [Fact]
        public void Weather_GetApi_Test()
        {
            RestClient restClient = new RestClient();
            var request = new RestRequest(WeatherApi.FINAL_URL + "Москва");
            var response = restClient.Get(request);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var data = JsonConvert.DeserializeObject<WeatherData>(response.Content);
            Assert.NotNull(data);
            Assert.IsType<WeatherData>(data);
        }
    }
}