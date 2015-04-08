namespace Fifthweek.Api.Blogs.Queries
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Dapper;

    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;

    [AutoConstructor]
    public partial class GetCreatorStatusQueryHandler : IQueryHandler<GetCreatorStatusQuery, CreatorStatus>
    {
        // Ensure we return the same subscription each time by ordering by creation date descending. This means the latest
        // subscription is returned. Latest seems to make most sense: if a user double-posts, they'll get the ID of the 
        // latest subscription, which they'll probably then start filling out. It also provides us with a mechanism for 
        // overriding / soft deleting subscriptions by inserting a new subscription record (not saying that's the solution 
        // in that case, but its another option enabled by the decision to sort descending!).
        private static readonly string Sql = string.Format(
            @"
            SELECT TOP 1	subscription.{6} AS SubscriptionId, ISNULL((SELECT 1 WHERE EXISTS(
	            SELECT		* 
	            FROM		{0} channel
	            INNER JOIN	{3} post -- Only yield rows when post(s) exist.
		            ON		channel.{1} = post.{4}
	            WHERE		channel.{2} = subscription.{6}))
	            , 0) AS HasAtLeastOnePost
            FROM			{5} subscription
            WHERE			subscription.{7} = @CreatorId
            ORDER BY		subscription.{8} DESC",
            Channel.Table,
            Channel.Fields.Id,
            Channel.Fields.BlogId,
            Post.Table,
            Post.Fields.ChannelId,
            Blog.Table,
            Blog.Fields.Id,
            Blog.Fields.CreatorId,
            Blog.Fields.CreationDate);

        private readonly IRequesterSecurity requesterSecurity;
        private readonly IFifthweekDbConnectionFactory connectionFactory;

        public async Task<CreatorStatus> HandleAsync(GetCreatorStatusQuery query)
        {
            query.AssertNotNull("query");

            UserId userId = await this.requesterSecurity.AuthenticateAsAsync(query.Requester, query.RequestedUserId); 

            return await this.GetCreatorStatusAsync(userId);
        }

        private async Task<CreatorStatus> GetCreatorStatusAsync(UserId creatorId)
        {
            using (var connection = this.connectionFactory.CreateConnection())
            {
                var creatorData = (await connection.QueryAsync<CreatorStatusData>(Sql, new { CreatorId = creatorId.Value })).SingleOrDefault();

                return creatorData == null
                    ? CreatorStatus.NoSubscriptions
                    : new CreatorStatus(new BlogId(creatorData.SubscriptionId), !creatorData.HasAtLeastOnePost);
            }
        }

        private class CreatorStatusData
        {
            public Guid SubscriptionId { get; set; }

            public bool HasAtLeastOnePost { get; set; }
        }
    }
}