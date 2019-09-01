﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RestSharp;

namespace MyBot
{
    public class WeatherApi
    {
        private const string API_URL = "http://api.apixu.com/v1/current.json";
        private const string API_KEY = "api.apixu";
        private const string FINAL_URL = API_URL + "?key=" + API_KEY + "&lang=ru&q=";
        private RestClient restClient = new RestClient();

        public async Task<string> GetWeatherInCityAsync(string city)
        {
            city = city.Replace(city.Substring(0, 1), city.Substring(0, 1).ToUpperInvariant());
            var url = FINAL_URL + city;

            var request = new RestRequest(url);
            var response = await restClient.ExecuteGetTaskAsync(request);

            var data = JsonConvert.DeserializeObject<WeatherData>(response.Content);
            string condition = data.Current.Condition.Text.ToLowerInvariant();
            int temp = (int)data.Current.TempC;
            return $"В городе {city} сейчас {condition}, {temp} градусов";
        }
    }
}