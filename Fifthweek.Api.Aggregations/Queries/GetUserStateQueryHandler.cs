namespace Fifthweek.Api.Aggregations.Queries
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Fifthweek.Api.Blogs;
    using Fifthweek.Api.Blogs.Queries;
    using Fifthweek.Api.Channels.Shared;
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
        private readonly IQueryHandler<GetAccountSettingsQuery, GetAccountSettingsResult> getAccountSettings;
        private readonly IQueryHandler<GetBlogChannelsAndCollectionsQuery, GetBlogChannelsAndCollectionsResult> getBlogChannelsAndCollections;
        private readonly IQueryHandler<GetUserSubscriptionsQuery, GetUserSubscriptionsResult> getBlogSubscriptions;

        public async Task<UserState> HandleAsync(GetUserStateQuery query)
        {
            query.AssertNotNull("query");

            GetUserSubscriptionsResult userSubscriptions = null;
            CreatorStatus creatorStatus = null;
            ChannelsAndCollections createdChannelsAndCollections = null;
            GetAccountSettingsResult accountSettings = null;
            BlogWithFileInformation blog = null;
            
            if (query.RequestedUserId != null)
            {
                await this.requesterSecurity.AuthenticateAsAsync(query.Requester, query.RequestedUserId);
                
                bool isCreator = await this.requesterSecurity.IsInRoleAsync(query.Requester, FifthweekRole.Creator);

                Task<CreatorStatus> creatorStatusTask = null;
                if (isCreator)
                {
                    creatorStatusTask = this.getCreatorStatus.HandleAsync(new GetCreatorStatusQuery(query.Requester, query.RequestedUserId));
                }

                var blogSubscriptionsTask = this.getBlogSubscriptions.HandleAsync(new GetUserSubscriptionsQuery(query.Requester, query.RequestedUserId));
                var accountSettingsTask = this.getAccountSettings.HandleAsync(new GetAccountSettingsQuery(query.Requester, query.RequestedUserId));

                if (isCreator)
                {
                    creatorStatus = await creatorStatusTask;

                    var blogChannelsAndCollectionsTask = Task.FromResult<GetBlogChannelsAndCollectionsResult>(null);
                    if (creatorStatus.BlogId != null)
                    {
                        blogChannelsAndCollectionsTask = this.getBlogChannelsAndCollections.HandleAsync(new GetBlogChannelsAndCollectionsQuery(creatorStatus.BlogId));
                    }

                    var blogChannelsAndCollections = await blogChannelsAndCollectionsTask;
                    if (blogChannelsAndCollections != null)
                    {
                        blog = blogChannelsAndCollections.Blog;
                        
                        // Temporary for backwards compatibility with live site.
                        createdChannelsAndCollections = new ChannelsAndCollections(blog.Channels);
                    }
                }

                accountSettings = await accountSettingsTask;
                userSubscriptions = await blogSubscriptionsTask;
            }

            List<ChannelId> creatorChannelIds = null;
            if (blog != null)
            {
                creatorChannelIds = blog.Channels.Select(v => v.ChannelId).Distinct().ToList();
            }

            List<ChannelId> subscribedChannelIds = null;
            if (userSubscriptions != null)
            {
                subscribedChannelIds = userSubscriptions.Blogs.SelectMany(v => v.Channels).Select(v => v.ChannelId).Distinct().ToList();
            }

            var userAccessSignatures = await this.getUserAccessSignatures.HandleAsync(new GetUserAccessSignaturesQuery(query.Requester, query.RequestedUserId, creatorChannelIds, subscribedChannelIds));

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