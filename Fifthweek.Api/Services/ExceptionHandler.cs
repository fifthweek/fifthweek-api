namespace Fifthweek.Api.Services
{
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;
    using System.Web.Hosting;

    using Microsoft.Owin.Security.OAuth;

    public class ExceptionHandler : IExceptionHandler
    {
        public Task<HttpResponseMessage> ReportExceptionAndCreateResponseAsync(
            HttpRequestMessage request,
            Exception exception)
        {
            return ExceptionHandlerUtilities.ReportExceptionAndCreateResponseAsync(request, exception);
        }

        public Task ReportExceptionAndCreateResponseAsync<T>(
            BaseValidatingContext<T> context,
            Exception exception)
        {
            return ExceptionHandlerUtilities.ReportExceptionAndCreateResponseAsync(context, exception);
        }

        public void ReportExceptionAsync(Exception exception)
        {
            ExceptionHandlerUtilities.ReportExceptionAsync(exception);
        } 
    }
}