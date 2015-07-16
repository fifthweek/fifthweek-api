namespace Fifthweek.Payments.Services.Credit
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Fifthweek.Api.Persistence.Payments;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Payments.Services.Credit;
    using Fifthweek.Shared;

    [AutoConstructor]
    public partial class TopUpUserAccountsWithCredit : ITopUpUserAccountsWithCredit
    {
        public const decimal MinimumAccountBalanceBeforeCharge = 20m;
        public const int MinimumPaymentAmount = 500;

        private readonly IGetUsersRequiringPaymentRetryDbStatement getUsersRequiringPaymentRetry;
        private readonly IApplyUserCredit applyUserCredit;
        private readonly IGetUserWeeklySubscriptionsCost getUserWeeklySubscriptionsCost;
        private readonly IIncrementPaymentStatusDbStatement incrementPaymentStatus;
        private readonly IGetUserPaymentOriginDbStatement getUserPaymentOrigin;

        public async Task<bool> ExecuteAsync(
            IReadOnlyList<CalculatedAccountBalanceResult> updatedAccountBalances, 
            List<PaymentProcessingException> errors)
        {
            updatedAccountBalances.AssertNotNull("updatedAccountBalances");
            errors.AssertNotNull("errors");

            var userIdsToRetry = await this.getUsersRequiringPaymentRetry.ExecuteAsync();
            var newUserIds = updatedAccountBalances
                .Where(v => v.AccountType == LedgerAccountType.Fifthweek
                        && v.Amount < MinimumAccountBalanceBeforeCharge)
                .Select(v => v.UserId);
            var allUserIds = userIdsToRetry.Concat(newUserIds).Distinct().ToList();

            // Increment all billing status immediately so the user maintains access to his newsfeed.
            await this.incrementPaymentStatus.ExecuteAsync(allUserIds);

            bool recalculateBalances = false;
            foreach (var userId in allUserIds)
            {
                try
                {
                    // Work out what we should charge the user.
                    var amountToCharge = await this.getUserWeeklySubscriptionsCost.ExecuteAsync(userId);

                    if (amountToCharge == 0)
                    {
                        // No subscriptions, so no need to charge.
                        continue;
                    }

                    amountToCharge = Math.Max(MinimumPaymentAmount, amountToCharge);

                    var origin = await this.getUserPaymentOrigin.ExecuteAsync(userId);
                    if (origin.StripeCustomerId == null || origin.PaymentStatus == PaymentStatus.None)
                    {
                        // If the user doesn't have a stripe customer ID then they haven't given us
                        // credit card details and we can't bill them.
                        // If the user has manually topped up since we incremented the billing status,
                        // it will have been set back to None and we don't need to top up again.
                        continue;
                    }

                    // And apply the charge.
                    await this.applyUserCredit.ExecuteAsync(userId, PositiveInt.Parse(amountToCharge), null, UserType.StandardUser);
                    recalculateBalances = true;
                }
                catch (Exception t)
                {
                    errors.Add(new PaymentProcessingException(t, userId, null));
                }
            }

            return recalculateBalances;
        }
    }
}