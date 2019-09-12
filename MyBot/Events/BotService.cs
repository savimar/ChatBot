using MyBot.Telegram;

namespace MyBot.Events
{
    class BotService
    {
        private IBotPublisher _botPublisher;
        private IBotSuscriber _botSuscriber;

        public BotService(IBotPublisher botPublisher, IBotSuscriber botSuscriber)
        {
            _botPublisher = botPublisher;
            _botSuscriber = botSuscriber;

            _botPublisher.BotEvent += HandleBotEventAsync;
        }

        private async void HandleBotEventAsync(object sender, BotEventArgs e)
        {
            var api = new TelegramApi();
            await _botSuscriber.SendMessageToTelegramAsync(e.Updates, api);
        }
    }
}