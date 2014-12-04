namespace Fifthweek.Api.QueryHandlers
{
    using System.Threading.Tasks;

    using Fifthweek.Api.Entities;
    using Fifthweek.Api.Models;
    using Fifthweek.Api.Queries;
    using Fifthweek.Api.Repositories;

    public class GetClientQueryHandler : IQueryHandler<GetClientQuery, Client>
    {
        private readonly IClientRepository clientRepository;

        public GetClientQueryHandler(IClientRepository clientRepository)
        {
            this.clientRepository = clientRepository;
        }

        public Task<Client> HandleAsync(GetClientQuery query)
        {
            return this.clientRepository.TryGetClientAsync(query.ClientId);
        }
    }
}