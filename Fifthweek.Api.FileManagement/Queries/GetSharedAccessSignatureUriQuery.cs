namespace Fifthweek.Api.FileManagement.Queries
{
    using Fifthweek.Api.Core;

    public class GetSharedAccessSignatureUriQuery : IQuery<string>
    {
        public GetSharedAccessSignatureUriQuery(FileId fileId)
        {
            this.FileId = fileId;
        }

        public FileId FileId { get; private set; }
    }
}