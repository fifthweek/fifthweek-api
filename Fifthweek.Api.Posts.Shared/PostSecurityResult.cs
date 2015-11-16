namespace Fifthweek.Api.Posts.Shared
{
    public enum PostSecurityResult
    {
        Denied,
        Subscriber,
        Owner,
        GuestList,
        FreePost
    }
}