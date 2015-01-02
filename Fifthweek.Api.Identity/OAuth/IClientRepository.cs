namespace Fifthweek.Api.Identity.OAuth
{
    using System.Threading.Tasks;

    public interface IClientRepository
    {
        Task<Client> TryGetClientAsync(ClientId clientId);
    }
}