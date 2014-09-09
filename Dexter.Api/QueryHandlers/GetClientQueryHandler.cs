namespace Dexter.Api.QueryHandlers
{
    using System.Threading.Tasks;

    using Dexter.Api.Entities;
    using Dexter.Api.Queries;
    using Dexter.Api.Repositories;

    public class GetClientQueryHandler : IQueryHandler<GetClientQuery, Client>
    {
        private readonly IClientRepository clientRepository;

        public GetClientQueryHandler(IClientRepository clientRepository)
        {
            this.clientRepository = clientRepository;
        }

        public Task<Client> HandleAsync(GetClientQuery query)
        {
            return this.clientRepository.TryGetClientAsync(query.ClientId.Value);
        }
    }
}