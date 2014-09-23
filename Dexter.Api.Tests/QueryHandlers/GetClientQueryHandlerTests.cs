namespace Dexter.Api.Tests.QueryHandlers
{
    using System.Threading.Tasks;

    using Dexter.Api.Entities;
    using Dexter.Api.Models;
    using Dexter.Api.Queries;
    using Dexter.Api.QueryHandlers;
    using Dexter.Api.Repositories;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestClass]
    public class GetClientQueryHandlerTests
    {
        [TestMethod]
        public async Task ItShouldReturnTheRequestedClient()
        {
            var clientRepository = new Mock<IClientRepository>();

            clientRepository.Setup(v => v.TryGetClientAsync("X")).ReturnsAsync(new Client { Id = "X" });

            var handler = new GetClientQueryHandler(clientRepository.Object);

            var result = await handler.HandleAsync(new GetClientQuery(new ClientId("X")));

            clientRepository.Verify(v => v.TryGetClientAsync("X"));

            Assert.IsNotNull(result);
            Assert.AreEqual("X", result.Id);
        }
    }
}