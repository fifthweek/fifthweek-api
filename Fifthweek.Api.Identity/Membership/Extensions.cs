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
    }
}