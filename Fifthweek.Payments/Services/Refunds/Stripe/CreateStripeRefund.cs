namespace Fifthweek.Payments.Services.Refunds.Stripe
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
    public partial class CreateStripeRefund : ICreateStripeRefund
    {
        public const string DuplicateString = "duplicate";
        public const string FraudulentString = "fraudulent";
        public const string RequestedByCustomerString = "requested_by_customer";
        public const string EnactingUserIdMetadataKey = "enacting-user-id";

        private readonly IStripeApiKeyRepository apiKeyRepository;
        private readonly IStripeService stripeService;

        public async Task ExecuteAsync(
            UserId enactingUserId,
            string stripeChargeId, 
            PositiveInt totalRefundAmount,
            RefundCreditReason reason,
            UserType userType)
        {
            enactingUserId.AssertNotNull("enactingUserId");
            stripeChargeId.AssertNotNull("stripeChargeId");
            totalRefundAmount.AssertNotNull("totalRefundAmount");

            var apiKey = this.apiKeyRepository.GetApiKey(userType);

            var options = new StripeRefundCreateOptions
            {
                Amount = totalRefundAmount.Value,
                Reason = this.GetReason(reason),
                Metadata = new Dictionary<string, string>
                {
                    { EnactingUserIdMetadataKey, enactingUserId.ToString() },
                }
            };

            await this.stripeService.RefundChargeAsync(stripeChargeId, options, apiKey);
        }

        internal string GetReason(RefundCreditReason reason)
        {
            switch (reason)
            {
                case RefundCreditReason.Duplicate:
                    return DuplicateString;

                case RefundCreditReason.Fraudulent:
                    return FraudulentString;

                case RefundCreditReason.RequestedByCustomer:
                    return RequestedByCustomerString;
            }

            throw new InvalidOperationException("Unknown reason: " + reason);
        }
    }
}