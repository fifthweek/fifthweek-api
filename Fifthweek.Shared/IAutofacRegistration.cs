namespace Fifthweek.Shared
{
    using Autofac;

    public interface IAutofacRegistration
    {
        void Register(ContainerBuilder builder);
    }
}