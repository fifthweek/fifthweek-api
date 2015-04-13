namespace Fifthweek.Api.Blogs.Commands
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;

    [AutoConstructor]
    public partial class UpdateFreeAccessUsersCommandHandler : ICommandHandler<UpdateFreeAccessUsersCommand>
    {
        private readonly IBlogSecurity blogSecurity;
        private readonly IRequesterSecurity requesterSecurity;
        private readonly IUpdateFreeAccessUsersDbStatement updateFreeAccessUsers;

        public async Task HandleAsync(UpdateFreeAccessUsersCommand command)
        {
            command.AssertNotNull("command");

            var authenticatedUserId = await this.requesterSecurity.AuthenticateAsync(command.Requester);
            await this.blogSecurity.AssertWriteAllowedAsync(authenticatedUserId, command.BlogId);

            // Remove duplicates
            var emailAddresses = new HashSet<ValidEmail>();
            foreach (var item in command.EmailAddresses)
            {
                emailAddresses.Add(item);
            }

            await this.updateFreeAccessUsers.ExecuteAsync(command.BlogId, emailAddresses.ToList());
        }
    }
}