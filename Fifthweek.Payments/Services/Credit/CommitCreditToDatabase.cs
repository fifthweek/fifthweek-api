namespace Fifthweek.Payments.Services.Credit
{
    using System.Threading.Tasks;

    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Payments.Services;
    using Fifthweek.Payments.Services.Credit.Taxamo;
    using Fifthweek.Shared;

    [AutoConstructor]
    public partial class CommitCreditToDatabase : ICommitCreditToDatabase
    {
        private readonly IUpdateAccountBalancesDbStatement updateAccountBalances;
        private readonly ISetUserPaymentOriginOriginalTaxamoTransactionKeyDbStatement setUserPaymentOriginOriginalTaxamoTransactionKey;
        private readonly ISaveCustomerCreditToLedgerDbStatement saveCustomerCreditToLedger;
        private readonly IClearBillingStatusDbStatement clearBillingStatus;

        public async Task HandleAsync(
            UserId userId, 
            TaxamoTransactionResult taxamoTransaction, 
            UserPaymentOriginResult origin, 
            StripeTransactionResult stripeTransaction)
        {
            userId.AssertNotNull("userId");
            taxamoTransaction.AssertNotNull("taxamoTransaction");
            origin.AssertNotNull("origin");
            stripeTransaction.AssertNotNull("stripeTransaction");

            // Persist credit to ledger.
            await this.saveCustomerCreditToLedger.ExecuteAsync(
                userId,
                stripeTransaction.Timestamp,
                taxamoTransaction.TotalAmount,
                taxamoTransaction.Amount,
                stripeTransaction.TransactionReference,
                stripeTransaction.StripeChargeId,
                taxamoTransaction.Key);

            // Clear the billing status, in case it a previous billing attempt failed.
            await this.clearBillingStatus.ExecuteAsync(userId);

            // Store original taxamo transaction key if not already stored.
            if (origin.OriginalTaxamoTransactionKey == null)
            {
                await this.setUserPaymentOriginOriginalTaxamoTransactionKey.ExecuteAsync(userId, taxamoTransaction.Key);
            }

            // Update account balance.
            await this.updateAccountBalances.ExecuteAsync(userId, stripeTransaction.Timestamp);
        }
    }
}