namespace Fifthweek.Api.Identity.OAuth
{
    using System;

    using Fifthweek.Api.Core;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;

    [AutoEqualityMembers, AutoConstructor]
    public partial class RefreshTokenId
    {
        public string Value { get; private set; }

        public static RefreshTokenId Create()
        {
            return new RefreshTokenId(Guid.NewGuid().EncodeGuid());
        }
    }
}