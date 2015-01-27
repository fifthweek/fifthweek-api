namespace Fifthweek.Api.Subscriptions.Commands
{
    using System.Threading.Tasks;

    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Membership.Events;
    using Fifthweek.CodeGeneration;

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