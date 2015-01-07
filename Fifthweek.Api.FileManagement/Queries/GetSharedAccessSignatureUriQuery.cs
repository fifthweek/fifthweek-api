namespace Fifthweek.Api.FileManagement.Queries
{
    using Fifthweek.Api.Core;

    [AutoConstructor]
    [AutoEqualityMembers]
    public partial class GetSharedAccessSignatureUriQuery : IQuery<string>
    {
        public FileId FileId { get; private set; }
    }
}