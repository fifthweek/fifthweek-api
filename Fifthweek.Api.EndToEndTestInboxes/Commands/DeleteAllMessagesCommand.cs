namespace Fifthweek.Api.EndToEndTestMailboxes.Commands
{
    using Fifthweek.Api.EndToEndTestMailboxes.Shared;
    using Fifthweek.CodeGeneration;

    [AutoConstructor, AutoEqualityMembers]
    public partial class DeleteAllMessagesCommand
    {
        public MailboxName MailboxName { get; private set; }
    }
}