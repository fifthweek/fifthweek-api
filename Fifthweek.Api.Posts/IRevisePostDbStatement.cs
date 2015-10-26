namespace Fifthweek.Api.Posts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Posts.Shared;

    public interface IRevisePostDbStatement
    {
        Task ExecuteAsync(
            PostId postId,
            ValidComment content,
            ValidPreviewText previewText,
            FileId previewImageId,
            IReadOnlyList<FileId> fileIds,
            int previewWordCount,
            int wordCount,
            int imageCount,
            int fileCount);
    }
}