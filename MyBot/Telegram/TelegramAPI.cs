using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RestSharp;

[assembly: InternalsVisibleTo("MyBot.xUnit.Tests")]
namespace MyBot.Telegram
{
    public class TelegramApi
    {
        internal const string API_URL = @"https://api.telegram.org/bot" + SecretKey.API_KEY + "/";
        private readonly RestClient _restClient = new RestClient();
        private long _lastUpdateId;


        internal async Task<Update[]> GeUpdatesAsync()
        {
            var json = await SendApiRequestAsync("getUpdates", "offset=" + _lastUpdateId);
            ApiResult apiResult = JsonConvert.DeserializeObject<ApiResult>(json);
            var updates = apiResult?.Result;
            if (apiResult != null && updates.Length > 0)
            {
                _lastUpdateId = updates[updates.Length - 1].UpdateId + 1;
            }

            return apiResult?.Result;
        }


        internal async Task SendMessageAsync(string text, long chatId, string keyboard)
        {
            await SendApiRequestAsync("sendMessage", $"chat_id={chatId} &text={text}&reply_markup={keyboard}");
        }

        internal async Task SendPhotoAsync(long chatId, string photo, string caption)
        {
            await SendApiRequestAsync("sendPhoto", $"chat_id={chatId} &photo={photo}&caption={caption}");
        }


        private async Task<string> SendApiRequestAsync(string apiMethod, string parameters)
        {
            string url;
            if (parameters == "offset=0")
            {
                url = API_URL + apiMethod;
            }
            else
            {
                url = API_URL + apiMethod + "?" + parameters;
            }

            var request = new RestRequest(url);
            Task<IRestResponse> task = _restClient.ExecuteTaskAsync(request);
            task.Wait();
            var response = await task;
            return response.Content;
        }
    }
}