namespace Fifthweek.Api.Posts.Queries
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Dapper;

    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.CodeGeneration;

    [AutoConstructor]
    public partial class GetCreatorNewsfeedQueryHandler : IQueryHandler<GetCreatorNewsfeedQuery, IReadOnlyList<NewsfeedPost>>
    {
        private static readonly string Sql = string.Format(
            @"SELECT    post.{1} AS PostId, {2}, {4}, {5}, {6}, {7}, {3}
            FROM        {0} post
            INNER JOIN  {8} channel
                ON      post.{2} = channel.{9}
            INNER JOIN  {11} subscription
                ON      channel.{10} = subscription.{12}
            WHERE       post.{3} <= @Now
            AND         subscription.{13} = @CreatorId
            ORDER BY    post.{3} DESC
            OFFSET      @StartIndex ROWS
            FETCH NEXT  @Count ROWS ONLY",
            Post.Table,
            Post.Fields.Id,
            Post.Fields.ChannelId,
            Post.Fields.LiveDate,
            Post.Fields.CollectionId,
            Post.Fields.Comment,
            Post.Fields.FileId,
            Post.Fields.ImageId,
            Channel.Table,
            Channel.Fields.Id,
            Channel.Fields.SubscriptionId,
            Subscription.Table,
            Subscription.Fields.Id,
            Subscription.Fields.CreatorId);

        private readonly IRequesterSecurity requesterSecurity;
        private readonly IFifthweekDbContext databaseContext;

        public async Task<IReadOnlyList<NewsfeedPost>> HandleAsync(GetCreatorNewsfeedQuery query)
        {
            query.AssertNotNull("query");

            // In future, this query will need to allow users who are subscribed to this creator too. This will require a 
            // IsAuthenticatedAs || IsSubscribedTo authorization.
            await this.requesterSecurity.AuthenticateAsAsync(query.Requester, query.RequestedUserId);
            await this.requesterSecurity.AssertInRoleAsync(query.Requester, FifthweekRole.Creator);

            return await this.GetCreatorBacklogAsync(query);
        }

        private async Task<IReadOnlyList<NewsfeedPost>> GetCreatorBacklogAsync(GetCreatorNewsfeedQuery query)
        {
            var parameters = new
            {
                CreatorId = query.RequestedUserId.Value,
                Now = DateTime.UtcNow,
                StartIndex = query.StartIndex.Value,
                Count = query.Count.Value
            };
            var entities = await this.databaseContext.Database.Connection.QueryAsync<NewsfeedPost.Builder>(Sql, parameters);
            return entities.Select(_ => _.Build()).ToList();
        }
    }
}