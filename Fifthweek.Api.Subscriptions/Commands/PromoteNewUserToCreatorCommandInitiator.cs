namespace Fifthweek.Api.Subscriptions.Commands
{
    using System.Threading.Tasks;

    using Fifthweek.Api.Core;
    using Fifthweek.CodeGeneration;

    using UserRegisteredEvent = Fifthweek.Api.Identity.Shared.Membership.Events.UserRegisteredEvent;

    [AutoConstructor]
    public partial class PromoteNewUserToCreatorCommandInitiator : IEventHandler<UserRegisteredEvent>
    {
        private readonly ICommandHandler<PromoteNewUserToCreatorCommand> promoteNewUserToCreator;

        public Task HandleAsync(UserRegisteredEvent @event)
        {
            return this.promoteNewUserToCreator.HandleAsync(
                new PromoteNewUserToCreatorCommand(@event.UserId));
        }
    }
}