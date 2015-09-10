namespace Fifthweek.Api.Blogs.Commands
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
    using Fifthweek.Payments.SnapshotCreation;
    using Fifthweek.Shared;

    [AutoConstructor]
    public partial class CreateBlogCommandHandler : ICommandHandler<CreateBlogCommand>
    {
        private readonly IBlogSecurity blogSecurity;
        private readonly IRequesterSecurity requesterSecurity;
        private readonly IFifthweekDbConnectionFactory connectionFactory;
        private readonly IRequestSnapshotService requestSnapshot;

        public async Task HandleAsync(CreateBlogCommand command)
        {
            command.AssertNotNull("command");

            var authenticatedUserId = await this.requesterSecurity.AuthenticateAsync(command.Requester);

            await this.blogSecurity.AssertCreationAllowedAsync(command.Requester);

            await this.CreateEntitiesAsync(command, authenticatedUserId);
        }

        private async Task CreateEntitiesAsync(CreateBlogCommand command, UserId authenticatedUserId)
        {
            var now = DateTime.UtcNow;
            var blog = new Blog(
                command.NewBlogId.Value,
                authenticatedUserId.Value,
                null,
                command.BlogName.Value,
                command.Introduction.Value,
                null,
                null,
                null,
                null,
                now);

            var channel = new Channel(
                command.FirstChannelId.Value,
                command.NewBlogId.Value,
                null,
                command.BlogName.Value,
                command.BasePrice.Value,
                true,
                now, 
                now);

            // Assuming no lock escalation, this transaction will hold X locks on the new rows and IX locks further up the hierarchy,
            // so no deadlocks are to be expected.
            using (var transaction = TransactionScopeBuilder.CreateAsync())
            {
                using (var connection = this.connectionFactory.CreateConnection())
                {
                    await connection.InsertAsync(blog);
                    await connection.InsertAsync(channel);
                }

                await this.requestSnapshot.ExecuteAsync(authenticatedUserId, SnapshotType.CreatorChannels);

                transaction.Complete();
            }
        }
    }
}