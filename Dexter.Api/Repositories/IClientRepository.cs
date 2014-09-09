namespace Dexter.Api.Repositories
{
    using System.Threading.Tasks;

    using Dexter.Api.Entities;

    public interface IClientRepository
    {
        Task<Client> TryGetClientAsync(string clientId);
    }
}