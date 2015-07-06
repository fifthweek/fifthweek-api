namespace Fifthweek.Api.Payments.Commands
{
    using System;
    using System.Threading.Tasks;

    using Fifthweek.Api.Payments.Stripe;
    using Fifthweek.Api.Payments.Taxamo;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;

    [AutoConstructor]
    public partial class PerformCreditRequest : IPerformCreditRequest
    {
        private readonly ITimestampCreator timestampCreator;
        private readonly IPerformStripeCharge performStripeCharge;
        private readonly IGuidCreator guidCreator;

        public async Task<StripeTransactionResult> HandleAsync(
            ApplyCreditRequestCommand command,
            TaxamoTransactionResult taxamoTransaction,
            UserPaymentOriginResult origin)
        {
            command.AssertNotNull("command");
            taxamoTransaction.AssertNotNull("taxamoTransaction");
            origin.AssertNotNull("origin");

            var timestamp = this.timestampCreator.Now();
            var transactionReference = this.guidCreator.CreateSqlSequential();

            // Perform stripe transaction.
            var stripeChargeId = await this.performStripeCharge.ExecuteAsync(
                origin.StripeCustomerId,
                taxamoTransaction.TotalAmount,
                command.UserId,
                transactionReference,
                taxamoTransaction.Key);

            return new StripeTransactionResult(timestamp, transactionReference, stripeChargeId);
        }
    }
}