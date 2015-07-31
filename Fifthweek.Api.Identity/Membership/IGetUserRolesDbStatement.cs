namespace Fifthweek.Api.Identity.Membership
{
    using System.Threading.Tasks;

    using Fifthweek.Api.Identity.Shared.Membership;

    public interface IGetUserRolesDbStatement
    {
        Task<GetUserRolesDbStatement.UserRoles> ExecuteAsync(UserId userId);
    }
}