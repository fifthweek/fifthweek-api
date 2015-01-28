namespace Fifthweek.Api.Collections.Commands
{
    using System;
    using System.Threading.Tasks;
    using System.Transactions;

    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;

    [AutoConstructor]
    public partial class CreateCollectionCommandHandler : ICommandHandler<CreateCollectionCommand>
    {
        // This is an exclusive lower-bound. To achieve and inclusive lower-bound of 1st April, we set the time to 1 minute before.
        private static readonly DateTime DefaultQueueLowerBound = new DateTime(2015, 3, 31, 23, 59, 0, DateTimeKind.Utc);

        private readonly IRequesterSecurity requesterSecurity;
        private readonly IChannelSecurity channelSecurity;
        private readonly IFifthweekDbContext databaseContext;
        private readonly IRandom random;

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

            // Spread default release dates so posts are not delivered on same date as standard.
            var hourOfWeek = (byte)this.random.Next(HourOfWeek.MinValue, HourOfWeek.MaxValue + 1);

            var releaseDate = new WeeklyReleaseTime(
                command.NewCollectionId.Value,
                null,
                hourOfWeek);

            // Assuming no lock escalation, this transaction will hold X locks on the new rows and IX locks further up the hierarchy,
            // so no deadlocks are to be expected.
            using (var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                await this.databaseContext.Database.Connection.InsertAsync(collection);
                await this.databaseContext.Database.Connection.InsertAsync(releaseDate);

                transaction.Complete();
            }
        }
    }
}