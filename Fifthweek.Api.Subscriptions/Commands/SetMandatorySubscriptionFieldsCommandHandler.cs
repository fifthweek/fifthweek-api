using System;
using System.Threading.Tasks;
using Fifthweek.Api.Core;
using Fifthweek.Api.Persistence;

namespace Fifthweek.Api.Subscriptions.Commands
{
    [AutoConstructor]
    public partial class SetMandatorySubscriptionFieldsCommandHandler : ICommandHandler<SetMandatorySubscriptionFieldsCommand>
    {
        private readonly IFifthweekDbContext fifthweekDbContext;
        private readonly ISubscriptionSecurity subscriptionSecurity;

        public async Task HandleAsync(SetMandatorySubscriptionFieldsCommand command)
        {
            if (command == null)
            {
                throw new ArgumentNullException("command");
            }

            var isUpdateAllowed = await this.subscriptionSecurity.IsUpdateAllowedAsync(command.Requester, command.SubscriptionId);
            if (!isUpdateAllowed)
            {
                throw new RecoverableException("Not allowed to update subscription");
            }

            await this.UpdateSubscriptionAsync(command);
            await this.CreateChannelAsync(command);
        }

        private Task UpdateSubscriptionAsync(SetMandatorySubscriptionFieldsCommand command)
        {
            var subscription = new Subscription
            {
                Id = command.SubscriptionId.Value,
                Name = command.SubscriptionName.Value,
                Tagline = command.Tagline.Value
            };

            return this.fifthweekDbContext.Database.Connection.UpdateAsync(subscription, Subscription.Fields.Name | Subscription.Fields.Tagline);
        }

        private Task CreateChannelAsync(SetMandatorySubscriptionFieldsCommand command)
        {
            var channel = new Channel
            {
                Id = command.SubscriptionId.Value, // Default channel uses same ID as subscription.
                SubscriptionId = command.SubscriptionId.Value,
                PriceInUsCentsPerWeek = command.BasePrice.Value,
                CreationDate = DateTime.UtcNow
            };

            return this.fifthweekDbContext.Database.Connection.UpsertAsync(channel, Channel.Fields.PriceInUsCentsPerWeek);
        }
    }
}