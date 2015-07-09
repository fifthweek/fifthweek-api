namespace Fifthweek.Payments.Services
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Fifthweek.CodeGeneration;
    using Fifthweek.Payments.Services.Credit;
    using Fifthweek.Payments.Shared;
    using Fifthweek.Shared;

    [AutoConstructor]
    public partial class ProcessAllPayments : IProcessAllPayments
    {
        private readonly ITimestampCreator timestampCreator;
        private readonly IGetAllSubscribersDbStatement getAllSubscribers;
        private readonly IProcessPaymentsForSubscriber processPaymentsForSubscriber;
        private readonly IUpdateAccountBalancesDbStatement updateAccountBalances;
        private readonly ITopUpUserAccountsWithCredit topUpUserAccountsWithCredit;

        public async Task ExecuteAsync(IKeepAliveHandler keepAliveHandler, List<PaymentProcessingException> errors)
        {
            keepAliveHandler.AssertNotNull("keepAliveHandler");
            errors.AssertNotNull("errors");

            var subscriberIds = await this.getAllSubscribers.ExecuteAsync();

            // We use the same end time for all subscribers so that when we update
            // all account balances at the end the timestamp is accurate.
            var endTimeExclusive = this.timestampCreator.Now();

            foreach (var subscriberId in subscriberIds)
            {
                try
                {
                    await this.processPaymentsForSubscriber.ExecuteAsync(subscriberId, endTimeExclusive, keepAliveHandler, errors);
                }
                catch (Exception t)
                {
                    errors.Add(new PaymentProcessingException(t, subscriberId, null));
                }
            }

            var updatedBalances = await this.updateAccountBalances.ExecuteAsync(null, endTimeExclusive);

            bool recalculateBalances = await this.topUpUserAccountsWithCredit.ExecuteAsync(updatedBalances, errors);

            if (recalculateBalances)
            {
                await this.updateAccountBalances.ExecuteAsync(null, endTimeExclusive);
            }
        }
    }
}