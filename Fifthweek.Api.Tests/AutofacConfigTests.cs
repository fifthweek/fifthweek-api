namespace Fifthweek.Api.Tests
{
    using Autofac;

    using Fifthweek.Api.CommandHandlers;
    using Fifthweek.Api.Commands;
    using Fifthweek.Api.Queries;
    using Fifthweek.Api.QueryHandlers;

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
            Assert.IsInstanceOfType(handler, typeof(ValidationCommandHandlerDecorator<NullCommand>));

            handler = ((ValidationCommandHandlerDecorator<NullCommand>)handler).Decorated;
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
            Assert.IsInstanceOfType(handler, typeof(ValidationQueryHandlerDecorator<NullQuery, bool>));

            handler = ((ValidationQueryHandlerDecorator<NullQuery, bool>)handler).Decorated;
            Assert.IsInstanceOfType(handler, typeof(NullQueryHandler));
        }
    }
}