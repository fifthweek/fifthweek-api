using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Fifthweek.Api.Core;
using Fifthweek.Api.Persistence;

namespace Fifthweek.Api.Subscriptions.Queries
{
    [AutoConstructor]
    public partial class GetCreatorStatusQueryHandler : IQueryHandler<GetCreatorStatusQuery, CreatorStatus>
    {
        private readonly IFifthweekDbContext dbContext;

        public async Task<CreatorStatus> HandleAsync(GetCreatorStatusQuery query)
        {
            if (query == null)
            {
                throw new ArgumentNullException("query");
            }

            var creatorId = query.CreatorId.Value;
            var subscriptionId = await (from subscription in this.dbContext.Subscriptions
                                        where subscription.CreatorId == creatorId
                                        select subscription.Id)
                                        .FirstOrDefaultAsync();

            return subscriptionId == default(Guid) 
                ? new CreatorStatus(null, false) 
                : new CreatorStatus(new SubscriptionId(subscriptionId), false); // We're not storing posts yet, so this will always be false.
        }
    }
}