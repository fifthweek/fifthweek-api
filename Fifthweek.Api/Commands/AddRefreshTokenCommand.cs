namespace Fifthweek.Api.Commands
{
    using Fifthweek.Api.Entities;

    public class AddRefreshTokenCommand
    {
        public AddRefreshTokenCommand(RefreshToken refreshToken)
        {
            this.RefreshToken = refreshToken;
        }

        public RefreshToken RefreshToken { get; private set; }
    }
}