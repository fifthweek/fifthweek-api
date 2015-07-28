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
    public class CreateStripeCustomerTests
    {
        private static readonly UserId UserId = UserId.Random();
        private static readonly string TokenId = "token";

        private static readonly string TestKey = "testkey";
        private static readonly string LiveKey = "livekey";
        private static readonly string CustomerId = "customerId";
        
        private Mock<IStripeApiKeyRepository> apiKeyRepository;
        private Mock<IStripeService> stripeService;

        private CreateStripeCustomer target;

        [TestInitialize]
        public void Initialize()
        {
            this.apiKeyRepository = new Mock<IStripeApiKeyRepository>(MockBehavior.Strict);
            this.stripeService = new Mock<IStripeService>(MockBehavior.Strict);

            this.target = new CreateStripeCustomer(
                this.apiKeyRepository.Object,
                this.stripeService.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task WhenUserIdIsNull_ItShouldThrowAnException()
        {
            await this.target.ExecuteAsync(null, TokenId, default(UserType));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task WhenTokenIsNull_ItShouldThrowAnException()
        {
            await this.target.ExecuteAsync(UserId, null, default(UserType));
        }

        [TestMethod]
        public async Task WhenStripeModeIsTest_ItShouldCreateACustomer()
        {
            this.apiKeyRepository.Setup(v => v.GetApiKey(UserType.TestUser)).Returns(TestKey);

            var expectedOptions = new StripeCustomerCreateOptions
            {
                Description = UserId.ToString(),
                Source = new StripeSourceOptions
                {
                    TokenId = TokenId,
                }
            };

            var customer = new StripeCustomer { Id = CustomerId };
            this.stripeService.Setup(v => v.CreateCustomerAsync(
                It.Is<StripeCustomerCreateOptions>(
                    x => JsonConvert.SerializeObject(x, Formatting.None) == JsonConvert.SerializeObject(expectedOptions, Formatting.None)),
                TestKey))
                .ReturnsAsync(customer);

            var result = await this.target.ExecuteAsync(UserId, TokenId, UserType.TestUser);

            Assert.AreEqual(CustomerId, result);
        }

        [TestMethod]
        public async Task WhenStripeModeIsLive_ItShouldCreateACustomer()
        {
            this.apiKeyRepository.Setup(v => v.GetApiKey(UserType.StandardUser)).Returns(LiveKey);

            var expectedOptions = new StripeCustomerCreateOptions
            {
                Description = UserId.ToString(),
                Source = new StripeSourceOptions
                {
                    TokenId = TokenId,
                }
            };

            var customer = new StripeCustomer { Id = CustomerId };
            this.stripeService.Setup(v => v.CreateCustomerAsync(
                It.Is<StripeCustomerCreateOptions>(
                    x => JsonConvert.SerializeObject(x, Formatting.None) == JsonConvert.SerializeObject(expectedOptions, Formatting.None)),
                LiveKey))
                .ReturnsAsync(customer);

            var result = await this.target.ExecuteAsync(UserId, TokenId, UserType.StandardUser);

            Assert.AreEqual(CustomerId, result);
        }
    }
}