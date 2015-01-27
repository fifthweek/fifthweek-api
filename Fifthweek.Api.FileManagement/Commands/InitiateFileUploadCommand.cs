namespace Fifthweek.Api.FileManagement.Commands
{
    using Fifthweek.Api.Identity.Membership;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;

    [AutoConstructor]
    [AutoEqualityMembers]
    public partial class InitiateFileUploadCommand
    {
        public Requester Requester { get; private set; }

        public FileId FileId { get; private set; }

        [Optional]
        public string FilePath { get; private set; }

        [Optional]
        public string Purpose { get; private set; }
    }
}