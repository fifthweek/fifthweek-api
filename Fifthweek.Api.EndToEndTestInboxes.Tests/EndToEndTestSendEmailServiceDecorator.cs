namespace Fifthweek.Api.EndToEndTestMailboxes.Tests
{
    using System;
    using System.Net.Mail;
    using System.Threading.Tasks;

    using Fifthweek.Api.Core;
    using Fifthweek.Api.EndToEndTestMailboxes.Shared;
    using Fifthweek.Shared;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestClass]
    public class EndToEndTestSendEmailServiceDecoratorTests
    {
        private const string NormalUser = "captain.phil@fifthweek.com";
        private const string NonAutomatedTestUser = "lawrence500@testing.fifthweek.com";
        private const string AutomatedTestUser = "wd_1234567890123@testing.fifthweek.com";
        private const string Subject = "Meow";
        private const string Body = "Paw-some";

        private static readonly MailAddress FromAddress = new MailAddress("a@b.com", "name");
        private static readonly MailboxName MailboxName = MailboxName.Parse("wd_1234567890123");

        private Mock<ISendEmailService> baseService;
        private Mock<ISetLatestMessageDbStatement> setLatestMessage;
        private EndToEndTestSendEmailServiceDecorator target;

        [TestInitialize]
        public void Initialize()
        {
            // Mock potentially side-effecting components with strict mock behaviour.
            this.baseService = new Mock<ISendEmailService>(MockBehavior.Strict);
            this.setLatestMessage = new Mock<ISetLatestMessageDbStatement>(MockBehavior.Strict);

            this.target = new EndToEndTestSendEmailServiceDecorator(this.baseService.Object, this.setLatestMessage.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task ItShouldRequireTo()
        {
            await this.target.SendEmailAsync(null, Subject, Body);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task ItShouldRequireSubject()
        {
            await this.target.SendEmailAsync(NormalUser, null, Body);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task ItShouldRequireBody()
        {
            await this.target.SendEmailAsync(NormalUser, Subject, null);
        }

        [TestMethod]
        public async Task ItShouldForwardNormalUsersToBaseService()
        {
            this.baseService.Setup(_ => _.SendEmailAsync(null, NormalUser, Subject, Body)).Returns(Task.FromResult(0)).Verifiable();

            await this.target.SendEmailAsync(NormalUser, Subject, Body);

            this.baseService.Verify();
        }

        [TestMethod]
        public async Task ItShouldForwardNormalUsersToBaseService2()
        {
            this.baseService.Setup(_ => _.SendEmailAsync(FromAddress, NormalUser, Subject, Body)).Returns(Task.FromResult(0)).Verifiable();

            await this.target.SendEmailAsync(FromAddress, NormalUser, Subject, Body);

            this.baseService.Verify();
        }

        [TestMethod]
        public async Task ItShouldForwardNonAutomatedTestUsersToBaseService()
        {
            this.baseService.Setup(_ => _.SendEmailAsync(null, NonAutomatedTestUser, Subject, Body)).Returns(Task.FromResult(0)).Verifiable();

            await this.target.SendEmailAsync(NonAutomatedTestUser, Subject, Body);

            this.baseService.Verify();
        }

        [TestMethod]
        public async Task ItShouldForwardNonAutomatedTestUsersToBaseService2()
        {
            this.baseService.Setup(_ => _.SendEmailAsync(FromAddress, NonAutomatedTestUser, Subject, Body)).Returns(Task.FromResult(0)).Verifiable();

            await this.target.SendEmailAsync(FromAddress, NonAutomatedTestUser, Subject, Body);

            this.baseService.Verify();
        }

        [TestMethod]
        public async Task ItShouldForwardAutomatedTestUsersToDbStatement()
        {
            this.setLatestMessage.Setup(_ => _.ExecuteAsync(MailboxName, Subject, Body)).Returns(Task.FromResult(0)).Verifiable();

            await this.target.SendEmailAsync(AutomatedTestUser, Subject, Body);

            this.setLatestMessage.Verify();
        }

        [TestMethod]
        public async Task ItShouldForwardAutomatedTestUsersToDbStatement2()
        {
            this.setLatestMessage.Setup(_ => _.ExecuteAsync(MailboxName, Subject, Body)).Returns(Task.FromResult(0)).Verifiable();

            await this.target.SendEmailAsync(FromAddress, AutomatedTestUser, Subject, Body);

            this.setLatestMessage.Verify();
        }
    }
}