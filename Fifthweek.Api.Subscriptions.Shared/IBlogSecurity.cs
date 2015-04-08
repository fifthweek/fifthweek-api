namespace Fifthweek.Api.Subscriptions.Shared
{
    using System.Threading.Tasks;

    using Fifthweek.Api.Identity.Shared.Membership;

    public interface IBlogSecurity
    {
        Task<bool> IsCreationAllowedAsync(Requester requester);

        Task<bool> IsWriteAllowedAsync(UserId requester, BlogId blogId);

        Task AssertCreationAllowedAsync(Requester requester);

        Task AssertWriteAllowedAsync(UserId requester, BlogId blogId);
    }
}