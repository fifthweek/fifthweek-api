namespace Fifthweek.Api.Collections.Commands
{
    using System.Threading.Tasks;

    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;

    [AutoConstructor]
    public partial class DeleteCollectionCommandHandler : ICommandHandler<DeleteCollectionCommand>
    {
        private readonly IRequesterSecurity requesterSecurity;
        private readonly ICollectionSecurity collectionSecurity;
        private readonly IDeleteCollectionDbStatement deleteCollection;
        private readonly IScheduleGarbageCollectionStatement scheduleGarbageCollection;

        public async Task HandleAsync(DeleteCollectionCommand command)
        {
            command.AssertNotNull("command");

            var userId = await this.requesterSecurity.AuthenticateAsync(command.Requester);
            await this.collectionSecurity.AssertWriteAllowedAsync(userId, command.CollectionId);

            await this.deleteCollection.ExecuteAsync(command.CollectionId);
            await this.scheduleGarbageCollection.ExecuteAsync();
        }
    }
}