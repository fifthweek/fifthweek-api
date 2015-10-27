namespace Fifthweek.Api.Posts
{
    using System.Threading.Tasks;

    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Posts.Shared;

    public class RequestFreePostDbStatement : IRequestFreePostDbStatement
    {
        public async Task ExecuteAsync(UserId requestorId, PostId postId)
        {
            throw new UnauthorizedException("Not allowed any free posts.");
        }
    }
}