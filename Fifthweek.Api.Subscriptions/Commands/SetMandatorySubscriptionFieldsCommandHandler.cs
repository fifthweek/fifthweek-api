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

            await this.subscriptionSecurity.CanUpdateAsync(command.Requester, command.SubscriptionId);
            
            //command.SubscriptionId

            throw new NotImplementedException();
        }
    }
}