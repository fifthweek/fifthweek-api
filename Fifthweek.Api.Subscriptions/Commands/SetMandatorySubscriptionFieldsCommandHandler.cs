using System;
using System.Threading.Tasks;
using Fifthweek.Api.Core;

namespace Fifthweek.Api.Subscriptions.Commands
{
    [AutoConstructor]
    public partial class SetMandatorySubscriptionFieldsCommandHandler : ICommandHandler<SetMandatorySubscriptionFieldsCommand>
    {
        private readonly ISubscriptionSecurity subscriptionSecurity;
        private ISubscriptionSecurity subscriptionSecurity2;

        public Task HandleAsync(SetMandatorySubscriptionFieldsCommand command)
        {
            if (command == null)
            {
                throw new ArgumentNullException("command");
            }
            
            //command.SubscriptionId

            throw new NotImplementedException();
        }
    }

    public interface ISubscriptionSecurity
    {
         
    }
}