using System;


namespace TelegramBot
{

    public class BotEventArgs : EventArgs
    {
        public Update[] Updates { get; set; }

        public BotEventArgs(Update[] updates)
        {
            Updates = updates;
        }

    }
}
