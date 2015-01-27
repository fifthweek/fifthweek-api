namespace Fifthweek.Api.Collections
{
    using System.Threading.Tasks;

    using Fifthweek.Api.Identity.Shared.Membership;

    public interface ICollectionOwnership
    {
        Task<bool> IsOwnerAsync(UserId userId, Shared.CollectionId collectionId);
    }
}