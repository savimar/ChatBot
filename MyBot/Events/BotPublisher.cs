using System;
using System.Threading.Tasks;
using MyBot.Telegram;

namespace MyBot.Events
{
    public class BotPublisher
    {
        public event EventHandler<BotEventArgs> BotEvent = null;

        public async Task RunBot()
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

        public void OnRaiseBotEvent(BotEventArgs botEventArgs)
        {
            BotEvent?.Invoke(this, botEventArgs);
        }
    }
}