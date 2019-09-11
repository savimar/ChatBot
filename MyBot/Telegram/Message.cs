using Newtonsoft.Json;

namespace MyBot.Telegram
{
    public class Message
    {
        [JsonProperty("chat")] public Chat Chat { get; set; }

        [JsonProperty("text")] public string Text { get; set; }
    }
}