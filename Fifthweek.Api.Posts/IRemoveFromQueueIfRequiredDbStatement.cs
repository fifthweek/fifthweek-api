namespace Fifthweek.Api.Posts
{
    using System;
    using System.Threading.Tasks;

    using Fifthweek.Api.Posts.Shared;

    public interface IRemoveFromQueueIfRequiredDbStatement
    {
        Task ExecuteAsync(PostId postId, DateTime now, Func<Task> potentialRemovalOperation);
    }
}