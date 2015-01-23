namespace Fifthweek.Api.Identity.OAuth
{
    using Fifthweek.Api.Identity.Membership;

    public interface IUserContext
    {
        Requester GetRequester();
    }
}