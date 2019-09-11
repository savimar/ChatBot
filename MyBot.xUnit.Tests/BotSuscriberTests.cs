using System;
using ApiAiSDK;
using MyBot.Events;
using Xunit;

namespace MyBot.xUnit.Tests
{
    public class BotSuscriberTests
    {
        private readonly BotSuscriber _suscriber = new BotSuscriber("test", new BotPublisher());

        [Fact]
        public void GetKeyboard_Positive_Start_Test()
        {
            string actual = _suscriber.GetKeyboard("������");
            string expected = "{\"inline_keyboard\":[[{\"text\":\"�����\",\"callback_data\":\"�����\"}]]}";
            Assert.Equal(expected, actual);
            Assert.IsType<string>(actual);
            Assert.NotEmpty(actual);
            Assert.NotNull(actual);
        }

        [Fact]
        public void GetKeyboard_Negative_Null_Test()
        {
            string actual = _suscriber.GetKeyboard(null);
            string expected =
                "{\"keyboard\":[[{\"text\":\"������\"},{\"text\":\"��� ����\"}],[{\"text\":\"������� �������\"},{\"text\":\"������\"}],[{\"text\":\"����� ������� ����\"}]],\"resize_keyboard\":true,\"one_time_keyboard\":true}";
            Assert.Equal(expected, actual);
            Assert.IsType<string>(actual);
            Assert.NotEmpty(actual);
            Assert.NotNull(actual);
        }

        [Fact]
        public async System.Threading.Tasks.Task AnswerQuestion_Exception_TestAsync()
        {
            await Assert.ThrowsAsync<NullReferenceException>(() => _suscriber.AnswerQuestionAsync(null));
        }

        [Fact]
        public void AnswerQuestion_Negative_Test()
        {
            string actual = _suscriber.AnswerQuestionAsync("null").Result;
            string expected = "������, � ���� �� �������";
            Assert.Equal(expected, actual);
            Assert.IsType<string>(actual);
            Assert.NotEmpty(actual);
            Assert.NotNull(actual);
        }

        [Fact]
        public void AnswerQuestion_Positive_Date_Test()
        {
            string actual = _suscriber.AnswerQuestionAsync("����� ������� ����").Result;
            string expected = "�������: " + DateTime.Now.ToString("dd.MM.yyyy");
            Assert.Equal(expected, actual);
            Assert.IsType<string>(actual);
            Assert.NotEmpty(actual);
            Assert.NotNull(actual);
        }
    }
}