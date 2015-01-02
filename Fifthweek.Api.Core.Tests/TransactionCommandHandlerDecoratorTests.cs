namespace Fifthweek.Api.Core.Tests
{
    using System.Threading.Tasks;
    using System.Transactions;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestClass]
    public class TransactionCommandHandlerDecoratorTests
    {
        [TestMethod]
        public async Task ItShouldExecuteCommandsInATransaction()
        {
            var command = new Mock<ICommandHandler<NullCommand>>();
            var decorator = new TransactionCommandHandlerDecorator<NullCommand>(command.Object);

            bool isInTransaction = false;

            command.Setup(v => v.HandleAsync(It.IsAny<NullCommand>()))
                .Callback(() => isInTransaction = Transaction.Current != null).Returns(Task.FromResult(0));

            await command.Object.HandleAsync(new NullCommand());

            Assert.IsFalse(isInTransaction);

            await decorator.HandleAsync(new NullCommand());

            Assert.IsTrue(isInTransaction);
        }
    }
}