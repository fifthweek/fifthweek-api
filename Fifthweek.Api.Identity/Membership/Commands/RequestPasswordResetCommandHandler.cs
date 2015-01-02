namespace Fifthweek.Api.Identity.Membership.Commands
{
    using System;
    using System.Threading.Tasks;

    using Fifthweek.Api.Core;

    public class RequestPasswordResetCommandHandler : ICommandHandler<RequestPasswordResetCommand>
    {
        private readonly IUserManager userManager;

        public RequestPasswordResetCommandHandler(IUserManager userManager)
        {
            this.userManager = userManager;
        }

        public async Task HandleAsync(RequestPasswordResetCommand command)
        {
            throw new NotImplementedException();
        }
    }
}