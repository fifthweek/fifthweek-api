namespace Fifthweek.Api.Services
{
    using System;
    using System.Net.Http;
    using System.Threading.Tasks;

    using Microsoft.Owin.Security.OAuth;

    public interface IExceptionHandler
    {
        Task<HttpResponseMessage> ReportExceptionAndCreateResponseAsync(
            HttpRequestMessage request,
            Exception exception);

        Task ReportExceptionAndCreateResponseAsync<T>(
            BaseValidatingContext<T> context,
            Exception exception);

        void ReportExceptionAsync(Exception exception);
    }
}