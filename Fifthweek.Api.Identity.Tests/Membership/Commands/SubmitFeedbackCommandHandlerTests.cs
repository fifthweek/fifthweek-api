namespace Fifthweek.Api.Identity.Tests.Membership.Commands
{
    using System;
    using System.Net.Mail;
    using System.Threading.Tasks;

    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Membership;
    using Fifthweek.Api.Identity.Membership.Commands;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Identity.Tests.Shared.Membership;
    using Fifthweek.Api.Posts.Shared;
    using Fifthweek.Shared;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    using Constants = Fifthweek.Api.Core.Constants;

    [TestClass]
    public class SubmitFeedbackCommandHandlerTests
    {
        private static readonly UserId UserId = UserId.Random();
        private static readonly Requester Requester = Requester.Authenticated(UserId);
        private static readonly Username Username = new Username("username");
        private static readonly ValidComment Message = ValidComment.Parse("A valid comment");
        private static readonly ValidEmail ValidEmail = ValidEmail.Parse("name@test.com");
        private static readonly string HtmlMessage = "html-message";

        private static readonly GetFeedbackUserDataDbStatement.GetFeedbackUserDataResult AccountSettings = new GetFeedbackUserDataDbStatement.GetFeedbackUserDataResult(
            ValidEmail,
            Username);

        private static readonly GetFeedbackUserDataDbStatement.GetFeedbackUserDataResult TestAccountSettings = new GetFeedbackUserDataDbStatement.GetFeedbackUserDataResult(
            new Email("something" + Constants.TestEmailDomain),
            Username);

        private static readonly string Activity = "Feedback from username, name@test.com: A valid comment";

        private Mock<IRequesterSecurity> requesterSecurity;
        private Mock<IGetFeedbackUserDataDbStatement> getFeedbackUserData;
        private Mock<IFifthweekActivityReporter> activityReporter;
        private Mock<IMarkdownRenderer> markdownRenderer;
        private Mock<ISendEmailService> sendEmailService;
        private Mock<IExceptionHandler> exceptionHandler;

        private SubmitFeedbackCommandHandler target;

        [TestInitialize]
        public void Initialize()
        {
            this.requesterSecurity = new Mock<IRequesterSecurity>();
            this.getFeedbackUserData = new Mock<IGetFeedbackUserDataDbStatement>(MockBehavior.Strict);
            this.activityReporter = new Mock<IFifthweekActivityReporter>(MockBehavior.Strict);
            this.markdownRenderer = new Mock<IMarkdownRenderer>(MockBehavior.Strict);
            this.sendEmailService = new Mock<ISendEmailService>(MockBehavior.Strict);
            this.exceptionHandler = new Mock<IExceptionHandler>(MockBehavior.Strict);

            this.requesterSecurity.SetupFor(Requester);

            this.target = new SubmitFeedbackCommandHandler(
                this.requesterSecurity.Object,
                this.getFeedbackUserData.Object,
                this.activityReporter.Object, 
                this.markdownRenderer.Object,
                this.sendEmailService.Object,
                this.exceptionHandler.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task WhenCommandIsNull_ItShouldThrowAnException()
        {
            await this.target.HandleAsync(null);
        }

        [TestMethod]
        [ExpectedException(typeof(UnauthorizedException))]
        public async Task WhenUnauthenticated_ItShouldThrowUnauthorizedException()
        {
            await this.target.HandleAsync(new SubmitFeedbackCommand(
                Requester.Unauthenticated,
                Message));
        }

        [TestMethod]
        public async Task WhenEmailIsFromTestDomain_ItShouldNotReport()
        {
            this.getFeedbackUserData.Setup(v => v.ExecuteAsync(UserId))
                .ReturnsAsync(TestAccountSettings);

            await this.target.HandleAsync(new SubmitFeedbackCommand(Requester, Message));
            // Test verification handled by strict behaviour.
        }

        [TestMethod]
        public async Task WhenReportingSucceeds_ItShouldCompleteSuccessfully()
        {
            this.getFeedbackUserData.Setup(v => v.ExecuteAsync(UserId))
                .ReturnsAsync(AccountSettings);

            this.activityReporter.Setup(v => v.ReportActivityAsync(Activity))
                .Returns(Task.FromResult(0))
                .Verifiable();

            this.markdownRenderer.Setup(v => v.GetHtml(Message.Value)).Returns(HtmlMessage);

            this.sendEmailService.Setup(v => v.SendEmailAsync(
                new MailAddress(AccountSettings.Email.Value, AccountSettings.Username.Value),
                Fifthweek.Shared.Constants.FifthweekEmailAddress.Address,
                "Feedback from " + AccountSettings.Username.Value,
                HtmlMessage))
                .Returns(Task.FromResult(0))
                .Verifiable();

            await this.target.HandleAsync(new SubmitFeedbackCommand(Requester, Message));

            this.activityReporter.Verify();
            this.sendEmailService.Verify();
        }

        [TestMethod]
        public async Task WhenReportingFails_ItShouldLogErrorAndCompleteSuccessfully()
        {
            this.getFeedbackUserData.Setup(v => v.ExecuteAsync(UserId))
                .ReturnsAsync(AccountSettings);
            
            this.activityReporter.Setup(v => v.ReportActivityAsync(Activity))
                .Throws(new DivideByZeroException());

            this.exceptionHandler.Setup(v => v.ReportExceptionAsync(It.IsAny<Exception>())).Verifiable();

            await this.target.HandleAsync(new SubmitFeedbackCommand(Requester, Message));

            this.exceptionHandler.Verify();
        }
    }
}