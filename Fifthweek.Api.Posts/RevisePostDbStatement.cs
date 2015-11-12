namespace Fifthweek.Api.Posts
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Dapper;

    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Posts.Shared;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;

    [AutoConstructor]
    public partial class RevisePostDbStatement : IRevisePostDbStatement
    {
        private static readonly string DeleteQuery = string.Format(
           "DELETE FROM {0} WHERE {1}=@PostId",
           PostFile.Table,
           PostFile.Fields.PostId);

        private readonly IFifthweekDbConnectionFactory connectionFactory;

        public async Task ExecuteAsync(
            PostId postId,
            ValidComment content,
            ValidPreviewText previewText,
            FileId previewImageId,
            IReadOnlyList<FileId> fileIds,
            int previewWordCount,
            int wordCount,
            int imageCount,
            int fileCount,
            int videoCount)
        {
            postId.AssertNotNull("postId");
            fileIds.AssertNotNull("fileIds");
            
            var post = new Post(postId.Value)
            {
                // QueueId = command.QueueId.Value, - Removed as this would require a queue defragmentation if post is already queued. Unnecessary complexity for MVP.
                PreviewText = previewText == null ? null : previewText.Value,
                PreviewImageId = previewImageId == null ? (Guid?)null : previewImageId.Value,
                Content = content == null ? null : content.Value,
                PreviewWordCount = previewWordCount,
                WordCount = wordCount,
                ImageCount = imageCount,
                FileCount = fileCount,
                VideoCount = videoCount
            };

            var postFiles = fileIds.Select(v => new PostFile(postId.Value, v.Value)).ToList();

            using (var transaction = TransactionScopeBuilder.CreateAsync())
            {
                using (var connection = this.connectionFactory.CreateConnection())
                {
                    // The order we access tables should match PostToChannelDbStatement.
                    var rowsUpdated = await connection.UpdateAsync(
                        post, 
                        Post.Fields.PreviewText 
                        | Post.Fields.PreviewImageId 
                        | Post.Fields.Content 
                        | Post.Fields.PreviewWordCount 
                        | Post.Fields.WordCount 
                        | Post.Fields.ImageCount 
                        | Post.Fields.FileCount
                        | Post.Fields.VideoCount);

                    if (rowsUpdated > 0)
                    {
                        await connection.ExecuteAsync(
                            DeleteQuery,
                            new { PostId = postId.Value });

                        if (fileIds.Count > 0)
                        {
                            await connection.InsertAsync(postFiles);
                        }
                    }
                }

                transaction.Complete();
            }
        }
    }
}