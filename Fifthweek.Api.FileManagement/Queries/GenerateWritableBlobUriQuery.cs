namespace Fifthweek.Api.FileManagement.Queries
{
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Membership;
    using Fifthweek.CodeGeneration;

    [AutoConstructor]
    [AutoEqualityMembers]
    public partial class GenerateWritableBlobUriQuery : IQuery<string>
    {
        public UserId AuthenticatedUserId { get; private set; }

        public FileId FileId { get; private set; }

        public string Purpose { get; private set; }
    }
}