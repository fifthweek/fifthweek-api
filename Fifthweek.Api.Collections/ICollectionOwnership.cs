namespace Fifthweek.Api.Collections
{
    using System.Threading.Tasks;

    using Fifthweek.Api.Identity.Membership;

    using UserId = Fifthweek.Api.Identity.Shared.Membership.UserId;

    public interface ICollectionOwnership
    {
        Task<bool> IsOwnerAsync(UserId userId, CollectionId collectionId);
    }
}