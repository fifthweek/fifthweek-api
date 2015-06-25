namespace Fifthweek.Payments.Services
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;

    [AutoConstructor]
    public partial class ProcessAllPayments : IProcessAllPayments
    {
        private readonly ITimestampCreator timestampCreator;
        private readonly IGetAllSubscribersDbStatement getAllSubscribers;
        private readonly IProcessPaymentsForSubscriber processPaymentsForSubscriber;
        private readonly IUpdateAccountBalancesDbStatement updateAccountBalances;

        public async Task ExecuteAsync(List<PaymentProcessingException> errors)
        {
            errors.AssertNotNull("errors");

            var subscriberIds = await this.getAllSubscribers.ExecuteAsync();

            // We use the same end time for all subscribers so that when we update
            // all account balances at the end the timestamp is accurate.
            var endTimeExclusive = this.timestampCreator.Now();

            foreach (var subscriberId in subscriberIds)
            {
                try
                {
                    await this.processPaymentsForSubscriber.ExecuteAsync(subscriberId, endTimeExclusive, errors);
                }
                catch (Exception t)
                {
                    errors.Add(new PaymentProcessingException(t, subscriberId, null));
                }
            }

            await this.updateAccountBalances.ExecuteAsync(null, endTimeExclusive);
        }
    }
}