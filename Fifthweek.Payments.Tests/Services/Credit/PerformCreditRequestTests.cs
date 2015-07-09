namespace Fifthweek.Payments.Tests.Services.Credit
{
    using System;
    using System.Threading.Tasks;

    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence.Payments;
    using Fifthweek.Payments.Services.Credit;
    using Fifthweek.Payments.Services.Credit.Stripe;
    using Fifthweek.Payments.Services.Credit.Taxamo;
    using Fifthweek.Shared;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestClass]
    public class PerformCreditRequestTests
    {
        private static readonly DateTime Now = DateTime.UtcNow;
        private static readonly Guid TransactionReference = Guid.NewGuid();
        private static readonly string StripeChargeId = Guid.NewGuid().ToString();
        private static readonly UserId UserId = UserId.Random();
        private static readonly Requester Requester = Requester.Authenticated(UserId);

        private static readonly TaxamoTransactionResult TaxamoTransaction = new TaxamoTransactionResult("key", new AmountInUsCents(10), new AmountInUsCents(12), new AmountInUsCents(2), 0.2m, "VAT", "GB", "England");
        private static readonly UserPaymentOriginResult Origin = new UserPaymentOriginResult("stripeCustomerId", "GB", "12345", "1.1.1.1", "ttk", BillingStatus.Retry1);

        private Mock<ITimestampCreator> timestampCreator;
        private Mock<IPerformStripeCharge> performStripeCharge;
        private Mock<IGuidCreator> guidCreator;

        private PerformCreditRequest target;

        [TestInitialize]
        public void Initialize()
        {
            this.timestampCreator = new Mock<ITimestampCreator>(MockBehavior.Strict);
            this.performStripeCharge = new Mock<IPerformStripeCharge>(MockBehavior.Strict);
            this.guidCreator = new Mock<IGuidCreator>(MockBehavior.Strict);

            this.target = new PerformCreditRequest(
                this.timestampCreator.Object,
                this.performStripeCharge.Object,
                this.guidCreator.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task WhenUserIdIsNull_ItShouldThrowAnException()
        {
            await this.target.HandleAsync(null, TaxamoTransaction, Origin);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task WhenTaxamoTransactionIsNull_ItShouldThrowAnException()
        {
            await this.target.HandleAsync(UserId, null, Origin);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task WhenOriginIsNull_ItShouldThrowAnException()
        {
            await this.target.HandleAsync(UserId, TaxamoTransaction, null);
        }

        [TestMethod]
        public async Task ItShouldPerformAStripeCharge()
        {
            this.timestampCreator.Setup(v => v.Now()).Returns(Now);
            this.guidCreator.Setup(v => v.CreateSqlSequential()).Returns(TransactionReference);

            this.performStripeCharge.Setup(v => v.ExecuteAsync(
                Origin.StripeCustomerId,
                TaxamoTransaction.TotalAmount,
                UserId,
                TransactionReference,
                TaxamoTransaction.Key))
                .ReturnsAsync(StripeChargeId);

            var result = await this.target.HandleAsync(UserId, TaxamoTransaction, Origin);

            Assert.AreEqual(new StripeTransactionResult(Now, TransactionReference, StripeChargeId), result);
        }
    }
}