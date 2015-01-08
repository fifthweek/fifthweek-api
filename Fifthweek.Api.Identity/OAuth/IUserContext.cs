namespace Fifthweek.Api.Identity.OAuth
{
    using Fifthweek.Api.Identity.Membership;

    public interface IUserContext
    {
        string GetUsername();

        UserId GetUserId();

        bool IsUserInRole(string role);

        bool IsAuthenticated { get; }
    }
}