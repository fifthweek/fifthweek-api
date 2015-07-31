namespace Fifthweek.Api.Identity.Membership
{
    using System.Threading.Tasks;

    using Fifthweek.Api.Identity.Shared.Membership;

    public interface IImpersonateIfRequired
    {
        Task<Requester> ExecuteAsync(Requester requester, UserId requestedUserId);
    }
}