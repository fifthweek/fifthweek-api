namespace Fifthweek.Api.Core.Tests
{
    using System;
    using System.Threading.Tasks;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestClass]
    public class RetryOnSqlDeadlockOrTimeoutDecoratorTests
    {
        [TestMethod]
        public async Task ItShouldRetryUpToTheMaximumtryCount()
        {
            var command = new Mock<ICommandHandler<NullCommand>>();
            var decorator = new RetryOnSqlDeadlockOrTimeoutCommandHandlerDecorator<NullCommand>(command.Object);

            int tryCount = 0;

            command.Setup(v => v.HandleAsync(It.IsAny<NullCommand>()))
                .Callback(() =>
                {
                    ++tryCount;
                    throw SqlExceptionMocker.MakeSqlException(
                        RetryOnSqlDeadlockOrTimeoutDecoratorBase.SqlDeadlockErrorCode);
                }).Returns(Task.FromResult(0));

            Exception exception = null;
            try
            {
                await decorator.HandleAsync(new NullCommand());
            }
            catch (Exception t)
            {
                exception = t;
            }

            Assert.IsNotNull(exception);
            Assert.IsTrue(exception.Message.Contains("Maximum retries reached"));
            Assert.AreEqual(RetryOnSqlDeadlockOrTimeoutDecoratorBase.MaxTries, tryCount);
        }

        [TestMethod]
        public async Task ItShouldRetryOnSqlDeadlock()
        {
            var query = new Mock<IQueryHandler<NullQuery, bool>>();
            var decorator = new RetryOnSqlDeadlockOrTimeoutQueryHandlerDecorator<NullQuery, bool>(query.Object);

            int tryCount = 0;

            query.Setup(v => v.HandleAsync(It.IsAny<NullQuery>()))
                .Callback(() =>
                {
                    ++tryCount;
                    if (tryCount == 1)
                    {
                        throw SqlExceptionMocker.MakeSqlException(
                            RetryOnSqlDeadlockOrTimeoutDecoratorBase.SqlDeadlockErrorCode);
                    }
                }).Returns(Task.FromResult(false));

            await decorator.HandleAsync(new NullQuery());

            Assert.AreEqual(2, tryCount);
        }

        [TestMethod]
        public async Task ItShouldRetryOnSqlTimeout()
        {
            var command = new Mock<ICommandHandler<NullCommand>>();
            var decorator = new RetryOnSqlDeadlockOrTimeoutCommandHandlerDecorator<NullCommand>(command.Object);

            int tryCount = 0;

            command.Setup(v => v.HandleAsync(It.IsAny<NullCommand>()))
                .Callback(() =>
                {
                    ++tryCount;
                    if (tryCount == 1)
                    {
                        throw SqlExceptionMocker.MakeSqlException(
                            RetryOnSqlDeadlockOrTimeoutDecoratorBase.SqlTimeoutErrorCode);
                    }
                }).Returns(Task.FromResult(0));

            await decorator.HandleAsync(new NullCommand());

            Assert.AreEqual(2, tryCount);
        }

        [TestMethod]
        public async Task ItShouldNotRetryOnStandardExceptions()
        {
            var command = new Mock<ICommandHandler<NullCommand>>();
            var decorator = new RetryOnSqlDeadlockOrTimeoutCommandHandlerDecorator<NullCommand>(command.Object);

            int tryCount = 0;

            command.Setup(v => v.HandleAsync(It.IsAny<NullCommand>()))
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
                await decorator.HandleAsync(new NullCommand());
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
            var command = new Mock<ICommandHandler<NullCommand>>();
            var decorator = new RetryOnSqlDeadlockOrTimeoutCommandHandlerDecorator<NullCommand>(command.Object);

            int tryCount = 0;

            command.Setup(v => v.HandleAsync(It.IsAny<NullCommand>()))
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
                await decorator.HandleAsync(new NullCommand());
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
            var command = new Mock<ICommandHandler<NullCommand>>();
            var decorator = new RetryOnSqlDeadlockOrTimeoutCommandHandlerDecorator<NullCommand>(command.Object);

            int tryCount = 0;

            command.Setup(v => v.HandleAsync(It.IsAny<NullCommand>()))
                .Callback(() =>
                {
                    ++tryCount;
                    if (tryCount < RetryOnSqlDeadlockOrTimeoutDecoratorBase.MaxTries - 1)
                    {
                        throw SqlExceptionMocker.MakeSqlException(
                            RetryOnSqlDeadlockOrTimeoutDecoratorBase.SqlDeadlockErrorCode);
                    }
                }).Returns(Task.FromResult(0));

            await decorator.HandleAsync(new NullCommand());

            Assert.AreEqual(RetryOnSqlDeadlockOrTimeoutDecoratorBase.MaxTries - 1, tryCount);
        }

        [TestMethod]
        public async Task ItShouldRetryQueriesUntilSuccess()
        {
            var query = new Mock<IQueryHandler<NullQuery, bool>>();
            var decorator = new RetryOnSqlDeadlockOrTimeoutQueryHandlerDecorator<NullQuery, bool>(query.Object);

            int tryCount = 0;

            query.Setup(v => v.HandleAsync(It.IsAny<NullQuery>()))
                .Callback(() =>
                {
                    ++tryCount;
                    if (tryCount < RetryOnSqlDeadlockOrTimeoutDecoratorBase.MaxTries - 1)
                    {
                        throw SqlExceptionMocker.MakeSqlException(
                            RetryOnSqlDeadlockOrTimeoutDecoratorBase.SqlDeadlockErrorCode);
                    }
                }).Returns(Task.FromResult(false));

            await decorator.HandleAsync(new NullQuery());

            Assert.AreEqual(RetryOnSqlDeadlockOrTimeoutDecoratorBase.MaxTries - 1, tryCount);
        }
    }
}