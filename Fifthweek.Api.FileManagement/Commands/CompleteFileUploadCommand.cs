namespace Fifthweek.Api.FileManagement.Commands
{
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Membership;

    [AutoConstructor]
    [AutoEqualityMembers]
    public partial class CompleteFileUploadCommand
    {
        public UserId Requester { get; private set; }

        public FileId FileId { get; private set; }
    }
}