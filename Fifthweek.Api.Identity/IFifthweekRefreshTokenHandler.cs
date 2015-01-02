namespace Fifthweek.Api.Identity
{
    using System.Threading.Tasks;

    using Microsoft.Owin.Security.Infrastructure;

    public interface IFifthweekRefreshTokenHandler
    {
        Task CreateAsync(AuthenticationTokenCreateContext context);

        Task ReceiveAsync(AuthenticationTokenReceiveContext context);
    }
}