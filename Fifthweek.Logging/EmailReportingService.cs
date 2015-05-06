namespace Fifthweek.Logging
{
    using System;
    using System.Threading.Tasks;

    using Fifthweek.Shared;

    public class EmailReportingService : IReportingService
    {
        public const string ErrorEmailAddress = "services@fifthweek.com";

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

            var message = t.ToString();
            var subject = t.Message.Replace("\r", string.Empty).Replace("\n", string.Empty);
            subject = subject.Substring(0,  Math.Min(subject.Length, 70));

            return this.sendEmailService.SendEmailAsync(
                developer == null ? ErrorEmailAddress : developer.FifthweekEmail,
                "Error: " + subject,
                message + Environment.NewLine + Environment.NewLine + identifier);
        }

        public Task ReportActivityAsync(string activity, Developer developer)
        {
            return this.sendEmailService.SendEmailAsync(
                developer == null ? ErrorEmailAddress : developer.FifthweekEmail,
                "Activity: " + activity,
                activity);
        }
    }
}