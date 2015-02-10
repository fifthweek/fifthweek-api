namespace Fifthweek.Api.Core.Tests
{
    using System;
    using System.ComponentModel;
    using System.Data.Entity.Core;
    using System.Data.SqlClient;
    using System.Diagnostics;
    using System.IO;
    using System.Threading.Tasks;

    using Microsoft.Practices.EnterpriseLibrary.TransientFaultHandling;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    using RetryLimitExceededException = Fifthweek.Api.Core.RetryLimitExceededException;

    [TestClass]
    public class RetryOnTransientErrorDecoratorTests
    {
        private Mock<IExceptionHandler> exceptionHandler;

        private Mock<ICommandHandler<TestCommand>> command;

        private Mock<ITransientErrorDetectionStrategy> transientErrorDetectionStrategy;

        private RetryOnTransientErrorCommandHandlerDecorator<TestCommand> decorator;

        [TestInitialize]
        public void TestInitialize()
        {
            this.exceptionHandler = new Mock<IExceptionHandler>();
            this.exceptionHandler.Setup(v => v.ReportExceptionAsync(It.IsAny<Exception>()))
                .Callback<Exception>(v => Trace.WriteLine(v));

            this.transientErrorDetectionStrategy = new Mock<ITransientErrorDetectionStrategy>();
            this.transientErrorDetectionStrategy.Setup(v => v.IsTransient(It.IsAny<Exception>())).Returns(true);

            this.command = new Mock<ICommandHandler<TestCommand>>();
            this.decorator = new RetryOnTransientErrorCommandHandlerDecorator<TestCommand>(
                this.exceptionHandler.Object,
                this.transientErrorDetectionStrategy.Object,
                this.command.Object,
                RetryOnTransientErrorDecoratorBase.MaxRetryCount,
                TimeSpan.FromMilliseconds(10));
        }

        [TestMethod]
        public async Task ItShouldRetryCommandsUpToTheMaximumtryCount()
        {
            int tryCount = 0;

            this.command.Setup(v => v.HandleAsync(It.IsAny<TestCommand>()))
                .Callback(() =>
                {
                    ++tryCount;
                    throw SqlExceptionMocker.MakeSqlException(
                        RetryOnTransientErrorDecoratorBase.SqlDeadlockErrorCode);
                }).Returns(Task.FromResult(0));

            Exception exception = null;
            try
            {
                await this.decorator.HandleAsync(new TestCommand());
            }
            catch (Exception t)
            {
                exception = t;
            }

            Assert.IsNotNull(exception);
            Assert.IsInstanceOfType(exception, typeof(RetryLimitExceededException));
            Assert.AreEqual(RetryOnTransientErrorDecoratorBase.MaxRetryCount + 1, tryCount);
            Assert.AreEqual(RetryOnTransientErrorDecoratorBase.MaxRetryCount, this.decorator.RetryCount);
        }

        [TestMethod]
        public async Task ItShouldRetryQueriesUpToTheMaximumtryCount()
        {
            var query = new Mock<IQueryHandler<TestQuery, bool>>();
            var queryDecorator = new RetryOnTransientErrorQueryHandlerDecorator<TestQuery, bool>(
                this.exceptionHandler.Object,
                this.transientErrorDetectionStrategy.Object,
                query.Object,
                RetryOnTransientErrorDecoratorBase.MaxRetryCount,
                TimeSpan.FromMilliseconds(10));

            int tryCount = 0;

            query.Setup(v => v.HandleAsync(It.IsAny<TestQuery>()))
                .Callback(() =>
                {
                    ++tryCount;
                    throw SqlExceptionMocker.MakeSqlException(
                        RetryOnTransientErrorDecoratorBase.SqlDeadlockErrorCode);
                }).Returns(Task.FromResult(false));

            Exception exception = null;
            try
            {
                await queryDecorator.HandleAsync(new TestQuery());
            }
            catch (Exception t)
            {
                exception = t;
            }

            Assert.IsNotNull(exception);
            Assert.IsInstanceOfType(exception, typeof(RetryLimitExceededException));
            Assert.AreEqual(RetryOnTransientErrorDecoratorBase.MaxRetryCount + 1, tryCount);
            Assert.AreEqual(RetryOnTransientErrorDecoratorBase.MaxRetryCount, queryDecorator.RetryCount);
        }

        [TestMethod]
        public async Task ItShouldRetryCommandsUntilSuccess()
        {
            int tryCount = 0;

            this.command.Setup(v => v.HandleAsync(It.IsAny<TestCommand>()))
                .Callback(() =>
                {
                    ++tryCount;
                    if (tryCount <= RetryOnTransientErrorDecoratorBase.MaxRetryCount)
                    {
                        throw SqlExceptionMocker.MakeSqlException(
                            RetryOnTransientErrorDecoratorBase.SqlDeadlockErrorCode);
                    }
                }).Returns(Task.FromResult(0));

            await this.decorator.HandleAsync(new TestCommand());

            Assert.AreEqual(RetryOnTransientErrorDecoratorBase.MaxRetryCount + 1, tryCount);
            Assert.AreEqual(RetryOnTransientErrorDecoratorBase.MaxRetryCount, this.decorator.RetryCount);
        }

        [TestMethod]
        public async Task ItShouldRetryQueriesUntilSuccess()
        {
            var query = new Mock<IQueryHandler<TestQuery, bool>>();
            var queryDecorator = new RetryOnTransientErrorQueryHandlerDecorator<TestQuery, bool>(
                this.exceptionHandler.Object,
                this.transientErrorDetectionStrategy.Object,
                query.Object,
                RetryOnTransientErrorDecoratorBase.MaxRetryCount,
                TimeSpan.FromMilliseconds(10));

            int tryCount = 0;

            query.Setup(v => v.HandleAsync(It.IsAny<TestQuery>()))
                .Callback(() =>
                {
                    ++tryCount;
                    if (tryCount <= RetryOnTransientErrorDecoratorBase.MaxRetryCount)
                    {
                        throw SqlExceptionMocker.MakeSqlException(
                            RetryOnTransientErrorDecoratorBase.SqlDeadlockErrorCode);
                    }
                }).Returns(Task.FromResult(false));

            await queryDecorator.HandleAsync(new TestQuery());

            Assert.AreEqual(RetryOnTransientErrorDecoratorBase.MaxRetryCount + 1, tryCount);
            Assert.AreEqual(RetryOnTransientErrorDecoratorBase.MaxRetryCount, queryDecorator.RetryCount);
        }

        [TestMethod]
        public async Task ItShouldRetryCommandsUntilNonTransientFailure()
        {
            int tryCount = 0;

            this.command.Setup(v => v.HandleAsync(It.IsAny<TestCommand>()))
                .Callback(() =>
                {
                    ++tryCount;
                    if (tryCount <= RetryOnTransientErrorDecoratorBase.MaxRetryCount)
                    {
                        throw SqlExceptionMocker.MakeSqlException(
                            RetryOnTransientErrorDecoratorBase.SqlDeadlockErrorCode);
                    }
                    else
                    {
                        this.transientErrorDetectionStrategy.Setup(v => v.IsTransient(It.IsAny<Exception>())).Returns(false);
                        throw new DivideByZeroException();
                    }
                }).Returns(Task.FromResult(0));

            Exception exception = null;
            try
            {
                await this.decorator.HandleAsync(new TestCommand());
            }
            catch (Exception t)
            {
                exception = t;
            }

            Assert.IsNotNull(exception);
            Assert.IsInstanceOfType(exception, typeof(DivideByZeroException));
            Assert.AreEqual(RetryOnTransientErrorDecoratorBase.MaxRetryCount + 1, tryCount);
            Assert.AreEqual(RetryOnTransientErrorDecoratorBase.MaxRetryCount, this.decorator.RetryCount);
        }

        [TestMethod]
        public async Task ItShouldImmediatelyRetryAfterTheFirstTransientFailure()
        {
            var retryStrategy =
                RetryOnTransientErrorDecoratorBase.CreateRetryStrategy(
                    RetryOnTransientErrorDecoratorBase.MaxRetryCount,
                    RetryOnTransientErrorDecoratorBase.MaxDelay);

            Assert.IsTrue(retryStrategy.FastFirstRetry);

            var shouldRetry = retryStrategy.GetShouldRetry();
            TimeSpan firstDelay;
            shouldRetry(0, new Exception(), out firstDelay);

            Assert.AreEqual(TimeSpan.Zero, firstDelay);
        }

        [TestMethod]
        public async Task ItShouldNotPosponeTheUserUnduely()
        {
            var retryStrategy = RetryOnTransientErrorDecoratorBase.CreateRetryStrategy(
                    RetryOnTransientErrorDecoratorBase.MaxRetryCount,
                    RetryOnTransientErrorDecoratorBase.MaxDelay);

            var shouldRetry = retryStrategy.GetShouldRetry();

            TimeSpan totalDelay = TimeSpan.Zero;
            for (int i = 0; i < RetryOnTransientErrorDecoratorBase.MaxRetryCount; i++)
            {
                TimeSpan newDelay;
                shouldRetry(i, new Exception(), out newDelay);
                Trace.WriteLine(string.Format("Retry {0}, Delay {1}", i, newDelay));
                totalDelay += newDelay;
            }

            Trace.WriteLine(string.Format("Total Delay {0}", totalDelay));

            Assert.IsTrue(totalDelay < TimeSpan.FromSeconds(30));
        }
    }
}