namespace Fifthweek.Api.Subscriptions.Commands
{
    using System;
    using System.Threading.Tasks;

    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Subscriptions.Shared;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;

    [AutoConstructor]
    public partial class UpdateSubscriptionCommandHandler : ICommandHandler<UpdateSubscriptionCommand>
    {
        private readonly ISubscriptionSecurity subscriptionSecurity;
        private readonly IFileSecurity fileSecurity;
        private readonly IRequesterSecurity requesterSecurity;
        private readonly IFifthweekDbConnectionFactory connectionFactory;

        public async Task HandleAsync(UpdateSubscriptionCommand command)
        {
            command.AssertNotNull("command");

            var authenticatedUserId = await this.requesterSecurity.AuthenticateAsync(command.Requester);

            await this.subscriptionSecurity.AssertWriteAllowedAsync(authenticatedUserId, command.SubscriptionId);

            if (command.HeaderImageFileId != null)
            {
                await this.fileSecurity.AssertReferenceAllowedAsync(authenticatedUserId, command.HeaderImageFileId);
            }

            await this.UpdateSubscriptionAsync(command);
        }

        private async Task UpdateSubscriptionAsync(UpdateSubscriptionCommand command)
        {
            var subscription = new Subscription(
                command.SubscriptionId.Value,
                default(Guid),
                null,
                command.SubscriptionName == null ? null : command.SubscriptionName.Value,
                command.Tagline == null ? null : command.Tagline.Value,
                command.Introduction == null ? null : command.Introduction.Value,
                command.Description == null ? null : command.Description.Value,
                command.Video == null ? null : command.Video.Value,
                command.HeaderImageFileId == null ? (Guid?)null : command.HeaderImageFileId.Value,
                null,
                default(DateTime));

            using (var connection = this.connectionFactory.CreateConnection())
            {
                await connection.UpdateAsync(
                    subscription,
                    Subscription.Fields.Name |
                    Subscription.Fields.Tagline |
                    Subscription.Fields.Introduction |
                    Subscription.Fields.Description |
                    Subscription.Fields.ExternalVideoUrl |
                    Subscription.Fields.HeaderImageFileId);
            }
        }
    }
}