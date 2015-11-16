namespace Fifthweek.Api.Posts
{
    using System;
    using System.Threading.Tasks;

    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Posts.Shared;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;

    [AutoConstructor]
    public partial class RequestFreePost : IRequestFreePost
    {
        private readonly IGetFreePostTimestamp getFreePostTimestamp;
        private readonly IRequestFreePostDbStatement requestFreePost;

        public async Task ExecuteAsync(UserId requestorId, PostId postId, DateTime now)
        {
            var timestamp = this.getFreePostTimestamp.Execute(now);
            var result = await this.requestFreePost.ExecuteAsync(requestorId, postId, timestamp, PostConstants.MaximumFreePostsPerInterval);

            if (!result)
            {
                throw new RecoverableException("You do not have any free posts available.");
            }
        }
    }
}