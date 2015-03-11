namespace Fifthweek.Api.Collections.Commands
{
    using System;
    using System.Threading.Tasks;
    using System.Transactions;

    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.CodeGeneration;

    [AutoConstructor]
    public partial class CreateCollectionCommandHandler : ICommandHandler<CreateCollectionCommand>
    {
        // This is an exclusive lower-bound. To achieve and inclusive lower-bound of 1st June, we set the time to 1 minute before.
        private static readonly DateTime DefaultQueueLowerBound = new DateTime(2015, 5, 31, 23, 59, 0, DateTimeKind.Utc);

        private readonly IRequesterSecurity requesterSecurity;
        private readonly IChannelSecurity channelSecurity;
        private readonly IFifthweekDbConnectionFactory connectionFactory;

        public async Task HandleAsync(CreateCollectionCommand command)
        {
            command.AssertNotNull("command");

            var userId = await this.requesterSecurity.AuthenticateAsync(command.Requester);
            await this.channelSecurity.AssertWriteAllowedAsync(userId, command.ChannelId);

            await this.CreateCollectionAsync(command);
        }

        private async Task CreateCollectionAsync(CreateCollectionCommand command)
        {
            var collection = new Collection(
                command.NewCollectionId.Value,
                command.ChannelId.Value,
                null,
                command.Name.Value,
                DefaultQueueLowerBound,
                DateTime.UtcNow);

            var releaseDate = new WeeklyReleaseTime(
                command.NewCollectionId.Value,
                null,
                command.InitialWeeklyReleaseTime.Value);

            // Assuming no lock escalation, this transaction will hold X locks on the new rows and IX locks further up the hierarchy,
            // so no deadlocks are to be expected.
            using (var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                using (var connection = this.connectionFactory.CreateConnection())
                {
                    await connection.InsertAsync(collection);
                    await connection.InsertAsync(releaseDate);
                }

                transaction.Complete();
            }
        }
    }
}