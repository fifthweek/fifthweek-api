﻿namespace Fifthweek.Api.Logging.Tests.Controllers
{
    using System;
    using System.Diagnostics;

    using Fifthweek.Api.Core;
    using Fifthweek.Api.Logging.Controllers;
    using Fifthweek.Shared;

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
        public void WhenLogMessageIsNull_ItShouldDoNothing()
        {
            var exceptionHandler = new Mock<IExceptionHandler>(MockBehavior.Strict);
            var traceService = new Mock<ITraceService>(MockBehavior.Strict);
            var controller = new LogController(exceptionHandler.Object, traceService.Object);

            controller.Post(null);
        }

        [TestMethod]
        public void WhenPayloadIsNull_ItShouldDoNothing()
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
        public void WhenLevelIsVerbose_ItShouldTraceVerboseMessage()
        {
            this.sampleMessage.Level = "verbose";
            this.EnsureTrace(this.sampleMessage, TraceLevel.Verbose);
        }

        [TestMethod]
        public void WhenLevelIsVerbose2_ItShouldTraceVerboseMessage()
        {
            this.sampleMessage.Level = "veRBOse";
            this.EnsureTrace(this.sampleMessage, TraceLevel.Verbose);
        }

        [TestMethod]
        public void WhenLevelIsWarn_ItShouldTraceVerboseMessage()
        {
            this.sampleMessage.Level = "warn";
            this.EnsureTrace(this.sampleMessage, TraceLevel.Warning);
        }

        [TestMethod]
        public void WhenLevelIsWarning_ItShouldTraceVerboseMessage()
        {
            this.sampleMessage.Level = "WARNING";
            this.EnsureTrace(this.sampleMessage, TraceLevel.Warning);
        }

        [TestMethod]
        public void WhenLevelIsInfo_ItShouldTraceVerboseMessage()
        {
            this.sampleMessage.Level = "info";
            this.EnsureTrace(this.sampleMessage, TraceLevel.Info);
        }

        [TestMethod]
        public void WhenLevelIsInformation_ItShouldTraceVerboseMessage()
        {
            this.sampleMessage.Level = "information";
            this.EnsureTrace(this.sampleMessage, TraceLevel.Info);
        }

        [TestMethod]
        public void WhenLevelIsNotRecognised_ItShouldReportError()
        {
            this.sampleMessage.Level = "fdsafdsaf";

            var exceptionHandler = new Mock<IExceptionHandler>();
            var traceService = new Mock<ITraceService>(MockBehavior.Strict);

            var controller = new LogController(exceptionHandler.Object, traceService.Object);

            controller.Post(this.sampleMessage);

            exceptionHandler.Verify(v => v.ReportExceptionAsync(It.IsAny<Exception>()), Times.Once);
        }

        [TestMethod]
        public void WhenExceptionIsThrows_ItShouldReportError()
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