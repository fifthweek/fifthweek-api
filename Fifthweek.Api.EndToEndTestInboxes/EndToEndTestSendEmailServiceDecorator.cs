namespace Fifthweek.Api.EndToEndTestMailboxes
{
    using System.Linq;
    using System.Threading.Tasks;

    using Fifthweek.Api.Core;
    using Fifthweek.Api.EndToEndTestMailboxes.Shared;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;

    using Constants = Fifthweek.Api.Core.Constants;

    [AutoConstructor]
    public partial class EndToEndTestSendEmailServiceDecorator : ISendEmailService
    {
        private readonly ISendEmailService baseService;
        private readonly ISetLatestMessageDbStatement setLatestMessage;

        public Task SendEmailAsync(string to, string subject, string message)
        {
            to.AssertNotNull("to");
            subject.AssertNotNull("subject");
            message.AssertNotNull("message");

            MailboxName mailboxName;
            if (to.EndsWith(Constants.TestEmailDomain) && MailboxName.TryParse(to.Split('@').FirstOrDefault(), out mailboxName))
            {
                return this.setLatestMessage.ExecuteAsync(mailboxName, subject, message);
            }

            return this.baseService.SendEmailAsync(to, subject, message);
        }
    }
}