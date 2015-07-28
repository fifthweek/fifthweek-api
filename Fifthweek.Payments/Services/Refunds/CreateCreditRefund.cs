namespace Fifthweek.Payments.Services.Refunds
{
    using System;
    using System.Threading.Tasks;

    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Payments.Services.Refunds.Stripe;
    using Fifthweek.Payments.Services.Refunds.Taxamo;
    using Fifthweek.Payments.Shared;
    using Fifthweek.Shared;

    using Newtonsoft.Json;

    [AutoConstructor]
    public partial class CreateCreditRefund : ICreateCreditRefund
    {
        private readonly IFifthweekRetryOnTransientErrorHandler retryOnTransientFailure;
        private readonly IGetCreditTransactionDbStatement getCreditTransaction;
        private readonly ICreateTaxamoRefund createTaxamoRefund;
        private readonly ICreateStripeRefund createStripeRefund;
        private readonly ICreateCreditRefundDbStatement createCreditRefundDbStatement;

        public async Task<UserId> ExecuteAsync(
            UserId enactingUserId, 
            TransactionReference transactionReference, 
            DateTime timestamp,
            PositiveInt refundCreditAmount,
            RefundCreditReason reason, 
            string comment)
        {
            enactingUserId.AssertNotNull("enactingUserId");
            transactionReference.AssertNotNull("transactionReference");
            refundCreditAmount.AssertNotNull("refundCreditAmount");

            var transaction = await this.retryOnTransientFailure.HandleAsync(
                () => this.getCreditTransaction.ExecuteAsync(transactionReference));

            if (transaction == null)
            {
                throw new BadRequestException(
                    "Transaction reference " + transactionReference + " was not a valid transaction, or the user is not a standard user.");
            }

            if (refundCreditAmount.Value > transaction.CreditAmountAvailableForRefund)
            {
                throw new BadRequestException(
                    "Requested refund amount was greater than amount available for refund.");
            }

            var userId = transaction.UserId;

            // Refund on taxamo, get tax refund amount.
            var taxamoResult = await this.retryOnTransientFailure.HandleAsync(
                () => this.createTaxamoRefund.ExecuteAsync(transaction.TaxamoTransactionKey, refundCreditAmount, UserType.StandardUser));

            try
            {
                // Call CreateCreditRefundDbStatement
                await this.retryOnTransientFailure.HandleAsync(
                    () => this.createCreditRefundDbStatement.ExecuteAsync(
                    enactingUserId,
                    userId,
                    timestamp,
                    taxamoResult.TotalRefundAmount,
                    refundCreditAmount,
                    transactionReference,
                    transaction.StripeChargeId,
                    transaction.TaxamoTransactionKey,
                    comment));

                // refund on stripe.
                await this.retryOnTransientFailure.HandleAsync(
                    () => this.createStripeRefund.ExecuteAsync(enactingUserId, transaction.StripeChargeId, taxamoResult.TotalRefundAmount, reason, UserType.StandardUser));
            }
            catch (Exception t)
            {
                var json = JsonConvert.SerializeObject(
                    new
                    {
                        TransactionReference = transactionReference,
                        Transaction = transaction,
                        TaxamoResult = taxamoResult,
                    });

                throw new FailedToRefundCreditException(json, t);
            }

            return userId;
        }
    }
}