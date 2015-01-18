namespace Fifthweek.Api.Subscriptions.Commands
{
    using System;
    using System.Threading.Tasks;

    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.CodeGeneration;

    [AutoConstructor]
    public partial class CreateSubscriptionCommandHandler : ICommandHandler<CreateSubscriptionCommand>
    {
        private readonly ISubscriptionSecurity subscriptionSecurity;
        private readonly IFifthweekDbContext fifthweekDbContext;

        public async Task HandleAsync(CreateSubscriptionCommand command)
        {
            command.AssertNotNull("command");

            UserId authenticatedUserId;
            command.Requester.AssertAuthenticated(out authenticatedUserId);

            await this.subscriptionSecurity.AssertCreationAllowedAsync(authenticatedUserId);

            await this.CreateSubscriptionAsync(command, authenticatedUserId);
            await this.CreateChannelAsync(command);
        }

        private Task CreateSubscriptionAsync(CreateSubscriptionCommand command, UserId authenticatedUserId)
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

            return this.fifthweekDbContext.Database.Connection.InsertAsync(subscription); 
        }

        private Task CreateChannelAsync(CreateSubscriptionCommand command)
        {
            var channel = new Channel(
                command.NewSubscriptionId.Value, // Default channel uses same ID as subscription.
                command.NewSubscriptionId.Value,
                null,
                command.BasePrice.Value,
                DateTime.UtcNow);

            return this.fifthweekDbContext.Database.Connection.InsertAsync(channel);
        }
    }
}