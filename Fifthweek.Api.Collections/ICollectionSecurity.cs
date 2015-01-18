namespace Fifthweek.Api.Collections
{
    using System.Threading.Tasks;

    using Fifthweek.Api.Identity.Membership;

    public interface ICollectionSecurity
    {
        Task<bool> IsPostingAllowedAsync(UserId requester, CollectionId collectionId);

        Task AssertPostingAllowedAsync(UserId requester, CollectionId collectionId);
    }
}