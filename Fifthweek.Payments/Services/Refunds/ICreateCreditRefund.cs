namespace Fifthweek.Payments.Services.Refunds
{
    using System;
    using System.Threading.Tasks;

    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Payments.Shared;
    using Fifthweek.Shared;

    public interface ICreateCreditRefund
    {
        Task<UserId> ExecuteAsync(UserId enactingUserId, TransactionReference transactionReference, DateTime timestamp, PositiveInt refundCreditAmount, RefundCreditReason reason, string comment);
    }
}