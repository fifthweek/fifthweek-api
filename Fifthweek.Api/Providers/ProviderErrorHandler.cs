namespace Fifthweek.Api.Providers
{
    using System;
    using System.Threading.Tasks;

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
                await RequestExceptionHandler.ReportExceptionAndCreateResponseAsync(context, exception);
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
                RequestExceptionHandler.ReportExceptionAsync(t);
                throw;
            }
        }
    }
}