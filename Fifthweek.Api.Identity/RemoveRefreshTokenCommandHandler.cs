namespace Fifthweek.Api.Identity
{
    using System.Threading.Tasks;

    using Fifthweek.Api.Core;

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