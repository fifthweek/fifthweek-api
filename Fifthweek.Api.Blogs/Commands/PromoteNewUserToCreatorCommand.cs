﻿namespace Fifthweek.Api.Blogs.Commands
{
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;

    [AutoConstructor, AutoEqualityMembers]
    public partial class PromoteNewUserToCreatorCommand
    {
        public UserId NewUserId { get; private set; }
    }
}