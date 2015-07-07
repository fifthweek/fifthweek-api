namespace Fifthweek.Api
{
    using System;

    using Fifthweek.Api.Core;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Logging;
    using Fifthweek.Shared;

    [AutoConstructor]
    public partial class ExceptionHandler : IExceptionHandler
    {
        private readonly IRequestContext requestContext;

        public void ReportExceptionAsync(Exception exception)
        {
            var developerName = ExceptionHandlerUtilities.GetDeveloperName(this.requestContext.Request);
            ExceptionHandlerUtilities.ReportExceptionAsync(exception, developerName);
        }

        public void ReportExceptionAsync(Exception exception, string developerName)
        {
            ExceptionHandlerUtilities.ReportExceptionAsync(exception, developerName);
        }
    }
}