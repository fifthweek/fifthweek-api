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
    public partial class UpdateCollectionCommandHandler : ICommandHandler<UpdateCollectionCommand>
    {
        private readonly IRequesterSecurity requesterSecurity;
        private readonly ICollectionSecurity collectionSecurity;
        private readonly IUpdateCollectionFieldsDbStatement updateCollectionFields;
        private readonly IUpdateWeeklyReleaseScheduleDbStatement updateWeeklyReleaseSchedule;

        public async Task HandleAsync(UpdateCollectionCommand command)
        {
            command.AssertNotNull("command");

            var userId = await this.requesterSecurity.AuthenticateAsync(command.Requester);
            await this.collectionSecurity.AssertWriteAllowedAsync(userId, command.CollectionId);

            // We do not wrap the following two operations in a transaction. It does not cause a breaking inconsistency if only 
            // the collection fields are updated without the new weekly release times. The user will notice the inconsistency
            // and retry if they desire.
            await this.updateCollectionFields.ExecuteAsync(new Collection(command.CollectionId.Value)
            {
                Name = command.Name.Value,
            });

            await this.updateWeeklyReleaseSchedule.ExecuteAsync(command.CollectionId, command.WeeklyReleaseSchedule, DateTime.UtcNow);
        }
    }
}