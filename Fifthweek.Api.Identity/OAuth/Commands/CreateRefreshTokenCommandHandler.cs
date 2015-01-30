namespace Fifthweek.Api.Identity.OAuth.Commands
{
    using System;
    using System.Threading.Tasks;

    using Fifthweek.Api.Core;
    using Fifthweek.Api.Persistence;
    using Fifthweek.CodeGeneration;

    [AutoConstructor]
    public partial class CreateRefreshTokenCommandHandler : ICommandHandler<CreateRefreshTokenCommand>
    {
        private readonly IUpsertRefreshTokenDbStatement upsertRefreshToken;

        public async Task HandleAsync(CreateRefreshTokenCommand command)
        {
            command.AssertNotNull("command");

            // Hash the refresh token ID so if anyone access the database they won't
            // have access to the real refresh tokens.
            var hashedRefreshTokenId = HashedRefreshTokenId.FromRefreshTokenId(command.RefreshTokenId);
            var token = new RefreshToken(
                hashedRefreshTokenId.Value,
                command.Username.Value,
                command.ClientId.Value,
                command.IssuedDate,
                command.ExpiresDate,
                command.ProtectedTicket);

            await this.upsertRefreshToken.ExecuteAsync(token);
        }
    }
}