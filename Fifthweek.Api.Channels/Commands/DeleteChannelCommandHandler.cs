namespace Fifthweek.Api.Channels.Commands
{
    using System.Threading.Tasks;

    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;

    [AutoConstructor]
    public partial class DeleteChannelCommandHandler : ICommandHandler<DeleteChannelCommand>
    {
        private readonly IRequesterSecurity requesterSecurity;
        private readonly IChannelSecurity collectionSecurity;
        private readonly IDeleteChannelDbStatement deleteChannel;
        private readonly IScheduleGarbageCollectionStatement scheduleGarbageCollection;

        public async Task HandleAsync(DeleteChannelCommand command)
        {
            command.AssertNotNull("command");

            var userId = await this.requesterSecurity.AuthenticateAsync(command.Requester);
            await this.collectionSecurity.AssertWriteAllowedAsync(userId, command.ChannelId);

            await this.deleteChannel.ExecuteAsync(command.ChannelId);
            await this.scheduleGarbageCollection.ExecuteAsync();
        }
    }
}