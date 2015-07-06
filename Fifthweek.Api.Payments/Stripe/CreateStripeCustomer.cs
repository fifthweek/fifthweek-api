namespace Fifthweek.Api.Payments.Stripe
{
    using System;
    using System.Threading.Tasks;

    using Fifthweek.Api.Identity.Shared.Membership;

    public class CreateStripeCustomer : ICreateStripeCustomer
    {
        //https://stripe.com/docs/tutorials/charges#saving-credit-card-details-for-later
        //https://support.stripe.com/questions/can-i-save-a-card-and-charge-it-later


        public async Task<string> ExecuteAsync(UserId userId, ValidStripeToken token)
        {
            throw new NotImplementedException();
        }
    }
}