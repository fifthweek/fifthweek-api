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
        private static readonly UserPaymentOriginResult Origin = new UserPaymentOriginResult("stripeCustomerId", PaymentOriginKeyType.Stripe, "GB", "12345", "1.1.1.1", "ttk", PaymentStatus.Retry1);

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
            await this.target.HandleAsync(null, Amount, ExpectedTotalAmount, default(UserType));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task WhenAmountIsNull_ItShouldThrowAnException()
        {
            await this.target.HandleAsync(UserId, null, ExpectedTotalAmount, default(UserType));
        }

        [TestMethod]
        [ExpectedException(typeof(CreditCardDetailsDoNotExistException))]
        public async Task WhenPaymentOriginKeyIsNull_ItShouldThrowAnException()
        {
            this.getUserPaymentOrigin.Setup(v => v.ExecuteAsync(UserId)).ReturnsAsync(
                new UserPaymentOriginResult(
                    null,
                    PaymentOriginKeyType.Stripe,
                    Origin.CountryCode,
                    Origin.CreditCardPrefix,
                    Origin.IpAddress,
                    Origin.OriginalTaxamoTransactionKey,
                    Origin.PaymentStatus));

            await this.target.HandleAsync(UserId, Amount, ExpectedTotalAmount, UserType.StandardUser);
        }

        [TestMethod]
        [ExpectedException(typeof(CreditCardDetailsDoNotExistException))]
        public async Task WhenPaymentOriginKeyTypeIsNone_ItShouldThrowAnException()
        {
            this.getUserPaymentOrigin.Setup(v => v.ExecuteAsync(UserId)).ReturnsAsync(
                new UserPaymentOriginResult(
                    Origin.PaymentOriginKey,
                    PaymentOriginKeyType.None,
                    Origin.CountryCode,
                    Origin.CreditCardPrefix,
                    Origin.IpAddress,
                    Origin.OriginalTaxamoTransactionKey,
                    Origin.PaymentStatus));

            await this.target.HandleAsync(UserId, Amount, ExpectedTotalAmount, UserType.StandardUser);
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
                Origin.OriginalTaxamoTransactionKey,
                UserType.StandardUser))
                .ReturnsAsync(TaxamoTransaction);

            var result = await this.target.HandleAsync(UserId, Amount, ExpectedTotalAmount, UserType.StandardUser);

            Assert.AreEqual(new InitializeCreditRequestResult(TaxamoTransaction, Origin), result);
        }

        [TestMethod]
        public async Task WhenUserTypeIsTestUser_ItShouldLoadOriginAndCreateTaxamoTransaction()
        {
            this.getUserPaymentOrigin.Setup(v => v.ExecuteAsync(UserId)).ReturnsAsync(Origin);

            this.createTaxamoTransaction.Setup(v => v.ExecuteAsync(
                Amount,
                Origin.CountryCode,
                Origin.CreditCardPrefix,
                Origin.IpAddress,
                Origin.OriginalTaxamoTransactionKey,
                UserType.TestUser))
                .ReturnsAsync(TaxamoTransaction);

            var result = await this.target.HandleAsync(UserId, Amount, ExpectedTotalAmount, UserType.TestUser);

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
                Origin.OriginalTaxamoTransactionKey,
                UserType.StandardUser))
                .ReturnsAsync(TaxamoTransaction);

            var result = await this.target.HandleAsync(UserId, Amount, null, UserType.StandardUser);

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
                Origin.OriginalTaxamoTransactionKey,
                UserType.StandardUser))
                .ReturnsAsync(TaxamoTransaction);

            this.deleteTaxamoTransaction.Setup(v => v.ExecuteAsync(TaxamoTransaction.Key, UserType.StandardUser))
                .Returns(Task.FromResult(0))
                .Verifiable();

            await ExpectedException.AssertExceptionAsync<BadRequestException>(
                () => this.target.HandleAsync(
                        UserId,
                        PositiveInt.Parse(10),
                        PositiveInt.Parse(11),
                        UserType.StandardUser));

            this.deleteTaxamoTransaction.Verify();
        }

        [TestMethod]
        public async Task WhenTaxamoAmountDoesNotEqualExpectedAmount_AndUserTypeIsTestUser_ItShouldCancelTransactionAndAbort()
        {
            this.getUserPaymentOrigin.Setup(v => v.ExecuteAsync(UserId)).ReturnsAsync(Origin);

            this.createTaxamoTransaction.Setup(v => v.ExecuteAsync(
                Amount,
                Origin.CountryCode,
                Origin.CreditCardPrefix,
                Origin.IpAddress,
                Origin.OriginalTaxamoTransactionKey,
                UserType.TestUser))
                .ReturnsAsync(TaxamoTransaction);

            this.deleteTaxamoTransaction.Setup(v => v.ExecuteAsync(TaxamoTransaction.Key, UserType.TestUser))
                .Returns(Task.FromResult(0))
                .Verifiable();

            await ExpectedException.AssertExceptionAsync<BadRequestException>(
                () => this.target.HandleAsync(
                        UserId,
                        PositiveInt.Parse(10),
                        PositiveInt.Parse(11),
                        UserType.TestUser));

            this.deleteTaxamoTransaction.Verify();
        }
    }
}