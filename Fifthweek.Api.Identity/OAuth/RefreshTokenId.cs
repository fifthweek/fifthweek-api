using Fifthweek.Api.Core;

namespace Fifthweek.Api.Identity.OAuth
{
    using System;

    [AutoEqualityMembers, AutoConstructor]
    public partial class RefreshTokenId
    {
        public string Value { get; private set; }

        public static RefreshTokenId Create()
        {
            return new RefreshTokenId(Guid.NewGuid().ToString("n"));
        }

        public override string ToString()
        {
            return this.Value;
        }
    }
}