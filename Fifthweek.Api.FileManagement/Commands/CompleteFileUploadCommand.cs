namespace Fifthweek.Api.FileManagement.Commands
{
    using Fifthweek.Api.Identity.Membership;
    using Fifthweek.CodeGeneration;

    [AutoConstructor]
    [AutoEqualityMembers]
    public partial class CompleteFileUploadCommand
    {
        public UserId AuthenticatedUserId { get; private set; }

        public FileId FileId { get; private set; }
    }
}