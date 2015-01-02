namespace Fifthweek.Api.Core
{
    using System.Web.Http.Dependencies;

    public static class Extensions
    {
        public static TResult GetService<TResult>(this IDependencyResolver resolver)
        {
            return (TResult)resolver.GetService(typeof(TResult));
        }
    }
}