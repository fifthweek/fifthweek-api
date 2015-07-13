namespace Fifthweek.Payments.Services.Credit.Stripe
{
    using System.Threading.Tasks;

    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Payments.Stripe;
    using Fifthweek.Shared;

    using global::Stripe;

    [AutoConstructor]
    public partial class UpdateStripeCustomerCreditCard : IUpdateStripeCustomerCreditCard
    {
        //http://stackoverflow.com/questions/20065939/change-credit-card-information-stripe
        private readonly IStripeApiKeyRepository apiKeyRepository;
        private readonly IStripeService stripeService;

        public async Task ExecuteAsync(UserId userId, string customerId, string tokenId, UserType userType)
        {
            userId.AssertNotNull("userId");
            customerId.AssertNotNull("customerId");
            tokenId.AssertNotNull("tokenId");

            var apiKey = this.apiKeyRepository.GetApiKey(userType);

            var cardCreateOptions = new StripeCardCreateOptions { Source = new StripeSourceOptions { TokenId = tokenId, } };
            var card = await this.stripeService.CreateCard(customerId, cardCreateOptions, apiKey);
            
            var customerUpdateOptions = new StripeCustomerUpdateOptions { DefaultSource = card.Id };
            await this.stripeService.UpdateCustomer(customerId, customerUpdateOptions, apiKey);
        }
    }
}