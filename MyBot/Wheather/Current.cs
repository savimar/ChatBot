﻿using Newtonsoft.Json;

namespace MyBot.Wheather
{
    public class Current
    {
        [JsonProperty("condition")] public Condition Condition { get; set; }

        [JsonProperty("temp_c")] public double TempC { get; set; }
    }
}