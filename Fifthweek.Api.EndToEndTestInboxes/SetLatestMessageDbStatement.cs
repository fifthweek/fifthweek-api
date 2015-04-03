namespace Fifthweek.Api.EndToEndTestMailboxes
{
    using System;
    using System.Threading.Tasks;

    using Fifthweek.Api.Core;
    using Fifthweek.Api.EndToEndTestMailboxes.Shared;
    using Fifthweek.Api.Persistence;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;

    [AutoConstructor]
    public partial class SetLatestMessageDbStatement : ISetLatestMessageDbStatement
    {
        private readonly IFifthweekDbConnectionFactory connectionFactory;

        public async Task ExecuteAsync(MailboxName mailboxName, string subject, string body)
        {
            mailboxName.AssertNotNull("mailboxName");
            subject.AssertNotNull("subject");
            body.AssertNotNull("body");

            var email = new EndToEndTestEmail(mailboxName.Value, subject, body, DateTime.UtcNow);

            using (var connection = this.connectionFactory.CreateConnection())
            {
                await connection.UpsertAsync(email, EndToEndTestEmail.Fields.Subject | EndToEndTestEmail.Fields.Body | EndToEndTestEmail.Fields.DateReceived);
            }
        }
    }
}