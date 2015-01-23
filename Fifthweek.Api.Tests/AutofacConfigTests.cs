namespace Fifthweek.Api.Tests
{
    using Autofac;

    using Fifthweek.Api.Core;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class AutofacConfigTests
    {
        [TestMethod]
        public void TestCommandDecorators()
        {
            var container = (ILifetimeScope)AutofacConfig.CreateContainer();
            container = container.BeginLifetimeScope("AutofacWebRequest");

            var handler = container.Resolve<ICommandHandler<NullCommand>>();
            Assert.IsInstanceOfType(handler, typeof(RetryOnRecoverableDatabaseErrorCommandHandlerDecorator<NullCommand>));

            handler = ((RetryOnRecoverableDatabaseErrorCommandHandlerDecorator<NullCommand>)handler).Decorated;
            Assert.IsInstanceOfType(handler, typeof(TransactionCommandHandlerDecorator<NullCommand>));

            handler = ((TransactionCommandHandlerDecorator<NullCommand>)handler).Decorated;
            Assert.IsInstanceOfType(handler, typeof(NullCommandHandler));
        }

        [TestMethod]
        public void TestQueryDecorators()
        {
            var container = (ILifetimeScope)AutofacConfig.CreateContainer();
            container = container.BeginLifetimeScope("AutofacWebRequest");

            var handler = container.Resolve<IQueryHandler<NullQuery, bool>>();
            Assert.IsInstanceOfType(handler, typeof(RetryOnRecoverableDatabaseErrorQueryHandlerDecorator<NullQuery, bool>));

            handler = ((RetryOnRecoverableDatabaseErrorQueryHandlerDecorator<NullQuery, bool>)handler).Decorated;
            Assert.IsInstanceOfType(handler, typeof(NullQueryHandler));
        }
    }
}