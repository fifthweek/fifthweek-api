namespace Fifthweek.Api.Posts
{
    using System;
    using System.Threading.Tasks;

    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Posts.Shared;


    public interface IPostToCollectionDbStatement
    {
        Task ExecuteAsync(
            Shared.PostId newPostId,
            CollectionId collectionId,
            ValidComment comment,
            DateTime? sheduledPostDate,
            bool isQueued,
            FileId fileId,
            bool isFileImage,
            DateTime now);
    }
}