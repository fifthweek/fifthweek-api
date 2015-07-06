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
    public class FifthweekRetryOnTransientErrorHandlerTests
    {
        private Mock<IExceptionHandler> exceptionHandler;

        private Mock<ICommandHandler<TestCommand>> command;

        private Mock<ITransientErrorDetectionStrategy> transientErrorDetectionStrategy;

        private FifthweekRetryOnTransientErrorHandler target;

        [TestInitialize]
        public void TestInitialize()
        {
            this.exceptionHandler = new Mock<IExceptionHandler>();
            this.exceptionHandler.Setup(v => v.ReportExceptionAsync(It.IsAny<Exception>()))
                .Callback<Exception>(v => Trace.WriteLine(v));

            this.transientErrorDetectionStrategy = new Mock<ITransientErrorDetectionStrategy>();
            this.transientErrorDetectionStrategy.Setup(v => v.IsTransient(It.IsAny<Exception>())).Returns(true);

            this.target = new FifthweekRetryOnTransientErrorHandler(
                this.exceptionHandler.Object,
                this.transientErrorDetectionStrategy.Object);
            
            this.target.MaxDelay = TimeSpan.FromMilliseconds(10);
        }

        [TestMethod]
        public async Task ItShouldRetrysNonVoidActionsUpToTheMaximumtryCount()
        {
            int tryCount = 0;

            Func<Task<int>> action = () =>
                {
                    ++tryCount;
                    throw SqlExceptionMocker.MakeSqlException(
                        FifthweekRetryOnTransientErrorHandler.SqlDeadlockErrorCode);
                };

            Exception exception = null;
            try
            {
                await this.target.HandleAsync(action);
            }
            catch (Exception t)
            {
                exception = t;
            }

            Assert.IsNotNull(exception);
            Assert.IsInstanceOfType(exception, typeof(RetryLimitExceededException));
            Assert.AreEqual(FifthweekRetryOnTransientErrorHandler.DefaultMaxRetryCount + 1, tryCount);
            Assert.AreEqual(FifthweekRetryOnTransientErrorHandler.DefaultMaxRetryCount, this.target.RetryCount);
        }

        [TestMethod]
        public async Task ItShouldRetryVoidActionsUpToTheMaximumtryCount()
        {
            int tryCount = 0;

            Func<Task> action = () =>
            {
                ++tryCount;
                throw SqlExceptionMocker.MakeSqlException(
                    FifthweekRetryOnTransientErrorHandler.SqlDeadlockErrorCode);
            };

            Exception exception = null;
            try
            {
                await this.target.HandleAsync(action);
            }
            catch (Exception t)
            {
                exception = t;
            }

            Assert.IsNotNull(exception);
            Assert.IsInstanceOfType(exception, typeof(RetryLimitExceededException));
            Assert.AreEqual(FifthweekRetryOnTransientErrorHandler.DefaultMaxRetryCount + 1, tryCount);
            Assert.AreEqual(FifthweekRetryOnTransientErrorHandler.DefaultMaxRetryCount, this.target.RetryCount);
        }

        [TestMethod]
        public async Task ItShouldRetryNonVoidActionsUntilSuccess()
        {
            int tryCount = 0;

            Func<Task<int>> action = () =>
            {
                    ++tryCount;
                    if (tryCount <= FifthweekRetryOnTransientErrorHandler.DefaultMaxRetryCount)
                    {
                        throw SqlExceptionMocker.MakeSqlException(
                            FifthweekRetryOnTransientErrorHandler.SqlDeadlockErrorCode);
                    }

                    return Task.FromResult(0);
                };

            await this.target.HandleAsync(action);

            Assert.AreEqual(FifthweekRetryOnTransientErrorHandler.DefaultMaxRetryCount + 1, tryCount);
            Assert.AreEqual(FifthweekRetryOnTransientErrorHandler.DefaultMaxRetryCount, this.target.RetryCount);
        }

        [TestMethod]
        public async Task ItShouldRetryVoidActionsUntilSuccess()
        {
            int tryCount = 0;

            Func<Task> action = () =>
            {
                ++tryCount;
                if (tryCount <= FifthweekRetryOnTransientErrorHandler.DefaultMaxRetryCount)
                {
                    throw SqlExceptionMocker.MakeSqlException(
                        FifthweekRetryOnTransientErrorHandler.SqlDeadlockErrorCode);
                }

                return Task.FromResult(0);
            };

            await this.target.HandleAsync(action);

            Assert.AreEqual(FifthweekRetryOnTransientErrorHandler.DefaultMaxRetryCount + 1, tryCount);
            Assert.AreEqual(FifthweekRetryOnTransientErrorHandler.DefaultMaxRetryCount, this.target.RetryCount);
        }

        [TestMethod]
        public async Task ItShouldRetryActionsUntilNonTransientFailure()
        {
            int tryCount = 0;

            Func<Task<int>> action = () =>
            {
                ++tryCount;
                if (tryCount <= FifthweekRetryOnTransientErrorHandler.DefaultMaxRetryCount)
                {
                    throw SqlExceptionMocker.MakeSqlException(
                        FifthweekRetryOnTransientErrorHandler.SqlDeadlockErrorCode);
                }
                else
                {
                    this.transientErrorDetectionStrategy.Setup(v => v.IsTransient(It.IsAny<Exception>())).Returns(false);
                    throw new DivideByZeroException();
                }
            };

            Exception exception = null;
            try
            {
                await this.target.HandleAsync(action);
            }
            catch (Exception t)
            {
                exception = t;
            }

            Assert.IsNotNull(exception);
            Assert.IsInstanceOfType(exception, typeof(DivideByZeroException));
            Assert.AreEqual(FifthweekRetryOnTransientErrorHandler.DefaultMaxRetryCount + 1, tryCount);
            Assert.AreEqual(FifthweekRetryOnTransientErrorHandler.DefaultMaxRetryCount, this.target.RetryCount);
        }

        [TestMethod]
        public async Task ItShouldImmediatelyRetryAfterTheFirstTransientFailure()
        {
            var retryStrategy =
                FifthweekRetryOnTransientErrorHandler.CreateRetryStrategy(
                    FifthweekRetryOnTransientErrorHandler.DefaultMaxRetryCount,
                    FifthweekRetryOnTransientErrorHandler.DefaultMaxDelay);

            Assert.IsTrue(retryStrategy.FastFirstRetry);

            var shouldRetry = retryStrategy.GetShouldRetry();
            TimeSpan firstDelay;
            shouldRetry(0, new Exception(), out firstDelay);

            Assert.AreEqual(TimeSpan.Zero, firstDelay);
        }

        [TestMethod]
        public async Task ItShouldNotPosponeTheUserUnduely()
        {
            var retryStrategy = FifthweekRetryOnTransientErrorHandler.CreateRetryStrategy(
                    FifthweekRetryOnTransientErrorHandler.DefaultMaxRetryCount,
                    FifthweekRetryOnTransientErrorHandler.DefaultMaxDelay);

            var shouldRetry = retryStrategy.GetShouldRetry();

            TimeSpan totalDelay = TimeSpan.Zero;
            for (int i = 0; i < FifthweekRetryOnTransientErrorHandler.DefaultMaxRetryCount; i++)
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