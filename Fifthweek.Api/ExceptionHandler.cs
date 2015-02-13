namespace Fifthweek.Api
{
    using System;
    using System.Net.Http;
    using System.Threading.Tasks;

    using Fifthweek.Api.Core;
    using Fifthweek.CodeGeneration;

    using Microsoft.Owin.Security.OAuth;

    [AutoConstructor]
    public partial class ExceptionHandler : IExceptionHandler
    {
        private readonly IRequestContext requestContext;

        public Task<HttpResponseMessage> ReportExceptionAndCreateResponseAsync(
            HttpRequestMessage request,
            Exception exception)
        {
            var developerName = ExceptionHandlerUtilities.GetDeveloperName(this.requestContext.HttpContext);
            return ExceptionHandlerUtilities.ReportExceptionAndCreateResponseAsync(request, exception, developerName);
        }

        public Task ReportExceptionAndCreateResponseAsync<T>(
            BaseValidatingContext<T> context,
            Exception exception)
        {
            var developerName = ExceptionHandlerUtilities.GetDeveloperName(this.requestContext.HttpContext);
            return ExceptionHandlerUtilities.ReportExceptionAndCreateResponseAsync(context, exception, developerName);
        }

        public void ReportExceptionAsync(Exception exception)
        {
            var developerName = ExceptionHandlerUtilities.GetDeveloperName(this.requestContext.HttpContext);
            ExceptionHandlerUtilities.ReportExceptionAsync(exception, developerName);
        } 
    }
}