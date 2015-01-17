namespace Fifthweek.Api
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    public static class FifthweekAssembliesResolver
    {
        public static readonly IEnumerable<Assembly> Assemblies = new[] 
        {
            Assembly.GetExecutingAssembly(),
            typeof(Shared.AutofacRegistration).Assembly,
            typeof(Core.AutofacRegistration).Assembly,
            typeof(Availability.AutofacRegistration).Assembly,
            typeof(Identity.Membership.AutofacRegistration).Assembly,
            typeof(Identity.OAuth.AutofacRegistration).Assembly,
            typeof(Logging.AutofacRegistration).Assembly,
            typeof(Persistence.AutofacRegistration).Assembly,
            typeof(SendGrid.AutofacRegistration).Assembly,
            typeof(Subscriptions.AutofacRegistration).Assembly,
            typeof(Posts.AutofacRegistration).Assembly,
            typeof(Azure.AutofacRegistration).Assembly,
            typeof(FileManagement.AutofacRegistration).Assembly
        }
        .Distinct().ToList().AsReadOnly();
    }
}
