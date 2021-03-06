namespace Fifthweek.Payments.Services
{
    using System;
    using System.Threading.Tasks;

    using Fifthweek.Api.Identity.Shared.Membership;

    public interface IProcessPaymentsBetweenSubscriberAndCreator
    {
        Task<CommittedAccountBalance> ExecuteAsync(UserId subscriberId, UserId creatorId, DateTime startTimeInclusive, DateTime endTimeExclusive, CommittedAccountBalance committedAccountBalance);
    }
}