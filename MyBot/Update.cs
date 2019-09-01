using System;
using System.Collections.Generic;
using System.Text;
using MyBot.Keyboad;
using Newtonsoft.Json;

namespace MyBot
{
    public class Update
    {
        [JsonProperty("update_id")]
        public long UpdateId { get; set; }

        [JsonProperty("message")]
        public  Message Message { get; set; }

        [JsonProperty("callback_query")]
        public CallbackQuery CallbackQuery { get; set; }
    }
}
