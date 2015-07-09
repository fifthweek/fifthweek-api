namespace Fifthweek.Payments.Tests.Services.Credit
{
    using System;
    using System.Threading.Tasks;

    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence.Payments;
    using Fifthweek.Payments.Services.Credit;
    using Fifthweek.Payments.Services.Credit.Taxamo;
    using Fifthweek.Shared;
    using Fifthweek.Tests.Shared;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestClass]
    public class InitializeCreditRequestTests
    {
        private static readonly UserId UserId = UserId.Random();
        private static readonly Requester Requester = Requester.Authenticated(UserId);
        private static readonly PositiveInt Amount = PositiveInt.Parse(10);
        private static readonly PositiveInt ExpectedTotalAmount = PositiveInt.Parse(12);

        private static readonly TaxamoTransactionResult TaxamoTransaction = new TaxamoTransactionResult("key", new AmountInUsCents(10), new AmountInUsCents(12), new AmountInUsCents(2), 0.2m, "VAT", "GB", "England");
        private static readonly UserPaymentOriginResult Origin = new UserPaymentOriginResult("stripeCustomerId", "GB", "12345", "1.1.1.1", "ttk", PaymentStatus.Retry1);

        private Mock<IGetUserPaymentOriginDbStatement> getUserPaymentOrigin;
        private Mock<IDeleteTaxamoTransaction> deleteTaxamoTransaction;
        private Mock<ICreateTaxamoTransaction> createTaxamoTransaction;

        private InitializeCreditRequest target;

        [TestInitialize]
        public void Initialize()
        {
            this.getUserPaymentOrigin = new Mock<IGetUserPaymentOriginDbStatement>(MockBehavior.Strict);
            this.deleteTaxamoTransaction = new Mock<IDeleteTaxamoTransaction>(MockBehavior.Strict);
            this.createTaxamoTransaction = new Mock<ICreateTaxamoTransaction>(MockBehavior.Strict);

            this.target = new InitializeCreditRequest(
                this.getUserPaymentOrigin.Object,
                this.deleteTaxamoTransaction.Object,
                this.createTaxamoTransaction.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task WhenCommandIsNull_ItShouldThrowAnException()
        {
            await this.target.HandleAsync(null, Amount, ExpectedTotalAmount);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task WhenAmountIsNull_ItShouldThrowAnException()
        {
            await this.target.HandleAsync(UserId, null, ExpectedTotalAmount);
        }

        [TestMethod]
        public async Task ItShouldLoadOriginAndCreateTaxamoTransaction()
        {
            this.getUserPaymentOrigin.Setup(v => v.ExecuteAsync(UserId)).ReturnsAsync(Origin);

            this.createTaxamoTransaction.Setup(v => v.ExecuteAsync(
                Amount,
                Origin.CountryCode,
                Origin.CreditCardPrefix,
                Origin.IpAddress,
                Origin.OriginalTaxamoTransactionKey))
                .ReturnsAsync(TaxamoTransaction);

            var result = await this.target.HandleAsync(UserId, Amount, ExpectedTotalAmount);

            Assert.AreEqual(new InitializeCreditRequestResult(TaxamoTransaction, Origin), result);
        }

        [TestMethod]
        public async Task WhenExpectedAmountIsNotSupplied_ItShouldLoadOriginAndCreateTaxamoTransaction()
        {
            this.getUserPaymentOrigin.Setup(v => v.ExecuteAsync(UserId)).ReturnsAsync(Origin);

            this.createTaxamoTransaction.Setup(v => v.ExecuteAsync(
                Amount,
                Origin.CountryCode,
                Origin.CreditCardPrefix,
                Origin.IpAddress,
                Origin.OriginalTaxamoTransactionKey))
                .ReturnsAsync(TaxamoTransaction);

            var result = await this.target.HandleAsync(UserId, Amount, null);

            Assert.AreEqual(new InitializeCreditRequestResult(TaxamoTransaction, Origin), result);
        }

        [TestMethod]
        public async Task WhenTaxamoAmountDoesNotEqualExpectedAmount_ItShouldCancelTransactionAndAbort()
        {
            this.getUserPaymentOrigin.Setup(v => v.ExecuteAsync(UserId)).ReturnsAsync(Origin);

            this.createTaxamoTransaction.Setup(v => v.ExecuteAsync(
                Amount,
                Origin.CountryCode,
                Origin.CreditCardPrefix,
                Origin.IpAddress,
                Origin.OriginalTaxamoTransactionKey))
                .ReturnsAsync(TaxamoTransaction);

            this.deleteTaxamoTransaction.Setup(v => v.ExecuteAsync(TaxamoTransaction.Key))
                .Returns(Task.FromResult(0))
                .Verifiable();

            await ExpectedException.AssertExceptionAsync<BadRequestException>(
                () => this.target.HandleAsync(
                        UserId,
                        PositiveInt.Parse(10),
                        PositiveInt.Parse(11)));

            this.deleteTaxamoTransaction.Verify();
        }
    }
}