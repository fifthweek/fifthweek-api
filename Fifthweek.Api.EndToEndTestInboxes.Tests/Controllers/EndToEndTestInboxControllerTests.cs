namespace Fifthweek.Api.EndToEndTestMailboxes.Tests.Controllers
{
    using System.Net;
    using System.Threading.Tasks;

    using Fifthweek.Api.Core;
    using Fifthweek.Api.EndToEndTestMailboxes.Commands;
    using Fifthweek.Api.EndToEndTestMailboxes.Controllers;
    using Fifthweek.Api.EndToEndTestMailboxes.Queries;
    using Fifthweek.Api.EndToEndTestMailboxes.Shared;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestClass]
    public class EndToEndTestInboxControllerTests
    {
        private const string Subject = "Meow";
        private const string Body = "Paw-some";

        private static readonly MailboxName MailboxName = MailboxName.Parse("wd_1234567890123");

        private Mock<ICommandHandler<DeleteAllMessagesCommand>> deleteAllMessages;
        private Mock<IQueryHandler<TryGetLatestMessageQuery, Message>> tryGetLatestMessage;
        private EndToEndTestInboxController target;

        [TestInitialize]
        public void TestInitialize()
        {
            this.deleteAllMessages = new Mock<ICommandHandler<DeleteAllMessagesCommand>>();
            this.tryGetLatestMessage = new Mock<IQueryHandler<TryGetLatestMessageQuery, Message>>();
            this.target = new EndToEndTestInboxController(this.deleteAllMessages.Object, this.tryGetLatestMessage.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(BadRequestException))]
        public async Task ItShouldRequireMailboxName()
        {
            await this.target.GetLatestMessageAndClearMailboxAsync(string.Empty);
        }

        [TestMethod]
        [ExpectedException(typeof(ModelValidationException))]
        public async Task ItShouldRequireValidMailboxName()
        {
            await this.target.GetLatestMessageAndClearMailboxAsync("oh dear");
        }

        [TestMethod]
        public async Task WhenEmailExists_ItShouldReturnFormattedEmail()
        {
            this.tryGetLatestMessage.Setup(_ => _.HandleAsync(new TryGetLatestMessageQuery(MailboxName))).ReturnsAsync(new Message(Subject, Body));

            var result = await this.target.GetLatestMessageAndClearMailboxAsync(MailboxName.Value);
            var response = await result.Content.ReadAsStringAsync();

            Assert.AreEqual(result.StatusCode, HttpStatusCode.OK);
            Assert.IsTrue(result.Content.Headers.ContentType.MediaType == "text/html");
            Assert.IsTrue(response.Contains(string.Format(@"<span id=""email-subject"">{0}</span>", Subject)));
            Assert.IsTrue(response.Contains(Body));
        }

        [TestMethod]
        public async Task WhenNoEmailsExist_ItShouldReturnEmptyNotification()
        {
            this.tryGetLatestMessage.Setup(_ => _.HandleAsync(new TryGetLatestMessageQuery(MailboxName))).ReturnsAsync(null);

            var result = await this.target.GetLatestMessageAndClearMailboxAsync(MailboxName.Value);
            var response = await result.Content.ReadAsStringAsync();

            Assert.AreEqual(result.StatusCode, HttpStatusCode.OK);
            Assert.IsTrue(result.Content.Headers.ContentType.MediaType == "text/html");
            Assert.IsTrue(response.Contains(@"<span id=""mailbox-empty"">Mailbox Empty!</span>"));
        }
    }
}