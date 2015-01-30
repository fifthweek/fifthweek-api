namespace Fifthweek.Api.Identity.OAuth
{
    using System.Threading.Tasks;

    public interface IRemoveRefreshTokenDbStatement
    {
        Task ExecuteAsync(HashedRefreshTokenId hashedTokenId);
    }
}