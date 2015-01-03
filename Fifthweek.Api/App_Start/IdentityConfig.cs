using System;
using System.Threading.Tasks;
using Autofac;
using Fifthweek.Api.Core;
using Fifthweek.Api.Persistence;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security.DataProtection;

namespace Fifthweek.Api
{
    public static class IdentityConfig
    {
        public static UserManager<ApplicationUser> CreateUserManager(ISendEmailService sendEmailService, IFifthweekDbContext dbContext)
        {
            var userStore = new UserStore<ApplicationUser>((FifthweekDbContext)dbContext);
            var provider = new DpapiDataProtectionProvider("Fifthweek");
            var userManager = new UserManager<ApplicationUser>(userStore)
            {
                UserTokenProvider = new DataProtectorTokenProvider<ApplicationUser>(provider.Create("EmailConfirmation")),
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