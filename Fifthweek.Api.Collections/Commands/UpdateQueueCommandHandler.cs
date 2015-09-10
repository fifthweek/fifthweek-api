namespace Fifthweek.Api.Collections.Commands
{
    using System;
    using System.Threading.Tasks;

    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;

    [AutoConstructor]
    public partial class UpdateQueueCommandHandler : ICommandHandler<UpdateQueueCommand>
    {
        private readonly IRequesterSecurity requesterSecurity;
        private readonly IQueueSecurity queueSecurity;
        private readonly IUpdateQueueFieldsDbStatement updateQueueFields;
        private readonly IUpdateWeeklyReleaseScheduleDbStatement updateWeeklyReleaseSchedule;

        public async Task HandleAsync(UpdateQueueCommand command)
        {
            command.AssertNotNull("command");

            var userId = await this.requesterSecurity.AuthenticateAsync(command.Requester);
            await this.queueSecurity.AssertWriteAllowedAsync(userId, command.QueueId);

            // We do not wrap the following two operations in a transaction. It does not cause a breaking inconsistency if only 
            // the queue fields are updated without the new weekly release times. The user will notice the inconsistency
            // and retry if they desire.
            await this.updateQueueFields.ExecuteAsync(new Queue(command.QueueId.Value)
            {
                Name = command.Name.Value,
            });

            await this.updateWeeklyReleaseSchedule.ExecuteAsync(command.QueueId, command.WeeklyReleaseSchedule, DateTime.UtcNow);
        }
    }
}