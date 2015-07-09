namespace Fifthweek.Api.Payments.Tests.Queries
{
    using System;
    using System.Threading.Tasks;

    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Identity.Tests.Shared.Membership;
    using Fifthweek.Api.Payments.Controllers;
    using Fifthweek.Api.Payments.Queries;
    using Fifthweek.Api.Persistence.Payments;
    using Fifthweek.Payments.Services.Credit;
    using Fifthweek.Payments.Services.Credit.Taxamo;
    using Fifthweek.Shared;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestClass]
    public class GetCreditRequestSummaryQueryHandlerTests
    {
        private static readonly UserId UserId = UserId.Random();
        private static readonly Requester Requester = Requester.Authenticated(UserId);

        private static readonly GetCreditRequestSummaryQuery Query = new GetCreditRequestSummaryQuery(
            Requester,
            UserId);

        private static readonly TaxamoTransactionResult TaxamoTransaction = new TaxamoTransactionResult("key", new AmountInUsCents(10), new AmountInUsCents(12), new AmountInUsCents(2), 0.2m, "VAT", "GB", "England");
        private static readonly UserPaymentOriginResult Origin = new UserPaymentOriginResult("stripeCustomerId", "GB", "12345", "1.1.1.1", "ttk", PaymentStatus.Retry1);

        private Mock<IRequesterSecurity> requesterSecurity;
        private Mock<IGetUserPaymentOriginDbStatement> getUserPaymentOrigin;
        private Mock<IGetTaxInformation> getTaxInformation;
        private Mock<IGetUserWeeklySubscriptionsCost> getUserWeeklySubscriptionCost;

        private GetCreditRequestSummaryQueryHandler target;

        [TestInitialize]
        public void Initialize()
        {
            this.requesterSecurity = new Mock<IRequesterSecurity>();
            this.getUserPaymentOrigin = new Mock<IGetUserPaymentOriginDbStatement>(MockBehavior.Strict);
            this.getTaxInformation = new Mock<IGetTaxInformation>(MockBehavior.Strict);
            this.getUserWeeklySubscriptionCost = new Mock<IGetUserWeeklySubscriptionsCost>(MockBehavior.Strict);

            this.requesterSecurity.SetupFor(Requester);

            this.target = new GetCreditRequestSummaryQueryHandler(
                this.requesterSecurity.Object,
                this.getUserPaymentOrigin.Object,
                this.getTaxInformation.Object,
                this.getUserWeeklySubscriptionCost.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task WhenQueryIsNull_ItShouldThrowAnException()
        {
            await this.target.HandleAsync(null);
        }


        [TestMethod]
        [ExpectedException(typeof(UnauthorizedException))]
        public async Task WhenUserIsNotAuthorized_ItShouldThrowAnException()
        {
            await this.target.HandleAsync(new GetCreditRequestSummaryQuery(
                Requester, UserId.Random()));
        }

        [TestMethod]
        [ExpectedException(typeof(UnauthorizedException))]
        public async Task WhenUserIsNotAuthenticated_ItShouldThrowAnException()
        {
            await this.target.HandleAsync(new GetCreditRequestSummaryQuery(
                Requester.Unauthenticated, UserId));
        }

        [TestMethod]
        public async Task ItShouldReturnTaxInformationSummary()
        {
            this.getUserPaymentOrigin.Setup(v => v.ExecuteAsync(UserId)).ReturnsAsync(Origin);

            this.getUserWeeklySubscriptionCost.Setup(v => v.ExecuteAsync(UserId)).ReturnsAsync(99);

            this.getTaxInformation.Setup(v => v.ExecuteAsync(PositiveInt.Parse(99), Origin.CountryCode, Origin.CreditCardPrefix, Origin.IpAddress, Origin.OriginalTaxamoTransactionKey))
                .ReturnsAsync(TaxamoTransaction);

            var result = await this.target.HandleAsync(Query);

            Assert.AreEqual(
                new CreditRequestSummary(
                    TaxamoTransaction.Amount.Value,
                    TaxamoTransaction.TotalAmount.Value,
                    TaxamoTransaction.TaxAmount.Value,
                    TaxamoTransaction.TaxRate,
                    TaxamoTransaction.TaxName,
                    TaxamoTransaction.TaxEntityName,
                    TaxamoTransaction.CountryName),
                result);
        }
    }
}