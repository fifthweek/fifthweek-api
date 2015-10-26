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
    public partial class GetCreatorBacklogDbStatement : IGetCreatorBacklogDbStatement
    {
        private static readonly string Sql = string.Format(
          @"SELECT    post.{1} AS PostId, post.{2}, {4}, {5}, {23}, {24}, {25}, {26}, {7} AS ImageId, {3}, post.{22}, image.{17} as ImageName, image.{18} as ImageExtension, image.{19} as ImageSize, image.{20} as ImageRenderWidth, image.{21} as ImageRenderHeight
            FROM        {0} post
            INNER JOIN  {9} channel
                ON      post.{2} = channel.{10}
            INNER JOIN  {12} subscription
                ON      channel.{11} = subscription.{13}
            LEFT OUTER JOIN {15} image
                ON      post.{7} = image.{16}
            WHERE       post.{3} > @Now
            AND         subscription.{14} = @CreatorId
            ORDER BY    post.{3} ASC, post.{22} ASC",
          Post.Table,
          Post.Fields.Id,
          Post.Fields.ChannelId,
          Post.Fields.LiveDate,
          Post.Fields.QueueId,
          Post.Fields.PreviewText,
          Post.Fields.Id,
          Post.Fields.PreviewImageId,
          Post.Fields.Id,
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
          Post.Fields.CreationDate,
          Post.Fields.PreviewWordCount,
          Post.Fields.WordCount,
          Post.Fields.ImageCount,
          Post.Fields.FileCount);

        private readonly IFifthweekDbConnectionFactory connectionFactory;

        public async Task<IReadOnlyList<BacklogPost>> ExecuteAsync(UserId creatorId, DateTime now)
        {
            creatorId.AssertNotNull("creatorId");

            var parameters = new
            {
                CreatorId = creatorId.Value,
                Now = now
            };

            using (var connection = this.connectionFactory.CreateConnection())
            {
                var entities = (await connection.QueryAsync<BacklogPost.Builder>(Sql, parameters)).ToList();
                foreach (var entity in entities)
                {
                    entity.LiveDate = DateTime.SpecifyKind(entity.LiveDate, DateTimeKind.Utc);
                }

                return entities.Select(_ => _.Build()).ToList();
            }
        }
    }
}