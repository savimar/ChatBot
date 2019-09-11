using System;
using MyBot.Telegram;

namespace MyBot.Events
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