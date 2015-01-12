using Fifthweek.Api.Core;
using Fifthweek.Api.Identity.Membership;

namespace Fifthweek.Api.Subscriptions.Queries
{
    [AutoConstructor, AutoEqualityMembers]
    public partial class GetCreatorStatusQuery : IQuery<CreatorStatus>
    {
        public UserId CreatorId { get; private set; }
    }
}