namespace Fifthweek.Api.Identity.Tests.Membership.Commands
{
    using System;
    using System.Threading.Tasks;

    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Membership.Commands;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Posts.Shared;
    using Fifthweek.Shared;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    using Constants = Fifthweek.Api.Core.Constants;

    [TestClass]
    public class SubmitFeedbackCommandHandlerTests
    {
        private static readonly ValidComment Message = ValidComment.Parse("A valid comment");
        private static readonly ValidEmail ValidEmail = ValidEmail.Parse("name@test.com");
        private static readonly string Activity = "Feedback: name@test.com, A valid comment";
        private static readonly string AnonymousActivity = "Feedback: Anonymous, A valid comment";

        private Mock<IFifthweekActivityReporter> activityReporter;
        private Mock<IExceptionHandler> exceptionHandler;

        private SubmitFeedbackCommandHandler target;

        [TestInitialize]
        public void Initialize()
        {
            this.activityReporter = new Mock<IFifthweekActivityReporter>(MockBehavior.Strict);
            this.exceptionHandler = new Mock<IExceptionHandler>(MockBehavior.Strict);

            this.target = new SubmitFeedbackCommandHandler(this.activityReporter.Object, this.exceptionHandler.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task WhenCommandIsNull_ItShouldThrowAnException()
        {
            await this.target.HandleAsync(null);
        }

        [TestMethod]
        public async Task WhenEmailIsFromTestDomain_ItShouldNotReport()
        {
            await this.target.HandleAsync(new SubmitFeedbackCommand(Message, ValidEmail.Parse("something" + Constants.TestEmailDomain)));
            // Test verification handled by strict behaviour.
        }

        [TestMethod]
        public async Task WhenReportingSucceeds_ItShouldCompleteSuccessfully()
        {
            this.activityReporter.Setup(v => v.ReportActivityAsync(Activity))
                .Returns(Task.FromResult(0))
                .Verifiable();

            await this.target.HandleAsync(new SubmitFeedbackCommand(Message, ValidEmail));

            this.activityReporter.Verify();
        }

        [TestMethod]
        public async Task WhenReportingSucceedsWithNoEmail_ItShouldCompleteSuccessfully()
        {
            this.activityReporter.Setup(v => v.ReportActivityAsync(AnonymousActivity))
                .Returns(Task.FromResult(0))
                .Verifiable();

            await this.target.HandleAsync(new SubmitFeedbackCommand(Message, null));

            this.activityReporter.Verify();
        }

        [TestMethod]
        public async Task WhenReportingFails_ItShouldLogErrorAndCompleteSuccessfully()
        {
            this.activityReporter.Setup(v => v.ReportActivityAsync(Activity))
                .Throws(new DivideByZeroException());

            this.exceptionHandler.Setup(v => v.ReportExceptionAsync(It.IsAny<Exception>())).Verifiable();

            await this.target.HandleAsync(new SubmitFeedbackCommand(Message, ValidEmail));

            this.exceptionHandler.Verify();
        }
    }
}