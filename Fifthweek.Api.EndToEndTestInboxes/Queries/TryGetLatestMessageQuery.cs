namespace Fifthweek.Api.EndToEndTestMailboxes.Queries
{
    using Fifthweek.Api.Core;
    using Fifthweek.Api.EndToEndTestMailboxes.Shared;
    using Fifthweek.CodeGeneration;

    [AutoConstructor, AutoEqualityMembers]
    public partial class TryGetLatestMessageQuery : IQuery<Message>
    {
        public MailboxName MailboxName { get; private set; }
    }
}