namespace Fifthweek.Api.Repositories
{
    using System.Threading.Tasks;

    using Fifthweek.Api.Entities;
    using Fifthweek.Api.Models;

    public interface IClientRepository
    {
        Task<Client> TryGetClientAsync(ClientId clientId);
    }
}