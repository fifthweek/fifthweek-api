namespace Fifthweek.Api.Services
{
    using System;
    using System.Threading.Tasks;

    public class EmailReportingService : IReportingService
    {
        private readonly ISendEmailService sendEmailService;

        public EmailReportingService(ISendEmailService sendEmailService)
        {
            this.sendEmailService = sendEmailService;
        }

        public Task ReportErrorAsync(Exception t, string identifier)
        {
            return this.sendEmailService.SendEmailAsync(
                Constants.ErrorEmailAddress,
                "An error occured: " + identifier,
                t.ToString());
        }
    }
}