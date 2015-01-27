namespace Fifthweek.Api.Identity.Membership.Commands
{
    using System.Threading.Tasks;

    using Fifthweek.Api.Core;

    public class UpdateLastAccessTokenDateCommandHandler : ICommandHandler<UpdateLastAccessTokenDateCommand>
    {
        private readonly IUpdateUserTimeStampsDbStatement updateUserTimeStamps;

        public UpdateLastAccessTokenDateCommandHandler(
            IUpdateUserTimeStampsDbStatement updateUserTimeStamps)
        {
            this.updateUserTimeStamps = updateUserTimeStamps;
        }

        public async Task HandleAsync(UpdateLastAccessTokenDateCommand command)
        {
            if (command.CreationType == UpdateLastAccessTokenDateCommand.AccessTokenCreationType.SignIn)
            {
                await this.updateUserTimeStamps.UpdateSignInAndAccessTokenAsync(command.UserId, command.Timestamp);
            }
            else
            {
                await this.updateUserTimeStamps.UpdateAccessTokenAsync(command.UserId, command.Timestamp);
            }
        }
    }
}