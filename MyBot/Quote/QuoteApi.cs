using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RestSharp;

namespace MyBot
{
    class QuoteApi
    {
        private const string API_URL = @"https://api.forismatic.com/api/1.0/?method=getQuote&format=json&lang=ru";
        private RestClient restClient = new RestClient();

        public async Task<string> GetQuote()
        {
            var request = new RestRequest(API_URL);
            var cancellationToken = new CancellationToken();
            var response = await restClient.ExecuteTaskAsync(request, cancellationToken);

            var data = JsonConvert.DeserializeObject<QuoteData>(response.Content);
            if (String.IsNullOrEmpty(data.QuoteAuthor))
            {
                data.QuoteAuthor = "неизвестен";
            }
            return $"Случайная цитата: \n \"{data.QuoteText}\" \n Автор: {data.QuoteAuthor}";
        }
    }
}
