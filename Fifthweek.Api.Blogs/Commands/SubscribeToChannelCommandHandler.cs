namespace Fifthweek.Api.Blogs.Commands
{
    using System;
    using System.Threading.Tasks;

    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;

    [AutoConstructor]
    public partial class SubscribeToChannelCommandHandler : ICommandHandler<SubscribeToChannelCommand>
    {
        private readonly IRequesterSecurity requesterSecurity;
        private readonly IUpdateChannelSubscriptionDbStatement updateChannelSubscription;
        private readonly IGetIsTestUserChannelDbStatement getIsTestUserChannel;

        public async Task HandleAsync(SubscribeToChannelCommand command)
        {
            command.AssertNotNull("command");

            var authenticatedUserId = await this.requesterSecurity.AuthenticateAsync(command.Requester);

            var isTestUser = await this.requesterSecurity.IsInRoleAsync(command.Requester, FifthweekRole.TestUser);
            if (isTestUser)
            {
                var isTestUserChannel = await this.getIsTestUserChannel.ExecuteAsync(command.ChannelId);
                if (!isTestUserChannel)
                {
                    // This is to prevent a security hole where people would be able to sign up
                    // as a test user and view standard user's content for free (as test user credit is
                    // not processed in the same way as standard user credit).
                    throw new UnauthorizedException(
                        "Test users are not authorized to subscribe to blogs created by standard users.");
                }
            }

            await this.updateChannelSubscription.ExecuteAsync(
                authenticatedUserId,
                command.ChannelId,
                command.AcceptedPrice,
                command.Timestamp);
        }
    }
}