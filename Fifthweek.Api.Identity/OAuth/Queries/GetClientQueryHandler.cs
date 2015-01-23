namespace Fifthweek.Api.Identity.OAuth.Queries
{
    using System.Threading.Tasks;

    using Fifthweek.Api.Core;

    [Decorator(typeof(RetryOnRecoverableDatabaseErrorQueryHandlerDecorator<,>))]
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