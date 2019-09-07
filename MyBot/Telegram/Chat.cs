using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyBot
{
    public class Chat
    {
        public long Id { get; set; }

        [JsonProperty("first_name")]
        public string FirstName { get; set; }

    }
}
