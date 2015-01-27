namespace Fifthweek.Api.Posts
{
    using System.Threading.Tasks;

    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;

    using FileId = Fifthweek.Api.FileManagement.Shared.FileId;

    [AutoConstructor]
    public partial class PostFileTypeChecks : IPostFileTypeChecks
    {
        private readonly IGetFileExtensionDbStatement getFileExtension;
        private readonly IMimeTypeMap mimeTypeMap;

        public Task<bool> IsValidForFilePostAsync(FileId fileId)
        {
            fileId.AssertNotNull("fileId");

            // Do we want to filter potentially dangerous files, or be agnostic like DropBox?
            // We need to consider technical people sharing code snippets which might have shell 
            // extensions (e.g. `.bat`), so we probably wouldn't want to filter on those?
            return Task.FromResult(true);
        }

        public async Task<bool> IsValidForImagePostAsync(FileId fileId)
        {
            fileId.AssertNotNull("fileId");

            var fileExtension = await this.getFileExtension.ExecuteAsync(fileId);
            var mimeType = this.mimeTypeMap.GetMimeType(fileExtension);

            return mimeType.StartsWith("image/");
        }

        public async Task AssertValidForFilePostAsync(FileId fileId)
        {
            fileId.AssertNotNull("fileId");

            var isValid = await this.IsValidForFilePostAsync(fileId);
            if (!isValid)
            {
                throw new RecoverableException("Not a valid file type for file posts."); // Should this be an UnauthorizedException?
            }
        }

        public async Task AssertValidForImagePostAsync(FileId fileId)
        {
            fileId.AssertNotNull("fileId");

            var isValid = await this.IsValidForImagePostAsync(fileId);
            if (!isValid)
            {
                throw new RecoverableException("Not a valid file type for image posts."); // Should this be an UnauthorizedException?
            }
        }
    }
}