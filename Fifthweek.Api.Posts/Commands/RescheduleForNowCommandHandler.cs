namespace Fifthweek.Api.Posts.Commands
{
    using System;
    using System.Threading.Tasks;

    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Posts.Shared;
    using Fifthweek.CodeGeneration;

    [AutoConstructor]
    public partial class RescheduleForNowCommandHandler : ICommandHandler<RescheduleForNowCommand>
    {
        private readonly IRequesterSecurity requesterSecurity;
        private readonly IPostSecurity postSecurity;
        private readonly ISetBacklogPostLiveDateToNowDbStatement setBacklogPostLiveDateToNow;

        public async Task HandleAsync(RescheduleForNowCommand command)
        {
            command.AssertNotNull("command");

            var userId = await this.requesterSecurity.AuthenticateAsync(command.Requester);
            await this.postSecurity.AssertWriteAllowedAsync(userId, command.PostId);

            var now = DateTime.UtcNow;
            await this.setBacklogPostLiveDateToNow.ExecuteAsync(command.PostId, now);
        }
    }
}