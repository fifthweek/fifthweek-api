namespace Fifthweek.Payments.Services.Credit
{
    using System;
    using System.Threading.Tasks;

    using Fifthweek.Api.Identity.Shared.Membership;

    public interface ISaveCustomerCreditToLedgerDbStatement
    {
        Task ExecuteAsync(UserId userId, DateTime timestamp, AmountInMinorDenomination totalAmount, AmountInMinorDenomination creditAmount, Guid transactionReference, string stripeChargeId, string taxamoTransactionKey);
    }
}