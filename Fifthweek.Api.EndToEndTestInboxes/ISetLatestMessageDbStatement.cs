namespace Fifthweek.Api.EndToEndTestMailboxes
{
    using System.Threading.Tasks;

    using Fifthweek.Api.EndToEndTestMailboxes.Shared;

    public interface ISetLatestMessageDbStatement
    {
        Task ExecuteAsync(MailboxName mailboxName, string subject, string body);
    }
}