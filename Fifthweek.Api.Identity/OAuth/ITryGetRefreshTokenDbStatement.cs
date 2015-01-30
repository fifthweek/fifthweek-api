namespace Fifthweek.Api.Identity.OAuth
{
    using System.Threading.Tasks;

    using Fifthweek.Api.Persistence;

    public interface ITryGetRefreshTokenDbStatement
    {
        Task<RefreshToken> ExecuteAsync(HashedRefreshTokenId hashedTokenId);
    }
}