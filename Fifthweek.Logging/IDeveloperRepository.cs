namespace Fifthweek.Logging
{
    using System.Threading.Tasks;

    public interface IDeveloperRepository
    {
        Task<Developer> TryGetByGitNameAsync(string gitName);
    }
}