namespace Fifthweek.Api.Repositories
{
    using System.Threading.Tasks;

    using Fifthweek.Api.Models;

    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    public interface IAuthenticationRepository
    {
        Task AddInternalUserAsync(string email, string username, string password);
        
        Task<IdentityUser> FindInternalUserAsync(string username, string password);
    }
}