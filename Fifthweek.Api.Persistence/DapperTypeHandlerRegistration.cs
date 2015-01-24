namespace Fifthweek.Api.Persistence
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    using Autofac;

    using Dapper;

    public static class DapperTypeHandlerRegistration
    {
        private static readonly object LockObject = new object();
        private static bool isRegistered;

        public static void Register(IEnumerable<Assembly> assemblies)
        {
            if (isRegistered)
            {
                return;
            }

            lock (LockObject)
            {
                if (isRegistered)
                {
                    return;
                }

                var types = assemblies.SelectMany(v => v.GetTypes());
                var typeHandlers = (from t in types
                                    where t.IsClass
                                    from i in t.GetInterfaces()
                                    where i.IsClosedTypeOf(typeof(IAutoRegisteredTypeHandler<>))
                                    select new { Type = t, Interface = i }).ToList();

                foreach (var typeHandler in typeHandlers)
                {
                    var primitiveType = typeHandler.Interface.GetGenericArguments().Single();
                    var typeHandlerObject = (SqlMapper.ITypeHandler)Activator.CreateInstance(typeHandler.Type);
                    SqlMapper.AddTypeHandler(primitiveType, typeHandlerObject);
                }

                isRegistered = true;
            }
        }
    }
}