namespace Fifthweek.Api
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Web.Http.Dispatcher;

    public static class FifthweekAssembliesResolver
    {
        public static IEnumerable<Assembly> GetAssemblies()
        {
            return new List<Assembly>
            {
                Assembly.GetExecutingAssembly(),
                typeof(Fifthweek.Api.Availability.AvailabilityController).Assembly,
                typeof(Fifthweek.Api.Core.TransactionCommandHandlerDecorator<>).Assembly,
                typeof(Fifthweek.Api.Identity.MembershipController).Assembly,
                typeof(Fifthweek.Api.Logging.LogController).Assembly,
                typeof(Fifthweek.Api.Persistence.ApplicationUser).Assembly,
                typeof(Fifthweek.Api.SendGrid.SendGridEmailService).Assembly
            };
        }
    }
}