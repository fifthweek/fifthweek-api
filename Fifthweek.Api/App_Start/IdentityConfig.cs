using System;
using System.Threading.Tasks;
using Autofac;
using Fifthweek.Api.Core;
using Fifthweek.Api.Identity.Membership;
using Fifthweek.Api.Persistence;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security.DataProtection;
using Owin;

namespace Fifthweek.Api
{
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

        public static UserManager<ApplicationUser> CreateUserManager(ISendEmailService sendEmailService, IFifthweekDbContext dbContext)
        {
            var userStore = new UserStore<ApplicationUser>((FifthweekDbContext)dbContext);
            var dataProtectionProvider = FarmedMachineDataProtectionProvider ?? NonFarmedMachineDataProtectionProvider;
            var userManager = new UserManager<ApplicationUser>(userStore)
            {
                UserTokenProvider = new DataProtectorTokenProvider<ApplicationUser>(dataProtectionProvider.Create()),
                EmailService = new MessageService(sendEmailService)
            };

            return userManager;
        }

        private class MessageService : IIdentityMessageService
        {
            private readonly ISendEmailService sendEmailService;

            public MessageService(ISendEmailService sendEmailService)
            {
                if (sendEmailService == null)
                {
                    throw new ArgumentNullException("sendEmailService");
                }

                this.sendEmailService = sendEmailService;
            }

            public Task SendAsync(IdentityMessage message)
            {
                return this.sendEmailService.SendEmailAsync(message.Destination, message.Subject, message.Body);
            }
        }
    }
}