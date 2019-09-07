using Newtonsoft.Json;


namespace TelegramBot
{
    public class Chat
    {
        public long Id { get; set; }

        [JsonProperty("first_name")]
        public string FirstName { get; set; }

    }
}
