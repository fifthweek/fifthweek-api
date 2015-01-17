namespace Fifthweek.Api.Persistence.Tests.Shared
{
    using System;

    public interface IWildcardEntity : IIdentityEquatable
    {
        Type EntityType { get; }

        object GetExpectedValue(object other);
    }
}