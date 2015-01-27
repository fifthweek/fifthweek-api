namespace Fifthweek.Api.FileManagement
{
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Membership;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;

    using UserId = Fifthweek.Api.Identity.Shared.Membership.UserId;

    [AutoConstructor]
    public partial class FileWaitingForUpload
    {
        public FileId FileId { get; private set; }

        public UserId UserId { get; private set; }

        public string FileNameWithoutExtension { get; private set; }

        public string FileExtension { get; private set; }

        public string Purpose { get; private set; }
    }
}