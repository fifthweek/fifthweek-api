namespace Fifthweek.Payments.Services.Refunds
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Dapper;

    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Payments;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Payments.Shared;
    using Fifthweek.Shared;

    [AutoConstructor]
    public partial class GetCreditTransactionInformation : IGetCreditTransactionInformation
    {
        private readonly IGetRecordsForTransactionDbStatement getRecordsForTransaction;

        public async Task<GetCreditTransactionResult> ExecuteAsync(TransactionReference transactionReference)
        {
            transactionReference.AssertNotNull("transactionReference");

            var records = await this.getRecordsForTransaction.ExecuteAsync(transactionReference);

            if (records.Count == 0)
            {
                return null;
            }

            if (records.All(v => v.TransactionType != LedgerTransactionType.CreditAddition))
            {
                throw new InvalidOperationException(
                    "Transaction reference " + transactionReference + " was not a CreditAddition record.");
            }

            var firstRecord = records.First();

            var userId = firstRecord.AccountOwnerId;
            var stripeChargeId = firstRecord.StripeChargeId;
            var taxamoTransactionKey = firstRecord.TaxamoTransactionKey;

            var totalCreditAmount = records.First(
                v => v.TransactionType == LedgerTransactionType.CreditAddition
                        && v.AccountType == LedgerAccountType.FifthweekCredit).Amount;

            var existingRefundAmounts = records.Where(
                v => v.TransactionType == LedgerTransactionType.CreditRefund
                        && v.AccountType == LedgerAccountType.FifthweekCredit);

            var creditAmountAvailableForRefund = totalCreditAmount + existingRefundAmounts.Sum(v => v.Amount);

            return new GetCreditTransactionResult(
                new UserId(userId),
                stripeChargeId,
                taxamoTransactionKey,
                totalCreditAmount,
                creditAmountAvailableForRefund);
        }

        [AutoConstructor, AutoEqualityMembers]
        public partial class GetCreditTransactionResult
        {
            public UserId UserId { get; private set; }

            public string StripeChargeId { get; private set; }

            public string TaxamoTransactionKey { get; private set; }

            public decimal TotalCreditAmount { get; private set; }
            
            public decimal CreditAmountAvailableForRefund { get; private set; }
        }
    }
}