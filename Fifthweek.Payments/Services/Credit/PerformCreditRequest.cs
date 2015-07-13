namespace Fifthweek.Payments.Services.Credit
{
    using System.Threading.Tasks;

    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Payments.Services.Credit.Stripe;
    using Fifthweek.Payments.Services.Credit.Taxamo;
    using Fifthweek.Payments.Stripe;
    using Fifthweek.Shared;

    [AutoConstructor]
    public partial class PerformCreditRequest : IPerformCreditRequest
    {
        private readonly ITimestampCreator timestampCreator;
        private readonly IPerformStripeCharge performStripeCharge;
        private readonly IGuidCreator guidCreator;

        public async Task<StripeTransactionResult> HandleAsync(
            UserId userId,
            TaxamoTransactionResult taxamoTransaction,
            UserPaymentOriginResult origin,
            UserType userType)
        {
            userId.AssertNotNull("userId");
            taxamoTransaction.AssertNotNull("taxamoTransaction");
            origin.AssertNotNull("origin");

            var timestamp = this.timestampCreator.Now();
            var transactionReference = this.guidCreator.CreateSqlSequential();

            // Perform stripe transaction.
            var stripeChargeId = await this.performStripeCharge.ExecuteAsync(
                origin.StripeCustomerId,
                taxamoTransaction.TotalAmount,
                userId,
                transactionReference,
                taxamoTransaction.Key,
                userType);

            return new StripeTransactionResult(timestamp, transactionReference, stripeChargeId);
        }
    }
}