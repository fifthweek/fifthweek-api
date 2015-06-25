namespace Fifthweek.Payments.Services
{
    using System;
    using System.Threading.Tasks;

    using Fifthweek.Api.Identity.Shared.Membership;

    public interface IGetPaymentProcessingData
    {
        Task<PaymentProcessingData> ExecuteAsync(UserId subscriberId, UserId creatorId, DateTime startTimeInclusive, DateTime endTimeExclusive);
    }
}