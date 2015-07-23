namespace Fifthweek.Api.Blogs.Queries
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;

    [AutoConstructor]
    public partial class GetUserSubscriptionsQueryHandler : IQueryHandler<GetUserSubscriptionsQuery, GetUserSubscriptionsResult>
    {
        private readonly IRequesterSecurity requesterSecurity;
        private readonly IGetUserSubscriptionsDbStatement getUserSubscriptions;
        private readonly IFileInformationAggregator fileInformationAggregator;
        
        public async Task<GetUserSubscriptionsResult> HandleAsync(GetUserSubscriptionsQuery query)
        {
            query.AssertNotNull("query");

            await this.requesterSecurity.AuthenticateAsAsync(query.Requester, query.RequestedUserId);
            var subscriptions = await this.getUserSubscriptions.ExecuteAsync(query.RequestedUserId);

            var results = new List<BlogSubscriptionStatus>();
            foreach (var item in subscriptions)
            {
                FileInformation profileImage = null;

                if (item.ProfileImageFileId != null)
                {
                    profileImage = await this.fileInformationAggregator.GetFileInformationAsync(
                    null,
                    item.ProfileImageFileId,
                    FilePurposes.ProfileImage);
                }

                results.Add(new BlogSubscriptionStatus(
                    item.BlogId,
                    item.Name,
                    item.CreatorId,
                    item.CreatorUsername,
                    profileImage,
                    item.FreeAccess,
                    item.Channels));
            }

            return new GetUserSubscriptionsResult(results);
        }
    }
}