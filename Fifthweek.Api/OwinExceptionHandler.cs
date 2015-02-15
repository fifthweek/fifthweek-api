namespace Fifthweek.Api
{
    using System;
    using System.Net.Http;
    using System.Threading.Tasks;

    using Fifthweek.Api.Core;
    using Fifthweek.CodeGeneration;

    using Microsoft.Owin;
    using Microsoft.Owin.Security.OAuth;

    [AutoConstructor]
    public partial class OwinExceptionHandler : IOwinExceptionHandler
    {
        public Task<HttpResponseMessage> ReportExceptionAndCreateResponseAsync(
            HttpRequestMessage request,
            Exception exception)
        {
            var developerName = ExceptionHandlerUtilities.GetDeveloperName(request);
            return ExceptionHandlerUtilities.ReportExceptionAndCreateResponseAsync(request, exception, developerName);
        }

        public Task ReportExceptionAndCreateResponseAsync<T>(
            BaseValidatingContext<T> context,
            Exception exception)
        {
            var developerName = ExceptionHandlerUtilities.GetDeveloperName(context.Request);
            return ExceptionHandlerUtilities.ReportExceptionAndCreateResponseAsync(context, exception, developerName);
        }

        public void ReportExceptionAsync(IOwinRequest request, Exception exception)
        {
            var developerName = ExceptionHandlerUtilities.GetDeveloperName(request);
            ExceptionHandlerUtilities.ReportExceptionAsync(exception, developerName);
        }
    }
}