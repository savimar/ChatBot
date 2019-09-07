using System;
using System.Collections.Generic;
using System.Text;

namespace MyBot
{
   
    public class BotEventArgs : EventArgs
    {
        public BotEventArgs(Update[] updates)
        {
            Updates = updates;
        }
        public Update[] Updates { get; set; }
    }
}
