
using System.Threading.Tasks;
using Newtonsoft.Json;
using RestSharp;


namespace MyBot
{
    public class TelegrammAPI
    {
        private const string API_URL = @"https://api.telegram.org/bot" + SecretKey.API_KEY + "/";
        RestClient restClient = new RestClient();
        private long lastUpdateId = 0;


        public async Task<Update[]> GeUpdatesAsync()
        {
            var json = await SendApiRequest("getUpdates", "offset=" + lastUpdateId);
            ApiResult apiResult = JsonConvert.DeserializeObject<ApiResult>(json);
            var updates = apiResult?.Result;
            if (apiResult != null && updates.Length > 0)
            {
                lastUpdateId = updates[updates.Length - 1].UpdateId + 1;
            }

            return apiResult?.Result;
        }


        public async Task SendMessage(string text, long chatId, string keyboard)
        {
            await SendApiRequest("sendMessage", $"chat_id={chatId} &text={text}&reply_markup={keyboard}");
        }


        public async Task SendPhoto(long chatId, string photo, string caption)
        {
            await SendApiRequest("sendPhoto", $"chat_id={chatId} &photo={photo}&caption={caption}");
        }


        public async Task<string> SendApiRequest(string apiMethod, string parameters)
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
            var response = await restClient.ExecuteGetTaskAsync(request);
            return response.Content;
        }
    }
}
