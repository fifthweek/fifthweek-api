namespace Fifthweek.Payments.Stripe
{
    using System.Threading.Tasks;

    using global::Stripe;

    public interface IStripeService
    {
        Task<StripeCustomer> CreateCustomerAsync(StripeCustomerCreateOptions options, string apiKey);

        Task UpdateCustomerAsync(string customerId, StripeCustomerUpdateOptions options, string apiKey);

        Task<StripeCard> CreateCardAsync(string customerId, StripeCardCreateOptions options, string apiKey);

        Task<StripeCharge> CreateChargeAsync(StripeChargeCreateOptions options, string apiKey);

        Task<StripeRefund> RefundChargeAsync(string chargeId, StripeRefundCreateOptions options, string apiKey);
    }
}