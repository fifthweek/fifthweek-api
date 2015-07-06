namespace Fifthweek.Api.Payments
{
    using System;
    using System.Threading.Tasks;

    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Shared;

    public interface ISaveCustomerCreditToLedgerDbStatement
    {
        Task ExecuteAsync(UserId userId, DateTime timestamp, AmountInUsCents totalAmount, AmountInUsCents creditAmount, Guid transactionReference, string stripeChargeId, string taxamoTransactionKey);
    }
}