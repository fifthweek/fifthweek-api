namespace Fifthweek.Api.Identity.Tests.OAuth
{
    using System;
    using System.Threading.Tasks;

    using Fifthweek.Api.Identity.OAuth;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class ClientRepositoryTests
    {
        private ClientRepository target;

        [TestInitialize]
        public void TestInitialize()
        {
            this.target = new ClientRepository();
        }

        [TestInitialize]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task WhenCalledWithANullClientId_ItShouldThrowAnException()
        {
            await this.target.TryGetClientAsync(null);
        }

        [TestInitialize]
        public async Task WhenCalledWithTheWebsiteClientId_ItShouldReturnTheClient()
        {
            var result = await this.target.TryGetClientAsync(new ClientId("fifthweek.web.1"));

            Assert.IsNotNull(result);
        }

        [TestInitialize]
        public async Task WhenCalledWithAnInvalidClientId_ItShouldReturnNull()
        {
            var result = await this.target.TryGetClientAsync(new ClientId("fds"));

            Assert.IsNull(result);
        }
    }
}