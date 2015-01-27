namespace Fifthweek.Api.Posts
{
    using System;
    using System.Threading.Tasks;

    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Posts.Shared;
    using Fifthweek.CodeGeneration;

    [AutoConstructor]
    public partial class PostToCollectionDbStatement : IPostToCollectionDbStatement
    {
        private readonly IPostToCollectionDbSubStatements subStatements;
        
        public Task ExecuteAsync(
            Shared.PostId newPostId,
            CollectionId collectionId,
            ValidComment comment,
            DateTime? sheduledPostDate,
            bool isQueued,
            FileId fileId,
            bool isFileImage,
            DateTime now)
        {
            newPostId.AssertNotNull("newPostId");
            collectionId.AssertNotNull("collectionId");
            fileId.AssertNotNull("fileId");

            var post = new Post(
                newPostId.Value,
                default(Guid), // Channel ID assigned with SQL.
                null,
                collectionId.Value,
                null,
                !isFileImage ? fileId.Value : (Guid?)null,
                null,
                isFileImage ? fileId.Value : (Guid?)null,
                null,
                comment == null ? null : comment.Value,
                false, // Queue flag assigned by sub-statements.
                default(DateTime), // Live date assigned by sub-statements.
                now);

            if (isQueued)
            {
                return this.subStatements.QueuePostAsync(post);
            }
            
            if (sheduledPostDate.HasValue)
            {
                return this.subStatements.SchedulePostAsync(post, sheduledPostDate.Value, now);
            }

            return this.subStatements.PostNowAsync(post, now);
        }
    }
}