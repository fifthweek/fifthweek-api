namespace Fifthweek.Api.Subscriptions.Commands
{
    using System;
    using System.Threading.Tasks;

    using Fifthweek.Api.Core;
    using Fifthweek.Api.Persistence;

    [AutoConstructor]
    public partial class CreateSubscriptionCommandHandler : ICommandHandler<CreateSubscriptionCommand>
    {
        private readonly IFifthweekDbContext fifthweekDbContext;
        private readonly ISubscriptionSecurity subscriptionSecurity;

        public async Task HandleAsync(CreateSubscriptionCommand command)
        {
            if (command == null)
            {
                throw new ArgumentNullException("command");
            }

            await this.subscriptionSecurity.AssertCreationAllowedAsync(command.Requester);

            await this.CreateSubscriptionAsync(command);
            await this.CreateChannelAsync(command);
        }

        private Task CreateSubscriptionAsync(CreateSubscriptionCommand command)
        {
            var subscription = new Subscription(
                command.NewSubscriptionId.Value,
                command.Requester.Value,
                null,
                command.SubscriptionName.Value,
                command.Tagline.Value,
                Introduction.Default.Value,
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