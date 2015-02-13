namespace Fifthweek.Api.EndToEndTestMailboxes.Controllers
{
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Threading.Tasks;
    using System.Web.Http;

    using Fifthweek.Api.Core;
    using Fifthweek.Api.EndToEndTestMailboxes.Commands;
    using Fifthweek.Api.EndToEndTestMailboxes.Queries;
    using Fifthweek.CodeGeneration;

    [RoutePrefix("testMailboxes"), AutoConstructor]
    public partial class EndToEndTestInboxController : ApiController
    {
        private readonly ICommandHandler<DeleteAllMessagesCommand> deleteAllMessages; 
        private readonly IQueryHandler<GetLatestMessageQuery, Message> getLatestMessage;
            
        [Route("{mailboxName}")]
        public async Task<HttpResponseMessage> GetLatestMessageAndClearMailboxAsync(string mailboxName)
        {
            mailboxName.AssertUrlParameterProvided("mailboxName");
            var parsedMailboxName = new MailboxNameData(mailboxName).Parse().MailboxName;

            var latestMessage = await this.getLatestMessage.HandleAsync(new GetLatestMessageQuery(parsedMailboxName));

            await this.deleteAllMessages.HandleAsync(new DeleteAllMessagesCommand(parsedMailboxName));

            var response = new HttpResponseMessage 
            { 
                Content = new StringContent(string.Format(
                    @"<html>
                    <body>
                    <div style=""width:700px; padding:30px; border:1px solid #333;"">
                        <p><strong><span id=""email-subject"">{0}</span></strong></p>
                        <hr />
                        <div id=""email-body"">
                        {1}
                        </div>
                    </div>
                    </body>
                    </html>", 
                    latestMessage.Subject, 
                    latestMessage.Body)) 
            };

            response.Content.Headers.ContentType = new MediaTypeHeaderValue("text/html");

            return response;
        }
    }
}