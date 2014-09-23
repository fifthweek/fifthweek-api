namespace Dexter.Api.CommandHandlers
{
    using System;
    using System.Threading.Tasks;

    using Dexter.Api.Commands;
    using Dexter.Api.Repositories;

    public class RemoveRefreshTokenCommandHandler : ICommandHandler<RemoveRefreshTokenCommand>
    {
        private readonly IRefreshTokenRepository refreshTokenRepository;

        private readonly ICommandHandler<SaveChangesCommand> saveChanges;

        public RemoveRefreshTokenCommandHandler(
            IRefreshTokenRepository refreshTokenRepository,
            ICommandHandler<SaveChangesCommand> saveChanges)
        {
            this.refreshTokenRepository = refreshTokenRepository;
            this.saveChanges = saveChanges;
        }

        public async Task HandleAsync(RemoveRefreshTokenCommand command)
        {
            var hashedId = command.HashedRefreshTokenId;

            var refreshToken = await this.refreshTokenRepository.TryGetRefreshTokenAsync(hashedId.Value);

            if (refreshToken == null)
            {
                throw new BadRequestException("Refresh token not found: " + hashedId);
            }

            await this.refreshTokenRepository.RemoveRefreshTokenAsync(refreshToken);

            await this.saveChanges.HandleAsync(SaveChangesCommand.Default);
        }
    }
}