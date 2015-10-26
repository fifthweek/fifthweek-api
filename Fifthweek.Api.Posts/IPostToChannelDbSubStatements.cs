namespace Fifthweek.Api.Posts
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Persistence;

    public interface IPostToChannelDbSubStatements
    {
        Task QueuePostAsync(Post post, IReadOnlyList<PostFile> postFiles);

        Task SchedulePostAsync(Post post, IReadOnlyList<PostFile> postFiles, DateTime? scheduledPostDate, DateTime now);
    }
}