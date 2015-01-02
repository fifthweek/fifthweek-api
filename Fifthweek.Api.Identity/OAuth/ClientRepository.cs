namespace Fifthweek.Api.Identity.OAuth
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Fifthweek.Api.Core;

    public class ClientRepository : IClientRepository
    {
        private readonly Dictionary<ClientId, Client> clients = new Dictionary<ClientId, Client>();

        public ClientRepository()
        {
            this.AddClient(new Client(
                new ClientId("fifthweek.web.1"),
                string.Empty,
                "Fifthweek Website",
                ApplicationType.JavaScript,
                true,
                (int)TimeSpan.FromDays(365).TotalMinutes,
                Constants.FifthweekWebsiteOriginRegex,
                Constants.FifthweekWebsiteOriginDefault));
        }

        public Task<Client> TryGetClientAsync(ClientId clientId)
        {
            Client result = null;
            this.clients.TryGetValue(clientId, out result);
            return Task.FromResult(result);
        }

        private void AddClient(Client client)
        {
            this.clients.Add(client.ClientId, client);
        }
    }
}