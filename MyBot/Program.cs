using MyBot.Events;

namespace MyBot
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var publisher = new BotPublisher();
            var subscriber = new BotSuscriber("subscriber");
            var service = new BotService(publisher, subscriber);

            publisher.RunBot();
        }
    }
}