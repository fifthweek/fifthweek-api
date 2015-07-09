namespace Fifthweek.Payments.Tests.Services.Credit
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence.Payments;
    using Fifthweek.Payments.Services;
    using Fifthweek.Payments.Services.Credit;
    using Fifthweek.Shared;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestClass]
    public class TopUpUserAccountsWithCreditTests
    {
        private Mock<IGetUsersRequiringBillingRetryDbStatement> getUsersRequiringBillingRetry;
        private Mock<IApplyStandardUserCredit> applyStandardUserCredit;
        private Mock<IGetUserWeeklySubscriptionsCost> getUserWeeklySubscriptionsCost;
        private Mock<IIncrementBillingStatusDbStatement> incrementBillingStatus;
        private Mock<IGetUserPaymentOriginDbStatement> getUserPaymentOrigin;

        private TopUpUserAccountsWithCredit target;

        [TestInitialize]
        public void Initialize()
        {
            this.getUsersRequiringBillingRetry = new Mock<IGetUsersRequiringBillingRetryDbStatement>(MockBehavior.Strict);
            this.applyStandardUserCredit = new Mock<IApplyStandardUserCredit>(MockBehavior.Strict);
            this.getUserWeeklySubscriptionsCost = new Mock<IGetUserWeeklySubscriptionsCost>(MockBehavior.Strict);
            this.incrementBillingStatus = new Mock<IIncrementBillingStatusDbStatement>(MockBehavior.Strict);
            this.getUserPaymentOrigin = new Mock<IGetUserPaymentOriginDbStatement>(MockBehavior.Strict);

            this.target = new TopUpUserAccountsWithCredit(
                this.getUsersRequiringBillingRetry.Object,
                this.applyStandardUserCredit.Object,
                this.getUserWeeklySubscriptionsCost.Object,
                this.incrementBillingStatus.Object,
                this.getUserPaymentOrigin.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task WhenUpdatedAccountBalancesIsNull_ItShouldThrowAnException()
        {
            await this.target.ExecuteAsync(null, new List<PaymentProcessingException>());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task WhenErrorsIsNull_ItShouldThrowAnException()
        {
            await this.target.ExecuteAsync(new List<CalculatedAccountBalanceResult>(), null);
        }

        [TestMethod]
        public async Task ItShouldChargeAllNecessaryUsers()
        {
            var userId1 = UserId.Random();
            var userId2 = UserId.Random();
            var userId3 = UserId.Random();
            var userId4 = UserId.Random();
            var userId5 = UserId.Random();
            var input = new List<CalculatedAccountBalanceResult>
            { 
                new CalculatedAccountBalanceResult(DateTime.UtcNow, UserId.Random(), LedgerAccountType.Fifthweek, TopUpUserAccountsWithCredit.MinimumAccountBalanceBeforeBilling),
                new CalculatedAccountBalanceResult(DateTime.UtcNow, UserId.Random(), LedgerAccountType.Stripe, 0),
                new CalculatedAccountBalanceResult(DateTime.UtcNow, userId1, LedgerAccountType.Fifthweek, TopUpUserAccountsWithCredit.MinimumAccountBalanceBeforeBilling - 1),
                new CalculatedAccountBalanceResult(DateTime.UtcNow, userId2, LedgerAccountType.Fifthweek, -1m),
            };

            var usersRequiringRetry = new List<UserId> { userId3, userId4, userId5 };
            this.getUsersRequiringBillingRetry.Setup(v => v.ExecuteAsync()).ReturnsAsync(usersRequiringRetry);

            var usersRequiringCharge = new List<UserId> { userId1, userId2, userId3, userId4, userId5 };
            this.incrementBillingStatus.Setup(v => v.ExecuteAsync(It.IsAny<IReadOnlyList<UserId>>()))
                .Callback<IReadOnlyList<UserId>>(v => CollectionAssert.AreEquivalent(usersRequiringCharge, v.ToList()))
                .Returns(Task.FromResult(0)).Verifiable();

            this.getUserWeeklySubscriptionsCost.Setup(v => v.ExecuteAsync(userId1)).ReturnsAsync(TopUpUserAccountsWithCredit.MinimumChargeWhenBilling - 1);
            this.getUserWeeklySubscriptionsCost.Setup(v => v.ExecuteAsync(userId2)).ReturnsAsync(TopUpUserAccountsWithCredit.MinimumChargeWhenBilling + 1);
            this.getUserWeeklySubscriptionsCost.Setup(v => v.ExecuteAsync(userId3)).ReturnsAsync(0);
            this.getUserWeeklySubscriptionsCost.Setup(v => v.ExecuteAsync(userId4)).ReturnsAsync(TopUpUserAccountsWithCredit.MinimumChargeWhenBilling * 2);
            this.getUserWeeklySubscriptionsCost.Setup(v => v.ExecuteAsync(userId5)).ReturnsAsync(TopUpUserAccountsWithCredit.MinimumChargeWhenBilling * 2);

            this.getUserPaymentOrigin.Setup(v => v.ExecuteAsync(userId1))
                .ReturnsAsync(new UserPaymentOriginResult("customer1", null, null, null, null, BillingStatus.Retry1));
            this.getUserPaymentOrigin.Setup(v => v.ExecuteAsync(userId2))
                .ReturnsAsync(new UserPaymentOriginResult("customer2", null, null, null, null, BillingStatus.Retry1));
            this.getUserPaymentOrigin.Setup(v => v.ExecuteAsync(userId4))
                .ReturnsAsync(new UserPaymentOriginResult(null, null, null, null, null, BillingStatus.Retry1));
            this.getUserPaymentOrigin.Setup(v => v.ExecuteAsync(userId5))
                .ReturnsAsync(new UserPaymentOriginResult("customer5", null, null, null, null, BillingStatus.None));

            this.applyStandardUserCredit.Setup(v => v.ExecuteAsync(userId1, PositiveInt.Parse(TopUpUserAccountsWithCredit.MinimumChargeWhenBilling), null))
                .Returns(Task.FromResult(0))
                .Verifiable();

            this.applyStandardUserCredit.Setup(v => v.ExecuteAsync(userId2, PositiveInt.Parse(TopUpUserAccountsWithCredit.MinimumChargeWhenBilling + 1), null))
                .Returns(Task.FromResult(0))
                .Verifiable();

            var result = await this.target.ExecuteAsync(input, new List<PaymentProcessingException>());

            this.incrementBillingStatus.Verify();
            this.applyStandardUserCredit.Verify();

            Assert.IsTrue(result);
        }

        [TestMethod]
        public async Task ItShouldReturnFalseIfNoUsersCharged()
        {
            var userId1 = UserId.Random();
            var userId2 = UserId.Random();
            var userId3 = UserId.Random();
            var userId4 = UserId.Random();
            var userId5 = UserId.Random();
            var input = new List<CalculatedAccountBalanceResult>
            { 
                new CalculatedAccountBalanceResult(DateTime.UtcNow, UserId.Random(), LedgerAccountType.Fifthweek, TopUpUserAccountsWithCredit.MinimumAccountBalanceBeforeBilling),
                new CalculatedAccountBalanceResult(DateTime.UtcNow, UserId.Random(), LedgerAccountType.Stripe, 0),
                new CalculatedAccountBalanceResult(DateTime.UtcNow, userId1, LedgerAccountType.Fifthweek, TopUpUserAccountsWithCredit.MinimumAccountBalanceBeforeBilling - 1),
                new CalculatedAccountBalanceResult(DateTime.UtcNow, userId2, LedgerAccountType.Fifthweek, -1m),
            };

            var usersRequiringRetry = new List<UserId> { userId3, userId4, userId5 };
            this.getUsersRequiringBillingRetry.Setup(v => v.ExecuteAsync()).ReturnsAsync(usersRequiringRetry);

            var usersRequiringCharge = new List<UserId> { userId1, userId2, userId3, userId4, userId5 };
            this.incrementBillingStatus.Setup(v => v.ExecuteAsync(It.IsAny<IReadOnlyList<UserId>>()))
                .Callback<IReadOnlyList<UserId>>(v => CollectionAssert.AreEquivalent(usersRequiringCharge, v.ToList()))
                .Returns(Task.FromResult(0)).Verifiable();

            this.getUserWeeklySubscriptionsCost.Setup(v => v.ExecuteAsync(userId1)).ReturnsAsync(0);
            this.getUserWeeklySubscriptionsCost.Setup(v => v.ExecuteAsync(userId2)).ReturnsAsync(0);
            this.getUserWeeklySubscriptionsCost.Setup(v => v.ExecuteAsync(userId3)).ReturnsAsync(0);
            this.getUserWeeklySubscriptionsCost.Setup(v => v.ExecuteAsync(userId4)).ReturnsAsync(TopUpUserAccountsWithCredit.MinimumChargeWhenBilling * 2);
            this.getUserWeeklySubscriptionsCost.Setup(v => v.ExecuteAsync(userId5)).ReturnsAsync(TopUpUserAccountsWithCredit.MinimumChargeWhenBilling * 2);

            this.getUserPaymentOrigin.Setup(v => v.ExecuteAsync(userId4))
                .ReturnsAsync(new UserPaymentOriginResult(null, null, null, null, null, BillingStatus.Retry1));
            this.getUserPaymentOrigin.Setup(v => v.ExecuteAsync(userId5))
                .ReturnsAsync(new UserPaymentOriginResult("customer5", null, null, null, null, BillingStatus.None));

            var result = await this.target.ExecuteAsync(input, new List<PaymentProcessingException>());

            this.incrementBillingStatus.Verify();

            Assert.IsFalse(result);
        }
    }
}