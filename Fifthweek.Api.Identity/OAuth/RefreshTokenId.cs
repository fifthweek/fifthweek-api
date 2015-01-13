namespace Fifthweek.Api.Identity.OAuth
{
    using System;

    using Fifthweek.Api.Core;

    [AutoEqualityMembers, AutoConstructor]
    public partial class RefreshTokenId
    {
        public string Value { get; private set; }

        public static RefreshTokenId Create()
        {
            return new RefreshTokenId(Guid.NewGuid().ToString("n"));
        }
    }
}