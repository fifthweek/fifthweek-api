namespace Fifthweek.Api.Blogs.Commands
{
    using System;
    using System.Threading.Tasks;

    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;

    [AutoConstructor]
    public partial class UpdateBlogSubscriptionsCommandHandler : ICommandHandler<UpdateBlogSubscriptionsCommand>
    {
        private readonly IRequesterSecurity requesterSecurity;
        private readonly IUpdateBlogSubscriptionsDbStatement updateBlogSubscriptions;
        
        public async Task HandleAsync(UpdateBlogSubscriptionsCommand command)
        {
            command.AssertNotNull("command");

            var authenticatedUserId = await this.requesterSecurity.AuthenticateAsync(command.Requester);

            await this.updateBlogSubscriptions.ExecuteAsync(
                    authenticatedUserId,
                    command.BlogId,
                    command.Channels,
                    DateTime.UtcNow);
        }
    }
}