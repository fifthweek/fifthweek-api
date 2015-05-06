namespace Fifthweek.Api.Tests
{
    using System;
    using System.Net.Http;
    using System.Threading.Tasks;

    using Fifthweek.Api.Core;
    using Fifthweek.Logging;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestClass]
    public class FifthweekActivityReporterTests
    {
        private Mock<IActivityReportingService> activityReportingService;
        private Mock<IRequestContext> requestContext;
        private Mock<IDeveloperRepository> developerRepository;
        private Mock<IExceptionHandler> exceptionHandler;

        private FifthweekActivityReporter target;

        public virtual void Initialize()
        {
            this.activityReportingService = new Mock<IActivityReportingService>();
            this.requestContext = new Mock<IRequestContext>();
            this.developerRepository = new Mock<IDeveloperRepository>();
            this.exceptionHandler = new Mock<IExceptionHandler>(MockBehavior.Strict);

            this.target = new FifthweekActivityReporter(
                this.activityReportingService.Object,
                this.requestContext.Object,
                this.developerRepository.Object,
                this.exceptionHandler.Object);
        }

        [TestClass]
        public class ReportActivityInBackground : FifthweekActivityReporterTests
        {
            [TestInitialize]
            public override void Initialize()
            {
                base.Initialize();
            }

            [TestMethod]
            public async Task WhenDeveloperNameExists_ItShouldReportActivityWithDeveloper()
            {
                var requestMessage = new HttpRequestMessage();
                requestMessage.Headers.Add(Constants.DeveloperNameRequestHeaderKey, "DeveloperName");
                this.requestContext.Setup(v => v.Request).Returns(requestMessage);

                var developer = new Developer("name", "slackName", "email");
                this.developerRepository.Setup(v => v.TryGetByGitNameAsync("DeveloperName")).ReturnsAsync(developer);

                this.activityReportingService.Setup(v => v.ReportActivityAsync("activity", developer))
                    .Returns(Task.FromResult(0))
                    .Verifiable();

                this.target.ReportActivityInBackground("activity");

                this.activityReportingService.Verify();
            }

            [TestMethod]
            public async Task WhenDeveloperNameDoesNotExist_ItShouldReportActivityWithoutDeveloper()
            {
                this.requestContext.Setup(v => v.Request).Returns(new HttpRequestMessage());

                this.developerRepository.Setup(v => v.TryGetByGitNameAsync(null)).ReturnsAsync(null);

                this.activityReportingService.Setup(v => v.ReportActivityAsync("activity", null))
                    .Returns(Task.FromResult(0))
                    .Verifiable();

                this.target.ReportActivityInBackground("activity");

                this.activityReportingService.Verify();
            }

            [TestMethod]
            public async Task WhenReportingFails_ItShouldReportError()
            {
                var requestMessage = new HttpRequestMessage();
                requestMessage.Headers.Add(Constants.DeveloperNameRequestHeaderKey, "DeveloperName");
                this.requestContext.Setup(v => v.Request).Returns(requestMessage);

                var developer = new Developer("name", "slackName", "email");
                this.developerRepository.Setup(v => v.TryGetByGitNameAsync("DeveloperName")).ReturnsAsync(developer);

                this.activityReportingService.Setup(v => v.ReportActivityAsync("activity", developer))
                    .Throws(new DivideByZeroException());

                this.exceptionHandler.Setup(v => v.ReportExceptionAsync(It.IsAny<DivideByZeroException>(), "DeveloperName")).Verifiable();

                this.target.ReportActivityInBackground("activity");

                this.exceptionHandler.Verify();
            }
        }

        [TestClass]
        public class ReportActivityAsync : FifthweekActivityReporterTests
        {
            [TestInitialize]
            public override void Initialize()
            {
                base.Initialize();
            }

            [TestMethod]
            public async Task WhenDeveloperNameExists_ItShouldReportActivityWithDeveloper()
            {
                var requestMessage = new HttpRequestMessage();
                requestMessage.Headers.Add(Constants.DeveloperNameRequestHeaderKey, "DeveloperName");
                this.requestContext.Setup(v => v.Request).Returns(requestMessage);

                var developer = new Developer("name", "slackName", "email");
                this.developerRepository.Setup(v => v.TryGetByGitNameAsync("DeveloperName")).ReturnsAsync(developer);

                this.activityReportingService.Setup(v => v.ReportActivityAsync("activity", developer))
                    .Returns(Task.FromResult(0))
                    .Verifiable();

                await this.target.ReportActivityAsync("activity");

                this.activityReportingService.Verify();
            }

            [TestMethod]
            public async Task WhenDeveloperNameDoesNotExist_ItShouldReportActivityWithoutDeveloper()
            {
                this.requestContext.Setup(v => v.Request).Returns(new HttpRequestMessage());

                this.developerRepository.Setup(v => v.TryGetByGitNameAsync(null)).ReturnsAsync(null);

                this.activityReportingService.Setup(v => v.ReportActivityAsync("activity", null))
                    .Returns(Task.FromResult(0))
                    .Verifiable();

                await this.target.ReportActivityAsync("activity");

                this.activityReportingService.Verify();
            }

            [TestMethod]
            [ExpectedException(typeof(DivideByZeroException))]
            public async Task WhenReportingFails_ItShouldPropogateError()
            {
                var requestMessage = new HttpRequestMessage();
                requestMessage.Headers.Add(Constants.DeveloperNameRequestHeaderKey, "DeveloperName");
                this.requestContext.Setup(v => v.Request).Returns(requestMessage);

                var developer = new Developer("name", "slackName", "email");
                this.developerRepository.Setup(v => v.TryGetByGitNameAsync("DeveloperName")).ReturnsAsync(developer);

                this.activityReportingService.Setup(v => v.ReportActivityAsync("activity", developer))
                    .Throws(new DivideByZeroException());

                await this.target.ReportActivityAsync("activity");
            }
        }
    }
}