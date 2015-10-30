namespace Fifthweek.Api.Posts.Queries
{
    using System.Threading.Tasks;

    using Fifthweek.Api.FileManagement.Shared;

    public interface IGetPostQueryAggregator
    {
        Task<GetPostQueryResult> ExecuteAsync(GetPostDbResult postData, bool hasAccess, bool isPreview, AccessSignatureExpiryInformation expiry);
    }
}