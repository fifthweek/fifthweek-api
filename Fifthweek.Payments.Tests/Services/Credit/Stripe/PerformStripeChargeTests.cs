namespace Fifthweek.Payments.Tests.Services.Credit.Stripe
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Payments.Services.Credit;
    using Fifthweek.Payments.Services.Credit.Stripe;
    using Fifthweek.Payments.Stripe;
    using Fifthweek.Tests.Shared;

    using global::Stripe;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    using Newtonsoft.Json;

    [TestClass]
    public class PerformStripeChargeTests
    {
        private static readonly UserId UserId = UserId.Random();
        private static readonly string StripeCustomerId = "stripeCustomerId";
        private static readonly AmountInMinorDenomination Amount = new AmountInMinorDenomination(100);
        private static readonly Guid TransactionReference = Guid.NewGuid();
        private static readonly string TaxamoTransactionKey = "ttk";

        private static readonly string TestKey = "testkey";
        private static readonly string LiveKey = "livekey";
        private static readonly string ChargeId = "chargeId";

        private Mock<IStripeApiKeyRepository> apiKeyRepository;
        private Mock<IStripeService> stripeService;

        private PerformStripeCharge target;

        [TestInitialize]
        public void Initialize()
        {
            this.apiKeyRepository = new Mock<IStripeApiKeyRepository>(MockBehavior.Strict);
            this.stripeService = new Mock<IStripeService>(MockBehavior.Strict);

            this.target = new PerformStripeCharge(
                this.apiKeyRepository.Object,
                this.stripeService.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task WhenStripeCustomerIdIsNull_ItShouldThrowAnException()
        {
            await this.target.ExecuteAsync(null, Amount, UserId, TransactionReference, TaxamoTransactionKey, default(UserType));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task WhenAmountIsNull_ItShouldThrowAnException()
        {
            await this.target.ExecuteAsync(StripeCustomerId, null, UserId, TransactionReference, TaxamoTransactionKey, default(UserType));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task WhenUserIdIsNull_ItShouldThrowAnException()
        {
            await this.target.ExecuteAsync(StripeCustomerId, Amount, null, TransactionReference, TaxamoTransactionKey, default(UserType));
        }

        [TestMethod]
        public async Task WhenStripeModeIsTest_ItShouldCreateACharge()
        {
            this.apiKeyRepository.Setup(v => v.GetApiKey(UserType.TestUser)).Returns(TestKey);

            var expectedOptions = new StripeChargeCreateOptions
            {
                Amount = Amount.Value,
                Currency = PerformStripeCharge.Currency,
                CustomerId = StripeCustomerId,
                Metadata = new Dictionary<string, string>
                {
                    { PerformStripeCharge.TransactionReferenceMetadataKey, TransactionReference.ToString() },
                    { PerformStripeCharge.TaxamoTransactionKeyMetadataKey, TaxamoTransactionKey },
                    { PerformStripeCharge.UserIdMetadataKey, UserId.ToString() },
                }
            };

            var charge = new StripeCharge { Id = ChargeId };
            this.stripeService.Setup(v => v.CreateCharge(
                It.Is<StripeChargeCreateOptions>(
                    x => JsonConvert.SerializeObject(x, Formatting.None) == JsonConvert.SerializeObject(expectedOptions, Formatting.None)),
                TestKey))
                .ReturnsAsync(charge);

            var result = await this.target.ExecuteAsync(StripeCustomerId, Amount, UserId, TransactionReference, TaxamoTransactionKey, UserType.TestUser);

            Assert.AreEqual(ChargeId, result);
        }

        [TestMethod]
        public async Task WhenStripeModeIsLive_ItShouldCreateACharge()
        {
            this.apiKeyRepository.Setup(v => v.GetApiKey(UserType.StandardUser)).Returns(LiveKey);

            var expectedOptions = new StripeChargeCreateOptions
            {
                Amount = Amount.Value,
                Currency = PerformStripeCharge.Currency,
                CustomerId = StripeCustomerId,
                Metadata = new Dictionary<string, string>
                {
                    { PerformStripeCharge.TransactionReferenceMetadataKey, TransactionReference.ToString() },
                    { PerformStripeCharge.TaxamoTransactionKeyMetadataKey, TaxamoTransactionKey },
                    { PerformStripeCharge.UserIdMetadataKey, UserId.ToString() },
                }   
            };

            var charge = new StripeCharge { Id = ChargeId };
            this.stripeService.Setup(v => v.CreateCharge(
                It.Is<StripeChargeCreateOptions>(
                    x => JsonConvert.SerializeObject(x, Formatting.None) == JsonConvert.SerializeObject(expectedOptions, Formatting.None)),
                LiveKey))
                .ReturnsAsync(charge);

            var result = await this.target.ExecuteAsync(StripeCustomerId, Amount, UserId, TransactionReference, TaxamoTransactionKey, UserType.StandardUser);

            Assert.AreEqual(ChargeId, result);
        }

        [TestMethod]
        public async Task WhenThrowsExceptionOutsideCreateCharge_ItShouldPropagateException()
        {
            this.apiKeyRepository.Setup(v => v.GetApiKey(UserType.TestUser)).Throws(new DivideByZeroException());

            await ExpectedException.AssertExceptionAsync<DivideByZeroException>(
                () => this.target.ExecuteAsync(StripeCustomerId, Amount, UserId, TransactionReference, TaxamoTransactionKey, UserType.TestUser));
        }

        [TestMethod]
        public async Task WhenThrowsExceptionDuringCreateCharge_ItShouldWrapAndPropagateException()
        {
            this.apiKeyRepository.Setup(v => v.GetApiKey(UserType.TestUser)).Returns(TestKey);

            var expectedOptions = new StripeChargeCreateOptions
            {
                Amount = Amount.Value,
                Currency = PerformStripeCharge.Currency,
                CustomerId = StripeCustomerId,
                Metadata = new Dictionary<string, string>
                {
                    { PerformStripeCharge.TransactionReferenceMetadataKey, TransactionReference.ToString() },
                    { PerformStripeCharge.TaxamoTransactionKeyMetadataKey, TaxamoTransactionKey },
                    { PerformStripeCharge.UserIdMetadataKey, UserId.ToString() },
                }   
            };

            this.stripeService.Setup(v => v.CreateCharge(
                It.Is<StripeChargeCreateOptions>(
                    x => JsonConvert.SerializeObject(x, Formatting.None) == JsonConvert.SerializeObject(expectedOptions, Formatting.None)),
                TestKey))
                .Throws(new DivideByZeroException());

            await ExpectedException.AssertExceptionAsync<StripeChargeFailedException>(
                () => this.target.ExecuteAsync(StripeCustomerId, Amount, UserId, TransactionReference, TaxamoTransactionKey, UserType.TestUser));
        }
    }
}