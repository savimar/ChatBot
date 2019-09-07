using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MyBot.Events
{
    public class BotSuscriber
    {
        private string id;
        public BotSuscriber(string id, BotPublisher publisher)
        {
            this.id = id;
            publisher.BotEvent += HandleBotEventAsync;
        }

        async void HandleBotEventAsync(object sender, BotEventArgs e)
        {
            var data = File.ReadAllText(
              Directory.GetCurrentDirectory() + @"\SolutionItems\answers.json");
            var questions = JsonConvert.DeserializeObject<Dictionary<string, string>>(data);
            var api = new TelegrammAPI();

            foreach (var update in e.Updates)
            {
                if (update?.CallbackQuery != null)
                {
                    if (update.CallbackQuery.Data == "старт")
                    {
                        await api.SendMessage("Поехали!", update.CallbackQuery.Message.Chat.Id, GetKeyboard(""));
                    }
                }
                else
                {
                    var question = update?.Message?.Text;
                    var answer = AnswerQuestion(question, questions);
                    await api.SendMessage(answer.Result, update.Message.Chat.Id, GetKeyboard(question.ToLowerInvariant()));
                }
            }
        }
        private static string GetKeyboard(string question)
        {

            if (question == "/start" || question == "начать")
            {
                var keyboardMarkup = new InlineKeyboardMarkup();
                keyboardMarkup.Keyboard = new InlineKeyboardButton[][]
                {
                   new InlineKeyboardButton[] { new InlineKeyboardButton("Старт", "старт") }
                };
                return JsonConvert.SerializeObject(keyboardMarkup);
            }
            else
            {
                var keyboardMarkup = new ReplyKeyboardMarkup();

                keyboardMarkup.Keyboard = new KeyboardButton[][]
                {
                           new KeyboardButton[] { new KeyboardButton("Привет"), new KeyboardButton("Как дела") },
                           new KeyboardButton[] { new KeyboardButton("Сколько времени"), new KeyboardButton("Цитата") },
                           new KeyboardButton[] { new KeyboardButton("Какой сегодня день") }
                };
                return JsonConvert.SerializeObject(keyboardMarkup);
            }

        }

        private async static Task<string> AnswerQuestion(string userQuestion, Dictionary<string, string> questions)
        {
            List<string> answers = new List<string>();
            userQuestion = userQuestion.ToLowerInvariant();

            foreach (var question in questions)
            {
                if (userQuestion.Contains(question.Key))
                {
                    answers.Add(question.Value);
                }

                if (userQuestion == "/start" || userQuestion == "начать")
                {
                    try
                    {
                        await GetStartMessage(answers);
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
            }

            if (answers.Count == 0)
            {
                answers.Add("я тебя не понимаю");
            }
            return String.Join(", ", answers);
        }

        private static async Task GetStartMessage(List<string> answers)
        {
            var api = new TelegrammAPI();
            string photo = WebUtility.UrlEncode("https://pickasso.info/image/EGDzy");
            var updates = await api.GeUpdatesAsync();
            if (updates.Length > 0)
            {
                string firstName = updates[0].Message.Chat.FirstName;
                string text = "Мы рады видеть Вас, " + firstName +
                              ", в нашем боте-помощнике. Вы можете здесь узнать какая погода, набрав в поле сообщений \"Какая погода в городе (далее город в именительном падеже)\", узнать какой сегодня день, точное время, получить случайную цитату или просто пообщаться с ботом по кнопкам клавиатуры";
                if (!answers.Contains(text))
                {
                    long chatId = updates[0].Message.Chat.Id;
                    await api.SendPhoto(chatId, photo, firstName + ", здравствуйте!");
                    answers.Add(text);
                }
            }
        }
    }
}

