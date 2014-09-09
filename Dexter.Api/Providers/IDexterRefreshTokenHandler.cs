namespace Dexter.Api.Providers
{
    using System.Threading.Tasks;

    using Microsoft.Owin.Security.Infrastructure;

    public interface IDexterRefreshTokenHandler
    {
        Task CreateAsync(AuthenticationTokenCreateContext context);

        Task ReceiveAsync(AuthenticationTokenReceiveContext context);
    }
}