namespace Fifthweek.Payments.Services.Credit.Stripe
{
    using System;
    using System.Threading.Tasks;

    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Payments.Stripe;
    using Fifthweek.Shared;

    using global::Stripe;

    [AutoConstructor]
    public partial class CreateStripeCustomer : ICreateStripeCustomer
    {
        // https://stripe.com/docs/tutorials/charges#saving-credit-card-details-for-later
        // https://support.stripe.com/questions/can-i-save-a-card-and-charge-it-later
        private readonly IStripeApiKeyRepository apiKeyRepository;
        private readonly IStripeService stripeService;

        public async Task<string> ExecuteAsync(UserId userId, string tokenId, UserType userType)
        {
            userId.AssertNotNull("userId");
            tokenId.AssertNotNull("tokenId");

            var apiKey = this.apiKeyRepository.GetApiKey(userType);

            var options = new StripeCustomerCreateOptions
            {
                Description = userId.ToString(),
                Source = new StripeSourceOptions
                {
                    TokenId = tokenId,
                }
            };

            var customer = await this.stripeService.CreateCustomerAsync(options, apiKey);

            return customer.Id;
        }
    }
}