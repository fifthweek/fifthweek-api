namespace Fifthweek.Api.CommandHandlers
{
    using System;
    using System.Threading.Tasks;

    using Fifthweek.Api.Commands;
    using Fifthweek.Api.Repositories;

    public class RemoveRefreshTokenCommandHandler : ICommandHandler<RemoveRefreshTokenCommand>
    {
        private readonly IRefreshTokenRepository refreshTokenRepository;

        public RemoveRefreshTokenCommandHandler(
            IRefreshTokenRepository refreshTokenRepository)
        {
            this.refreshTokenRepository = refreshTokenRepository;
        }

        public async Task HandleAsync(RemoveRefreshTokenCommand command)
        {
            await this.refreshTokenRepository.RemoveRefreshToken(command.HashedRefreshTokenId.Value);
        }
    }
}