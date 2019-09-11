using Newtonsoft.Json;

namespace MyBot.Keyboads
{
    public class InlineKeyboardButton
    {
        [JsonProperty("text")] public string Text { get; set; }


        [JsonProperty("callback_data")] public string CallbackData { get; set; }


        public InlineKeyboardButton(string text, string callbackData)
        {
            Text = text;
            CallbackData = callbackData;
        }
    }
}