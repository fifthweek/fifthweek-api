namespace Fifthweek.Api.Payments
{
    using System.Threading.Tasks;

    using Fifthweek.Api.Identity.Shared.Membership;

    public interface IGetUserPaymentOriginDbStatement
    {
        Task<UserPaymentOriginResult> ExecuteAsync(UserId userId);
    }
}