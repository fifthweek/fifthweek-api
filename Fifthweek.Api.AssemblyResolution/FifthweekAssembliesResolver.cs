namespace Fifthweek.Api.AssemblyResolution
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    public static class FifthweekAssembliesResolver
    {
        public static readonly IEnumerable<Assembly> FifthweekAssemblies = new[] 
        {
            Assembly.GetCallingAssembly(),
            typeof(Shared.AutofacRegistration).Assembly,
            typeof(Core.AutofacRegistration).Assembly,
            typeof(Availability.AutofacRegistration).Assembly,
            typeof(Channels.AutofacRegistration).Assembly,
            typeof(Channels.Shared.ChannelId).Assembly,
            typeof(Collections.AutofacRegistration).Assembly,
            typeof(Collections.Shared.CollectionId).Assembly,
            typeof(EndToEndTestMailboxes.SetLatestMessageDbStatement).Assembly,
            typeof(EndToEndTestMailboxes.Shared.MailboxName).Assembly,
            typeof(Identity.Membership.AutofacRegistration).Assembly,
            typeof(Identity.OAuth.AutofacRegistration).Assembly,
            typeof(Identity.Shared.Membership.UserId).Assembly,
            typeof(Logging.AutofacRegistration).Assembly,
            typeof(Persistence.AutofacRegistration).Assembly,
            typeof(Posts.AutofacRegistration).Assembly,
            typeof(Posts.Shared.PostId).Assembly,
            typeof(SendGrid.AutofacRegistration).Assembly,
            typeof(Blogs.AutofacRegistration).Assembly,
            typeof(Blogs.Shared.BlogId).Assembly,
            typeof(Azure.AutofacRegistration).Assembly,
            typeof(FileManagement.AutofacRegistration).Assembly,
            typeof(FileManagement.Shared.FileId).Assembly,
            typeof(Aggregations.AutofacRegistration).Assembly,
            typeof(Payments.AutofacRegistration).Assembly,
            typeof(Fifthweek.Payments.AutofacRegistration).Assembly,
            typeof(Fifthweek.Payments.SnapshotCreation.AutofacRegistration).Assembly,
            typeof(Fifthweek.Payments.Shared.TransactionReference).Assembly
        }
        .Distinct().ToList().AsReadOnly();

        public static IEnumerable<Assembly> Assemblies
        {
            get
            {
                if (ExtraAssemblies == null)
                {
                    return FifthweekAssemblies;
                }
                 
                return FifthweekAssemblies.Concat(ExtraAssemblies);
            }
        }

        internal static IEnumerable<Assembly> ExtraAssemblies { get; set; }
    }
}
