namespace Fifthweek.Api.Posts
{
    using Autofac;

    using Fifthweek.Api.Core;

    public class AutofacRegistration : IAutofacRegistration
    {
        public void Register(ContainerBuilder builder)
        {
            builder.RegisterType<PostToCollectionDbStatement>().As<IPostToCollectionDbStatement>();
        }
    }
}