namespace Fifthweek.Api.Posts
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
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
            ValidComment content,
            DateTime? sheduledPostDate,
            QueueId queueId,
            ValidPreviewText previewText,
            FileId previewImageId,
            IReadOnlyList<FileId> fileIds,
            int previewWordCount,
            int wordCount,
            int imageCount,
            int fileCount,
            int videoCount,
            DateTime now)
        {
            newPostId.AssertNotNull("newPostId");
            content.AssertNotNull("content");
            channelId.AssertNotNull("channelId");

            var post = new Post(
                newPostId.Value,
                channelId.Value,
                null,
                queueId == null ? (Guid?)null : queueId.Value,
                null,
                previewImageId == null ? (Guid?)null : previewImageId.Value,
                null,
                previewText == null ? null : previewText.Value,
                content.Value,
                previewWordCount,
                wordCount,
                imageCount,
                fileCount,
                videoCount,
                default(DateTime), // Live date assigned by sub-statements.
                now);

            var postFiles = fileIds.EmptyIfNull().Select(v => new PostFile(newPostId.Value, v.Value)).ToList();

            if (queueId != null)
            {
                return this.subStatements.QueuePostAsync(post, postFiles);
            }

            return this.subStatements.SchedulePostAsync(post, postFiles, sheduledPostDate, now);
        }
    }
}