namespace Fifthweek.Api.Posts
{
    using System;
    using System.Threading.Tasks;

    using Fifthweek.Api.Persistence;

    public interface IPostToChannelDbSubStatements
    {
        Task QueuePostAsync(Post post);

        Task SchedulePostAsync(Post post, DateTime? scheduledPostDate, DateTime now);
    }
}