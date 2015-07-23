namespace Fifthweek.Api.Blogs.Queries
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;

    [AutoConstructor]
    public partial class GetBlogSubscriberInformationQueryHandler : IQueryHandler<GetBlogSubscriberInformationQuery, BlogSubscriberInformation>
    {
        private readonly IRequesterSecurity requesterSecurity;
        private readonly IBlogSecurity blogSecurity;
        private readonly IFileInformationAggregator fileInformationAggregator;
        private readonly IGetBlogSubscriberInformationDbStatement getBlogSubscriberInformation;
        private readonly IGetCreatorRevenueDbStatement getCreatorRevenue;

        public async Task<BlogSubscriberInformation> HandleAsync(GetBlogSubscriberInformationQuery query)
        {
            query.AssertNotNull("query");

            var userId = await this.requesterSecurity.AuthenticateAsync(query.Requester);
            await this.blogSecurity.AssertWriteAllowedAsync(userId, query.BlogId);

            var databaseResultTask = this.getBlogSubscriberInformation.ExecuteAsync(query.BlogId);
            var revenue = await this.getCreatorRevenue.ExecuteAsync(userId);
            var databaseResult = await databaseResultTask;

            var subscribers = new List<BlogSubscriberInformation.Subscriber>(); 
            foreach (var user in databaseResult.Subscribers.GroupBy(v => v.UserId))
            {
                var first = user.First();
                var fileInformation = await this.fileInformationAggregator.GetFileInformationAsync(null, first.ProfileImageFileId, FilePurposes.ProfileImage);

                var channels = new List<BlogSubscriberInformation.SubscriberChannel>();
                foreach (var item in user)
                {
                    channels.Add(
                        new BlogSubscriberInformation.SubscriberChannel(
                            item.ChannelId,
                            item.SubscriptionStartDate,
                            item.AcceptedPriceInUsCentsPerWeek));
                }

                subscribers.Add(
                    new BlogSubscriberInformation.Subscriber(
                        first.Username,
                        first.UserId,
                        fileInformation,
                        first.FreeAccessEmail,
                        channels));
            }

            return new BlogSubscriberInformation(revenue.TotalRevenue, subscribers);
        }
    }
}