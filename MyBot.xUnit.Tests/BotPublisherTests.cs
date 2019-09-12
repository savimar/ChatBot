using System;
using System.Threading.Tasks;
using Moq;
using MyBot.Events;
using MyBot.Telegram;
using Xunit;

namespace MyBot.xUnit.Tests
{
    public class BotPublisherTests
    {
        [Fact]
        public async Task OnRaise_BotEvent_TestAsync()
        {
            var x = new BotPublisher();
            Update[] updates = {new Update(23), new Update(56), new Update(12)};
            var arg = new BotEventArgs(updates);

            var evt = await Assert.RaisesAnyAsync<BotEventArgs>(
                h => x.BotEvent += h,
                h => x.BotEvent -= h,
                () => Task.Run(() => x.OnRaiseBotEvent(arg)));

            Assert.NotNull(evt);
            Assert.Equal(x, evt.Sender);
            Assert.Equal(arg, evt.Arguments);
        }

        [Fact]
        public void OnRaise_BotEvent_MockEventTest()
        {
            Mock<IBotPublisher> publisherMock = new Mock<IBotPublisher>();
            Mock<IBotSuscriber> suscriberMock = new Mock<IBotSuscriber>();
            BotService service = new BotService(publisherMock.Object, suscriberMock.Object);
            publisherMock.Raise(m => m.BotEvent += null, new BotEventArgs(new Update[0]));
            suscriberMock.Verify(x => x.SendMessageToTelegramAsync(It.IsAny<Update[]>(), It.IsAny<TelegramApi>()),
                Times.AtLeastOnce);
        }
    }
}