using Newtonsoft.Json;
using TelegramBot.Keyboad;

namespace TelegramBot
{
    public class Update
    {
        [JsonProperty("update_id")]
        public long UpdateId { get; set; }

        [JsonProperty("message")]
        public Message Message { get; set; }

        [JsonProperty("callback_query")]
        public CallbackQuery CallbackQuery { get; set; }
    }
}
