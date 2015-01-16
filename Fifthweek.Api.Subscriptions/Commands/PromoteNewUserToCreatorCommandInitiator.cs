using System.Threading.Tasks;
using Fifthweek.Api.Core;
using Fifthweek.Api.Identity.Membership.Events;

namespace Fifthweek.Api.Subscriptions.Commands
{
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;

    [AutoConstructor]
    public partial class PromoteNewUserToCreatorCommandInitiator : IEventHandler<UserRegisteredEvent>
    {
        private readonly ICommandHandler<PromoteNewUserToCreatorCommand> promoteNewUserToCreator;

        public Task HandleAsync(UserRegisteredEvent @event)
        {
            return promoteNewUserToCreator.HandleAsync(new PromoteNewUserToCreatorCommand(
                @event.UserId
            ));
        }
    }
}