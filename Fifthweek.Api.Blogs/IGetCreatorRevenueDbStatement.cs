namespace Fifthweek.Api.Blogs
{
    using System.Threading.Tasks;

    using Fifthweek.Api.Identity.Shared.Membership;

    public interface IGetCreatorRevenueDbStatement
    {
        Task<GetCreatorRevenueDbStatement.GetCreatorRevenueDbStatementResult> ExecuteAsync(UserId userId);
    }
}