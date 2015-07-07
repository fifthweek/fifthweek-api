namespace Fifthweek.Shared
{
    using System;

    public interface IExceptionHandler
    {
        void ReportExceptionAsync(
            Exception exception);

        void ReportExceptionAsync(Exception exception, string developerName);
    }
}