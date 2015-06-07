namespace Fifthweek.Api.Blogs.Commands
{
    using System;
    using System.Threading.Tasks;

    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Payments.Services;
    using Fifthweek.Shared;

    [AutoConstructor]
    public partial class AcceptChannelSubscriptionPriceChangeCommandHandler : ICommandHandler<AcceptChannelSubscriptionPriceChangeCommand>
    {
        private readonly IRequesterSecurity requesterSecurity;
        private readonly IAcceptChannelSubscriptionPriceChangeDbStatement acceptPriceChange;
        
        public async Task HandleAsync(AcceptChannelSubscriptionPriceChangeCommand command)
        {
            command.AssertNotNull("command");

            var authenticatedUserId = await this.requesterSecurity.AuthenticateAsync(command.Requester);

            await this.acceptPriceChange.ExecuteAsync(
                authenticatedUserId,
                command.ChannelId,
                command.AcceptedPrice,
                DateTime.UtcNow);
        }
    }
}