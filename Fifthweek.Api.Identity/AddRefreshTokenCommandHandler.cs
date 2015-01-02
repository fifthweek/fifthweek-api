namespace Fifthweek.Api.Identity
{
    using System.Threading.Tasks;

    using Fifthweek.Api.Core;

    public class AddRefreshTokenCommandHandler : ICommandHandler<AddRefreshTokenCommand>
    {
        private readonly IRefreshTokenRepository refreshTokenRepository;

        public AddRefreshTokenCommandHandler(
            IRefreshTokenRepository refreshTokenRepository)
        {
            this.refreshTokenRepository = refreshTokenRepository;
        }

        public async Task HandleAsync(AddRefreshTokenCommand command)
        {
            var token = command.RefreshToken;

            await this.refreshTokenRepository.RemoveRefreshTokens(token.Username, token.ClientId);
            await this.refreshTokenRepository.AddRefreshTokenAsync(token);
        }
    }
}