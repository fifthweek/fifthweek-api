namespace Fifthweek.Api.Repositories
{
    using System.Threading.Tasks;

    using Fifthweek.Api.Models;

    public interface IDeveloperRepository
    {
        Task<Developer> TryGetByGitNameAsync(string gitName);
    }
}