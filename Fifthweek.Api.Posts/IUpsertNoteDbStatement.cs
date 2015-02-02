namespace Fifthweek.Api.Posts
{
    using System;
    using System.Threading.Tasks;

    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Posts.Shared;

    public interface IUpsertNoteDbStatement
    {
        Task ExecuteAsync(
            PostId postId,
            ChannelId channelId,
            ValidNote note,
            DateTime? sheduledPostDate,
            DateTime now);
    }
}