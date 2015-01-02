namespace Fifthweek.Api.Identity
{
    using System.Threading.Tasks;

    public interface IClientRepository
    {
        Task<Client> TryGetClientAsync(ClientId clientId);
    }
}