using Fifthweek.Api.Core;

namespace Fifthweek.Api.Identity.OAuth
{
    [AutoEqualityMembers, AutoConstructor]
    public partial class ClientId
    {
        public string Value { get; private set; }
    }
}