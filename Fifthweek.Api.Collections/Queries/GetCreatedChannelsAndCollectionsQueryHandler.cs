namespace Fifthweek.Api.Collections.Queries
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Membership;
    using Fifthweek.CodeGeneration;

    [AutoConstructor]
    public partial class GetCreatedChannelsAndCollectionsQueryHandler : IQueryHandler<GetCreatedChannelsAndCollectionsQuery, ChannelsAndCollections>
    {
        private readonly IGetChannelsAndCollectionsDbStatement getChannelsAndCollections;

        public Task<ChannelsAndCollections> HandleAsync(GetCreatedChannelsAndCollectionsQuery query)
        {
            query.AssertNotNull("query");
            query.Requester.AssertAuthenticatedAs(query.RequestedCreatorId);

            return this.getChannelsAndCollections.ExecuteAsync(query.RequestedCreatorId);
        }
    }
}