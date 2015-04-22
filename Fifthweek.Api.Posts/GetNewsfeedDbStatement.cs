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
    using Fifthweek.Api.Posts.Queries;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;

    [AutoConstructor]
    public partial class GetNewsfeedDbStatement : IGetNewsfeedDbStatement
    {
        private static readonly string SqlStart = string.Format(
            @"SELECT    blog.{13} AS CreatorId, post.{1} AS PostId, {2}, {4}, {5}, {6}, {7}, {3}, post.{21}, [file].{16} as FileName, [file].{17} as FileExtension, [file].{18} as FileSize, image.{16} as ImageName, image.{17} as ImageExtension, image.{18} as ImageSize, image.{19} as ImageRenderWidth, image.{20} as ImageRenderHeight
            FROM        {0} post
            INNER JOIN  {8} channel
                ON      post.{2} = channel.{9}
            INNER JOIN  {11} blog
                ON      channel.{10} = blog.{12}
            LEFT OUTER JOIN {14} [file]
                ON      post.{6} = [file].{15}
            LEFT OUTER JOIN {14} image
                ON      post.{7} = image.{15}
            WHERE       post.{3} <= @Now",
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
            Channel.Fields.BlogId,
            Blog.Table,
            Blog.Fields.Id,
            Blog.Fields.CreatorId,
            File.Table,
            File.Fields.Id,
            File.Fields.FileNameWithoutExtension,
            File.Fields.FileExtension,
            File.Fields.BlobSizeBytes,
            File.Fields.RenderWidth,
            File.Fields.RenderHeight,
            Post.Fields.CreationDate);

        private static readonly string CreatorFilter = string.Format(
            @"
            AND blog.{0} = @CreatorId",
            Blog.Fields.CreatorId);

        private static readonly string ChannelFilter = string.Format(
            @"
            AND channel.{0} IN @RequestedChannelIds",
            Channel.Fields.Id);

        private static readonly string CollectionFilter = string.Format(
            @"
            AND post.{0} IN @RequestedCollectionIds",
            Post.Fields.CollectionId);

        private static readonly string SubscriptionFilter = string.Format(
            @"
            AND channel.{0} IN 
            (
                SELECT sub.{3} 
                FROM {2} sub 
                WHERE 
                    sub.{4} = @RequestorId 
                    AND 
                    (
                        sub.{5} >= channel.{6}
                        OR
                        blog.{1} IN
                        (
                            SELECT fau.{8} 
                            FROM {7} fau
                            INNER JOIN {10} u ON u.{11} = fau.{9}
                            WHERE u.{12} = @RequestorId
                        )
                    )
            )",
            Channel.Fields.Id,
            Blog.Fields.Id,
            ChannelSubscription.Table,
            ChannelSubscription.Fields.ChannelId,
            ChannelSubscription.Fields.UserId,
            ChannelSubscription.Fields.AcceptedPriceInUsCentsPerWeek,
            Channel.Fields.PriceInUsCentsPerWeek,
            FreeAccessUser.Table,
            FreeAccessUser.Fields.BlogId,
            FreeAccessUser.Fields.Email,
            FifthweekUser.Table,
            FifthweekUser.Fields.Email,
            FifthweekUser.Fields.Id);

        private static readonly string SqlEndBackwardsWithOrigin = string.Format(
            @"
            AND post.{0} <= @Origin",
            Post.Fields.LiveDate);

        private static readonly string SqlEndBackwards = string.Format(
            @"
            ORDER BY    post.{0} DESC, post.{1} DESC
            OFFSET      @StartIndex ROWS
            FETCH NEXT  @Count ROWS ONLY",
            Post.Fields.LiveDate,
            Post.Fields.CreationDate);

        private static readonly string SqlEndForwards = string.Format(
            @"
            AND post.{0} > @Origin
            ORDER BY    post.{0} ASC, post.{1} ASC
            OFFSET      @StartIndex ROWS
            FETCH NEXT  @Count ROWS ONLY",
            Post.Fields.LiveDate,
            Post.Fields.CreationDate);

        private readonly IFifthweekDbConnectionFactory connectionFactory;

        public async Task<IReadOnlyList<NewsfeedPost>> ExecuteAsync(
            UserId requestorId,
            UserId creatorId,
            IReadOnlyList<ChannelId> requestedChannelIds,
            IReadOnlyList<CollectionId> requestedCollectionIds,
            DateTime now,
            DateTime origin,
            bool searchForwards,
            NonNegativeInt startIndex,
            PositiveInt count)
        {
            requestorId.AssertNotNull("requestorId");
            startIndex.AssertNotNull("startIndex");
            count.AssertNotNull("count");

            var parameters = new
            {
                RequestorId = requestorId.Value,
                CreatorId = creatorId == null ? null : (Guid?)creatorId.Value,
                RequestedChannelIds = requestedChannelIds == null ? null : requestedChannelIds.Select(v => v.Value).ToList(),
                RequestedCollectionIds = requestedCollectionIds == null ? null : requestedCollectionIds.Select(v => v.Value).ToList(),
                Now = now,
                Origin = origin,
                StartIndex = startIndex.Value,
                Count = count.Value
            };

            using (var connection = this.connectionFactory.CreateConnection())
            {
                var query = new StringBuilder();
                query.Append(SqlStart);

                if (requestedChannelIds != null && requestedChannelIds.Count > 0)
                {
                    query.Append(ChannelFilter);
                }

                if (requestedCollectionIds != null && requestedCollectionIds.Count > 0)
                {
                    query.Append(CollectionFilter);
                }

                if (creatorId != null)
                {
                    query.Append(CreatorFilter);
                }

                if (!requestorId.Equals(creatorId))
                {
                    query.Append(SubscriptionFilter);
                }

                if (searchForwards)
                {
                    query.Append(SqlEndForwards);
                }
                else
                {
                    if (now != origin)
                    {
                        query.Append(SqlEndBackwardsWithOrigin);
                    }

                    query.Append(SqlEndBackwards);
                }

                var entities = (await connection.QueryAsync<NewsfeedPost.Builder>(query.ToString(), parameters)).ToList();
                foreach (var entity in entities)
                {
                    entity.LiveDate = DateTime.SpecifyKind(entity.LiveDate, DateTimeKind.Utc);
                    entity.CreationDate = DateTime.SpecifyKind(entity.CreationDate, DateTimeKind.Utc);
                }

                return entities.Select(_ => _.Build()).ToList();
            }
        }
    }
}