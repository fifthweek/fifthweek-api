namespace Fifthweek.Api.FileManagement.Queries
{
    using System.Threading.Tasks;

    using Fifthweek.Api.Azure;
    using Fifthweek.Api.Core;

    [AutoConstructor]
    public partial class GenerateWritableBlobUriQueryHandler : IQueryHandler<GenerateWritableBlobUriQuery, string>
    {
        private readonly IBlobService blobService;

        private readonly IBlobNameCreator blobNameCreator;

        public Task<string> HandleAsync(GenerateWritableBlobUriQuery query)
        {
            // Check the requester has access to this file.

            const string ContainerName = FileManagement.Constants.FileBlobContainerName;
            var blobName = this.blobNameCreator.CreateFileName(query.FileId);
            return this.blobService.GetBlobSasUriForWritingAsync(ContainerName, blobName);
        }
    }
}