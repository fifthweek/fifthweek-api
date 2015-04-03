namespace Fifthweek.Api.Posts.Commands
{
    using System;
    using System.Threading.Tasks;

    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Posts.Shared;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;

    [AutoConstructor]
    public partial class RescheduleForNowCommandHandler : ICommandHandler<RescheduleForNowCommand>
    {
        private readonly IRequesterSecurity requesterSecurity;
        private readonly IPostSecurity postSecurity;
        private readonly ISetPostLiveDateDbStatement setPostLiveDate;
        private readonly IRemoveFromQueueIfRequiredDbStatement removeFromQueueIfRequired;

        public async Task HandleAsync(RescheduleForNowCommand command)
        {
            command.AssertNotNull("command");

            var userId = await this.requesterSecurity.AuthenticateAsync(command.Requester);
            await this.postSecurity.AssertWriteAllowedAsync(userId, command.PostId);

            await this.RescheduleForNowAsync(command.PostId);
        }

        private Task RescheduleForNowAsync(PostId postId)
        {
            var now = DateTime.UtcNow;

            return this.removeFromQueueIfRequired.ExecuteAsync(
                postId,
                now,
                () => this.setPostLiveDate.ExecuteAsync(postId, now, now));
        }
    }
}