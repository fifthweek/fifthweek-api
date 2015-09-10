namespace Fifthweek.Api.Posts
{
    using System;
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
            ValidComment comment,
            DateTime? sheduledPostDate,
            QueueId queueId,
            FileId fileId,
            FileId imageId,
            DateTime now);
    }
}