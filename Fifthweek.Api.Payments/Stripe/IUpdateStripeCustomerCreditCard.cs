namespace Fifthweek.Api.Payments.Stripe
{
    using System.Threading.Tasks;

    public interface IUpdateStripeCustomerCreditCard
    {
        Task ExecuteAsync(string customerId, ValidStripeToken token);
    }
}