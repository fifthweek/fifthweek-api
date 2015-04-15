namespace Fifthweek.Api.Blogs.Commands
{
    using System;
    using System.Threading.Tasks;

    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;

    [AutoConstructor]
    public partial class UnsubscribeFromChannelCommandHandler : ICommandHandler<UnsubscribeFromChannelCommand>
    {
        private readonly IRequesterSecurity requesterSecurity;
        private readonly IUnsubscribeFromChannelDbStatement unsubscribeFromChannel;

        public async Task HandleAsync(UnsubscribeFromChannelCommand command)
        {
            command.AssertNotNull("command");

            var authenticatedUserId = await this.requesterSecurity.AuthenticateAsync(command.Requester);

            await this.unsubscribeFromChannel.ExecuteAsync(
                authenticatedUserId,
                command.ChannelId);
        }
    }
}