namespace Fifthweek.Api.Core
{
    using System;

    public interface IExceptionHandler
    {
        void ReportExceptionAsync(
            Exception exception);

        void ReportExceptionAsync(Exception exception, string developerName);
    }
}