namespace Fifthweek.Api.Core
{
    using System;
    using System.Net.Http;
    using System.Threading.Tasks;

    using Microsoft.Owin;
    using Microsoft.Owin.Security.OAuth;

    public interface IOwinExceptionHandler
    {
        Task<HttpResponseMessage> ReportExceptionAndCreateResponseAsync(
            HttpRequestMessage request,
            Exception exception);

        Task ReportExceptionAndCreateResponseAsync<T>(
            BaseValidatingContext<T> context,
            Exception exception);

        void ReportExceptionAsync(
            IOwinRequest request,
            Exception exception);
    }
}