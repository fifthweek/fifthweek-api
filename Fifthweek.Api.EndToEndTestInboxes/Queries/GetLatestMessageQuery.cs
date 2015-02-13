namespace Fifthweek.Api.EndToEndTestMailboxes.Queries
{
    using Fifthweek.Api.Core;
    using Fifthweek.Api.EndToEndTestMailboxes.Shared;
    using Fifthweek.CodeGeneration;

    [AutoConstructor, AutoEqualityMembers]
    public partial class GetLatestMessageQuery : IQuery<Message>
    {
        public MailboxName MailboxName { get; private set; }
    }
}