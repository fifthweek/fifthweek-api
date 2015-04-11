namespace Fifthweek.Api.Blogs.Commands
{
    using System.Collections.Generic;

    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;

    [AutoConstructor, AutoEqualityMembers]
    public partial class UpdateFreeAccessUsersCommand
    {
        public Requester Requester { get; private set; }

        public BlogId BlogId { get; private set; }
        
        public IReadOnlyList<ValidEmail> EmailAddresses { get; private set; }
    }
}