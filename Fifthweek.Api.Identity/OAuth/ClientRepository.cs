namespace Fifthweek.Api.Identity.OAuth
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Fifthweek.Api.Core;

    public class ClientRepository : IClientRepository
    {
        private static readonly Dictionary<ClientId, Client> Clients = new Dictionary<ClientId, Client>();

        private readonly object syncRoot = new object();

        static ClientRepository()
        {
            AddClient(new Client(
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
            clientId.AssertNotNull("clientId");
            lock (this.syncRoot)
            {
                Client result = null;
                Clients.TryGetValue(clientId, out result);
                return Task.FromResult(result);
            }
        }

        private static void AddClient(Client client)
        {
            client.AssertNotNull("client");
            client.ClientId.AssertNotNull("clientId");

            Clients.Add(client.ClientId, client);
        }
    }
}