using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using MyBot.Telegram;


namespace MyBot.Events
{
    public interface IBotSuscriber
    {
        Task SendMessageToTelegramAsync(Update[] updates, TelegramApi api);
    }
}