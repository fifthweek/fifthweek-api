namespace Dexter.Api.Tests.CommandHandlers
{
    using System;
    using System.Threading.Tasks;

    using Dexter.Api.CommandHandlers;
    using Dexter.Api.Commands;
    using Dexter.Api.Repositories;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestClass]
    public class SaveChangesCommandHandlerTests
    {
        [TestMethod]
        public async Task ItShouldSaveChanges()
        {
            var context = new Mock<IDexterDbContext>();

            context.Setup(v => v.SaveChangesAsync()).ReturnsAsync(1);

            var handler = new SaveChangesCommandHandler(context.Object);

            await handler.HandleAsync(SaveChangesCommand.Default);

            context.Verify(v => v.SaveChangesAsync());
        }

        [TestMethod]
        public async Task ItShouldThrowAnExceptionIfZeroResultCountIsReturned()
        {
            var context = new Mock<IDexterDbContext>();

            context.Setup(v => v.SaveChangesAsync()).ReturnsAsync(0);

            var handler = new SaveChangesCommandHandler(context.Object);

            Exception exception = null;

            try
            {
                await handler.HandleAsync(SaveChangesCommand.Default);
            }
            catch (Exception t)
            {
                exception = t;
            }

            Assert.IsNotNull(exception);

            context.Verify(v => v.SaveChangesAsync());
        }
    }
}