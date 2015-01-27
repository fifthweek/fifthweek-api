namespace Fifthweek.Api.Posts
{
    using System;
    using System.Threading.Tasks;

    using Fifthweek.Api.Collections;
    using Fifthweek.Api.FileManagement;

    using FileId = Fifthweek.Api.FileManagement.Shared.FileId;

    public interface IPostToCollectionDbStatement
    {
        Task ExecuteAsync(
            PostId newPostId,
            CollectionId collectionId,
            ValidComment comment,
            DateTime? sheduledPostDate,
            bool isQueued,
            FileId fileId,
            bool isFileImage,
            DateTime now);
    }
}