namespace Fifthweek.Payments.SnapshotCreation
{
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;

    [AutoConstructor]
    [AutoEqualityMembers]
    public partial class CreateSnapshotMessage
    {
        public CreateSnapshotMessage()
        {
        }

        [Optional]
        public UserId UserId { get; set; }

        public SnapshotType SnapshotType { get; set; }
    }
}