using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyBot
{
   public class User
    {
        public long Id { get; set; }
        [JsonProperty("first_name")]

        public string FirstName { get; set; }

        [JsonProperty("username")]
        public string Username { get; set; }
    }
}
