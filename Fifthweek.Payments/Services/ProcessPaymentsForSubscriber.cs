namespace Fifthweek.Payments.Services
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;

    [AutoConstructor]
    public partial class ProcessPaymentsForSubscriber : IProcessPaymentsForSubscriber
    {
        public static readonly TimeSpan MinimumProcessingPeriod = TimeSpan.FromSeconds(5);

        private readonly IGetCreatorsAndFirstSubscribedDatesDbStatement getCreatorsAndFirstSubscribedDates;
        private readonly IProcessPaymentsBetweenSubscriberAndCreator processPaymentsBetweenSubscriberAndCreator;
        private readonly IGetLatestCommittedLedgerDateDbStatement getLatestCommittedLedgerDate;

        public async Task ExecuteAsync(UserId subscriberId, DateTime endTimeExclusive, List<PaymentProcessingException> errors)
        {
            subscriberId.AssertNotNull("subscriberId");
            errors.AssertNotNull("errors");

            var creators = await this.getCreatorsAndFirstSubscribedDates.ExecuteAsync(subscriberId);

            foreach (var creator in creators)
            {
                try
                {
                    var latestCommittedLedgerDate = await this.getLatestCommittedLedgerDate.ExecuteAsync(subscriberId, creator.CreatorId);

                    var startTimeInclusive = latestCommittedLedgerDate ?? this.GetPaymentProcessingStartDate(creator.FirstSubscribedDate);

                    if ((endTimeExclusive - startTimeInclusive) <= MinimumProcessingPeriod)
                    {
                        continue;
                    }

                    await this.processPaymentsBetweenSubscriberAndCreator.ExecuteAsync(
                        subscriberId,
                        creator.CreatorId,
                        startTimeInclusive,
                        endTimeExclusive);
                }
                catch (Exception t)
                {
                    errors.Add(new PaymentProcessingException(t, subscriberId, creator.CreatorId));
                }
            }
        }

        /// <summary>
        /// We always start the ledger entries on the same date, which doesn't necessarily correspond with
        /// the subscriber's billing weeks for individual channels, because it allows us to display billing
        /// information in a sensible way... e.g. "what you spent this week".
        /// </summary>
        internal DateTime GetPaymentProcessingStartDate(DateTime input)
        {
            var ticksInWeek = TimeSpan.FromDays(7).Ticks;
            return new DateTime(input.Ticks - (input.Ticks % ticksInWeek), DateTimeKind.Utc);
        }
    }
}