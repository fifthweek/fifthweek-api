namespace Fifthweek.Api.Collections.Shared
{
    using System.Threading.Tasks;

    using Fifthweek.Api.Identity.Shared.Membership;

    public interface ICollectionSecurity
    {
        Task<bool> IsPostingAllowedAsync(UserId requester, CollectionId collectionId);

        Task AssertPostingAllowedAsync(UserId requester, CollectionId collectionId);
    }
}