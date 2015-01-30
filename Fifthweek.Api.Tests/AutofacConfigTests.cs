namespace Fifthweek.Api.Tests
{
    using System.Reflection;

    using Autofac;

    using Fifthweek.Api.AssemblyResolution;
    using Fifthweek.Api.Core;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class AutofacConfigTests
    {
        static AutofacConfigTests()
        {
            FifthweekAssembliesResolver.ExtraAssemblies = new[] { Assembly.GetExecutingAssembly() };
        }

        [TestMethod]
        public void TestCommandHandlerWithDefaultDecorators()
        {
            var container = (ILifetimeScope)AutofacConfig.CreateContainer();
            container = container.BeginLifetimeScope("AutofacWebRequest");

            var handler = container.Resolve<ICommandHandler<TestCommandWithDefaultDecorators>>();
            Assert.IsInstanceOfType(handler, typeof(RetryOnTransientErrorCommandHandlerDecorator<TestCommandWithDefaultDecorators>));

            handler = ((RetryOnTransientErrorCommandHandlerDecorator<TestCommandWithDefaultDecorators>)handler).Decorated;
            Assert.IsInstanceOfType(handler, typeof(TestCommandHandlerWithDefaultDecorators));
        }

        [TestMethod]
        public void TestCommandHandlerWithCustomDecorator()
        {
            var container = (ILifetimeScope)AutofacConfig.CreateContainer();
            container = container.BeginLifetimeScope("AutofacWebRequest");

            var handler = container.Resolve<ICommandHandler<TestCommandWithCustomDecorator>>();
            Assert.IsInstanceOfType(handler, typeof(RetryOnTransientErrorCommandHandlerDecorator<TestCommandWithCustomDecorator>));

            handler = ((RetryOnTransientErrorCommandHandlerDecorator<TestCommandWithCustomDecorator>)handler).Decorated;
            Assert.IsInstanceOfType(handler, typeof(TestCommandHandlerDecorator<TestCommandWithCustomDecorator>));

            handler = ((TestCommandHandlerDecorator<TestCommandWithCustomDecorator>)handler).Decorated;
            Assert.IsInstanceOfType(handler, typeof(TestCommandHandlerWithCustomDecorator));
        }

        [TestMethod]
        public void TestCommandHandlerWithCustomOuterDecorator()
        {
            var container = (ILifetimeScope)AutofacConfig.CreateContainer();
            container = container.BeginLifetimeScope("AutofacWebRequest");

            var handler = container.Resolve<ICommandHandler<TestCommandWithCustomOuterDecorator>>();
            Assert.IsInstanceOfType(handler, typeof(TestCommandHandlerDecorator<TestCommandWithCustomOuterDecorator>));

            handler = ((TestCommandHandlerDecorator<TestCommandWithCustomOuterDecorator>)handler).Decorated;
            Assert.IsInstanceOfType(handler, typeof(RetryOnTransientErrorCommandHandlerDecorator<TestCommandWithCustomOuterDecorator>));

            handler = ((RetryOnTransientErrorCommandHandlerDecorator<TestCommandWithCustomOuterDecorator>)handler).Decorated;
            Assert.IsInstanceOfType(handler, typeof(TestCommandHandlerWithCustomOuterDecorator));
        }

        [TestMethod]
        public void TestCommandHandlerWithNoDecorator()
        {
            var container = (ILifetimeScope)AutofacConfig.CreateContainer();
            container = container.BeginLifetimeScope("AutofacWebRequest");

            var handler = container.Resolve<ICommandHandler<TestCommandWithNoDecorator>>();
            Assert.IsInstanceOfType(handler, typeof(TestCommandHandlerWithNoDecorator));
        }

        [TestMethod]
        public void TestQueryWithDefaultDecorators()
        {
            var container = (ILifetimeScope)AutofacConfig.CreateContainer();
            container = container.BeginLifetimeScope("AutofacWebRequest");

            var handler = container.Resolve<IQueryHandler<TestQueryWithDefaultDecorators, bool>>();
            Assert.IsInstanceOfType(handler, typeof(RetryOnTransientErrorQueryHandlerDecorator<TestQueryWithDefaultDecorators, bool>));

            handler = ((RetryOnTransientErrorQueryHandlerDecorator<TestQueryWithDefaultDecorators, bool>)handler).Decorated;
            Assert.IsInstanceOfType(handler, typeof(TestQueryHandlerWithDefaultDecorators));
        }

        [TestMethod]
        public void TestQueryWithCustomDecorator()
        {
            var container = (ILifetimeScope)AutofacConfig.CreateContainer();
            container = container.BeginLifetimeScope("AutofacWebRequest");

            var handler = container.Resolve<IQueryHandler<TestQueryWithCustomDecorator, bool>>();
            Assert.IsInstanceOfType(handler, typeof(RetryOnTransientErrorQueryHandlerDecorator<TestQueryWithCustomDecorator, bool>));

            handler = ((RetryOnTransientErrorQueryHandlerDecorator<TestQueryWithCustomDecorator, bool>)handler).Decorated;
            Assert.IsInstanceOfType(handler, typeof(TestQueryHandlerDecorator<TestQueryWithCustomDecorator, bool>));

            handler = ((TestQueryHandlerDecorator<TestQueryWithCustomDecorator, bool>)handler).Decorated;
            Assert.IsInstanceOfType(handler, typeof(TestQueryHandlerWithCustomDecorator));
        }

        [TestMethod]
        public void TestQueryWithCustomOuterDecorator()
        {
            var container = (ILifetimeScope)AutofacConfig.CreateContainer();
            container = container.BeginLifetimeScope("AutofacWebRequest");

            var handler = container.Resolve<IQueryHandler<TestQueryWithCustomOuterDecorator, bool>>();
            Assert.IsInstanceOfType(handler, typeof(TestQueryHandlerDecorator<TestQueryWithCustomOuterDecorator, bool>));

            handler = ((TestQueryHandlerDecorator<TestQueryWithCustomOuterDecorator, bool>)handler).Decorated;
            Assert.IsInstanceOfType(handler, typeof(RetryOnTransientErrorQueryHandlerDecorator<TestQueryWithCustomOuterDecorator, bool>));

            handler = ((RetryOnTransientErrorQueryHandlerDecorator<TestQueryWithCustomOuterDecorator, bool>)handler).Decorated;
            Assert.IsInstanceOfType(handler, typeof(TestQueryHandlerWithCustomOuterDecorator));
        }

        [TestMethod]
        public void TestQueryWithNoDecorator()
        {
            var container = (ILifetimeScope)AutofacConfig.CreateContainer();
            container = container.BeginLifetimeScope("AutofacWebRequest");

            var handler = container.Resolve<IQueryHandler<TestQueryWithNoDecorator, bool>>();
            Assert.IsInstanceOfType(handler, typeof(TestQueryHandlerWithNoDecorator));
        }
    }
}