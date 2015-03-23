namespace Fifthweek.Api.Posts
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Dapper;

    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Posts.Queries;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;

    [AutoConstructor]
    public partial class GetCreatorNewsfeedDbStatement : IGetCreatorNewsfeedDbStatement
    {
        private static readonly string Sql = string.Format(
            @"SELECT    post.{1} AS PostId, {2}, {4}, {5}, {6}, {7}, {3}, [file].{16} as FileName, [file].{17} as FileExtension, [file].{18} as FileSize, image.{16} as ImageName, image.{17} as ImageExtension, image.{18} as ImageSize
            FROM        {0} post
            INNER JOIN  {8} channel
                ON      post.{2} = channel.{9}
            INNER JOIN  {11} subscription
                ON      channel.{10} = subscription.{12}
            LEFT OUTER JOIN {14} [file]
                ON      post.{6} = [file].{15}
            LEFT OUTER JOIN {14} image
                ON      post.{7} = image.{15}
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
            Subscription.Fields.CreatorId,
            File.Table,
            File.Fields.Id,
            File.Fields.FileNameWithoutExtension,
            File.Fields.FileExtension,
            File.Fields.BlobSizeBytes);

        private readonly IFifthweekDbConnectionFactory connectionFactory;

        public async Task<IReadOnlyList<NewsfeedPost>> ExecuteAsync(UserId creatorId, DateTime now, NonNegativeInt startIndex, PositiveInt count)
        {
            creatorId.AssertNotNull("creatorId");
            startIndex.AssertNotNull("startIndex");
            count.AssertNotNull("count");

            var parameters = new
            {
                CreatorId = creatorId.Value,
                Now = DateTime.UtcNow,
                StartIndex = startIndex.Value,
                Count = count.Value
            };

            using (var connection = this.connectionFactory.CreateConnection())
            {
                var entities = (await connection.QueryAsync<NewsfeedPost.Builder>(Sql, parameters)).ToList();
                foreach (var entity in entities)
                {
                    entity.LiveDate = DateTime.SpecifyKind(entity.LiveDate, DateTimeKind.Utc);
                }

                return entities.Select(_ => _.Build()).ToList();
            }
        }
    }
}