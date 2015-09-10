namespace Fifthweek.Api.Channels.Commands
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;

    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;

    [AutoConstructor]
    public partial class UpdateChannelCommandHandler : ICommandHandler<UpdateChannelCommand>
    {
        private readonly IRequesterSecurity requesterSecurity;
        private readonly IChannelSecurity channelSecurity;
        private readonly IUpdateChannelDbStatement updateChannel;

        public async Task HandleAsync(UpdateChannelCommand command)
        {
            command.AssertNotNull("command");

            var userId = await this.requesterSecurity.AuthenticateAsync(command.Requester);
            await this.channelSecurity.AssertWriteAllowedAsync(userId, command.ChannelId);

            await this.updateChannel.ExecuteAsync(
                userId,
                command.ChannelId,
                command.Name,
                command.Price,
                command.IsVisibleToNonSubscribers,
                DateTime.UtcNow);
        }
    }
}