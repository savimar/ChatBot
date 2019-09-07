using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyBot
{
    public class InlineKeyboardMarkup : IKeyboardMarkup
    {
        [JsonProperty("inline_keyboard")]
        public InlineKeyboardButton[][] Keyboard { get; set; }
    }
}
