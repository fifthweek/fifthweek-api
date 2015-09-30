namespace Fifthweek.Shared
{
    using System.Net.Mail;
    using System.Threading.Tasks;

    public interface ISendEmailService
    {
        Task SendEmailAsync(
            string to,
            string subject,
            string message);

        Task SendEmailAsync(
            MailAddress from,
            string to,
            string subject,
            string content);
    }
}