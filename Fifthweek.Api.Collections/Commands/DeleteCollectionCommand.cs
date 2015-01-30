namespace Fifthweek.Api.Collections.Commands
{
    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;

    [AutoConstructor, AutoEqualityMembers]
    public partial class DeleteCollectionCommand
    {
        public Requester Requester { get; private set; }

        public CollectionId CollectionId { get; private set; }
    }
}