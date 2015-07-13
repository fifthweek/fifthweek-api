namespace Fifthweek.Payments.Tests.Services.Credit.Stripe
{
    using System;
    using System.Threading.Tasks;

    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Payments.Services.Credit.Stripe;
    using Fifthweek.Payments.Stripe;

    using global::Stripe;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    using Newtonsoft.Json;

    [TestClass]
    public class UpdateStripeCustomerCreditCardTests
    {
        private static readonly UserId UserId = UserId.Random();
        private static readonly string CustomerId = "customerId";
        private static readonly string TokenId = "token";

        private static readonly string CardId = "cardId";
        private static readonly string TestKey = "testkey";
        private static readonly string LiveKey = "livekey";

        private Mock<IStripeApiKeyRepository> apiKeyRepository;
        private Mock<IStripeService> stripeService;

        private UpdateStripeCustomerCreditCard target;

        [TestInitialize]
        public void Initialize()
        {
            this.apiKeyRepository = new Mock<IStripeApiKeyRepository>(MockBehavior.Strict);
            this.stripeService = new Mock<IStripeService>(MockBehavior.Strict);

            this.target = new UpdateStripeCustomerCreditCard(
                this.apiKeyRepository.Object,
                this.stripeService.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task WhenUserIdIsNull_ItShouldThrowAnException()
        {
            await this.target.ExecuteAsync(null, CustomerId, TokenId, default(UserType));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task WhenCustomerIdIsNull_ItShouldThrowAnException()
        {
            await this.target.ExecuteAsync(UserId, null, TokenId, default(UserType));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task WhenTokenIsNull_ItShouldThrowAnException()
        {
            await this.target.ExecuteAsync(UserId, CustomerId, null, default(UserType));
        }

        [TestMethod]
        public async Task WhenStripeModeIsTest_ItShouldCreateACustomer()
        {
            this.apiKeyRepository.Setup(v => v.GetApiKey(UserType.TestUser)).Returns(TestKey);

            var expectedCardCreateOptions = new StripeCardCreateOptions { Source = new StripeSourceOptions { TokenId = TokenId } };
            var card = new StripeCard { Id = CardId };
            this.stripeService.Setup(v => v.CreateCard(
                CustomerId,
                It.Is<StripeCardCreateOptions>(
                    x => JsonConvert.SerializeObject(x, Formatting.None) == JsonConvert.SerializeObject(expectedCardCreateOptions, Formatting.None)),
                TestKey))
                .ReturnsAsync(card);


            var expectedCustomerUpdateOptions = new StripeCustomerUpdateOptions { DefaultSource = CardId };
            this.stripeService.Setup(v => v.UpdateCustomer(
                CustomerId,
                It.Is<StripeCustomerUpdateOptions>(
                    x => JsonConvert.SerializeObject(x, Formatting.None) == JsonConvert.SerializeObject(expectedCustomerUpdateOptions, Formatting.None)),
                TestKey))
                .Returns(Task.FromResult(0))
                .Verifiable();

            await this.target.ExecuteAsync(UserId, CustomerId, TokenId, UserType.TestUser);

            this.stripeService.Verify();
        }

        [TestMethod]
        public async Task WhenStripeModeIsLive_ItShouldCreateACustomer()
        {
            this.apiKeyRepository.Setup(v => v.GetApiKey(UserType.StandardUser)).Returns(LiveKey);

            var expectedCardCreateOptions = new StripeCardCreateOptions { Source = new StripeSourceOptions { TokenId = TokenId } };
            var card = new StripeCard { Id = CardId };
            this.stripeService.Setup(v => v.CreateCard(
                CustomerId,
                It.Is<StripeCardCreateOptions>(
                    x => JsonConvert.SerializeObject(x, Formatting.None) == JsonConvert.SerializeObject(expectedCardCreateOptions, Formatting.None)),
                LiveKey))
                .ReturnsAsync(card);

            var expectedCustomerUpdateOptions = new StripeCustomerUpdateOptions { DefaultSource = CardId };
            this.stripeService.Setup(v => v.UpdateCustomer(
                CustomerId,
                It.Is<StripeCustomerUpdateOptions>(
                    x => JsonConvert.SerializeObject(x, Formatting.None) == JsonConvert.SerializeObject(expectedCustomerUpdateOptions, Formatting.None)),
                LiveKey))
                .Returns(Task.FromResult(0))
                .Verifiable();

            await this.target.ExecuteAsync(UserId, CustomerId, TokenId, UserType.StandardUser);

            this.stripeService.Verify();
        }
    }
}