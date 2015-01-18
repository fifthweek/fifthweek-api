namespace Fifthweek.Api.Collections
{
    using Autofac;

    using Fifthweek.Shared;

    public class AutofacRegistration : IAutofacRegistration
    {
        public void Register(ContainerBuilder builder)
        {
            builder.RegisterType<CollectionOwnership>().As<ICollectionOwnership>();
            builder.RegisterType<CollectionSecurity>().As<ICollectionSecurity>();
        }
    }
}