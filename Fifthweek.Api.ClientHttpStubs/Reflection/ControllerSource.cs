namespace Fifthweek.Api.ClientHttpStubs.Reflection
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Web.Http;

    using Fifthweek.CodeGeneration;

    [AutoConstructor]
    public partial class ControllerSource
    {
        private readonly IEnumerable<Assembly> assemblies; 

        public IEnumerable<Type> GetControllerTypes()
        {
            return this.assemblies
                .SelectMany(_ => _.GetTypes())
                .Where(t => !t.IsAbstract)
                .Where(IsControllerType);
        }

        private static bool IsControllerType(Type type)
        {
            return type != typeof(ApiController) && typeof(ApiController).IsAssignableFrom(type);
        }
    }
}