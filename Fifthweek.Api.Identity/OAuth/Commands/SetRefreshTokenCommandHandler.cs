namespace Fifthweek.Api.Identity.OAuth.Commands
{
    using System;
    using System.Threading.Tasks;

    using Fifthweek.Api.Core;
    using Fifthweek.Api.Persistence;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;

    [AutoConstructor]
    public partial class SetRefreshTokenCommandHandler : ICommandHandler<SetRefreshTokenCommand>
    {
        private readonly IRefreshTokenIdEncryptionService encryptionService;
        private readonly IUpsertRefreshTokenDbStatement upsertRefreshToken;

        public async Task HandleAsync(SetRefreshTokenCommand command)
        {
            command.AssertNotNull("command");

            // Encrypt the refresh token ID so if anyone access the database they won't
            // have access to the real refresh tokens.
            var encryptedRefreshTokenId = this.encryptionService.EncryptRefreshTokenId(command.RefreshTokenId);
            var token = new RefreshToken(
                command.Username.Value,
                command.ClientId.Value,
                encryptedRefreshTokenId.Value,
                command.IssuedDate,
                command.ExpiresDate, 
                command.ProtectedTicket);

            await this.upsertRefreshToken.ExecuteAsync(token);
        }
    }
}