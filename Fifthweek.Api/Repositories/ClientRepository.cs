namespace Fifthweek.Api.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Threading.Tasks;

    using Fifthweek.Api.Entities;
    using Fifthweek.Api.Models;

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
                Constants.FifthweekWebsiteOrigin));
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