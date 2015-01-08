namespace Fifthweek.Api.Core
{
    using Autofac;

    public interface IAutofacRegistration
    {
        void Register(ContainerBuilder builder);
    }
}