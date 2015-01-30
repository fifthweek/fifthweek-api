namespace Fifthweek.Api.Identity.OAuth
{
    using System.Threading.Tasks;

    using Fifthweek.Api.Identity.Shared.Membership;

    public interface IGetUserAndRolesFromCredentialsDbStatement
    {
        Task<UserIdAndRoles> ExecuteAsync(Username username, Password password);
    }
}