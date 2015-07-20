namespace Fifthweek.Payments.Services
{
    using System;
    using System.Threading.Tasks;

    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;

    [AutoConstructor]
    public partial class ProcessPaymentsBetweenSubscriberAndCreator : IProcessPaymentsBetweenSubscriberAndCreator
    {
        private readonly IGetPaymentProcessingData getPaymentProcessingData;
        private readonly IProcessPaymentProcessingData processPaymentProcessingData;
        private readonly IPersistPaymentProcessingResults persistPaymentProcessingResults;

        public async Task<CommittedAccountBalance> ExecuteAsync(UserId subscriberId, UserId creatorId, DateTime startTimeInclusive, DateTime endTimeExclusive, CommittedAccountBalance committedAccountBalance)
        {
            subscriberId.AssertNotNull("subscriberId");
            creatorId.AssertNotNull("creatorId");
            committedAccountBalance.AssertNotNull("committedAccountBalance");

            var paymentProcessingData = await this.getPaymentProcessingData.ExecuteAsync(
                subscriberId,
                creatorId,
                startTimeInclusive,
                endTimeExclusive,
                committedAccountBalance);

            var results = await this.processPaymentProcessingData.ExecuteAsync(paymentProcessingData);

            await this.persistPaymentProcessingResults.ExecuteAsync(paymentProcessingData, results);

            return results.CommittedAccountBalance;
        }
    }
}