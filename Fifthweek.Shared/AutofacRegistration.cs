namespace Fifthweek.Shared
{
    using Autofac;

    public class AutofacRegistration : IAutofacRegistration
    {
        public void Register(ContainerBuilder builder)
        {
            builder.RegisterType<MimeTypeMap>().As<IMimeTypeMap>();
            builder.RegisterType<ApplicationRandom>().As<IRandom>().SingleInstance();
        }
    }
}