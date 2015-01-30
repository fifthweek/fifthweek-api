namespace Fifthweek.Api.Identity.OAuth.Queries
{
    using System;
    using System.Collections.Generic;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.CodeGeneration;

    [AutoConstructor]
    public partial class GetUserClaimsIdentityQueryHandler : IQueryHandler<GetUserClaimsIdentityQuery, UserClaimsIdentity>
    {
        private readonly IGetUserAndRolesFromCredentialsDbStatement getUserAndRolesFromCredentials;
        private readonly IGetUserAndRolesFromUserIdDbStatement getUserAndRolesFromUserId;

        public async Task<UserClaimsIdentity> HandleAsync(GetUserClaimsIdentityQuery query)
        {
            query.AssertNotNull("query");

            this.ValidateQuery(query);

            if (string.IsNullOrWhiteSpace(query.AuthenticationType))
            {
                throw new BadRequestException("Authentication type not specified.");
            }

            UserId userId;
            Username username;
            IReadOnlyList<string> roles;

            if (query.UserId != null)
            {
                var user = await this.getUserAndRolesFromUserId.ExecuteAsync(query.UserId);

                if (user == null)
                {
                    throw new UnauthorizedException("The UserId " + query.UserId + " was not found.");
                }

                userId = query.UserId;
                username = user.Username;
                roles = user.Roles;
            }
            else
            {
                var user = await this.getUserAndRolesFromCredentials.ExecuteAsync(query.Username, query.Password);

                if (user == null)
                {
                    throw new BadRequestException("Invalid username or password.");
                }

                userId = user.UserId;
                username = query.Username;
                roles = user.Roles;
            }

            var identity = new ClaimsIdentity(query.AuthenticationType);
            identity.AddClaim(new Claim(ClaimTypes.Name, username.Value));
            identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, userId.Value.EncodeGuid()));

            foreach (var role in roles)
            {
                identity.AddClaim(new Claim(ClaimTypes.Role, role));
            }

            return new UserClaimsIdentity(userId, username, identity);
        }

        private void ValidateQuery(GetUserClaimsIdentityQuery query)
        {
            if (query.UserId != null)
            {
                if (query.Username != null || query.Password != null)
                {
                    throw new InvalidOperationException(
                        "If UserId is specified then both Username and Password should be null.");
                }
            }
            else
            {
                if (query.Username == null || query.Password == null)
                {
                    throw new InvalidOperationException("If UserId is null then both Username and Password should be provided.");
                }
            }
        }
    }
}