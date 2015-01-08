namespace Fifthweek.Api.FileManagement.Commands
{
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Membership;

    [AutoConstructor]
    [AutoEqualityMembers]
    public partial class InitiateFileUploadRequestCommand
    {
        public FileId FileId { get; private set; }

        public UserId UserId { get; private set; }
    }
}