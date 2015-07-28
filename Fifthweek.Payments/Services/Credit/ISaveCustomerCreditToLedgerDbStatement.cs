namespace Fifthweek.Payments.Services.Credit
{
    using System;
    using System.Threading.Tasks;

    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Payments.Shared;

    public interface ISaveCustomerCreditToLedgerDbStatement
    {
        Task ExecuteAsync(UserId userId, DateTime timestamp, AmountInMinorDenomination totalAmount, AmountInMinorDenomination creditAmount, TransactionReference transactionReference, string stripeChargeId, string taxamoTransactionKey);
    }
}