namespace Fifthweek.Api.Posts
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Dapper;

    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Api.Persistence.Payments;
    using Fifthweek.Api.Posts.Queries;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;

    [AutoConstructor]
    public partial class GetPreviewNewsfeedDbStatement : IGetPreviewNewsfeedDbStatement
    {
        public static readonly PositiveInt MaxCommentLength = PositiveInt.Parse(400);

        private readonly IFifthweekDbConnectionFactory connectionFactory;

        public async Task<GetNewsfeedDbResult> ExecuteAsync(
            UserId requestorId,
            UserId creatorId,
            IReadOnlyList<ChannelId> requestedChannelIds,
            DateTime now,
            DateTime origin,
            bool searchForwards,
            NonNegativeInt startIndex,
            PositiveInt count)
        {
            startIndex.AssertNotNull("startIndex");
            count.AssertNotNull("count");

            var parameters = new
            {
                RequestorId = requestorId == null ? null : (Guid?)requestorId.Value,
                CreatorId = creatorId == null ? null : (Guid?)creatorId.Value,
                RequestedChannelIds = requestedChannelIds == null ? null : requestedChannelIds.Select(v => v.Value).ToList(),
                Now = now,
                Origin = origin,
                StartIndex = startIndex.Value,
                Count = count.Value
            };

            using (var connection = this.connectionFactory.CreateConnection())
            {
                var query = new StringBuilder();
                query.Append(GetNewsfeedDbStatement.GetSqlStart(requestorId, MaxCommentLength));
                query.Append(GetNewsfeedDbStatement.CreateFilter(null, creatorId, requestedChannelIds, now, origin, searchForwards, startIndex, count));

                var entities = (await connection.QueryAsync<NewsfeedPost.Builder>(query.ToString(), parameters)).ToList();
                GetNewsfeedDbStatement.ProcessNewsfeedResults(entities);
                
                return new GetNewsfeedDbResult(entities.Select(_ => _.Build()).ToList(), 0);
            }
        }
    }
}