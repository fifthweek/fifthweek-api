namespace Fifthweek.Api.Posts
{
    using System;
    using System.Threading.Tasks;

    using Fifthweek.Api.Posts.Shared;

    public interface ISetPostLiveDateDbStatement
    {
        Task ExecuteAsync(PostId postId, DateTime newTime, DateTime now);
    }
}