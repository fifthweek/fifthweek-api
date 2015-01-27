namespace Fifthweek.Api.Posts.Queries
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Dapper;

    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Membership;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.CodeGeneration;

    using UserId = Fifthweek.Api.Identity.Shared.Membership.UserId;

    [AutoConstructor]
    public partial class GetCreatorBacklogQueryHandler : IQueryHandler<GetCreatorBacklogQuery, IReadOnlyList<BacklogPost>>
    {
        private static readonly string Sql = string.Format(
            @"SELECT    post.{1} AS PostId, {2}, {4}, {5}, {6}, {7}, {8}, {3}
            FROM        {0} post
            INNER JOIN  {9} channel
                ON      post.{2} = channel.{10}
            INNER JOIN  {12} subscription
                ON      channel.{11} = subscription.{13}
            WHERE       post.{3} > @Now
            AND         subscription.{14} = @CreatorId
            ORDER BY    post.{3} DESC, post.{8} DESC",
            Post.Table,
            Post.Fields.Id,
            Post.Fields.ChannelId,
            Post.Fields.LiveDate,
            Post.Fields.CollectionId,
            Post.Fields.Comment,
            Post.Fields.FileId,
            Post.Fields.ImageId,
            Post.Fields.ScheduledByQueue,
            Channel.Table,
            Channel.Fields.Id,
            Channel.Fields.SubscriptionId,
            Subscription.Table,
            Subscription.Fields.Id,
            Subscription.Fields.CreatorId);

        private readonly IRequesterSecurity requesterSecurity;
        private readonly IFifthweekDbContext databaseContext;

        public async Task<IReadOnlyList<BacklogPost>> HandleAsync(GetCreatorBacklogQuery query)
        {
            query.AssertNotNull("query");

            await this.requesterSecurity.AuthenticateAsAsync(query.Requester, query.RequestedUserId);
            await this.requesterSecurity.AssertInRoleAsync(query.Requester, FifthweekRole.Creator);

            return await this.GetCreatorBacklogAsync(query.RequestedUserId);
        }

        private async Task<IReadOnlyList<BacklogPost>> GetCreatorBacklogAsync(UserId creatorId)
        {
            var parameters = new
            {
                CreatorId = creatorId.Value,
                Now = DateTime.UtcNow
            };
            var entities = await this.databaseContext.Database.Connection.QueryAsync<BacklogPost.Builder>(Sql, parameters);
            return entities.Select(_ => _.Build()).ToList();
        }
    }
}