namespace Fifthweek.Api
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Security;
    using System.Threading.Tasks;
    using System.Web;
    using System.Web.Hosting;

    using Fifthweek.Api.Core;
    using Fifthweek.Api.Logging;
    using Fifthweek.Shared;

    using Microsoft.Owin;
    using Microsoft.Owin.Security.OAuth;

    public static class ExceptionHandlerUtilities
    {
        public static async Task<HttpResponseMessage> ReportExceptionAndCreateResponseAsync(
            HttpRequestMessage request,
            Exception exception,
            string developerName)
        {
            string identifier = string.Empty;
            try
            {
                // I don't want to use Autofac here as it may be the dependency resolution
                // causing the error.
                var developer = await HardwiredDependencies.NewDefaultDeveloperRepository().TryGetByGitNameAsync(developerName);
                identifier = exception.GetExceptionIdentifier();

                await HardwiredDependencies.NewErrorReportingService().ReportErrorAsync(exception, identifier, developer);
            }
            catch (Exception t)
            {
                System.Diagnostics.Trace.TraceError("Failed to report errors: " + t);

                return request.CreateErrorResponse(
                        HttpStatusCode.InternalServerError,
                        "An error occurred and could not be reported: " + identifier);
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
            else if (exception is UnauthenticatedException)
            {
                // Yes, it is counterintuative that an UnauthenticatedException goes to Unauthorized status code,
                // while UnauthorizedException goes to Forbidden staus code, but that is the recommended practice.
                // Even the tooltip for HttpStatusCode.Unauthorized says so.
                return request.CreateErrorResponse(HttpStatusCode.Unauthorized, HttpStatusCode.Unauthorized.ToString());
            }
            else if (exception is UnauthorizedException || exception is UnauthorizedAccessException)
            {
                return request.CreateErrorResponse(HttpStatusCode.Forbidden, HttpStatusCode.Unauthorized.ToString());
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
            Exception exception, 
            string developerName)
        {
            string identifier = string.Empty;
            try
            {
                // I don't want to use Autofac here as it may be the dependency resolution
                // causing the error.
                var developer = await HardwiredDependencies.NewDefaultDeveloperRepository().TryGetByGitNameAsync(developerName);
                identifier = exception.GetExceptionIdentifier();

                await HardwiredDependencies.NewErrorReportingService().ReportErrorAsync(exception, identifier, developer);
                context.SetError("internal_error", "Something went wrong: " + identifier);
            }
            catch (Exception t)
            {
                System.Diagnostics.Trace.TraceError("Failed to report errors: " + t);
                context.SetError("internal_error", "An error occurred and could not be reported: " + identifier);
            }
        }

        public static void ReportExceptionAsync(Exception exception, string developerName)
        {
            try
            {
                var identifier = exception.GetExceptionIdentifier();

                HostingEnvironment.QueueBackgroundWorkItem(ct => ReportErrorInBackground(developerName, exception, identifier));
            }
            catch (Exception t)
            {
                System.Diagnostics.Trace.TraceError("Failed to report errors: " + t);
            }
        }

        public static string GetDeveloperName(HttpRequestMessage request)
        {
            if (request != null && request.Headers != null)
            {
                IEnumerable<string> values;
                if (request.Headers.TryGetValues(Core.Constants.DeveloperNameRequestHeaderKey, out values))
                {
                    return values.FirstOrDefault();
                }
            }

            return null;
        }

        public static string GetDeveloperName(IOwinRequest request)
        {
            if (request != null && request.Headers != null)
            {
                string[] values;
                if (request.Headers.TryGetValue(Core.Constants.DeveloperNameRequestHeaderKey, out values))
                {
                    return values.FirstOrDefault();
                }
            }

            return null;
        }

        private static async Task ReportErrorInBackground(string developerName, Exception exception, string exceptionIdentifier)
        {
            // I don't want to use Autofac here as it may be the dependency resolution
            // causing the error.
            var developer = await HardwiredDependencies.NewDefaultDeveloperRepository().TryGetByGitNameAsync(developerName);
            await HardwiredDependencies.NewErrorReportingService().ReportErrorAsync(exception, exceptionIdentifier, developer);
        }
    }
}