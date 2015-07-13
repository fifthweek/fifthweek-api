namespace Fifthweek.Payments.Services.Credit.Stripe
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Payments.Stripe;
    using Fifthweek.Shared;

    using global::Stripe;

    [AutoConstructor]
    public partial class PerformStripeCharge : IPerformStripeCharge
    {
        public const string UserIdMetadataKey = "user-id";
        public const string TransactionReferenceMetadataKey = "transaction-reference";
        public const string TaxamoTransactionKeyMetadataKey = "taxamo-transaction-key";
        public const string Currency = "USD";

        //https://stripe.com/docs/tutorials/charges
        private readonly IStripeApiKeyRepository apiKeyRepository;
        private readonly IStripeService stripeService;

        public async Task<string> ExecuteAsync(
            string stripeCustomerId, 
            AmountInUsCents amount,
            UserId userId,
            Guid transactionReference,
            string taxamoTransactionKey, 
            UserType userType)
        {
            stripeCustomerId.AssertNotNull("stripeCustomerId");
            amount.AssertNotNull("amount");
            userId.AssertNotNull("userId");

            var apiKey = this.apiKeyRepository.GetApiKey(userType);
        
            var options = new StripeChargeCreateOptions
            {
                Amount = amount.Value,
                Currency = Currency,
                CustomerId = stripeCustomerId,
                Metadata = new Dictionary<string, string>
                {
                    { TransactionReferenceMetadataKey, transactionReference.ToString() },
                    { TaxamoTransactionKeyMetadataKey, taxamoTransactionKey },
                    { UserIdMetadataKey, userId.ToString() },
                }
            };

            try
            {
                var result = await this.stripeService.CreateCharge(options, apiKey);
                return result.Id;
            }
            catch (Exception t)
            {
                throw new StripeChargeFailedException(t);
            }
        }
    }
}