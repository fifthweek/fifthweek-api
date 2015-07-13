namespace Fifthweek.Payments.Stripe
{
    using System.Threading.Tasks;

    using global::Stripe;

    public interface IStripeService
    {
        Task<StripeCustomer> CreateCustomer(StripeCustomerCreateOptions options, string apiKey);

        Task UpdateCustomer(string customerId, StripeCustomerUpdateOptions options, string apiKey);

        Task<StripeCard> CreateCard(string customerId, StripeCardCreateOptions options, string apiKey);

        Task<StripeCharge> CreateCharge(StripeChargeCreateOptions options, string apiKey);
    }
}