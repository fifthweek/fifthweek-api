﻿using Fifthweek.Api.Core;

namespace Fifthweek.Api.Identity.Membership.Commands
{
    [AutoEqualityMembers, AutoConstructor]
    public partial class RequestPasswordResetCommand
    {
        [Optional]
        public ValidEmail Email { get; private set; }

        [Optional]
        public ValidUsername Username { get; private set; }
    }
}