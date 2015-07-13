namespace Fifthweek.Payments.Services.Credit.Stripe
{
    using System.Threading.Tasks;

    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Payments.Stripe;

    public interface IUpdateStripeCustomerCreditCard
    {
        Task ExecuteAsync(UserId userId, string customerId, string tokenId, UserType userType);
    }
}