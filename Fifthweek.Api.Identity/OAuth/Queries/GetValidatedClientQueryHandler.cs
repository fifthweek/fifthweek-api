namespace Fifthweek.Api.Identity.OAuth.Queries
{
    using System;
    using System.Threading.Tasks;

    using Fifthweek.Api.Core;
    using Fifthweek.Shared;

    public class GetValidatedClientQueryHandler : IQueryHandler<GetValidatedClientQuery, Client>
    {
        private readonly IClientRepository clientRepository;

        public GetValidatedClientQueryHandler(IClientRepository clientRepository)
        {
            this.clientRepository = clientRepository;
        }

        public async Task<Client> HandleAsync(GetValidatedClientQuery query)
        {
            query.AssertNotNull("query");

            var client = await this.clientRepository.TryGetClientAsync(query.ClientId);

            if (client == null)
            {
                throw new ClientRequestException(string.Format("Client '{0}' is not registered in the system.", query.ClientId.Value));
            }

            if (!client.Active)
            {
                throw new ClientRequestException("Client is inactive.");
            }

            if (client.ApplicationType == ApplicationType.NativeConfidential)
            {
                if (string.IsNullOrWhiteSpace(query.ClientSecret))
                {
                    throw new ClientRequestException("Client secret should be sent.");
                }
                 
                if (client.Secret != Helper.GetHash(query.ClientSecret))
                {
                    throw new ClientRequestException("Client secret is invalid.");
                }
            }

            return client;
        }
    }
}