namespace Fifthweek.Api.Identity.OAuth
{
    using System;
    using System.Diagnostics;
    using System.Threading.Tasks;
    using System.Web;

    using Fifthweek.Api.Core;

    using Microsoft.Owin;
    using Microsoft.Owin.Security.OAuth;

    public static class ProviderErrorHandler
    {
        public static async Task CallAndHandleError<T>(Func<Task> action, BaseValidatingContext<T> context, IExceptionHandler exceptionHandler)
        {
            Exception exception = null;
            try
            {
                await action();

                if (context.HasError)
                {
                    Trace.TraceWarning(
                        "Non-exceptional error during authorization. Error: '{0}', ErrorDescription: '{1}', ErrorUri: '{2}'",
                        context.Error,
                        context.ErrorDescription,
                        context.ErrorUri);
                }
            }
            catch (Exception t)
            {
                exception = t;
            }

            if (exception != null)
            {
                await exceptionHandler.ReportExceptionAndCreateResponseAsync(context, exception);
            }
        }

        public static async Task CallAndHandleError(Func<Task> action, IExceptionHandler exceptionHandler)
        {
            try
            {
                await action();
            }
            catch (Exception t)
            {
                exceptionHandler.ReportExceptionAsync(t);
                throw;
            }
        }
    }
}