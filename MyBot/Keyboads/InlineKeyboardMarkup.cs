using Newtonsoft.Json;

namespace MyBot.Keyboads
{
    public class InlineKeyboardMarkup : IKeyboardMarkup
    {
        [JsonProperty("inline_keyboard")] public InlineKeyboardButton[][] Keyboard { get; set; }
    }
}