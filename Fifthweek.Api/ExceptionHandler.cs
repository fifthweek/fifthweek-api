namespace Fifthweek.Api
{
    using System;

    using Fifthweek.Api.Core;
    using Fifthweek.CodeGeneration;

    [AutoConstructor]
    public partial class ExceptionHandler : IExceptionHandler
    {
        private readonly IRequestContext requestContext;

        public void ReportExceptionAsync(Exception exception)
        {
            var developerName = ExceptionHandlerUtilities.GetDeveloperName(this.requestContext.Request);
            ExceptionHandlerUtilities.ReportExceptionAsync(exception, developerName);
        }
    }
}