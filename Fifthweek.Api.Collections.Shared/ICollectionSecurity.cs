namespace Fifthweek.Api.Collections.Shared
{
    using System.Threading.Tasks;

    using Fifthweek.Api.Identity.Shared.Membership;

    public interface ICollectionSecurity
    {
        Task<bool> IsWriteAllowedAsync(UserId requester, CollectionId collectionId);

        Task AssertWriteAllowedAsync(UserId requester, CollectionId collectionId);
    }
}