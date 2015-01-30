namespace Fifthweek.Api.Identity.Tests.OAuth.Queries
{
    using System;
    using System.Threading.Tasks;

    using Fifthweek.Api.Identity.OAuth;
    using Fifthweek.Api.Identity.OAuth.Queries;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestClass]
    public class GetValidatedClientQueryHandlerTests
    {
        private static readonly string Secret = "secret";
        private static readonly string HashedSecret = Helper.GetHash("secret");
        private static readonly string Name = "name";
        private static readonly ApplicationType ApplicationType = ApplicationType.JavaScript;
        private static readonly bool IsActive = true;
        private static readonly int LifetimeMinutes = 100;
        private static readonly string AllowedOriginRegex = ".*";
        private static readonly string AllowedOrigin = "*";
        private static readonly ClientId ClientId = new ClientId(Guid.NewGuid().ToString());

        private GetValidatedClientQueryHandler target;
        private Mock<IClientRepository> clientRepository;

        [TestInitialize]
        public void TestInitialize()
        {
            this.clientRepository = new Mock<IClientRepository>();
            this.target = new GetValidatedClientQueryHandler(this.clientRepository.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task WhenQueryIsNull_ItShouldThrowAnException()
        {
            await this.target.HandleAsync(null);
        }

        [TestMethod]
        public async Task WhenCalledWithValidClientId_ItShouldReturnTheRequestedClient()
        {
            var client = new Client(
                ClientId,
                HashedSecret,
                Name,
                ApplicationType,
                IsActive,
                LifetimeMinutes,
                AllowedOriginRegex,
                AllowedOrigin);

            this.clientRepository.Setup(v => v.TryGetClientAsync(ClientId)).ReturnsAsync(client);

            var result = await this.target.HandleAsync(new GetValidatedClientQuery(ClientId, Secret));

            Assert.IsNotNull(result);
            Assert.AreEqual(client, result);
        }

        [TestMethod]
        [ExpectedException(typeof(ClientRequestException))]
        public async Task WhenClientIsNotFound_ItShouldThrowAnException()
        {
            this.clientRepository.Setup(v => v.TryGetClientAsync(ClientId)).ReturnsAsync(null);

            await this.target.HandleAsync(new GetValidatedClientQuery(ClientId, Secret));
        }

        [TestMethod]
        [ExpectedException(typeof(ClientRequestException))]
        public async Task WhenClientIsNotActive_ItShouldThrowAnException()
        {
            var client = new Client(
                ClientId,
                HashedSecret,
                Name,
                ApplicationType,
                false,
                LifetimeMinutes,
                AllowedOriginRegex,
                AllowedOrigin);

            this.clientRepository.Setup(v => v.TryGetClientAsync(ClientId)).ReturnsAsync(client);

            await this.target.HandleAsync(new GetValidatedClientQuery(ClientId, Secret));
        }

        [TestMethod]
        public async Task WhenClientIsJavaScript_ItShouldNotCheckTheSecret()
        {
            var client = new Client(
                ClientId,
                HashedSecret,
                Name,
                ApplicationType.JavaScript,
                IsActive,
                LifetimeMinutes,
                AllowedOriginRegex,
                AllowedOrigin);

            this.clientRepository.Setup(v => v.TryGetClientAsync(ClientId)).ReturnsAsync(client);

            var result = await this.target.HandleAsync(new GetValidatedClientQuery(ClientId, null));

            Assert.IsNotNull(result);
            Assert.AreEqual(client, result);
        }

        [TestMethod]
        [ExpectedException(typeof(ClientRequestException))]
        public async Task WhenClientIsNativeConfidential_AndSecretIsNull_ItShouldThrowAnException()
        {
            var client = new Client(
                ClientId,
                HashedSecret,
                Name,
                ApplicationType.NativeConfidential,
                IsActive,
                LifetimeMinutes,
                AllowedOriginRegex,
                AllowedOrigin);

            this.clientRepository.Setup(v => v.TryGetClientAsync(ClientId)).ReturnsAsync(client);

            await this.target.HandleAsync(new GetValidatedClientQuery(ClientId, null));
        }

        [TestMethod]
        [ExpectedException(typeof(ClientRequestException))]
        public async Task WhenClientIsNativeConfidential_AndSecretIsEmptyString_ItShouldThrowAnException()
        {
            var client = new Client(
                ClientId,
                HashedSecret,
                Name,
                ApplicationType.NativeConfidential,
                IsActive,
                LifetimeMinutes,
                AllowedOriginRegex,
                AllowedOrigin);

            this.clientRepository.Setup(v => v.TryGetClientAsync(ClientId)).ReturnsAsync(client);

            await this.target.HandleAsync(new GetValidatedClientQuery(ClientId, " "));
        }

        [TestMethod]
        [ExpectedException(typeof(ClientRequestException))]
        public async Task WhenClientIsNativeConfidential_AndSecretIsInvalid_ItShouldThrowAnException()
        {
            var client = new Client(
                ClientId,
                HashedSecret,
                Name,
                ApplicationType.NativeConfidential,
                IsActive,
                LifetimeMinutes,
                AllowedOriginRegex,
                AllowedOrigin);

            this.clientRepository.Setup(v => v.TryGetClientAsync(ClientId)).ReturnsAsync(client);

            await this.target.HandleAsync(new GetValidatedClientQuery(ClientId, "invalid_secret"));
        }

        [TestMethod]
        public async Task WhenClientIsNativeConfidential_AndSecretIsValid_ItShouldReturnTheClient()
        {
            var client = new Client(
                ClientId,
                HashedSecret,
                Name,
                ApplicationType.NativeConfidential,
                IsActive,
                LifetimeMinutes,
                AllowedOriginRegex,
                AllowedOrigin);

            this.clientRepository.Setup(v => v.TryGetClientAsync(ClientId)).ReturnsAsync(client);

            var result = await this.target.HandleAsync(new GetValidatedClientQuery(ClientId, Secret));

            Assert.IsNotNull(result);
            Assert.AreEqual(client, result);
        }
    }
}