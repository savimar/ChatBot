using Newtonsoft.Json;

namespace MyBot.Keyboads
{
    public class User
    {
        public long Id { get; set; }

        [JsonProperty("first_name")] public string FirstName { get; set; }

        [JsonProperty("username")] public string Username { get; set; }
    }
}