namespace Fifthweek.Payments.Services.Credit.Stripe
{
    using System.Threading.Tasks;

    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Payments.Stripe;

    public interface ICreateStripeCustomer
    {
        Task<string> ExecuteAsync(UserId userId, string tokenId, UserType userType);
    }
}