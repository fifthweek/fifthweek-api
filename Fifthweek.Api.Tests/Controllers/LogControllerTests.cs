namespace Fifthweek.Api.Tests
{
    using System;
    using System.Diagnostics;

    using Fifthweek.Api.Controllers;
    using Fifthweek.Api.Models;
    using Fifthweek.Api.Services;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    using Newtonsoft.Json.Linq;

    [TestClass]
    public class LogControllerTests
    {
        private BrowserLogMessage sampleMessage;

        [TestInitialize]
        public void TestInitialize()
        {
            this.sampleMessage = new BrowserLogMessage { Payload = JToken.Parse("{ test: 'hi' }"), };
        }

        [TestMethod]
        public void WhenLogMessageIsNullItShouldDoNothing()
        {
            var exceptionHandler = new Mock<IExceptionHandler>(MockBehavior.Strict);
            var traceService = new Mock<ITraceService>(MockBehavior.Strict);
            var controller = new LogController(exceptionHandler.Object, traceService.Object);

            controller.Post(null);
        }

        [TestMethod]
        public void WhenPayloadIsNullItShouldDoNothing()
        {
            var exceptionHandler = new Mock<IExceptionHandler>(MockBehavior.Strict);
            var traceService = new Mock<ITraceService>(MockBehavior.Strict);
            var controller = new LogController(exceptionHandler.Object, traceService.Object);

            controller.Post(new BrowserLogMessage
            {
                Payload = null,
            });
        }

        [TestMethod]
        public void WhenLevelIsVerboseShouldTraceVerboseMessage()
        {
            this.sampleMessage.Level = "verbose";
            this.EnsureTrace(this.sampleMessage, TraceLevel.Verbose);
        }

        [TestMethod]
        public void WhenLevelIsVerbose2ShouldTraceVerboseMessage()
        {
            this.sampleMessage.Level = "veRBOse";
            this.EnsureTrace(this.sampleMessage, TraceLevel.Verbose);
        }

        [TestMethod]
        public void WhenLevelIsWarnShouldTraceVerboseMessage()
        {
            this.sampleMessage.Level = "warn";
            this.EnsureTrace(this.sampleMessage, TraceLevel.Warning);
        }

        [TestMethod]
        public void WhenLevelIsWarningShouldTraceVerboseMessage()
        {
            this.sampleMessage.Level = "WARNING";
            this.EnsureTrace(this.sampleMessage, TraceLevel.Warning);
        }

        [TestMethod]
        public void WhenLevelIsInfoShouldTraceVerboseMessage()
        {
            this.sampleMessage.Level = "info";
            this.EnsureTrace(this.sampleMessage, TraceLevel.Info);
        }

        [TestMethod]
        public void WhenLevelIsInformationShouldTraceVerboseMessage()
        {
            this.sampleMessage.Level = "information";
            this.EnsureTrace(this.sampleMessage, TraceLevel.Info);
        }

        [TestMethod]
        public void WhenLevelIsNotRecognisedShouldReportError()
        {
            this.sampleMessage.Level = "fdsafdsaf";

            var exceptionHandler = new Mock<IExceptionHandler>();
            var traceService = new Mock<ITraceService>(MockBehavior.Strict);

            var controller = new LogController(exceptionHandler.Object, traceService.Object);

            controller.Post(this.sampleMessage);

            exceptionHandler.Verify(v => v.ReportExceptionAsync(It.IsAny<Exception>()), Times.Once);
        }

        [TestMethod]
        public void WhenExceptionIsThrowsShouldReportError()
        {
            this.sampleMessage.Level = "info";

            var exceptionHandler = new Mock<IExceptionHandler>();
            var traceService = new Mock<ITraceService>();

            traceService.Setup(v => v.Log(It.IsAny<TraceLevel>(), It.IsAny<string>())).Throws<Exception>();
            
            var controller = new LogController(exceptionHandler.Object, traceService.Object);

            controller.Post(this.sampleMessage);

            exceptionHandler.Verify(v => v.ReportExceptionAsync(It.IsAny<Exception>()), Times.Once);
        }

        private void EnsureTrace(BrowserLogMessage logMessage, TraceLevel level)
        {
            var exceptionHandler = new Mock<IExceptionHandler>(MockBehavior.Strict);
            var traceService = new Mock<ITraceService>();
            
            var controller = new LogController(exceptionHandler.Object, traceService.Object);

            controller.Post(logMessage);

            traceService.Verify(v => v.Log(level, It.IsAny<string>()), Times.AtLeastOnce);
        }
    }
}