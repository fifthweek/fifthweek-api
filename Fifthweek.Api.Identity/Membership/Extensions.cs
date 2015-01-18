namespace Fifthweek.Api.Identity.Membership
{
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.OAuth;

    public static class Extensions
    {
        public static Requester GetRequester(this IUserContext userContext)
        {
            var userId = userContext.TryGetUserId();
            return userId != null 
                ? Requester.Authenticated(userId) 
                : Requester.Unauthenticated;
        }

        public static void AssertAuthorizedFor(this Requester requester, UserId requiredUserId)
        {
            UserId userId;
            requester.AssertAuthenticated(out userId);

            if (requiredUserId != null && !requiredUserId.Equals(userId))
            {
                throw new UnauthorizedException(
                    "The user " + requiredUserId.Value.EncodeGuid() + " was required, but user "
                    + userId.Value.EncodeGuid() + " was authenticated.");
            }
        }
    }
}