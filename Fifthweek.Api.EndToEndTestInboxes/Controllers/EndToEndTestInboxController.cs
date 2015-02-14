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
        private readonly IQueryHandler<TryGetLatestMessageQuery, Message> tryGetLatestMessage;
            
        [Route("{mailboxName}")]
        public async Task<HttpResponseMessage> GetLatestMessageAndClearMailboxAsync(string mailboxName)
        {
            mailboxName.AssertUrlParameterProvided("mailboxName");
            var parsedMailboxName = new MailboxNameData(mailboxName).Parse().MailboxName;

            string responseBody;
            var latestMessage = await this.tryGetLatestMessage.HandleAsync(new TryGetLatestMessageQuery(parsedMailboxName));
            if (latestMessage == null)
            {
                responseBody = @"<p class=""lead text-danger""><span id=""mailbox-empty"">Mailbox Empty!</span></p>";
            }
            else
            {
                await this.deleteAllMessages.HandleAsync(new DeleteAllMessagesCommand(parsedMailboxName));

                responseBody = string.Format(
                    @"<p class=""lead""><span id=""email-subject"">{0}</span></p>
                    <hr />
                    <div id=""email-body"">{1}</div>", 
                    latestMessage.Subject, 
                    latestMessage.Body);
            }

            var response = new HttpResponseMessage
            {
                Content = new StringContent(ApplyHtmlTemplate(responseBody))
            };

            response.Content.Headers.ContentType = new MediaTypeHeaderValue("text/html");

            return response;
        }

        private static string ApplyHtmlTemplate(string innerHtml)
        {
            return string.Format(
                @"<!DOCTYPE html>
                <html ng-app>
                <head>
                    <script src=""//ajax.googleapis.com/ajax/libs/angularjs/1.3.13/angular.min.js""></script>
                    <link rel=""stylesheet"" href=""https://maxcdn.bootstrapcdn.com/bootstrap/3.3.2/css/bootstrap.min.css"">
                    <link rel=""stylesheet"" href=""https://maxcdn.bootstrapcdn.com/bootstrap/3.3.2/css/bootstrap-theme.min.css"">
                </head>
                <body>
                <div style=""width:700px; padding:30px 30px 10px 30px; margin:50px auto; border: 1px solid #aaa;"">
                    {0}
                </div>
                </body>
                </html>",
                innerHtml);
        }
    }
}