namespace Fifthweek.Api.Models
{
    using System.ComponentModel.DataAnnotations;

    using Fifthweek.Api.Models;

    public class Client
    {
        public Client(ClientId clientId, string secret, string name, ApplicationType applicationType, bool active, int refreshTokenLifeTimeMinutes, string allowedOrigin)
        {
            this.ClientId = clientId;
            this.Secret = secret;
            this.Name = name;
            this.ApplicationType = applicationType;
            this.Active = active;
            this.RefreshTokenLifeTimeMinutes = refreshTokenLifeTimeMinutes;
            this.AllowedOrigin = allowedOrigin;
        }

        public ClientId ClientId { get; private set; }

        public string Secret { get; private set; }

        public string Name { get; private set; }

        public ApplicationType ApplicationType { get; private set; }

        public bool Active { get; private set; }

        public int RefreshTokenLifeTimeMinutes { get; private set; }

        public string AllowedOrigin { get; private set; }
    }
}