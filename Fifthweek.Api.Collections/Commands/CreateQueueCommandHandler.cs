namespace Fifthweek.Api.Collections.Commands
{
    using System;
    using System.Threading.Tasks;
    using System.Transactions;

    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;

    [AutoConstructor]
    public partial class CreateQueueCommandHandler : ICommandHandler<CreateQueueCommand>
    {
        private readonly IRequesterSecurity requesterSecurity;
        private readonly IBlogSecurity blogSecurity;
        private readonly IFifthweekDbConnectionFactory connectionFactory;

        public async Task HandleAsync(CreateQueueCommand command)
        {
            command.AssertNotNull("command");

            var userId = await this.requesterSecurity.AuthenticateAsync(command.Requester);
            await this.blogSecurity.AssertWriteAllowedAsync(userId, command.BlogId);

            await this.CreateQueueAsync(command);
        }

        private async Task CreateQueueAsync(CreateQueueCommand command)
        {
            var queue = new Queue(
                command.NewQueueId.Value,
                command.BlogId.Value,
                null,
                command.Name.Value,
                DateTime.UtcNow);

            var releaseDate = new WeeklyReleaseTime(
                command.NewQueueId.Value,
                null,
                (byte)command.InitialWeeklyReleaseTime.Value);

            // Assuming no lock escalation, this transaction will hold X locks on the new rows and IX locks further up the hierarchy,
            // so no deadlocks are to be expected.
            using (var transaction = TransactionScopeBuilder.CreateAsync())
            {
                using (var connection = this.connectionFactory.CreateConnection())
                {
                    await connection.InsertAsync(queue);
                    await connection.InsertAsync(releaseDate);
                }

                transaction.Complete();
            }
        }
    }
}