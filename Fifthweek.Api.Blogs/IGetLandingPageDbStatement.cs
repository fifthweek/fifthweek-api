namespace Fifthweek.Api.Blogs
{
    using System.Threading.Tasks;

    using Fifthweek.Api.Identity.Shared.Membership;

    public interface IGetLandingPageDbStatement
    {
        Task<GetLandingPageDbStatement.GetLandingPageDbResult> ExecuteAsync(Username username);
    }
}