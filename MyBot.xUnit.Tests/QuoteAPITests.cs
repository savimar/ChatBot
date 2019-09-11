using System;
using System.Net;
using MyBot.Quote;
using Newtonsoft.Json;
using RestSharp;
using Xunit;

namespace MyBot.xUnit.Tests
{
    public class QuoteApiTests
    {
        private QuoteApi _api = new QuoteApi();

        [Fact]
        public void GetQuote_Positive_Test()
        {
            string actual = _api.GetQuote().Result;
            Assert.StartsWith("Случайная цитата:", actual);
            Assert.NotEmpty(actual);
            Assert.IsType<string>(actual);
            Assert.NotNull(actual);
        }

        [Fact]
        public void Quote_GetApi_Test()
        {
            RestClient restClient = new RestClient();
            var request = new RestRequest(QuoteApi.API_URL);
            var response = restClient.Get(request);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var data = JsonConvert.DeserializeObject<QuoteData>(response.Content);
            Assert.NotNull(data);
            Assert.IsType<QuoteData>(data);
        }
    }
}