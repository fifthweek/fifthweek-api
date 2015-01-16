using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Fifthweek.Api.Core;
using Fifthweek.Api.Persistence;

namespace Fifthweek.Api.Subscriptions.Queries
{
    using Fifthweek.Api.Identity.Membership;
    using Fifthweek.Shared;

    [AutoConstructor]
    public partial class GetCreatorStatusQueryHandler : IQueryHandler<GetCreatorStatusQuery, CreatorStatus>
    {
        private readonly IFifthweekDbContext dbContext;

        public async Task<CreatorStatus> HandleAsync(GetCreatorStatusQuery query)
        {
            query.AssertNotNull("query");
            query.CreatorId.AssertAuthenticated();

            // Ensure we return the same subscription each time by ordering by creation date descending. This means the latest
            // subscription is returned. Latest seems to make most sense: if a user double-posts, they'll get the ID of the 
            // latest subscription, which they'll probably then start filling out. It also provides us with a mechanism for 
            // overriding / soft deleting subscriptions by inserting a new subscription record (not saying that's the solution 
            // in that case, but its another option enabled by the decision to sort descending!).
            var creatorId = query.CreatorId.Value;
            var subscriptionId = await (from subscription in this.dbContext.Subscriptions
                                        where subscription.CreatorId == creatorId
                                        orderby subscription.CreationDate descending 
                                        select subscription.Id)
                                        .FirstOrDefaultAsync();

            return subscriptionId == default(Guid) 
                ? CreatorStatus.NoSubscriptions
                : new CreatorStatus(new SubscriptionId(subscriptionId), true); // We're not storing posts yet, so this will always be true.
        }
    }
}