using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyBot
{
    public class InlineKeyboardButton
    {
        [JsonProperty("text")]
        public string Text { get; set; }


        [JsonProperty("callback_data")]
        public string CallbackData { get; set; }


        public InlineKeyboardButton(string text, string callbackData)
        {
            Text = text;
            CallbackData = callbackData;
        }
    }
}
