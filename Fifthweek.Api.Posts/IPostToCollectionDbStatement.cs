namespace Fifthweek.Api.Posts
{
    using System;
    using System.Threading.Tasks;

    using Fifthweek.Api.Collections;
    using Fifthweek.Api.FileManagement;
    using Fifthweek.Api.Subscriptions;

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