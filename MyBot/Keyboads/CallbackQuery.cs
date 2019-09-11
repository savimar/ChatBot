using MyBot.Telegram;
using Newtonsoft.Json;

namespace MyBot.Keyboads
{
    public class CallbackQuery
    {
        [JsonProperty("id")] public string Id { get; set; }

        [JsonProperty("from")] public User From { get; set; }

        [JsonProperty("message")] public Message Message { get; set; }

        [JsonProperty("data")] public string Data { get; set; }
    }
}