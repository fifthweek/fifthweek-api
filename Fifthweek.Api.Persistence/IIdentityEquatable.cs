namespace Fifthweek.Api.Persistence
{
    public interface IIdentityEquatable
    {
        bool IdentityEquals(object other);
    }
}