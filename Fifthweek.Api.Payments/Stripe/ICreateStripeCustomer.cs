namespace Fifthweek.Api.Payments.Stripe
{
    using System.Threading.Tasks;

    using Fifthweek.Api.Identity.Shared.Membership;

    public interface ICreateStripeCustomer
    {
        Task<string> ExecuteAsync(UserId userId, ValidStripeToken token);
    }
}