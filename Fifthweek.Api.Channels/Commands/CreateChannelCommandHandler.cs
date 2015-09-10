namespace Fifthweek.Api.Channels.Commands
{
    using System;
    using System.Threading.Tasks;

    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Payments.SnapshotCreation;
    using Fifthweek.Shared;

    [AutoConstructor]
    public partial class CreateChannelCommandHandler : ICommandHandler<CreateChannelCommand>
    {
        private readonly IRequesterSecurity requesterSecurity;
        private readonly IBlogSecurity blogSecurity;
        private readonly IFifthweekDbConnectionFactory connectionFactory;
        private readonly IRequestSnapshotService requestSnapshot;

        public async Task HandleAsync(CreateChannelCommand command)
        {
            command.AssertNotNull("command");

            var userId = await this.requesterSecurity.AuthenticateAsync(command.Requester);
            await this.blogSecurity.AssertWriteAllowedAsync(userId, command.BlogId);

            await this.CreateChannelAsync(userId, command);
        }

        private async Task CreateChannelAsync(UserId userId, CreateChannelCommand command)
        {
            var now = DateTime.UtcNow;
            var channel = new Channel(
                command.NewChannelId.Value,
                command.BlogId.Value,
                null,
                command.Name.Value,
                command.Price.Value,
                command.IsVisibleToNonSubscribers,
                now,
                now);


            using (var transaction = TransactionScopeBuilder.CreateAsync())
            {
                using (var connection = this.connectionFactory.CreateConnection())
                {
                    await connection.InsertAsync(channel);
                }

                await this.requestSnapshot.ExecuteAsync(userId, SnapshotType.CreatorChannels);

                transaction.Complete();
            }
        }
    }
}