namespace Fifthweek.Api.EndToEndTestMailboxes.Controllers
{
    using Fifthweek.Api.EndToEndTestMailboxes.Shared;
    using Fifthweek.CodeGeneration;

    [AutoConstructor]
    public partial class MailboxNameData
    {
        [Parsed(typeof(MailboxName))]
        public string MailboxName { get; private set; } 
    }
}