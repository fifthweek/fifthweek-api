namespace Fifthweek.Api.Identity.OAuth
{
    using System.Threading.Tasks;

    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;

    public interface ITryGetRefreshTokenDbStatement
    {
        Task<RefreshToken> ExecuteAsync(ClientId clientId, Username username);
    }
}