namespace Fifthweek.Payments.Services.Credit.Stripe
{
    using System.Threading.Tasks;

    public class UpdateStripeCustomerCreditCard : IUpdateStripeCustomerCreditCard
    {
        //http://stackoverflow.com/questions/20065939/change-credit-card-information-stripe

        public async Task ExecuteAsync(string customerId, string token)
        {

        }
    }
}