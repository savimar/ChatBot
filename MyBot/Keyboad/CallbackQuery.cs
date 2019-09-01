﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyBot.Keyboad
{
   public class CallbackQuery
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("from")]
        public User From { get; set; }

        [JsonProperty("message")]
        public Message Message { get; set; }

        [JsonProperty("data")]
        public string Data { get; set; }


    }
}