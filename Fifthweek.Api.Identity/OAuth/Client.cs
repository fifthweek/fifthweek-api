using Fifthweek.Api.Core;

namespace Fifthweek.Api.Identity.OAuth
{
    [AutoEqualityMembers, AutoConstructor]
    public partial class Client
    {
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