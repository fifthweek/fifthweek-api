namespace Fifthweek.Payments.Services.Refunds
{
    using System;
    using System.Threading.Tasks;

    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Payments.Shared;

    public interface ICreateTransactionRefund
    {
        Task<CreateTransactionRefund.CreateTransactionRefundResult> ExecuteAsync(
            UserId enactingUserId,
            TransactionReference transactionReference,
            DateTime timestamp,
            string comment);
    }
}