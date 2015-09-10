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
    public partial class DeleteQueueCommandHandler : ICommandHandler<DeleteQueueCommand>
    {
        private readonly IRequesterSecurity requesterSecurity;
        private readonly IQueueSecurity queueSecurity;
        private readonly IDeleteQueueDbStatement deleteQueue;
        private readonly IScheduleGarbageCollectionStatement scheduleGarbageCollection;

        public async Task HandleAsync(DeleteQueueCommand command)
        {
            command.AssertNotNull("command");

            var userId = await this.requesterSecurity.AuthenticateAsync(command.Requester);
            await this.queueSecurity.AssertWriteAllowedAsync(userId, command.QueueId);

            await this.deleteQueue.ExecuteAsync(command.QueueId);
            await this.scheduleGarbageCollection.ExecuteAsync();
        }
    }
}