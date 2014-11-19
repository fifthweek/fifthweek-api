namespace Fifthweek.Api
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Web.Http.Dependencies;

    using Autofac;
    using Autofac.Core.Lifetime;
    using Autofac.Integration.WebApi;

    public class ErrorLoggingWebApiDependencyResolver : IDependencyResolver
    {
        private readonly IDependencyScope childScope;

        private readonly ILifetimeScope lifetimeScope;

        public ErrorLoggingWebApiDependencyResolver(IDependencyScope childScope, ILifetimeScope lifetimeScope)
        {
            this.childScope = childScope;
            this.lifetimeScope = lifetimeScope;
        }

        public void Dispose()
        {
            this.childScope.Dispose();
        }

        public object GetService(Type serviceType)
        {
            object result;
            try
            {
                result = this.childScope.GetService(serviceType);
            }
            catch (Exception t)
            {
                Trace.WriteLine(t);
                throw;
            }

            if (result == null)
            {
                TraceResolveError(serviceType, this.lifetimeScope);
            }

            return result;
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return this.childScope.GetServices(serviceType);
        }

        public IDependencyScope BeginScope()
        {
            var newLifetimeScope = this.lifetimeScope.BeginLifetimeScope(MatchingScopeLifetimeTags.RequestLifetimeScopeTag);
            return new ErrorLoggingWebApiDependencyResolver(new AutofacWebApiDependencyScope(newLifetimeScope), newLifetimeScope);
        }

        private static void TraceResolveError(Type serviceType, ILifetimeScope resolver)
        {
            if (serviceType.FullName.Contains("Fifthweek"))
            {
                try
                {
                    resolver.Resolve(serviceType);
                    throw new Exception("Unexpectedly resolved service.");
                }
                catch (Exception t)
                {
                    Trace.WriteLine(t);
                }
            }
        }
    }
}