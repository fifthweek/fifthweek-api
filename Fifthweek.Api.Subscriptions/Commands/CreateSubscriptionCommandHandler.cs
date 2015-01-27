namespace Fifthweek.Api.Subscriptions.Commands
{
    using System;
    using System.Threading.Tasks;
    using System.Transactions;

    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Membership;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.CodeGeneration;

    using UserId = Fifthweek.Api.Identity.Shared.Membership.UserId;

    [AutoConstructor]
    public partial class CreateSubscriptionCommandHandler : ICommandHandler<CreateSubscriptionCommand>
    {
        private readonly ISubscriptionSecurity subscriptionSecurity;
        private readonly IRequesterSecurity requesterSecurity;
        private readonly IFifthweekDbContext fifthweekDbContext;

        public async Task HandleAsync(CreateSubscriptionCommand command)
        {
            command.AssertNotNull("command");

            var authenticatedUserId = await this.requesterSecurity.AuthenticateAsync(command.Requester);

            await this.subscriptionSecurity.AssertCreationAllowedAsync(authenticatedUserId);

            await this.CreateEntitiesAsync(command, authenticatedUserId);
        }

        private async Task CreateEntitiesAsync(CreateSubscriptionCommand command, UserId authenticatedUserId)
        {
            var subscription = new Subscription(
                command.NewSubscriptionId.Value,
                authenticatedUserId.Value,
                null,
                command.SubscriptionName.Value,
                command.Tagline.Value,
                ValidIntroduction.Default.Value,
                null,
                null,
                null,
                null,
                DateTime.UtcNow);

            var channel = new Channel(
                command.NewSubscriptionId.Value, // Default channel uses same ID as subscription.
                command.NewSubscriptionId.Value,
                null,
                string.Empty,
                command.BasePrice.Value,
                DateTime.UtcNow);

            // Assuming no lock escalation, this transaction will hold X locks on the new rows and IX locks further up the hierarchy.
            using (var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                await this.fifthweekDbContext.Database.Connection.InsertAsync(subscription);
                await this.fifthweekDbContext.Database.Connection.InsertAsync(channel);

                transaction.Complete();
            }
        }
    }
}