namespace Fifthweek.Api.Blogs.Controllers
{
    using System.Collections.Generic;

    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;

    [AutoConstructor, AutoEqualityMembers]
    public partial class PutFreeAccessUsersResult
    {
        public IReadOnlyList<Email> InvalidEmailAddresses { get; private set; }
    }
}