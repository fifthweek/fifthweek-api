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
            typeof(Channels.Shared.ChannelId).Assembly,
            typeof(Collections.AutofacRegistration).Assembly,
            typeof(Identity.Membership.AutofacRegistration).Assembly,
            typeof(Identity.OAuth.AutofacRegistration).Assembly,
            typeof(Posts.AutofacRegistration).Assembly,
            typeof(Subscriptions.AutofacRegistration).Assembly,
            typeof(FileManagement.AutofacRegistration).Assembly
        }
        .Distinct().ToList().AsReadOnly();
    }
}