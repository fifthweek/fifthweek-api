namespace Fifthweek.Api.Commands
{
    using Fifthweek.Api.Entities;
    using Fifthweek.Api.Models;

    public class RemoveRefreshTokenCommand
    {
        public RemoveRefreshTokenCommand(HashedRefreshTokenId hashedRefreshTokenId)
        {
            this.HashedRefreshTokenId = hashedRefreshTokenId;
        }

        public HashedRefreshTokenId HashedRefreshTokenId { get; private set; }
    }
}