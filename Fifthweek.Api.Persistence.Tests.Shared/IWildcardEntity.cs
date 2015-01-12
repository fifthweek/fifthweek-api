namespace Fifthweek.Api.Persistence.Tests.Shared
{
    public interface IWildcardEntity : IIdentityEquatable
    {
        bool WildcardEquals(object other);
    }
}