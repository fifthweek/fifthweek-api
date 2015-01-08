namespace Fifthweek.Api.Persistence
{
    using Microsoft.Owin.Security.DataProtection;

    using Owin;

    public static class IdentityConfig
    {
        // This allows us to use AutoFac instead of IOwinContext:
        // http://tech.trailmax.info/2014/06/asp-net-identity-and-cryptographicexception-when-running-your-site-on-microsoft-azure-web-sites/
        internal static IDataProtectionProvider FarmedMachineDataProtectionProvider { get; private set; }

        internal static IDataProtectionProvider NonFarmedMachineDataProtectionProvider
        {
            get
            {
                return new DpapiDataProtectionProvider("Fifthweek");
            }
        }

        public static void Register(IAppBuilder app)
        {
            FarmedMachineDataProtectionProvider = app.GetDataProtectionProvider();
        }
    }
}