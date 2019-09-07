using Newtonsoft.Json;


namespace TelegramBot
{
    public class InlineKeyboardMarkup : IKeyboardMarkup
    {
        [JsonProperty("inline_keyboard")]
        public InlineKeyboardButton[][] Keyboard { get; set; }
    }
}
