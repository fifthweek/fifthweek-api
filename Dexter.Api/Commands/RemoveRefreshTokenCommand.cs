namespace Dexter.Api.Commands
{
    using Dexter.Api.Entities;
    using Dexter.Api.Models;

    public class RemoveRefreshTokenCommand
    {
        public RemoveRefreshTokenCommand(RefreshTokenId refreshTokenId)
        {
            this.RefreshTokenId = refreshTokenId;
        }

        public RefreshTokenId RefreshTokenId { get; private set; }
    }
}