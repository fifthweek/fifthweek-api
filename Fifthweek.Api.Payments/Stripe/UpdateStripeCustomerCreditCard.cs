namespace Fifthweek.Api.Payments.Stripe
{
    using System.Threading.Tasks;

    using Fifthweek.Api.Identity.Shared.Membership;

    public class UpdateStripeCustomerCreditCard : IUpdateStripeCustomerCreditCard
    {
        //http://stackoverflow.com/questions/20065939/change-credit-card-information-stripe

        public async Task ExecuteAsync(string customerId, ValidStripeToken token)
        {

        }
    }
}