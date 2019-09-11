using Newtonsoft.Json;

namespace MyBot.Telegram
{
    public class Chat
    {
        public long Id { get; set; }

        [JsonProperty("first_name")] public string FirstName { get; set; }
    }
}