namespace Fifthweek.Payments.Services.Credit.Stripe
{
    using System.Threading.Tasks;

    public interface IUpdateStripeCustomerCreditCard
    {
        Task ExecuteAsync(string customerId, string token);
    }
}