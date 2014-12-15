namespace Fifthweek.Api.CommandHandlers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Fifthweek.Api.Commands;
    using Fifthweek.Api.Repositories;

    public class UpdateLastAccessTokenDateCommandHandler : ICommandHandler<UpdateLastAccessTokenDateCommand>
    {
        private readonly IUserManager userManager;

        public UpdateLastAccessTokenDateCommandHandler(
            IUserManager userManager)
        {
            this.userManager = userManager;
        }

        public async Task HandleAsync(UpdateLastAccessTokenDateCommand command)
        {
            var user = await this.userManager.FindByNameAsync(command.Username);
            if (user == null)
            {
                throw new Exception("The username '" + command.Username + "' was not found when updating last access token date.");
            }

            if (command.CreationType == UpdateLastAccessTokenDateCommand.AccessTokenCreationType.SignIn)
            {
                user.LastSignInDate = command.Timestamp;
            }

            user.LastAccessTokenDate = command.Timestamp;

            var result = await this.userManager.UpdateAsync(user);

            if (!result.Succeeded)
            {
                throw new AggregateException("Failed to update timestamps for user " + command.Username, result.Errors.Select(v => new Exception(v)));
            }
        }
    }
}