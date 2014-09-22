namespace Dexter.Api.Commands
{
    using Dexter.Api.Entities;
    using Dexter.Api.Models;

    public class RemoveRefreshTokenCommand
    {
        public RemoveRefreshTokenCommand(HashedRefreshTokenId hashedRefreshTokenId)
        {
            this.HashedRefreshTokenId = hashedRefreshTokenId;
        }

        public HashedRefreshTokenId HashedRefreshTokenId { get; private set; }
    }
}