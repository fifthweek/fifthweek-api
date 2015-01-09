namespace Fifthweek.Api.FileManagement.Queries
{
    using System.Security;
    using System.Threading.Tasks;

    using Fifthweek.Api.Azure;
    using Fifthweek.Api.Core;

    [AutoConstructor]
    public partial class GenerateWritableBlobUriQueryHandler : IQueryHandler<GenerateWritableBlobUriQuery, string>
    {
        private readonly IBlobService blobService;

        private readonly IBlobNameCreator blobNameCreator;

        private readonly IFileRepository fileRepository;

        public async Task<string> HandleAsync(GenerateWritableBlobUriQuery query)
        {
            await this.fileRepository.AssertFileBelongsToUserAsync(query.Requester, query.FileId);

            const string ContainerName = FileManagement.Constants.FileBlobContainerName;
            var blobName = this.blobNameCreator.CreateFileName(query.FileId);
            var url = await this.blobService.GetBlobSasUriForWritingAsync(ContainerName, blobName);
            return url;
        }
    }
}