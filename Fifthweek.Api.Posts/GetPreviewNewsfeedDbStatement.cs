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
        public static readonly PositiveInt MaxPreviewTextLength = PositiveInt.Parse(400);

        private readonly IFifthweekDbConnectionFactory connectionFactory;

        public async Task<GetPreviewNewsfeedDbResult> ExecuteAsync(
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
                query.Append(GetNewsfeedDbStatement.GetSqlStart(requestorId, MaxPreviewTextLength));
                query.Append(GetNewsfeedDbStatement.CreateFilter(null, creatorId, requestedChannelIds, now, origin, searchForwards, startIndex, count, true));

                var entities = (await connection.QueryAsync<PreviewNewsfeedPost.Builder>(query.ToString(), parameters)).ToList();
                ProcessNewsfeedResults(entities);
                
                return new GetPreviewNewsfeedDbResult(entities.Select(_ => _.Build()).ToList());
            }
        }

        private static void ProcessNewsfeedResults(List<PreviewNewsfeedPost.Builder> entities)
        {
            foreach (var entity in entities)
            {
                entity.LiveDate = DateTime.SpecifyKind(entity.LiveDate, DateTimeKind.Utc);
                entity.CreationDate = DateTime.SpecifyKind(entity.CreationDate, DateTimeKind.Utc);
            }
        }
    }
}