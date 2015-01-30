namespace Fifthweek.Api.Identity.OAuth.Commands
{
    using System.Threading.Tasks;

    using Fifthweek.Api.Core;
    using Fifthweek.CodeGeneration;

    [AutoConstructor]
    public partial class RemoveRefreshTokenCommandHandler : ICommandHandler<RemoveRefreshTokenCommand>
    {
        private readonly IRemoveRefreshTokenDbStatement removeRefreshToken;

        public async Task HandleAsync(RemoveRefreshTokenCommand command)
        {
            command.AssertNotNull("command");

            await this.removeRefreshToken.ExecuteAsync(command.HashedRefreshTokenId);
        }
    }
}