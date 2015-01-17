namespace Fifthweek.Api.Posts
{
    using Autofac;

    using Fifthweek.Shared;

    public class AutofacRegistration : IAutofacRegistration
    {
        public void Register(ContainerBuilder builder)
        {
            builder.RegisterType<PostToCollectionDbStatement>().As<IPostToCollectionDbStatement>();
            builder.RegisterType<PostToCollectionDbSubStatements>().As<IPostToCollectionDbSubStatements>();
            builder.RegisterType<PostFileTypeChecks>().As<IPostFileTypeChecks>();
        }
    }
}