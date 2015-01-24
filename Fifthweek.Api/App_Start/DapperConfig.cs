namespace Fifthweek.Api
{
    using Fifthweek.Api.Persistence;

    public static class DapperConfig
    {
        public static void Register()
        {
            DapperTypeHandlerRegistration.Register(FifthweekAssembliesResolver.Assemblies);            
        }
    }
}