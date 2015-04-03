namespace Fifthweek.Shared
{
    using System.Threading.Tasks;

    public interface ISendEmailService
    {
        Task SendEmailAsync(
            string to,
            string subject,
            string message);
    }
}