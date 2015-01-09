namespace Fifthweek.Api.FileManagement.Queries
{
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Membership;

    [AutoConstructor]
    [AutoEqualityMembers]
    public partial class GenerateWritableBlobUriQuery : IQuery<string>
    {
        public UserId Requester { get; private set; }

        public FileId FileId { get; private set; }
    }
}