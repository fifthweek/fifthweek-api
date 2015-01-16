namespace Fifthweek.Api.Posts
{
    using System;
    using System.Threading.Tasks;

    using Fifthweek.Api.Persistence;

    public interface IPostToCollectionDbSubStatements
    {
        Task QueuePostAsync(Post unscheduledPostWithoutChannel);

        Task SchedulePostAsync(Post unscheduledPostWithoutChannel, DateTime scheduledPostDate, DateTime now);

        Task PostNowAsync(Post unscheduledPostWithoutChannel, DateTime now);
    }
}