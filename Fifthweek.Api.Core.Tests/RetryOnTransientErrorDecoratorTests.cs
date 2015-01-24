namespace Fifthweek.Api.Core.Tests
{
    using System;
    using System.Data.Entity.Infrastructure;
    using System.Threading.Tasks;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestClass]
    public class RetryOnTransientErrorDecoratorTests
    {
        [TestMethod]
        public async Task ItShouldRetryUpToTheMaximumtryCount()
        {
            var command = new Mock<ICommandHandler<TestCommand>>();
            var decorator = new RetryOnTransientErrorCommandHandlerDecorator<TestCommand>(
                command.Object,
                RetryOnTransientErrorDecoratorBase.MaxRetryCount,
                TimeSpan.FromMilliseconds(10));

            int tryCount = 0;

            command.Setup(v => v.HandleAsync(It.IsAny<TestCommand>()))
                .Callback(() =>
                {
                    ++tryCount;
                    throw SqlExceptionMocker.MakeSqlException(
                        RetryOnTransientErrorDecoratorBase.SqlDeadlockErrorCode);
                }).Returns(Task.FromResult(0));

            Exception exception = null;
            try
            {
                await decorator.HandleAsync(new TestCommand());
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
        public async Task ItShouldRetryOnSqlDeadlock()
        {
            var query = new Mock<IQueryHandler<TestQuery, bool>>();
            var decorator = new RetryOnTransientErrorQueryHandlerDecorator<TestQuery, bool>(
                query.Object,
                RetryOnTransientErrorDecoratorBase.MaxRetryCount,
                TimeSpan.FromMilliseconds(10));

            int tryCount = 0;

            query.Setup(v => v.HandleAsync(It.IsAny<TestQuery>()))
                .Callback(() =>
                {
                    ++tryCount;
                    if (tryCount == 1)
                    {
                        throw SqlExceptionMocker.MakeSqlException(
                            RetryOnTransientErrorDecoratorBase.SqlDeadlockErrorCode);
                    }
                }).Returns(Task.FromResult(false));

            await decorator.HandleAsync(new TestQuery());

            Assert.AreEqual(2, tryCount);
        }

        [TestMethod]
        public async Task ItShouldRetryOnSqlTimeout()
        {
            var command = new Mock<ICommandHandler<TestCommand>>();
            var decorator = new RetryOnTransientErrorCommandHandlerDecorator<TestCommand>(
                command.Object,
                RetryOnTransientErrorDecoratorBase.MaxRetryCount,
                TimeSpan.FromMilliseconds(10));

            int tryCount = 0;

            command.Setup(v => v.HandleAsync(It.IsAny<TestCommand>()))
                .Callback(() =>
                {
                    ++tryCount;
                    if (tryCount == 1)
                    {
                        throw SqlExceptionMocker.MakeSqlException(
                            RetryOnTransientErrorDecoratorBase.SqlTimeoutErrorCode);
                    }
                }).Returns(Task.FromResult(0));

            await decorator.HandleAsync(new TestCommand());

            Assert.AreEqual(2, tryCount);
        }

        [TestMethod]
        public async Task ItShouldRetryOnTimeout()
        {
            var command = new Mock<ICommandHandler<TestCommand>>();
            var decorator = new RetryOnTransientErrorCommandHandlerDecorator<TestCommand>(
                command.Object,
                RetryOnTransientErrorDecoratorBase.MaxRetryCount,
                TimeSpan.FromMilliseconds(10));

            int tryCount = 0;

            command.Setup(v => v.HandleAsync(It.IsAny<TestCommand>()))
                .Callback(() =>
                {
                    ++tryCount;
                    if (tryCount == 1)
                    {
                        throw new TimeoutException("A timeout occured");
                    }
                }).Returns(Task.FromResult(0));

            await decorator.HandleAsync(new TestCommand());

            Assert.AreEqual(2, tryCount);
        }

        [TestMethod]
        public async Task ItShouldRetryOnAzureTransientError()
        {
            var command = new Mock<ICommandHandler<TestCommand>>();
            var decorator = new RetryOnTransientErrorCommandHandlerDecorator<TestCommand>(
                command.Object,
                RetryOnTransientErrorDecoratorBase.MaxRetryCount,
                TimeSpan.FromMilliseconds(10));

            int tryCount = 0;

            command.Setup(v => v.HandleAsync(It.IsAny<TestCommand>()))
                .Callback(() =>
                {
                    ++tryCount;
                    if (tryCount == 1)
                    {
                        throw SqlExceptionMocker.MakeSqlException(
                            40501);
                    }
                }).Returns(Task.FromResult(0));

            await decorator.HandleAsync(new TestCommand());

            Assert.AreEqual(2, tryCount);
        }

        [TestMethod]
        public async Task ItShouldRetryOnNestedSqlError()
        {
            var command = new Mock<ICommandHandler<TestCommand>>();
            var decorator = new RetryOnTransientErrorCommandHandlerDecorator<TestCommand>(
                command.Object,
                RetryOnTransientErrorDecoratorBase.MaxRetryCount,
                TimeSpan.FromMilliseconds(10));

            int tryCount = 0;

            command.Setup(v => v.HandleAsync(It.IsAny<TestCommand>()))
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

            await decorator.HandleAsync(new TestCommand());

            Assert.AreEqual(2, tryCount);
        }

        [TestMethod]
        public async Task ItShouldRetryOnNestedAggregateSqlError()
        {
            var command = new Mock<ICommandHandler<TestCommand>>();
            var decorator = new RetryOnTransientErrorCommandHandlerDecorator<TestCommand>(
                command.Object,
                RetryOnTransientErrorDecoratorBase.MaxRetryCount,
                TimeSpan.FromMilliseconds(10));

            int tryCount = 0;

            command.Setup(v => v.HandleAsync(It.IsAny<TestCommand>()))
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

            await decorator.HandleAsync(new TestCommand());

            Assert.AreEqual(2, tryCount);
        }

        [TestMethod]
        public async Task ItShouldNotRetryOnStandardExceptions()
        {
            var command = new Mock<ICommandHandler<TestCommand>>();
            var decorator = new RetryOnTransientErrorCommandHandlerDecorator<TestCommand>(
                command.Object,
                RetryOnTransientErrorDecoratorBase.MaxRetryCount,
                TimeSpan.FromMilliseconds(10));

            int tryCount = 0;

            command.Setup(v => v.HandleAsync(It.IsAny<TestCommand>()))
                .Callback(() =>
                {
                    ++tryCount;
                    if (tryCount == 1)
                    {
                        throw new Exception();
                    }
                }).Returns(Task.FromResult(0));

            Exception exception = null;
            try
            {
                await decorator.HandleAsync(new TestCommand());
            }
            catch (Exception t)
            {
                exception = t;
            }

            Assert.AreEqual(1, tryCount);
            Assert.IsNotNull(exception);
        }

        [TestMethod]
        public async Task ItShouldNotRetryOnUnsupportedSqlExceptionErrorNumbers()
        {
            var command = new Mock<ICommandHandler<TestCommand>>();
            var decorator = new RetryOnTransientErrorCommandHandlerDecorator<TestCommand>(
                command.Object,
                RetryOnTransientErrorDecoratorBase.MaxRetryCount,
                TimeSpan.FromMilliseconds(10));

            int tryCount = 0;

            command.Setup(v => v.HandleAsync(It.IsAny<TestCommand>()))
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
                await decorator.HandleAsync(new TestCommand());
            }
            catch (Exception t)
            {
                exception = t;
            }

            Assert.AreEqual(1, tryCount);
            Assert.IsNotNull(exception);
        }

        [TestMethod]
        public async Task ItShouldRetryCommandsUntilSuccess()
        {
            var command = new Mock<ICommandHandler<TestCommand>>();
            var decorator = new RetryOnTransientErrorCommandHandlerDecorator<TestCommand>(
                command.Object,
                RetryOnTransientErrorDecoratorBase.MaxRetryCount,
                TimeSpan.FromMilliseconds(10));

            int tryCount = 0;

            command.Setup(v => v.HandleAsync(It.IsAny<TestCommand>()))
                .Callback(() =>
                {
                    ++tryCount;
                    if (tryCount < RetryOnTransientErrorDecoratorBase.MaxRetryCount)
                    {
                        throw SqlExceptionMocker.MakeSqlException(
                            RetryOnTransientErrorDecoratorBase.SqlDeadlockErrorCode);
                    }
                }).Returns(Task.FromResult(0));

            await decorator.HandleAsync(new TestCommand());

            Assert.AreEqual(RetryOnTransientErrorDecoratorBase.MaxRetryCount, tryCount);
        }

        [TestMethod]
        public async Task ItShouldRetryQueriesUntilSuccess()
        {
            var query = new Mock<IQueryHandler<TestQuery, bool>>();
            var decorator = new RetryOnTransientErrorQueryHandlerDecorator<TestQuery, bool>(
                query.Object,
                RetryOnTransientErrorDecoratorBase.MaxRetryCount,
                TimeSpan.FromMilliseconds(10));

            int tryCount = 0;

            query.Setup(v => v.HandleAsync(It.IsAny<TestQuery>()))
                .Callback(() =>
                {
                    ++tryCount;
                    if (tryCount < RetryOnTransientErrorDecoratorBase.MaxRetryCount)
                    {
                        throw SqlExceptionMocker.MakeSqlException(
                            RetryOnTransientErrorDecoratorBase.SqlDeadlockErrorCode);
                    }
                }).Returns(Task.FromResult(false));

            await decorator.HandleAsync(new TestQuery());

            Assert.AreEqual(RetryOnTransientErrorDecoratorBase.MaxRetryCount, tryCount);
        }
    }
}