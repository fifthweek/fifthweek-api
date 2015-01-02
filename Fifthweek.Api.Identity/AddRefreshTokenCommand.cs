namespace Fifthweek.Api.Identity
{
    using Fifthweek.Api.Persistence;

    public class AddRefreshTokenCommand
    {
        public AddRefreshTokenCommand(RefreshToken refreshToken)
        {
            this.RefreshToken = refreshToken;
        }

        public RefreshToken RefreshToken { get; private set; }
    }
}