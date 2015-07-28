namespace Fifthweek.Payments.Tests.Services.Credit
{
    using System;
    using System.Threading.Tasks;

    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence.Payments;
    using Fifthweek.Payments.Services.Credit;
    using Fifthweek.Payments.Services.Credit.Stripe;
    using Fifthweek.Payments.Services.Credit.Taxamo;
    using Fifthweek.Payments.Shared;
    using Fifthweek.Payments.Stripe;
    using Fifthweek.Shared;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestClass]
    public class PerformCreditRequestTests
    {
        private static readonly DateTime Now = DateTime.UtcNow;
        private static readonly TransactionReference TransactionReference = TransactionReference.Random();
        private static readonly string StripeChargeId = Guid.NewGuid().ToString();
        private static readonly UserId UserId = UserId.Random();
        private static readonly Requester Requester = Requester.Authenticated(UserId);

        private static readonly TaxamoTransactionResult TaxamoTransaction = new TaxamoTransactionResult("key", new AmountInMinorDenomination(10), new AmountInMinorDenomination(12), new AmountInMinorDenomination(2), 0.2m, "VAT", "GB", "England");
        private static readonly UserPaymentOriginResult Origin = new UserPaymentOriginResult("stripeCustomerId", PaymentOriginKeyType.Stripe, "GB", "12345", "1.1.1.1", "ttk", PaymentStatus.Retry1);

        private Mock<IPerformStripeCharge> performStripeCharge;

        private PerformCreditRequest target;

        [TestInitialize]
        public void Initialize()
        {
            this.performStripeCharge = new Mock<IPerformStripeCharge>(MockBehavior.Strict);

            this.target = new PerformCreditRequest(
                this.performStripeCharge.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task WhenUserIdIsNull_ItShouldThrowAnException()
        {
            await this.target.HandleAsync(null, Now, TransactionReference, TaxamoTransaction, Origin, default(UserType));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task WhenTransactionReferencesNull_ItShouldThrowAnException()
        {
            await this.target.HandleAsync(UserId, Now, null, TaxamoTransaction, Origin, default(UserType));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task WhenTaxamoTransactionIsNull_ItShouldThrowAnException()
        {
            await this.target.HandleAsync(UserId, Now, TransactionReference, null, Origin, default(UserType));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task WhenOriginIsNull_ItShouldThrowAnException()
        {
            await this.target.HandleAsync(UserId, Now, TransactionReference, TaxamoTransaction, null, default(UserType));
        }

        [TestMethod]
        public async Task ItShouldPerformAStripeCharge()
        {
            this.performStripeCharge.Setup(v => v.ExecuteAsync(
                Origin.PaymentOriginKey,
                TaxamoTransaction.TotalAmount,
                UserId,
                TransactionReference,
                TaxamoTransaction.Key,
                UserType.TestUser))
                .ReturnsAsync(StripeChargeId);

            var result = await this.target.HandleAsync(UserId, Now, TransactionReference, TaxamoTransaction, Origin, UserType.TestUser);

            Assert.AreEqual(new StripeTransactionResult(Now, TransactionReference, StripeChargeId), result);
        }

        [TestMethod]
        public async Task ItShouldPerformAStripeCharge2()
        {
            this.performStripeCharge.Setup(v => v.ExecuteAsync(
                Origin.PaymentOriginKey,
                TaxamoTransaction.TotalAmount,
                UserId,
                TransactionReference,
                TaxamoTransaction.Key,
                UserType.StandardUser))
                .ReturnsAsync(StripeChargeId);

            var result = await this.target.HandleAsync(UserId, Now, TransactionReference, TaxamoTransaction, Origin, UserType.StandardUser);

            Assert.AreEqual(new StripeTransactionResult(Now, TransactionReference, StripeChargeId), result);
        }
    }
}