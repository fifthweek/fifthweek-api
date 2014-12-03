namespace Fifthweek.Api.Services
{
    using System.Net;
    using System.Net.Mail;
    using System.Threading.Tasks;

    public interface ISendEmailService
    {
        Task SendEmailAsync(
            string to,
            string subject,
            string message);
    }
}