namespace Fifthweek.Payments.Stripe
{
    using System.Threading.Tasks;

    using global::Stripe;

    public class StripeService : IStripeService
    {
        public async Task<StripeCustomer> CreateCustomerAsync(StripeCustomerCreateOptions options, string apiKey)
        {
            var customerService = new StripeCustomerService(apiKey);
            var customer = customerService.Create(options);
            return customer;
        }

        public async Task UpdateCustomerAsync(string customerId, StripeCustomerUpdateOptions options, string apiKey)
        {
            var customerService = new StripeCustomerService(apiKey);
            customerService.Update(customerId, options);
        }

        public async Task<StripeCard> CreateCardAsync(string customerId, StripeCardCreateOptions options, string apiKey)
        {
            var cardService = new StripeCardService(apiKey);
            var card = cardService.Create(customerId, options);
            return card;
        }

        public async Task<StripeCharge> CreateChargeAsync(StripeChargeCreateOptions options, string apiKey)
        {
            var chargeService = new StripeChargeService(apiKey);
            var charge = chargeService.Create(options);
            return charge;
        }

        public async Task<StripeRefund> RefundChargeAsync(string chargeId, StripeRefundCreateOptions options, string apiKey)
        {
            var refundService = new StripeRefundService(apiKey);
            var refund = refundService.Create(chargeId, options);
            return refund;
        }
    }
}