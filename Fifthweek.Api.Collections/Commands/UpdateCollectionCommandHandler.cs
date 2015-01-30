namespace Fifthweek.Api.Collections.Commands
{
    using System.Linq;
    using System.Threading.Tasks;
    using System.Transactions;

    using Dapper;

    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.CodeGeneration;

    [AutoConstructor]
    public partial class UpdateCollectionCommandHandler : ICommandHandler<UpdateCollectionCommand>
    {
        private static readonly string DeleteWeeklyReleaseTimesSql = string.Format(
            @"
            DELETE FROM {0}
            WHERE {1} = @CollectionId",
            WeeklyReleaseTime.Table,
            WeeklyReleaseTime.Fields.CollectionId);

        private readonly IRequesterSecurity requesterSecurity;
        private readonly ICollectionSecurity collectionSecurity;
        private readonly IChannelSecurity channelSecurity;
        private readonly IFifthweekDbContext databaseContext;

        public async Task HandleAsync(UpdateCollectionCommand command)
        {
            command.AssertNotNull("command");

            var userId = await this.requesterSecurity.AuthenticateAsync(command.Requester);
            await this.collectionSecurity.AssertWriteAllowedAsync(userId, command.CollectionId);
            await this.channelSecurity.AssertWriteAllowedAsync(userId, command.ChannelId);

            await this.UpdateCollectionAsync(command);
        }

        private async Task UpdateCollectionAsync(UpdateCollectionCommand command)
        {
            var newWeeklyReleaseTimes = command.WeeklyReleaseSchedule.Value.Select(
                _ => new WeeklyReleaseTime(command.CollectionId.Value, null, _.Value));

            var collectionIdParameter = new
            {
                CollectionId = command.CollectionId.Value
            };

            var collection = new Collection(command.CollectionId.Value)
            {
                Name = command.Name.Value,
                ChannelId = command.ChannelId.Value
            };

            var updatedFields = 
                Collection.Fields.Name | 
                Collection.Fields.ChannelId;

            // Team vote: we do not wrap the following in a transaction. It does not cause a breaking inconsistency if only 
            // the collection entity is updated without the new weekly release times. The user will notice the inconsistency
            // and retry if they desire.
            await this.databaseContext.Database.Connection.UpdateAsync(collection, updatedFields);

            // Transaction required on the following, as database must always contain at least one weekly release time per 
            // collection. The absence of weekly release times would cause a breaking inconsistency.
            using (var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                await this.databaseContext.Database.Connection.ExecuteAsync(DeleteWeeklyReleaseTimesSql, collectionIdParameter);
                await this.databaseContext.Database.Connection.InsertAsync(newWeeklyReleaseTimes);

                transaction.Complete();
            }
        }
    }
}