namespace Fifthweek.Api.FileManagement.Commands
{
    using Fifthweek.Api.Identity.Membership;
    using Fifthweek.CodeGeneration;

    [AutoConstructor]
    [AutoEqualityMembers]
    public partial class InitiateFileUploadCommand
    {
        public UserId AuthenticatedUserId { get; private set; }

        public FileId FileId { get; private set; }

        [Optional]
        public string FilePath { get; private set; }

        [Optional]
        public string Purpose { get; private set; }
    }
}