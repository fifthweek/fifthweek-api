using System;
using System.Threading.Tasks;
using Fifthweek.Api.Core;
using Fifthweek.Api.Persistence;

namespace Fifthweek.Api.Subscriptions.Commands
{
    [AutoConstructor]
    public partial class PromoteNewUserToCreatorCommandHandler : ICommandHandler<PromoteNewUserToCreatorCommand>
    {
        private readonly IFifthweekDbContext fifthweekDbContext;

        public async Task HandleAsync(PromoteNewUserToCreatorCommand command)
        {
            if (command == null)
            {
                throw new ArgumentNullException("command");
            }

            await this.CreateSubscriptionAsync(command);
        }

        private Task CreateSubscriptionAsync(PromoteNewUserToCreatorCommand command)
        {
            var subscription = new Subscription
            {
                Id = command.NewUserId.Value, // Default subscription uses same ID as creator.
                CreatorId = command.NewUserId.Value,
                CreationDate = DateTime.UtcNow
            };

            return this.fifthweekDbContext.Database.Connection.InsertAsync(subscription);
        }
    }
}