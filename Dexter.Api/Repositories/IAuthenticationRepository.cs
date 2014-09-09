namespace Dexter.Api.Repositories
{
    using System.Threading.Tasks;

    using Dexter.Api.Models;

    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    public interface IAuthenticationRepository
    {
        Task AddInternalUserAsync(string username, string password);

        Task AddExternalUserAsync(string username, string provider, string providerKey);

        Task<IdentityUser> FindExternalUserAsync(string provider, string providerKey);
        
        Task<IdentityUser> FindInternalUserAsync(string username, string password);
    }
}