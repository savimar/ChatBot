using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyBot
{
   public class ReplyKeyboardMarkup : IKeyboardMarkup
    {
        [JsonProperty("keyboard")]
        public KeyboardButton[][] Keyboard { get; set; }

        [JsonProperty("resize_keyboard")]
        public bool ResizeKeyboard { get; set; } = true;

        [JsonProperty("one_time_keyboard")]
        public bool OneTimeKeyboard { get; set; } = true;

    }
}
