namespace Fifthweek.Api.Aggregations.Queries
{
    using System.Threading.Tasks;

    using Fifthweek.Api.Blogs;
    using Fifthweek.Api.Blogs.Queries;
    using Fifthweek.Api.Collections.Queries;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement.Queries;
    using Fifthweek.Api.Identity.Membership;
    using Fifthweek.Api.Identity.Membership.Queries;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;

    [AutoConstructor]
    [Decorator(OmitDefaultDecorators = true)]
    public partial class GetUserStateQueryHandler : IQueryHandler<GetUserStateQuery, UserState>
    {
        private readonly IRequesterSecurity requesterSecurity;

        private readonly IQueryHandler<GetUserAccessSignaturesQuery, UserAccessSignatures> getUserAccessSignatures;
        private readonly IQueryHandler<GetCreatorStatusQuery, CreatorStatus> getCreatorStatus;
        private readonly IQueryHandler<GetCreatedChannelsAndCollectionsQuery, ChannelsAndCollections> getCreatedChannelsAndCollections;
        private readonly IQueryHandler<GetAccountSettingsQuery, GetAccountSettingsResult> getAccountSettings;
        private readonly IQueryHandler<GetBlogQuery, GetBlogResult> getBlog;
        private readonly IQueryHandler<GetUserSubscriptionsQuery, GetUserSubscriptionsResult> getBlogSubscriptions;

        public async Task<UserState> HandleAsync(GetUserStateQuery query)
        {
            query.AssertNotNull("query");

            var userAccessSignatures = await this.getUserAccessSignatures.HandleAsync(new GetUserAccessSignaturesQuery(query.Requester, query.RequestedUserId));

            GetUserSubscriptionsResult userSubscriptions = null;
            CreatorStatus creatorStatus = null;
            ChannelsAndCollections createdChannelsAndCollections = null;
            GetAccountSettingsResult accountSettings = null;
            GetBlogResult blog = null;
            
            if (query.RequestedUserId != null)
            {
                await this.requesterSecurity.AuthenticateAsAsync(query.Requester, query.RequestedUserId);

                var blogSubscriptionsTask = this.getBlogSubscriptions.HandleAsync(new GetUserSubscriptionsQuery(query.Requester));

                bool isCreator = await this.requesterSecurity.IsInRoleAsync(query.Requester, FifthweekRole.Creator);
               
                if (isCreator)
                {
                    var creatorStatusTask = this.getCreatorStatus.HandleAsync(new GetCreatorStatusQuery(query.Requester, query.RequestedUserId));
                    var createdChannelsAndCollectionsTask = this.getCreatedChannelsAndCollections.HandleAsync(new GetCreatedChannelsAndCollectionsQuery(query.Requester, query.RequestedUserId));
                    var accountSettingsTask = this.getAccountSettings.HandleAsync(new GetAccountSettingsQuery(query.Requester, query.RequestedUserId));

                    creatorStatus = await creatorStatusTask;

                    var subscriptionTask = Task.FromResult<GetBlogResult>(null);
                    if (creatorStatus.BlogId != null)
                    {
                        subscriptionTask = this.getBlog.HandleAsync(new GetBlogQuery(creatorStatus.BlogId));
                    }

                    createdChannelsAndCollections = await createdChannelsAndCollectionsTask;
                    accountSettings = await accountSettingsTask;
                    blog = await subscriptionTask;
                }

                userSubscriptions = await blogSubscriptionsTask;
            }

            return new UserState(
                userAccessSignatures, 
                creatorStatus, 
                createdChannelsAndCollections,
                accountSettings,
                blog,
                userSubscriptions);
        }
    }
}