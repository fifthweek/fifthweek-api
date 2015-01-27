namespace Fifthweek.Api.Identity.OAuth
{
    using Fifthweek.Api.Identity.Membership;
    using Fifthweek.Api.Identity.Shared.Membership;

    public interface IUserContext
    {
        Requester GetRequester();
    }
}