namespace Fifthweek.Api.Collections
{
    using System.Threading.Tasks;

    public interface ICountQueuedPostsInCollectionDbStatement
    {
        Task<int> ExecuteAsync(CollectionId collectionId);
    }
}