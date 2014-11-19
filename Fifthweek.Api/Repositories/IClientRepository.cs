namespace Fifthweek.Api.Repositories
{
    using System.Threading.Tasks;

    using Fifthweek.Api.Entities;

    public interface IClientRepository
    {
        Task<Client> TryGetClientAsync(string clientId);
    }
}