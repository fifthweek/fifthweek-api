namespace Dexter.Api.Repositories
{
    using System.Data.Entity;
    using System.Threading.Tasks;

    using Dexter.Api.Entities;

    public class ClientRepository : IClientRepository
    {
        private readonly IDexterDbContext dexterDbContext;

        public ClientRepository(IDexterDbContext dexterDbContext)
        {
            this.dexterDbContext = dexterDbContext;
        }

        public Task<Client> TryGetClientAsync(string clientId)
        {
            // TODO: This cast is dodgy. Perhaps use solution such as these:
            // http://stackoverflow.com/questions/21800967/no-findasync-method-on-idbsett
            // http://stackoverflow.com/questions/23730949/adapter-pattern-for-idbset-properties-of-a-dbcontext-class
            var set = (DbSet<Client>)this.dexterDbContext.Clients;
            return set.FindAsync(clientId);
        }
    }
}