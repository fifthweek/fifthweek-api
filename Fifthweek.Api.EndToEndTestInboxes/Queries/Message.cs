namespace Fifthweek.Api.EndToEndTestMailboxes.Queries
{
    using Fifthweek.CodeGeneration;

    [AutoConstructor, AutoEqualityMembers]
    public partial class Message
    {
        public string Subject { get; private set; }

        public string Body { get; private set; }
    }
}