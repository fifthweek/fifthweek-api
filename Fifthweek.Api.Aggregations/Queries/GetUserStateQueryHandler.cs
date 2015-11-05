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
        private readonly IQueryHandler<GetAccountSettingsQuery, GetAccountSettingsResult> getAccountSettings;
        private readonly IQueryHandler<GetBlogChannelsAndQueuesQuery, GetBlogChannelsAndQueuesResult> getBlogChannelsAndQueues;
        private readonly IQueryHandler<GetUserSubscriptionsQuery, GetUserSubscriptionsResult> getBlogSubscriptions;
        private readonly IImpersonateIfRequired impersonateIfRequired;

        public async Task<UserState> HandleAsync(GetUserStateQuery query)
        {
            query.AssertNotNull("query");

            GetUserSubscriptionsResult userSubscriptions = null;
            GetAccountSettingsResult accountSettings = null;
            BlogWithFileInformation blog = null;
            
            if (query.RequestedUserId != null)
            {
                if (query.Impersonate)
                {
                    var impersonatingRequester = await this.impersonateIfRequired.ExecuteAsync(query.Requester, query.RequestedUserId);
                    if (impersonatingRequester != null)
                    {
                        query = new GetUserStateQuery(impersonatingRequester, query.RequestedUserId, false, query.Now);
                    }
                }

                await this.requesterSecurity.AuthenticateAsAsync(query.Requester, query.RequestedUserId);
                
                var isCreator = await this.requesterSecurity.IsInRoleAsync(query.Requester, FifthweekRole.Creator);

                var blogSubscriptionsTask = this.getBlogSubscriptions.HandleAsync(new GetUserSubscriptionsQuery(query.Requester, query.RequestedUserId));
                var accountSettingsTask = this.getAccountSettings.HandleAsync(new GetAccountSettingsQuery(
                    query.Requester, query.RequestedUserId, query.Now));

                if (isCreator)
                {
                    var blogChannelsAndCollections = await this.getBlogChannelsAndQueues.HandleAsync(new GetBlogChannelsAndQueuesQuery(query.RequestedUserId));
                    if (blogChannelsAndCollections != null)
                    {
                        blog = blogChannelsAndCollections.Blog;
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
            List<ChannelId> freeAccessChannelIds = null;
            if (userSubscriptions != null)
            {
                bool hasFunds = accountSettings != null && (accountSettings.AccountBalance > 0 || accountSettings.IsRetryingPayment);
                if (hasFunds)
                {
                    subscribedChannelIds =
                        userSubscriptions.Blogs.SelectMany(v => v.Channels)
                            .Where(v => v.AcceptedPrice >= v.Price)
                            .Select(v => v.ChannelId)
                            .Distinct()
                            .ToList();
                }

                freeAccessChannelIds = userSubscriptions.FreeAccessChannelIds.Distinct().ToList();
            }

            var userAccessSignatures = await this.getUserAccessSignatures.HandleAsync(
                new GetUserAccessSignaturesQuery(query.Requester, query.RequestedUserId, creatorChannelIds, subscribedChannelIds, freeAccessChannelIds));

            return new UserState(
                userAccessSignatures, 
                accountSettings,
                blog,
                userSubscriptions);
        }
    }
}