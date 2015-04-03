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
    using Fifthweek.Shared;

    [AutoConstructor]
    public partial class PostToCollectionDbStatement : IPostToCollectionDbStatement
    {
        private readonly IPostToCollectionDbSubStatements subStatements;
        
        public Task ExecuteAsync(
            PostId newPostId,
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

            return this.subStatements.SchedulePostAsync(post, sheduledPostDate, now);
        }
    }
}