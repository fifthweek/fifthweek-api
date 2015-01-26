namespace Fifthweek.Api.Core.Tests
{
    using System;
    using System.Data.SqlClient;
    using System.Diagnostics;
    using System.IO;
    using System.Threading.Tasks;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestClass]
    public class RetryOnTransientErrorDecoratorTests
    {
        private Mock<IExceptionHandler> exceptionHandler;

        private Mock<ICommandHandler<TestCommand>> command;

        private RetryOnTransientErrorCommandHandlerDecorator<TestCommand> decorator;

        [TestInitialize]
        public void TestInitialize()
        {
            this.exceptionHandler = new Mock<IExceptionHandler>();
            this.command = new Mock<ICommandHandler<TestCommand>>();
            this.decorator = new RetryOnTransientErrorCommandHandlerDecorator<TestCommand>(
                this.exceptionHandler.Object,
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
        }

        [TestMethod]
        public async Task ItShouldRetryQueriesUpToTheMaximumtryCount()
        {
            var query = new Mock<IQueryHandler<TestQuery, bool>>();
            var queryDecorator = new RetryOnTransientErrorQueryHandlerDecorator<TestQuery, bool>(
                this.exceptionHandler.Object,
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
        }

        [TestMethod]
        public async Task ItShouldRetryQueriesUntilSuccess()
        {
            var query = new Mock<IQueryHandler<TestQuery, bool>>();
            var queryDecorator = new RetryOnTransientErrorQueryHandlerDecorator<TestQuery, bool>(
                this.exceptionHandler.Object,
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
        }

        [TestMethod]
        public async Task ItShouldRetryOnSqlDeadlock()
        {
            int tryCount = 0;

            this.command.Setup(v => v.HandleAsync(It.IsAny<TestCommand>()))
                .Callback(() =>
                {
                    ++tryCount;
                    if (tryCount == 1)
                    {
                        throw SqlExceptionMocker.MakeSqlException(
                            RetryOnTransientErrorDecoratorBase.SqlDeadlockErrorCode);
                    }
                }).Returns(Task.FromResult(false));

            await this.decorator.HandleAsync(new TestCommand());

            Assert.AreEqual(2, tryCount);
        }

        [TestMethod]
        public async Task ItShouldRetryOnSqlTimeout()
        {
            int tryCount = 0;

            this.command.Setup(v => v.HandleAsync(It.IsAny<TestCommand>()))
                .Callback(() =>
                {
                    ++tryCount;
                    if (tryCount == 1)
                    {
                        throw SqlExceptionMocker.MakeSqlException(
                            RetryOnTransientErrorDecoratorBase.SqlTimeoutErrorCode);
                    }
                }).Returns(Task.FromResult(0));

            await this.decorator.HandleAsync(new TestCommand());

            Assert.AreEqual(2, tryCount);
        }

        [TestMethod]
        public async Task ItShouldRetryOnTimeout()
        {
            int tryCount = 0;

            this.command.Setup(v => v.HandleAsync(It.IsAny<TestCommand>()))
                .Callback(() =>
                {
                    ++tryCount;
                    if (tryCount == 1)
                    {
                        throw new TimeoutException("A timeout occured");
                    }
                }).Returns(Task.FromResult(0));

            await this.decorator.HandleAsync(new TestCommand());

            Assert.AreEqual(2, tryCount);
        }

        [TestMethod]
        public async Task ItShouldRetryOnSqlAzureTransientError()
        {
            int tryCount = 0;

            this.command.Setup(v => v.HandleAsync(It.IsAny<TestCommand>()))
                .Callback(() =>
                {
                    ++tryCount;
                    if (tryCount == 1)
                    {
                        throw SqlExceptionMocker.MakeSqlException(
                            40501);
                    }
                }).Returns(Task.FromResult(0));

            await this.decorator.HandleAsync(new TestCommand());

            Assert.AreEqual(2, tryCount);
        }

        [TestMethod]
        public async Task ItShouldRetryOnSqlAzureTransientError2()
        {
            int tryCount = 0;

            this.command.Setup(v => v.HandleAsync(It.IsAny<TestCommand>()))
                .Callback(() =>
                {
                    ++tryCount;
                    if (tryCount == 1)
                    {
                        throw SqlExceptionMocker.MakeSqlException(
                            40143);
                    }
                }).Returns(Task.FromResult(0));

            await this.decorator.HandleAsync(new TestCommand());

            Assert.AreEqual(2, tryCount);
        }

        [TestMethod]
        public async Task ItShouldRetryOnAzureStorageTransientError()
        {
            int tryCount = 0;

            this.command.Setup(v => v.HandleAsync(It.IsAny<TestCommand>()))
                .Callback(() =>
                {
                    ++tryCount;
                    if (tryCount == 1)
                    {
                        throw new IOException();
                    }
                }).Returns(Task.FromResult(0));

            await this.decorator.HandleAsync(new TestCommand());

            Assert.AreEqual(2, tryCount);
        }

        [TestMethod]
        public async Task ItShouldRetryOnNestedSqlError()
        {
            int tryCount = 0;

            this.command.Setup(v => v.HandleAsync(It.IsAny<TestCommand>()))
                .Callback(() =>
                {
                    ++tryCount;
                    if (tryCount == 1)
                    {
                        throw new InvalidOperationException(
                            "blah",
                            new DivideByZeroException(
                                "blah",
                                SqlExceptionMocker.MakeSqlException(
                                    RetryOnTransientErrorDecoratorBase.SqlTimeoutErrorCode)));
                    }
                }).Returns(Task.FromResult(0));

            await this.decorator.HandleAsync(new TestCommand());

            Assert.AreEqual(2, tryCount);
        }

        [TestMethod]
        public async Task ItShouldRetryOnNestedAggregateSqlError()
        {
            int tryCount = 0;

            this.command.Setup(v => v.HandleAsync(It.IsAny<TestCommand>()))
                .Callback(() =>
                {
                    ++tryCount;
                    if (tryCount == 1)
                    {
                        throw new AggregateException(
                            "blah",
                            new DivideByZeroException(
                                "blah",
                                new Exception("Dead end")),
                            new DivideByZeroException(
                                "blah",
                                new AggregateException(
                                    "blah",
                                    new ExternalErrorException("blah"),
                                    new Exception(
                                        "blah",
                                        SqlExceptionMocker.MakeSqlException(RetryOnTransientErrorDecoratorBase.SqlTimeoutErrorCode)),
                                    new EntryPointNotFoundException("blah"))));
                    }
                }).Returns(Task.FromResult(0));

            await this.decorator.HandleAsync(new TestCommand());

            Assert.AreEqual(2, tryCount);
        }

        [TestMethod]
        public async Task ItShouldNotRetryOnStandardExceptions()
        {
            int tryCount = 0;

            this.command.Setup(v => v.HandleAsync(It.IsAny<TestCommand>()))
                .Callback(() =>
                {
                    ++tryCount;
                    if (tryCount == 1)
                    {
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

            Assert.AreEqual(1, tryCount);
            Assert.IsInstanceOfType(exception, typeof(DivideByZeroException));
        }

        [TestMethod]
        public async Task ItShouldNotRetryOnUnsupportedSqlExceptionErrorNumbers()
        {
            int tryCount = 0;

            this.command.Setup(v => v.HandleAsync(It.IsAny<TestCommand>()))
                .Callback(() =>
                {
                    ++tryCount;
                    if (tryCount == 1)
                    {
                        throw SqlExceptionMocker.MakeSqlException(0);
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

            Assert.AreEqual(1, tryCount);
            Assert.IsInstanceOfType(exception, typeof(SqlException));
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