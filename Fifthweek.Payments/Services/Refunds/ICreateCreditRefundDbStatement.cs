namespace Fifthweek.Payments.Services.Refunds
{
    using System;
    using System.Threading.Tasks;

    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Payments.Shared;
    using Fifthweek.Shared;

    public interface ICreateCreditRefundDbStatement
    {
        Task ExecuteAsync(
            UserId administratorId,
            UserId userId,
            DateTime timestamp,
            PositiveInt totalAmount,
            PositiveInt creditAmount,
            TransactionReference transactionReference,
            string stripeChargeId, 
            string taxamoTransactionKey,
            string comment);
    }
}