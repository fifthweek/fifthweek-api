namespace Fifthweek.Api.Posts
{
    using Autofac;

    using Fifthweek.Api.Posts.Shared;
    using Fifthweek.Shared;

    public class AutofacRegistration : IAutofacRegistration
    {
        public void Register(ContainerBuilder builder)
        {
            builder.RegisterType<PostToCollectionDbStatement>().As<IPostToCollectionDbStatement>();
            builder.RegisterType<PostToCollectionDbSubStatements>().As<IPostToCollectionDbSubStatements>();
            builder.RegisterType<PostFileTypeChecks>().As<IPostFileTypeChecks>();
            builder.RegisterType<PostSecurity>().As<IPostSecurity>();
            builder.RegisterType<PostOwnership>().As<IPostOwnership>();
            builder.RegisterType<ScheduledDateClippingFunction>().As<IScheduledDateClippingFunction>();
        }
    }
}