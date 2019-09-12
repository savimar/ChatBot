using System;
using System.Collections.Generic;
using System.Net;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using ApiAiSDK;
using MyBot.Keyboads;
using MyBot.Quote;
using MyBot.Telegram;
using MyBot.Wheather;
using Newtonsoft.Json;

[assembly: InternalsVisibleTo("MyBot.xUnit.Tests")]

namespace MyBot.Events
{
    public class BotSuscriber : IBotSuscriber
    {
        private string _id;

        public BotSuscriber(string id)
        {
            _id = id;
        }

        public async Task SendMessageToTelegramAsync(Update[] updates, TelegramApi api)
        {
            foreach (var update in updates)
            {
                if (update?.CallbackQuery != null)
                {
                    if (update.CallbackQuery.Data == "старт")
                    {
                        await api.SendMessageAsync("Поехали!", update.CallbackQuery.Message.Chat.Id, GetKeyboard(""));
                    }
                }
                else
                {
                    var question = update?.Message?.Text;
                    var answer = await AnswerQuestionAsync(question);
                    await api.SendMessageAsync(answer, update.Message.Chat.Id,
                        GetKeyboard(question?.ToLowerInvariant()));
                }
            }
        }


        internal string GetKeyboard(string question)
        {
            if (question == "/start" || question == "начать")
            {
                var keyboardMarkup = new InlineKeyboardMarkup();
                keyboardMarkup.Keyboard = new InlineKeyboardButton[][]
                {
                    new InlineKeyboardButton[] {new InlineKeyboardButton("Старт", "старт")}
                };
                return JsonConvert.SerializeObject(keyboardMarkup);
            }
            else
            {
                var keyboardMarkup = new ReplyKeyboardMarkup();

                keyboardMarkup.Keyboard = new KeyboardButton[][]
                {
                    new KeyboardButton[] {new KeyboardButton("Привет"), new KeyboardButton("Как дела")},
                    new KeyboardButton[] {new KeyboardButton("Сколько времени"), new KeyboardButton("Цитата")},
                    new KeyboardButton[] {new KeyboardButton("Какой сегодня день")}
                };
                return JsonConvert.SerializeObject(keyboardMarkup);
            }
        }

        internal async Task<string> AnswerQuestionAsync(string userQuestion)
        {
            AIConfiguration config = new AIConfiguration("dialogflow.com", SupportedLanguage.Russian);
            var apiAi = new ApiAi(config);
            List<string> answers = new List<string>();
            userQuestion = userQuestion.ToLowerInvariant();

            if (userQuestion == "/start" || userQuestion == "начать")
            {
                try
                {
                    await GetStartMessageAsync(answers);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }

            if (userQuestion.Contains("сколько времени"))
            {
                var time = DateTime.Now.ToString("HH:mm");
                string timeString = $"Точное время: {time}";
                if (!answers.Contains(timeString))
                {
                    answers.Add(timeString);
                }
            }

            if (userQuestion.Contains("какой сегодня день"))
            {
                var date = DateTime.Now.ToString("dd.MM.yyyy");
                string dateString = $"Сегодня: {date}";
                if (!answers.Contains(dateString))
                {
                    answers.Add(dateString);
                }
            }

            if (userQuestion.StartsWith("какая погода в городе"))
            {
                var words = userQuestion.Split(' ');
                var city = words[words.Length - 1];
                var weatherApi = new WeatherApi();
                var forecast = await weatherApi.GetWeatherInCityAsync(city);
                if (!answers.Contains(forecast))
                {
                    answers.Add(forecast);
                }
            }

            if (userQuestion == "цитата")
            {
                var quoteApi = new QuoteApi();
                string quote = await quoteApi.GetQuote();
                if (!answers.Contains(quote))
                {
                    answers.Add(quote);
                }
            }


            if (answers.Count == 0)
            {
                var response = apiAi.TextRequest(userQuestion);
                string answer = response.Result.Fulfillment.Speech;
                if (String.IsNullOrEmpty(answer))
                {
                    answer = "Прости, я тебя не понимаю";
                }

                answers.Add(answer);
            }

            return String.Join(", ", answers);
        }

        private async Task GetStartMessageAsync(List<string> answers)
        {
            var api = new TelegramApi();
            string photo = WebUtility.UrlEncode("https://pickasso.info/image/EGDzy");
            var updates = await api.GeUpdatesAsync();
            if (updates.Length > 0)
            {
                string firstName = updates[0].Message.Chat.FirstName;
                string text = "Мы рады видеть Вас, " + firstName +
                              ", в нашем боте-помощнике. Вы можете здесь узнать какая погода, набрав в поле сообщений \"Какая погода в городе (далее город в именительном падеже)\", узнать какой сегодня день, точное время, получить случайную цитату или просто пообщаться с ботом по кнопкам клавиатуры или введите свое сообщениме в поле сообщений";
                if (!answers.Contains(text))
                {
                    long chatId = updates[0].Message.Chat.Id;
                    await api.SendPhotoAsync(chatId, photo, firstName + ", здравствуйте!");
                    answers.Add(text);
                }
            }
        }
    }
}