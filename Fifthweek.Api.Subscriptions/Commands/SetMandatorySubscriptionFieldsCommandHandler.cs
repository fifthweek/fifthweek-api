using System;
using System.Threading.Tasks;
using Fifthweek.Api.Core;

namespace Fifthweek.Api.Subscriptions.Commands
{
    [AutoConstructor]
    public partial class SetMandatorySubscriptionFieldsCommandHandler : ICommandHandler<SetMandatorySubscriptionFieldsCommand>
    {
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
            //command.SubscriptionId

            throw new NotImplementedException();
        }
    }
}