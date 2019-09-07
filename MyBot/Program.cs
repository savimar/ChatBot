using TelegramBot.Events;

namespace TelegramBot
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var publisher = new BotPublisher();
            var subscriber = new BotSuscriber("subscriber", publisher);
            publisher.RunBot();

        }


    }
}
