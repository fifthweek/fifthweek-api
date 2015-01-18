namespace Fifthweek.Api.Identity.OAuth
{
    using Fifthweek.Api.Identity.Membership;

    public interface IUserContext
    {
        bool IsAuthenticated { get; }

        string TryGetUsername();

        UserId TryGetUserId();

        bool IsUserInRole(string role);
    }
}