using Fifthweek.Api.Core;

namespace Fifthweek.Api.Identity.OAuth
{
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;

    [AutoEqualityMembers, AutoConstructor]
    public partial class ClientId
    {
        public string Value { get; private set; }
    }
}