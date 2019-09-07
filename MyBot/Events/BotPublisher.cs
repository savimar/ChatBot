using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MyBot.Events
{
    public class BotPublisher
    {
        public event EventHandler<BotEventArgs> BotEvent = null;

        public async Task RunBot()
        {
            var api = new TelegrammAPI();
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

        private void OnRaiseBotEvent(BotEventArgs botEventArgs)
        {
            EventHandler<BotEventArgs> handler = BotEvent;
            handler(this, botEventArgs);
        }
    }
}
