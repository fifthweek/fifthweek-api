namespace Fifthweek.Api.Identity.OAuth.Commands
{
    using Fifthweek.Api.Persistence;

    public partial class AddRefreshTokenCommand
    {
        public AddRefreshTokenCommand(RefreshToken refreshToken)
        {
            this.RefreshToken = refreshToken;
        }

        public RefreshToken RefreshToken { get; private set; }
    }
}