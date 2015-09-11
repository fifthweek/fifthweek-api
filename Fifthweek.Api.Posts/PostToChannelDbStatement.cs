namespace Fifthweek.Api.Posts
{
    using System;
    using System.Threading.Tasks;

    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Posts.Shared;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;

    [AutoConstructor]
    public partial class PostToChannelDbStatement : IPostToChannelDbStatement
    {
        private readonly IPostToChannelDbSubStatements subStatements;
        
        public Task ExecuteAsync(
            PostId newPostId,
            ChannelId channelId,
            ValidComment comment,
            DateTime? sheduledPostDate,
            QueueId queueId,
            FileId fileId,
            FileId imageId,
            DateTime now)
        {
            newPostId.AssertNotNull("newPostId");
            channelId.AssertNotNull("channelId");

            var post = new Post(
                newPostId.Value,
                channelId.Value,
                null,
                queueId == null ? (Guid?)null : queueId.Value,
                null,
                fileId == null ? (Guid?)null : fileId.Value,
                null,
                imageId == null ? (Guid?)null : imageId.Value,
                null,
                comment == null ? null : comment.Value,
                default(DateTime), // Live date assigned by sub-statements.
                now);

            if (queueId != null)
            {
                return this.subStatements.QueuePostAsync(post);
            }

            sheduledPostDate.AssertNotNull("sheduledPostDate");

            return this.subStatements.SchedulePostAsync(post, sheduledPostDate, now);
        }
    }
}