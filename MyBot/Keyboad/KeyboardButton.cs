using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyBot
{
   public class KeyboardButton
    {
       [JsonProperty("text")]
       public string Text { get; set; }

       public KeyboardButton(string text)
       {
           this.Text = text;
       }
    }
}
