namespace Fifthweek.Api.Posts
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Posts.Shared;

    public interface IPostToChannelDbStatement
    {
        Task ExecuteAsync(
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
            DateTime now);
    }
}