namespace Fifthweek.Api.Payments.Tests.Queries
{
    using System;
    using System.Threading.Tasks;

    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Identity.Tests.Shared.Membership;
    using Fifthweek.Api.Payments.Controllers;
    using Fifthweek.Api.Payments.Queries;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Api.Persistence.Payments;
    using Fifthweek.Payments;
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
            UserId,
            null);

        private static readonly GetCreditRequestSummaryQuery QueryWithOverride =
            new GetCreditRequestSummaryQuery(
                Requester,
                UserId,
            new GetCreditRequestSummaryQuery.LocationData(
                ValidCountryCode.Parse("GB"),
                ValidCreditCardPrefix.Parse("987654"), 
                ValidIpAddress.Parse("2.2.2.2")));

        private static readonly TaxamoCalculationResult TaxamoTransaction = new TaxamoCalculationResult(new AmountInUsCents(10), new AmountInUsCents(12), new AmountInUsCents(2), 0.2m, "VAT", "GB", "England", null);
        private static readonly UserPaymentOriginResult Origin = new UserPaymentOriginResult("stripeCustomerId", PaymentOriginKeyType.Stripe, "GB", "12345", "1.1.1.1", "ttk", PaymentStatus.Retry1);

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
                Requester, UserId.Random(), null));
        }

        [TestMethod]
        [ExpectedException(typeof(UnauthorizedException))]
        public async Task WhenUserIsNotAuthenticated_ItShouldThrowAnException()
        {
            await this.target.HandleAsync(new GetCreditRequestSummaryQuery(
                Requester.Unauthenticated, UserId, null));
        }

        [TestMethod]
        public async Task WhenStandardUser_ItShouldReturnTaxInformationSummary()
        {
            this.getUserPaymentOrigin.Setup(v => v.ExecuteAsync(UserId)).ReturnsAsync(Origin);

            this.requesterSecurity.Setup(v => v.IsInRoleAsync(Requester, FifthweekRole.TestUser)).ReturnsAsync(false);

            this.getUserWeeklySubscriptionCost.Setup(v => v.ExecuteAsync(UserId)).ReturnsAsync(TopUpUserAccountsWithCredit.MinimumPaymentAmount + 1);

            this.getTaxInformation
                .Setup(v => v.ExecuteAsync(
                    PositiveInt.Parse(TopUpUserAccountsWithCredit.MinimumPaymentAmount + 1),
                    Origin.CountryCode,
                    Origin.CreditCardPrefix,
                    Origin.IpAddress,
                    Origin.OriginalTaxamoTransactionKey,
                    UserType.StandardUser))
                .ReturnsAsync(TaxamoTransaction);

            var result = await this.target.HandleAsync(Query);

            Assert.AreEqual(
                new CreditRequestSummary(TopUpUserAccountsWithCredit.MinimumPaymentAmount + 1, TaxamoTransaction),
                result);
        }

        [TestMethod]
        public async Task WhenTestUser_ItShouldReturnTaxInformationSummary()
        {
            this.getUserPaymentOrigin.Setup(v => v.ExecuteAsync(UserId)).ReturnsAsync(Origin);

            this.requesterSecurity.Setup(v => v.IsInRoleAsync(Requester, FifthweekRole.TestUser)).ReturnsAsync(true);

            this.getUserWeeklySubscriptionCost.Setup(v => v.ExecuteAsync(UserId)).ReturnsAsync(TopUpUserAccountsWithCredit.MinimumPaymentAmount + 1);

            this.getTaxInformation
                .Setup(v => v.ExecuteAsync(
                    PositiveInt.Parse(TopUpUserAccountsWithCredit.MinimumPaymentAmount + 1),
                    Origin.CountryCode,
                    Origin.CreditCardPrefix,
                    Origin.IpAddress,
                    Origin.OriginalTaxamoTransactionKey,
                    UserType.TestUser))
                .ReturnsAsync(TaxamoTransaction);

            var result = await this.target.HandleAsync(Query);

            Assert.AreEqual(
                new CreditRequestSummary(TopUpUserAccountsWithCredit.MinimumPaymentAmount + 1, TaxamoTransaction),
                result);
        }

        [TestMethod]
        public async Task WhenSubscriptionCstIsLessThanMinimumCharge_ItShouldReturnTaxInformationSummaryForMinimumCharge()
        {
            this.getUserPaymentOrigin.Setup(v => v.ExecuteAsync(UserId)).ReturnsAsync(Origin);
            this.requesterSecurity.Setup(v => v.IsInRoleAsync(Requester, FifthweekRole.TestUser)).ReturnsAsync(false);

            this.getUserWeeklySubscriptionCost.Setup(v => v.ExecuteAsync(UserId)).ReturnsAsync(TopUpUserAccountsWithCredit.MinimumPaymentAmount - 1);

            this.getTaxInformation
                .Setup(v => v.ExecuteAsync(
                    PositiveInt.Parse(TopUpUserAccountsWithCredit.MinimumPaymentAmount),
                    Origin.CountryCode, 
                    Origin.CreditCardPrefix,
                    Origin.IpAddress,
                    Origin.OriginalTaxamoTransactionKey,
                    UserType.StandardUser))
                .ReturnsAsync(TaxamoTransaction);

            var result = await this.target.HandleAsync(Query);

            Assert.AreEqual(
                new CreditRequestSummary(TopUpUserAccountsWithCredit.MinimumPaymentAmount - 1, TaxamoTransaction),
                result);
        }

        [TestMethod]
        public async Task WhenLocationDataSupplied_ItShouldNotRequestOrigin()
        {
            this.requesterSecurity.Setup(v => v.IsInRoleAsync(Requester, FifthweekRole.TestUser)).ReturnsAsync(false);

            this.getUserWeeklySubscriptionCost.Setup(v => v.ExecuteAsync(UserId)).ReturnsAsync(TopUpUserAccountsWithCredit.MinimumPaymentAmount + 1);

            this.getTaxInformation
                .Setup(v => v.ExecuteAsync(
                    PositiveInt.Parse(TopUpUserAccountsWithCredit.MinimumPaymentAmount + 1),
                    QueryWithOverride.LocationDataOverride.CountryCode.Value,
                    QueryWithOverride.LocationDataOverride.CreditCardPrefix.Value,
                    QueryWithOverride.LocationDataOverride.IpAddress.Value,
                    null,
                    UserType.StandardUser))
                .ReturnsAsync(TaxamoTransaction);

            var result = await this.target.HandleAsync(QueryWithOverride);

            Assert.AreEqual(
                new CreditRequestSummary(TopUpUserAccountsWithCredit.MinimumPaymentAmount + 1, TaxamoTransaction),
                result);
        }
    }
}