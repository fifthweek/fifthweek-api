namespace Fifthweek.Api.Tests.QueryHandlers
{
    using System.Threading.Tasks;

    using Fifthweek.Api.Identity;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestClass]
    public class GetClientQueryHandlerTests
    {
        [TestMethod]
        public async Task ItShouldReturnTheRequestedClient()
        {
            var clientRepository = new Mock<IClientRepository>();

            clientRepository.Setup(v => v.TryGetClientAsync(new ClientId("X"))).ReturnsAsync(new Client(new ClientId("X"), null, null, ApplicationType.JavaScript, true, 100, null, null));

            var handler = new GetClientQueryHandler(clientRepository.Object);

            var result = await handler.HandleAsync(new GetClientQuery(new ClientId("X")));

            clientRepository.Verify(v => v.TryGetClientAsync(new ClientId("X")));

            Assert.IsNotNull(result);
            Assert.AreEqual("X", result.ClientId.Value);
        }
    }
}