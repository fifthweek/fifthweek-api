namespace Fifthweek.Api.Identity
{
    public class RemoveRefreshTokenCommand
    {
        public RemoveRefreshTokenCommand(HashedRefreshTokenId hashedRefreshTokenId)
        {
            this.HashedRefreshTokenId = hashedRefreshTokenId;
        }

        public HashedRefreshTokenId HashedRefreshTokenId { get; private set; }
    }
}