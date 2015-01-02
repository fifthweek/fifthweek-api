namespace Fifthweek.Api.Identity
{
    public class Client
    {
        public Client(
            ClientId clientId,
            string secret,
            string name,
            ApplicationType applicationType,
            bool active,
            int refreshTokenLifeTimeMinutes,
            string allowedOriginRegex,
            string defaultAllowedOrigin)
        {
            this.ClientId = clientId;
            this.Secret = secret;
            this.Name = name;
            this.ApplicationType = applicationType;
            this.Active = active;
            this.RefreshTokenLifeTimeMinutes = refreshTokenLifeTimeMinutes;
            this.AllowedOriginRegex = allowedOriginRegex;
            this.DefaultAllowedOrigin = defaultAllowedOrigin;
        }

        public ClientId ClientId { get; private set; }

        public string Secret { get; private set; }

        public string Name { get; private set; }

        public ApplicationType ApplicationType { get; private set; }

        public bool Active { get; private set; }

        public int RefreshTokenLifeTimeMinutes { get; private set; }

        public string AllowedOriginRegex { get; private set; }

        public string DefaultAllowedOrigin { get; private set; }
    }
}