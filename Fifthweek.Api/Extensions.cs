namespace Fifthweek.Api
{
    using System;
    using System.Web.Http.Dependencies;

    public static class Extensions
    {
        public static TResult GetService<TResult>(this IDependencyResolver resolver)
        {
            return (TResult)resolver.GetService(typeof(TResult));
        }

        public static string GetExceptionIdentifier(this Exception t)
        {
            var hashCode = t.ToString().GetHashCode() + "." + DateTime.UtcNow.ToString("yyMMddHHmmss");
            return hashCode;
        }
    }
}