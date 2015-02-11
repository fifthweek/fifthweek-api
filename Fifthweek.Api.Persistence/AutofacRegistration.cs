namespace Fifthweek.Api.Persistence
{
    using System;
    using System.Threading.Tasks;

    using Autofac;

    using Fifthweek.Api.Core;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Shared;

    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.Owin;

    public class AutofacRegistration : IAutofacRegistration
    {
        public void Register(ContainerBuilder builder)
        {
            builder.RegisterType<FifthweekDbConnectionFactory>().As<IFifthweekDbConnectionFactory>().SingleInstance();
            builder.Register(c => CreateUserManager(c.Resolve<ISendEmailService>(), c.Resolve<IFifthweekDbConnectionFactory>())).As<IUserManager>();
        }

        public static FifthweekUserManager CreateUserManager(ISendEmailService sendEmailService, IFifthweekDbConnectionFactory connectionFactory)
        {
            var userStore = new FifthweekUserStore(connectionFactory.CreateContext());
            var dataProtectionProvider = IdentityConfig.FarmedMachineDataProtectionProvider ?? IdentityConfig.NonFarmedMachineDataProtectionProvider;
            var userManager = new FifthweekUserManager(userStore)
            {
                UserTokenProvider = new DataProtectorTokenProvider<FifthweekUser, Guid>(dataProtectionProvider.Create()),
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