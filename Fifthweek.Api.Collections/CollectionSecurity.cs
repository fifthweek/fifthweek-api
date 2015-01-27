namespace Fifthweek.Api.Collections
{
    using System.Threading.Tasks;

    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Membership;
    using Fifthweek.CodeGeneration;

    using UserId = Fifthweek.Api.Identity.Shared.Membership.UserId;

    [AutoConstructor]
    public partial class CollectionSecurity : ICollectionSecurity
    {
        private readonly ICollectionOwnership collectionOwnership;

        public Task<bool> IsPostingAllowedAsync(UserId requester, CollectionId collectionId)
        {
            requester.AssertNotNull("requester");
            collectionId.AssertNotNull("collectionId");

            return this.collectionOwnership.IsOwnerAsync(requester, collectionId);
        }

        public async Task AssertPostingAllowedAsync(UserId requester, CollectionId collectionId)
        {
            requester.AssertNotNull("requester");
            collectionId.AssertNotNull("collectionId");

            var isPostingAllowed = await this.IsPostingAllowedAsync(requester, collectionId);
            if (!isPostingAllowed)
            {
                throw new UnauthorizedException(string.Format("Not allowed to post to collection. {0} {1}", requester, collectionId));
            }
        }
    }
}