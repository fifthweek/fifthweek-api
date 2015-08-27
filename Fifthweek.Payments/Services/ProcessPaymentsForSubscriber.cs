namespace Fifthweek.Payments.Services
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Azure;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Payments.Shared;
    using Fifthweek.Shared;
    using Fifthweek.WebJobs.Shared;

    [AutoConstructor]
    public partial class ProcessPaymentsForSubscriber : IProcessPaymentsForSubscriber
    {
        public static readonly TimeSpan MinimumProcessingPeriod = TimeSpan.FromSeconds(5);

        private readonly IGetCreatorsAndFirstSubscribedDatesDbStatement getCreatorsAndFirstSubscribedDates;
        private readonly IGetCommittedAccountBalanceDbStatement getCommittedAccountBalanceDbStatement;
        private readonly IProcessPaymentsBetweenSubscriberAndCreator processPaymentsBetweenSubscriberAndCreator;
        private readonly IGetLatestCommittedLedgerDateDbStatement getLatestCommittedLedgerDate;

        public async Task ExecuteAsync(UserId subscriberId, DateTime endTimeExclusive, IKeepAliveHandler keepAliveHandler, List<PaymentProcessingException> errors)
        {
            keepAliveHandler.AssertNotNull("keepAliveHandler");
            subscriberId.AssertNotNull("subscriberId");
            errors.AssertNotNull("errors");

            var creators = await this.getCreatorsAndFirstSubscribedDates.ExecuteAsync(subscriberId);

            var committedAccountBalanceValue = await this.getCommittedAccountBalanceDbStatement.ExecuteAsync(subscriberId);

            if (committedAccountBalanceValue < 0)
            {
                errors.Add(new PaymentProcessingException(string.Format("Committed account balance was {0} for user {1}.", committedAccountBalanceValue, subscriberId), subscriberId, null));
                committedAccountBalanceValue = 0m;
            }

            var committedAccountBalance = new CommittedAccountBalance(committedAccountBalanceValue);

            foreach (var creator in creators)
            {
                try
                {
                    await keepAliveHandler.KeepAliveAsync();

                    var latestCommittedLedgerDate = await this.getLatestCommittedLedgerDate.ExecuteAsync(subscriberId, creator.CreatorId);

                    var startTimeInclusive = latestCommittedLedgerDate ?? PaymentProcessingUtilities.GetPaymentProcessingStartDate(creator.FirstSubscribedDate);

                    if ((endTimeExclusive - startTimeInclusive) <= MinimumProcessingPeriod)
                    {
                        continue;
                    }

                    committedAccountBalance = await this.processPaymentsBetweenSubscriberAndCreator.ExecuteAsync(
                        subscriberId,
                        creator.CreatorId,
                        startTimeInclusive,
                        endTimeExclusive,
                        committedAccountBalance);
                }
                catch (Exception t)
                {
                    errors.Add(new PaymentProcessingException(t, subscriberId, creator.CreatorId));
                }
            }
        }
    }
}