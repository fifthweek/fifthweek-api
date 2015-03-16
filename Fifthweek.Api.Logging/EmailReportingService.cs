namespace Fifthweek.Api.Logging
{
    using System;
    using System.Threading.Tasks;

    using Fifthweek.Api.Core;

    public class EmailReportingService : IReportingService
    {
        private readonly ISendEmailService sendEmailService;

        public EmailReportingService(ISendEmailService sendEmailService)
        {
            this.sendEmailService = sendEmailService;
        }

        public Task ReportErrorAsync(Exception t, string identifier, Developer developer)
        {
            if (developer != null && string.IsNullOrWhiteSpace(developer.FifthweekEmail))
            {
                return Task.FromResult(true);
            }

            return this.sendEmailService.SendEmailAsync(
                developer == null ? Constants.ErrorEmailAddress : developer.FifthweekEmail,
                "An error occurred: " + identifier,
                t.ToString());
        }
    }
}