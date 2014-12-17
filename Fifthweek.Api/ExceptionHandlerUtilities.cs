namespace Fifthweek.Api
{
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;
    using System.Web.Hosting;
    using System.Web.Http.Controllers;
    using System.Web.Http.Filters;

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
                var reportingService = Constants.DefaultReportingService;
                identifier = exception.GetExceptionIdentifier();

                await reportingService.ReportErrorAsync(exception, identifier);
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
            else
            {
                // For anything else we want to hide the error details.
                return request.CreateErrorResponse(
                        HttpStatusCode.InternalServerError,
                        "Something went wrong: " + identifier);
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
                var reportingService = Constants.DefaultReportingService;
                identifier = exception.GetExceptionIdentifier();

                await reportingService.ReportErrorAsync(exception, identifier);
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
                // I don't want to use Autofac here as it may be the dependency resolution
                // causing the error.
                var reportingService = Constants.DefaultReportingService;
                var identifier = exception.GetExceptionIdentifier();

                HostingEnvironment.QueueBackgroundWorkItem(ct => reportingService.ReportErrorAsync(exception, identifier));
            }
            catch (Exception t)
            {
                System.Diagnostics.Trace.TraceError("Failed to report errors: " + t);
            }
        }
    }
}