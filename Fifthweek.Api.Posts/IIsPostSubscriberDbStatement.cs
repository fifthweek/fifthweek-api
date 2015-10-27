namespace Fifthweek.Api.Posts
{
    using System;
    using System.Threading.Tasks;

    using Fifthweek.Api.Identity.Shared.Membership;

    public interface IIsPostSubscriberDbStatement
    {
        Task<bool> ExecuteAsync(UserId userId, Shared.PostId postId, DateTime now);
    }
}