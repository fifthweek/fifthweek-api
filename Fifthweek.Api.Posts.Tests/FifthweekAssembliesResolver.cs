namespace Fifthweek.Api.Posts.Tests
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
            typeof(Channels.AutofacRegistration).Assembly,
            typeof(Collections.AutofacRegistration).Assembly,
            typeof(Identity.Membership.AutofacRegistration).Assembly,
            typeof(Identity.OAuth.AutofacRegistration).Assembly,
            typeof(Persistence.AutofacRegistration).Assembly,
            typeof(Posts.AutofacRegistration).Assembly,
            typeof(Subscriptions.AutofacRegistration).Assembly,
            typeof(FileManagement.AutofacRegistration).Assembly
        }
        .Distinct().ToList().AsReadOnly();
    }
}