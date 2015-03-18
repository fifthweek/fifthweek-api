namespace Fifthweek.Api.Azure
{
    using System;

    public class AzureConfiguration : IAzureConfiguration
    {
        private const string FifthweekCdnDomainKey = "CDN_DOMAIN";

        public AzureConfiguration()
        {
            // This is an environment variable so that if we forget to configure it on azure 
            // it will be empty, rather than inheriting the web.config value.
            this.CdnDomain = Environment.GetEnvironmentVariable(FifthweekCdnDomainKey);
        }

        public string CdnDomain { get; private set; }
    }
}