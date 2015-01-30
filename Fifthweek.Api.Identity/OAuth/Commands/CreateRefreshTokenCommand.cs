using Fifthweek.Api.Core;

namespace Fifthweek.Api.Identity.OAuth.Commands
{
    using System;

    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;

    [AutoEqualityMembers, AutoConstructor]
    public partial class CreateRefreshTokenCommand
    {
        public RefreshTokenId RefreshTokenId { get; private set; }

        public ClientId ClientId { get; private set; }

        public Username Username { get; private set; }

        public string ProtectedTicket { get; private set; }

        public DateTime IssuedDate { get; private set; }

        public DateTime ExpiresDate { get; private set; }
    }
}