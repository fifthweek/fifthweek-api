namespace Fifthweek.Api.Collections
{
    using System.Threading.Tasks;

    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;

    [AutoConstructor]
    public partial class CollectionSecurity : ICollectionSecurity
    {
        private readonly ICollectionOwnership collectionOwnership;

        public Task<bool> IsWriteAllowedAsync(UserId requester, Shared.CollectionId collectionId)
        {
            requester.AssertNotNull("requester");
            collectionId.AssertNotNull("collectionId");

            return this.collectionOwnership.IsOwnerAsync(requester, collectionId);
        }

        public async Task AssertWriteAllowedAsync(UserId requester, Shared.CollectionId collectionId)
        {
            requester.AssertNotNull("requester");
            collectionId.AssertNotNull("collectionId");

            var isWriteAllowed = await this.IsWriteAllowedAsync(requester, collectionId);
            if (!isWriteAllowed)
            {
                throw new UnauthorizedException("Not allowed to write to collection. {0} {1}", requester, collectionId);
            }
        }
    }
}