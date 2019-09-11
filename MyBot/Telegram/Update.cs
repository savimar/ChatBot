using MyBot.Keyboads;
using Newtonsoft.Json;

namespace MyBot.Telegram
{
    public class Update
    {
        [JsonProperty("update_id")] public long UpdateId { get; set; }

        [JsonProperty("message")] public Message Message { get; set; }

        [JsonProperty("callback_query")] public CallbackQuery CallbackQuery { get; set; }
    }
}