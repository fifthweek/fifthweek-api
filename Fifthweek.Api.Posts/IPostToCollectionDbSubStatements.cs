namespace Fifthweek.Api.Posts
{
    using System;
    using System.Threading.Tasks;

    using Fifthweek.Api.Persistence;

    public interface IPostToCollectionDbSubStatements
    {
        Task QueuePostAsync(Post post);

        Task SchedulePostAsync(Post post, DateTime scheduledPostDate, DateTime now);

        Task PostNowAsync(Post post, DateTime now);
    }
}