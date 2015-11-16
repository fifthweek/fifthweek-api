namespace Fifthweek.Api.Identity.Membership
{
    using System.Threading.Tasks;

    using Fifthweek.Api.Identity.Shared.Membership;

    public interface IGetFeedbackUserDataDbStatement
    {
        Task<GetFeedbackUserDataDbStatement.GetFeedbackUserDataResult> ExecuteAsync(UserId userId);
    }
}