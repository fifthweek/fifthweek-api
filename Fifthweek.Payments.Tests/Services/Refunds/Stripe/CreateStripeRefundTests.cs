namespace Fifthweek.Payments.Tests.Services.Refunds.Stripe
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Payments.Services.Credit.Stripe;
    using Fifthweek.Payments.Services.Refunds;
    using Fifthweek.Payments.Services.Refunds.Stripe;
    using Fifthweek.Payments.Stripe;
    using Fifthweek.Shared;

    using global::Stripe;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    using Newtonsoft.Json;

    [TestClass]
    public class CreateStripeRefundTests
    {
        private static readonly UserId UserId = UserId.Random();
        private static readonly string StripeChargeId = "stripeChargeId";
        private static readonly PositiveInt TotalRefundAmount = PositiveInt.Parse(123);

        private static readonly string TestKey = "testkey";
        private static readonly string LiveKey = "livekey";
        private static readonly string CustomerId = "customerId";

        private Mock<IStripeApiKeyRepository> apiKeyRepository;
        private Mock<IStripeService> stripeService;

        private CreateStripeRefund target;

        [TestInitialize]
        public void Initialize()
        {
            this.apiKeyRepository = new Mock<IStripeApiKeyRepository>(MockBehavior.Strict);
            this.stripeService = new Mock<IStripeService>(MockBehavior.Strict);

            this.target = new CreateStripeRefund(
                this.apiKeyRepository.Object,
                this.stripeService.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task WhenEnactingUserIdIsNull_ItShouldThrowAnException()
        {
            await this.target.ExecuteAsync(null, StripeChargeId, TotalRefundAmount, default(RefundCreditReason), default(UserType));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task WhenStripeChargeIdIsNull_ItShouldThrowAnException()
        {
            await this.target.ExecuteAsync(UserId, null, TotalRefundAmount, default(RefundCreditReason), default(UserType));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task WhenTotalRefundAmountIsNull_ItShouldThrowAnException()
        {
            await this.target.ExecuteAsync(UserId, StripeChargeId, null, default(RefundCreditReason), default(UserType));
        }

        [TestMethod]
        public async Task WhenStripeModeIsTest_ItShouldCreateACustomer()
        {
            this.apiKeyRepository.Setup(v => v.GetApiKey(UserType.TestUser)).Returns(TestKey);

            var expectedOptions = new StripeRefundCreateOptions
            {
                Amount = TotalRefundAmount.Value,
                Reason = "requested_by_customer",
                Metadata = new Dictionary<string, string>
                {
                    { CreateStripeRefund.EnactingUserIdMetadataKey, UserId.ToString() },
                }
            };

            var stripeRefund = new StripeRefund { Id = CustomerId };
            this.stripeService.Setup(v => v.RefundChargeAsync(
                StripeChargeId,
                It.Is<StripeRefundCreateOptions>(
                    x => JsonConvert.SerializeObject(x, Formatting.None) == JsonConvert.SerializeObject(expectedOptions, Formatting.None)),
                TestKey))
                .ReturnsAsync(stripeRefund)
                .Verifiable();

            await this.target.ExecuteAsync(UserId, StripeChargeId, TotalRefundAmount, RefundCreditReason.RequestedByCustomer, UserType.TestUser);

            this.stripeService.Verify();
        }

        [TestMethod]
        public async Task WhenStripeModeIsLive_ItShouldCreateACustomer()
        {
            this.apiKeyRepository.Setup(v => v.GetApiKey(UserType.StandardUser)).Returns(LiveKey);

            var expectedOptions = new StripeRefundCreateOptions
            {
                Amount = TotalRefundAmount.Value,
                Reason = "requested_by_customer",
                Metadata = new Dictionary<string, string>
                {
                    { CreateStripeRefund.EnactingUserIdMetadataKey, UserId.ToString() },
                }
            };

            var stripeRefund = new StripeRefund { Id = CustomerId };
            this.stripeService.Setup(v => v.RefundChargeAsync(
                StripeChargeId,
                It.Is<StripeRefundCreateOptions>(
                    x => JsonConvert.SerializeObject(x, Formatting.None) == JsonConvert.SerializeObject(expectedOptions, Formatting.None)),
                TestKey))
                .ReturnsAsync(stripeRefund)
                .Verifiable();

            await this.target.ExecuteAsync(UserId, StripeChargeId, TotalRefundAmount, RefundCreditReason.RequestedByCustomer, UserType.StandardUser);
            this.stripeService.Verify();
        }

        [TestMethod]
        public void WhenGetReasonIsCalledWithDuplicate_ItShouldReturnDuplicate()
        {
            Assert.AreEqual(CreateStripeRefund.DuplicateString, this.target.GetReason(RefundCreditReason.Duplicate));
        }

        [TestMethod]
        public void WhenGetReasonIsCalledWithFraudulent_ItShouldReturnFraudulent()
        {
            Assert.AreEqual(CreateStripeRefund.FraudulentString, this.target.GetReason(RefundCreditReason.Fraudulent));
        }

        [TestMethod]
        public void WhenGetReasonIsCalledWithRequestedByCustomer_ItShouldReturnRequestedByCustomer()
        {
            Assert.AreEqual(CreateStripeRefund.RequestedByCustomerString, this.target.GetReason(RefundCreditReason.RequestedByCustomer));
        }
    }
}