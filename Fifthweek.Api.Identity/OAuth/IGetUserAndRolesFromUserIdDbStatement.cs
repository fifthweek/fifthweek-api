namespace Fifthweek.Api.Identity.OAuth
{
    using System.Threading.Tasks;

    using Fifthweek.Api.Identity.Shared.Membership;

    public interface IGetUserAndRolesFromUserIdDbStatement
    {
        Task<UsernameAndRoles> ExecuteAsync(UserId userId);
    }
}