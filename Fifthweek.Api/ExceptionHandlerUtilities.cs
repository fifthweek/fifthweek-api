namespace Fifthweek.Api
{
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Security;
    using System.Threading.Tasks;
    using System.Web;
    using System.Web.Hosting;

    using Fifthweek.Api.Core;
    using Fifthweek.Api.Logging;

    using Microsoft.Owin.Security.OAuth;

    public static class ExceptionHandlerUtilities
    {
        public static async Task<HttpResponseMessage> ReportExceptionAndCreateResponseAsync(
            HttpRequestMessage request,
            Exception exception)
        {
            string identifier = string.Empty;
            try
            {
                // I don't want to use Autofac here as it may be the dependency resolution
                // causing the error.
                var developer = await Constants.DefaultDeveloperRepository.TryGetByGitNameAsync(GetDeveloperName());
                identifier = exception.GetExceptionIdentifier();

                await Constants.DefaultReportingService.ReportErrorAsync(exception, identifier, developer);
            }
            catch (Exception t)
            {
                System.Diagnostics.Trace.TraceError("Failed to report errors: " + t);

                return request.CreateErrorResponse(
                        HttpStatusCode.InternalServerError,
                        "An error occured and could not be reported: " + identifier);
            }

            if (exception is RecoverableException)
            {
                // A bad request means there was a problem with the input, so we need
                // to tell them what the error was.
                // However we still report the issue above as the problem should have been picked up on
                // the client before sending the request, and so we need to fix it.
                return request.CreateErrorResponse(
                    HttpStatusCode.BadRequest,
                    exception.Message);
            }
            else if (exception is ForbiddenException || exception is SecurityException)
            {
                return request.CreateErrorResponse(HttpStatusCode.Forbidden, HttpStatusCode.Forbidden.ToString());
            }
            else if (exception is UnauthorizedException || exception is UnauthorizedAccessException)
            {
                return request.CreateErrorResponse(HttpStatusCode.Unauthorized, HttpStatusCode.Unauthorized.ToString());
            }
            else
            {
                // For anything else we want to hide the error details.
                return request.CreateErrorResponse(
                        HttpStatusCode.InternalServerError,
                        "Something went wrong, please try again later. The following number tells us the problem you're having :) " + identifier);
            }
        }

        public static async Task ReportExceptionAndCreateResponseAsync<T>(
            BaseValidatingContext<T> context,
            Exception exception)
        {
            string identifier = string.Empty;
            try
            {
                // I don't want to use Autofac here as it may be the dependency resolution
                // causing the error.
                var developer = await Constants.DefaultDeveloperRepository.TryGetByGitNameAsync(GetDeveloperName());
                identifier = exception.GetExceptionIdentifier();

                await Constants.DefaultReportingService.ReportErrorAsync(exception, identifier, developer);
                context.SetError("internal_error", "Something went wrong: " + identifier);
            }
            catch (Exception t)
            {
                System.Diagnostics.Trace.TraceError("Failed to report errors: " + t);
                context.SetError("internal_error", "An error occured and could not be reported: " + identifier);
            }
        }

        public static void ReportExceptionAsync(Exception exception)
        {
            try
            {
                var developerName = GetDeveloperName();
                var identifier = exception.GetExceptionIdentifier();

                HostingEnvironment.QueueBackgroundWorkItem(ct => ReportErrorInBackground(developerName, exception, identifier));
            }
            catch (Exception t)
            {
                System.Diagnostics.Trace.TraceError("Failed to report errors: " + t);
            }
        }

        private static async Task ReportErrorInBackground(string developerName, Exception exception, string exceptionIdentifier)
        {
            // I don't want to use Autofac here as it may be the dependency resolution
            // causing the error.
            var developer = await Constants.DefaultDeveloperRepository.TryGetByGitNameAsync(developerName);
            await Constants.DefaultReportingService.ReportErrorAsync(exception, exceptionIdentifier, developer);
        }

        private static string GetDeveloperName()
        {
            if (HttpContext.Current != null)
            {
                return HttpContext.Current.Request.Headers[Constants.DeveloperNameRequestHeaderKey];
            }

            return null;
        }
    }
}