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
            typeof(Collections.Shared.CollectionId).Assembly,
            typeof(Identity.Shared.Membership.UserId).Assembly,
            typeof(FileManagement.Shared.FileId).Assembly,
            typeof(Subscriptions.AutofacRegistration).Assembly
        }
        .Distinct().ToList().AsReadOnly();
    }
}