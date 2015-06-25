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
        
        public async Task ExecuteAsync(UserId subscriberId, UserId creatorId, DateTime startTimeInclusive, DateTime endTimeExclusive)
        {
            subscriberId.AssertNotNull("subscriberId");
            creatorId.AssertNotNull("creatorId");

            var paymentProcessingData = await this.getPaymentProcessingData.ExecuteAsync(
                subscriberId,
                creatorId,
                startTimeInclusive,
                endTimeExclusive);

            var results = await this.processPaymentProcessingData.ExecuteAsync(paymentProcessingData);

            await this.persistPaymentProcessingResults.ExecuteAsync(paymentProcessingData, results);
        }
    }
}