namespace Fifthweek.Api.Identity
{
    using System;
    using System.Threading.Tasks;

    using Fifthweek.Api.Core;

    using Microsoft.Owin.Security.OAuth;

    public static class ProviderErrorHandler
    {
        public static async Task CallAndHandleError<T>(Func<Task> action, BaseValidatingContext<T> context)
        {
            Exception exception = null;
            try
            {
                await action();
            }
            catch (Exception t)
            {
                exception = t;
            }

            if (exception != null)
            {
                await Helper.GetOwinScopeService<IExceptionHandler>().ReportExceptionAndCreateResponseAsync(context, exception);
            }
        }

        public static async Task CallAndHandleError(Func<Task> action)
        {
            try
            {
                await action();
            }
            catch (Exception t)
            {
                Helper.GetOwinScopeService<IExceptionHandler>().ReportExceptionAsync(t);
                throw;
            }
        }
    }
}