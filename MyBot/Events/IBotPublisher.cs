using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;


namespace MyBot.Events
{
    public interface IBotPublisher
    {
        event EventHandler<BotEventArgs> BotEvent;
        void RunBot();
    }
}