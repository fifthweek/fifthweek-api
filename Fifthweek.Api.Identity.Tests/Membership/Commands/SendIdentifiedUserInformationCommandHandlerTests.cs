namespace Fifthweek.Api.Identity.Tests.Membership.Commands
{
    using System;
    using System.Threading.Tasks;

    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Membership.Commands;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Shared;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    using Constants = Fifthweek.Api.Core.Constants;

    [TestClass]
    public class SendIdentifiedUserInformationCommandHandlerTests
    {
        private static readonly string Name = "name";
        private static readonly Email Email = ValidEmail.Parse("name@test.com");
        private static readonly Username Username = ValidUsername.Parse("username");
        private static readonly string ActivityPrefix = "Identified User: ";
        private static readonly string UpdatingPrefix = "Updated Identified User: ";

        private Mock<IFifthweekActivityReporter> activityReporter;
        private Mock<IExceptionHandler> exceptionHandler;

        private SendIdentifiedUserInformationCommandHandler target;

        [TestInitialize]
        public void Initialize()
        {
            this.activityReporter = new Mock<IFifthweekActivityReporter>(MockBehavior.Strict);
            this.exceptionHandler = new Mock<IExceptionHandler>(MockBehavior.Strict);

            this.target = new SendIdentifiedUserInformationCommandHandler(this.activityReporter.Object, this.exceptionHandler.Object);
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
            await this.target.HandleAsync(new SendIdentifiedUserInformationCommand(false, ValidEmail.Parse("something" + Constants.TestEmailDomain), Name, Username));
            // Test verification handled by strict behaviour.
        }

        [TestMethod]
        public async Task WhenEmailIsFromFifthweekDomain_ItShouldNotReport()
        {
            await this.target.HandleAsync(new SendIdentifiedUserInformationCommand(false, ValidEmail.Parse("something" + Constants.FifthweekEmailDomain), Name, Username));
            // Test verification handled by strict behaviour.
        }
        
        [TestMethod]
        public async Task WhenEmailExists_ItShouldReportWithEmail()
        {
            this.activityReporter.Setup(v => v.ReportActivityAsync(ActivityPrefix + "name@test.com"))
                .Returns(Task.FromResult(0))
                .Verifiable();

            await this.target.HandleAsync(new SendIdentifiedUserInformationCommand(false, Email, null, null));

            this.activityReporter.Verify();
        }

        [TestMethod]
        public async Task WhenNameExists_ItShouldReportWithName()
        {
            this.activityReporter.Setup(v => v.ReportActivityAsync(ActivityPrefix + "name, name@test.com"))
                .Returns(Task.FromResult(0))
                .Verifiable();

            await this.target.HandleAsync(new SendIdentifiedUserInformationCommand(false, Email, Name, null));

            this.activityReporter.Verify();
        }

        [TestMethod]
        public async Task WhenUsernameExists_ItShouldReportWithUsername()
        {
            this.activityReporter.Setup(v => v.ReportActivityAsync(ActivityPrefix + "username, name@test.com"))
                .Returns(Task.FromResult(0))
                .Verifiable();

            await this.target.HandleAsync(new SendIdentifiedUserInformationCommand(false, Email, null, Username));

            this.activityReporter.Verify();
        }

        [TestMethod]
        public async Task WhenNameAndUsernameExists_ItShouldReportWithNameAndUsername()
        {
            this.activityReporter.Setup(v => v.ReportActivityAsync(ActivityPrefix + "name, username, name@test.com"))
                .Returns(Task.FromResult(0))
                .Verifiable();

            await this.target.HandleAsync(new SendIdentifiedUserInformationCommand(false, Email, Name, Username));

            this.activityReporter.Verify();
        }

        [TestMethod]
        public async Task WhenUpdating_ItShouldReportWithUpdatingPrefix()
        {
            this.activityReporter.Setup(v => v.ReportActivityAsync(UpdatingPrefix + "name, username, name@test.com"))
                .Returns(Task.FromResult(0))
                .Verifiable();

            await this.target.HandleAsync(new SendIdentifiedUserInformationCommand(true, Email, Name, Username));

            this.activityReporter.Verify();
        }

        [TestMethod]
        public async Task WhenReportingFails_ItShouldLogErrorAndCompleteSuccessfully()
        {
            this.activityReporter.Setup(v => v.ReportActivityAsync(ActivityPrefix + "name@test.com"))
                .Throws(new DivideByZeroException());

            this.exceptionHandler.Setup(v => v.ReportExceptionAsync(It.IsAny<Exception>())).Verifiable();

            await this.target.HandleAsync(new SendIdentifiedUserInformationCommand(false, Email, null, null));

            this.exceptionHandler.Verify();
        }
    }
}