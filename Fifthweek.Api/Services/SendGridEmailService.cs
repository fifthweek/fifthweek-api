namespace Fifthweek.Api.Services
{
    using System;
    using System.Net;
    using System.Net.Mail;
    using System.Threading.Tasks;

    using SendGrid;

    public class SendGridEmailService : ISendEmailService
    {
        private readonly NetworkCredential credentials = new NetworkCredential(
            Environment.GetEnvironmentVariable("SENDGRID_USERNAME"),
            Environment.GetEnvironmentVariable("SENDGRID_PASSWORD"));

        public Task SendEmailAsync(
            string to,
            string subject,
            string content)
        {
            if (string.IsNullOrWhiteSpace(this.credentials.UserName))
            {
                throw new Exception("The username for SendGrid was not found in your environment variables.");
            }

            if (string.IsNullOrWhiteSpace(this.credentials.Password))
            {
                throw new Exception("The password for SendGrid was not found in your environment variables.");
            }

            if (string.IsNullOrWhiteSpace(to))
            {
                throw new Exception("No \"to\" address was specified for the email.");
            }

            var message = new SendGridMessage();
            message.AddTo(to);
            message.From = Constants.FifthweekEmailAddress;
            message.Subject = subject;
            message.Text = content;

            var transportWeb = new Web(this.credentials);
            return transportWeb.DeliverAsync(message);
        }
    }
}