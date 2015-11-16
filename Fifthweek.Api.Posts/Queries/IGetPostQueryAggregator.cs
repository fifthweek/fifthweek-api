namespace Fifthweek.Api.Posts.Queries
{
    using System.Threading.Tasks;

    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Posts.Shared;

    public interface IGetPostQueryAggregator
    {
        Task<GetPostQueryResult> ExecuteAsync(GetPostDbResult postData, PostSecurityResult access, AccessSignatureExpiryInformation expiry);
    }
}