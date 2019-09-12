using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MyBot.Telegram;

[assembly: InternalsVisibleTo("MyBot.xUnit.Tests")]

namespace MyBot.Events
{
    public class BotPublisher : IBotPublisher
    {
        public event EventHandler<BotEventArgs> BotEvent = null;

        public async void RunBot()
        {
            var api = new TelegramApi();
            while (true)
            {
                try
                {
                    var updates = await api.GeUpdatesAsync();
                    if (updates != null)
                    {
                        OnRaiseBotEvent(new BotEventArgs(updates));
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
        }

        internal void OnRaiseBotEvent(BotEventArgs botEventArgs)
        {
            BotEvent?.Invoke(this, botEventArgs);
        }
    }
}