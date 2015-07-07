namespace Fifthweek.Api
{
    using System;
    using System.Threading.Tasks;

    using Fifthweek.Api.Core;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Logging;
    using Fifthweek.Shared;

    [AutoConstructor]
    public partial class FifthweekActivityReporter : IFifthweekActivityReporter
    {
        private readonly IActivityReportingService activityReporter;
        private readonly IRequestContext requestContext;
        private readonly IDeveloperRepository developerRepository;
        private readonly IExceptionHandler exceptionHandler;

        public async void ReportActivityInBackground(string activity)
        {
            var developerName = ExceptionHandlerUtilities.GetDeveloperName(this.requestContext.Request);
            var developer = await this.developerRepository.TryGetByGitNameAsync(developerName);

            try
            {
                await this.activityReporter.ReportActivityAsync(activity, developer);
            }
            catch (Exception t)
            {
                this.exceptionHandler.ReportExceptionAsync(t, developerName);
            }
        }

        public async Task ReportActivityAsync(string activity)
        {
            var developerName = ExceptionHandlerUtilities.GetDeveloperName(this.requestContext.Request);
            var developer = await this.developerRepository.TryGetByGitNameAsync(developerName);

            await this.activityReporter.ReportActivityAsync(activity, developer);
        }
    }
}