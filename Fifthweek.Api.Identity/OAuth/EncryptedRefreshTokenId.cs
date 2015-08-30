namespace Fifthweek.Api.Identity.OAuth
{
    using Fifthweek.Api.Core;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;

    [AutoEqualityMembers, AutoConstructor]
    public partial class EncryptedRefreshTokenId
    {
        public string Value { get; private set; }
    }
}