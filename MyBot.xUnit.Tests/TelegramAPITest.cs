using System;
using System.Net;
using MyBot.Telegram;
using Newtonsoft.Json;
using RestSharp;
using Xunit;

namespace MyBot.xUnit.Tests
{
    public class TelegramApiTest
    {
        [Fact]
        public void Telegram_GetApi_Test()
        {
            RestClient restClient = new RestClient();
            var request = new RestRequest(TelegramApi.API_URL + "getUpdates");
            var response = restClient.Get(request);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var data = JsonConvert.DeserializeObject<ApiResult>(response.Content);
            Assert.NotNull(data);
            Assert.IsType<ApiResult>(data);
        }
    }
}