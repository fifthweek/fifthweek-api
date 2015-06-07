namespace Fifthweek.Payments.Services
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

        public UserId UserId { get; set; }

        public SnapshotType SnapshotType { get; set; }
    }
}