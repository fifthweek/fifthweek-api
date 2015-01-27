namespace Fifthweek.Api.FileManagement
{
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;

    [AutoConstructor]
    public partial class FileWaitingForUpload
    {
        public Shared.FileId FileId { get; private set; }

        public UserId UserId { get; private set; }

        public string FileNameWithoutExtension { get; private set; }

        public string FileExtension { get; private set; }

        public string Purpose { get; private set; }
    }
}