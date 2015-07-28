namespace Fifthweek.Payments.Services.Refunds
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Payments;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Payments.Shared;
    using Fifthweek.Shared;

    [AutoConstructor]
    public partial class CreateCreditRefundDbStatement : ICreateCreditRefundDbStatement
    {
        private readonly IGuidCreator guidCreator;
        private readonly IFifthweekDbConnectionFactory connectionFactory;

        public async Task ExecuteAsync(
            UserId administratorId,
            UserId userId,
            DateTime timestamp,
            PositiveInt totalAmount, 
            PositiveInt creditAmount,
            TransactionReference transactionReference,
            string stripeChargeId, 
            string taxamoTransactionKey,
            string comment)
        {
            administratorId.AssertNotNull("administratorId");
            userId.AssertNotNull("userId");
            totalAmount.AssertNotNull("totalAmount");
            creditAmount.AssertNotNull("creditAmount");
            stripeChargeId.AssertNotNull("stripeChargeId");
            taxamoTransactionKey.AssertNotNull("taxamoTransactionKey");
            comment.AssertNotNull("comment");

            if (totalAmount.Value < creditAmount.Value)
            {
                throw new InvalidOperationException(string.Format(
                    "The total charged amount ({0}) cannot be less than the amount credited ({1}).",
                    totalAmount.Value,
                    creditAmount.Value));
            }

            var formattedComment = string.Format("{0} (Performed By {1})", comment, administratorId);
            
            var committedRecords = new List<AppendOnlyLedgerRecord>();

            committedRecords.Add(new AppendOnlyLedgerRecord(
                this.guidCreator.CreateSqlSequential(),
                userId.Value,
                null,
                timestamp,
                totalAmount.Value,
                LedgerAccountType.Stripe,
                LedgerTransactionType.CreditRefund,
                transactionReference.Value,
                null,
                formattedComment,
                stripeChargeId,
                taxamoTransactionKey));

            committedRecords.Add(new AppendOnlyLedgerRecord(
                this.guidCreator.CreateSqlSequential(),
                userId.Value,
                null,
                timestamp,
                -creditAmount.Value,
                LedgerAccountType.FifthweekCredit,
                LedgerTransactionType.CreditRefund,
                transactionReference.Value,
                null,
                formattedComment,
                stripeChargeId,
                taxamoTransactionKey));

            var tax = totalAmount.Value - creditAmount.Value;

            if (tax > 0)
            {
                committedRecords.Add(new AppendOnlyLedgerRecord(
                    this.guidCreator.CreateSqlSequential(),
                    userId.Value,
                    null,
                    timestamp,
                    -tax,
                    LedgerAccountType.SalesTax,
                    LedgerTransactionType.CreditRefund,
                    transactionReference.Value,
                    null,
                    formattedComment,
                    stripeChargeId,
                    taxamoTransactionKey));
            }

            using (var connection = this.connectionFactory.CreateConnection())
            {
                await connection.InsertAsync(committedRecords);
            }
        }
    }
}