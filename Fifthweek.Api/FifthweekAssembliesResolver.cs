namespace Fifthweek.Api
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Web.Http.Dispatcher;

    using Fifthweek.Api.Availability.Controllers;
    using Fifthweek.Api.Identity.Membership.Controllers;
    using Fifthweek.Api.Logging.Controllers;
    using Fifthweek.Api.Persistence.Identity;

    public static class FifthweekAssembliesResolver
    {
        public static IEnumerable<Assembly> GetAssemblies()
        {
            return new List<Assembly>
            {
                Assembly.GetExecutingAssembly(),
                typeof(AvailabilityController).Assembly,
                typeof(Fifthweek.Api.Core.TransactionCommandHandlerDecorator<>).Assembly,
                typeof(MembershipController).Assembly,
                typeof(LogController).Assembly,
                typeof(FifthweekUser).Assembly,
                typeof(Fifthweek.Api.SendGrid.SendGridEmailService).Assembly
            };
        }
    }
}