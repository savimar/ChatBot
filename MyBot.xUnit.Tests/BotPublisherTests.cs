using System;
using System.Threading.Tasks;
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
            var arg = new BotEventArgs(new Update[0]);

            var evt = await Assert.RaisesAnyAsync<BotEventArgs>(
                h => x.BotEvent += h,
                h => x.BotEvent -= h,
                () => Task.Run(() => x.OnRaiseBotEvent(arg)));

            Assert.NotNull(evt);
            Assert.Equal(x, evt.Sender);
            Assert.Equal(arg, evt.Arguments);
        }
    }
}