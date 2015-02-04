namespace Fifthweek.Api.Persistence.Tests.Shared
{
    using System;

    public interface IWildcardEntity : IIdentityEquatable
    {
        Type EntityType { get; }

        IIdentityEquatable GetExpectedValue(IIdentityEquatable other);
    }
}