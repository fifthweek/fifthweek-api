namespace Fifthweek.Api.Collections
{
    using System.Threading.Tasks;

    using Fifthweek.Api.Identity.Membership;

    public interface ICollectionOwnership
    {
        Task<bool> IsOwnerAsync(UserId userId, CollectionId collectionId);
    }
}