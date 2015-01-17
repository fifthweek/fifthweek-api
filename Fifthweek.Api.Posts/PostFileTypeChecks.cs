namespace Fifthweek.Api.Posts
{
    using System.Threading.Tasks;

    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement;
    using Fifthweek.CodeGeneration;

    [AutoConstructor]
    public partial class PostFileTypeChecks : IPostFileTypeChecks
    {
        private readonly IGetFileExtensionDbStatement getFileExtension;

        public async Task<bool> IsValidForFilePostAsync(FileId fileId)
        {
            var fileExtension = await this.getFileExtension.ExecuteAsync(fileId);
            throw new System.NotImplementedException();
        }

        public async Task<bool> IsValidForImagePostAsync(FileId fileId)
        {
            var fileExtension = await this.getFileExtension.ExecuteAsync(fileId);
            throw new System.NotImplementedException();
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
            var isValid = await this.IsValidForImagePostAsync(fileId);
            if (!isValid)
            {
                throw new RecoverableException("Not a valid file type for image posts."); // Should this be an UnauthorizedException?
            }
        }
    }
}