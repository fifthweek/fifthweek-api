namespace Fifthweek.Api.Identity.OAuth
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Dapper;

    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;

    using Microsoft.AspNet.Identity;

    [AutoConstructor]
    public partial class GetUserAndRolesFromCredentialsDbStatement : IGetUserAndRolesFromCredentialsDbStatement
    {
        private static readonly string Query = string.Format(
            @"SELECT u.{0}, u.{3}, role.{4}
              FROM {1} u 
                LEFT OUTER JOIN {6} as userRole
                 ON u.{0} = userRole.{9}
                LEFT OUTER JOIN {5} role
                 ON role.{7} = userRole.{8}
              WHERE {2} = @{2}",
            FifthweekUser.Fields.Id,
            FifthweekUser.Table,
            FifthweekUser.Fields.UserName,
            FifthweekUser.Fields.PasswordHash,
            FifthweekRole.Fields.Name,
            FifthweekRole.Table,
            FifthweekUserRole.Table,
            FifthweekRole.Fields.Id,
            FifthweekUserRole.Fields.RoleId,
            FifthweekUserRole.Fields.UserId);

        private readonly IUserManager userManager;
        private readonly IFifthweekDbConnectionFactory connectionFactory;

        public async Task<UserIdAndRoles> ExecuteAsync(Username username, Password password)
        {
            username.AssertNotNull("username");
            password.AssertNotNull("password");

            using (var connection = this.connectionFactory.CreateConnection())
            {
                var result = (await connection.QueryAsync<DapperResult>(
                    Query,
                    new { UserName = username.Value })).ToList();

                if (result.Count > 0)
                {
                    if (result.Select(v => v.Id).Distinct().Count() != 1)
                    {
                        throw new InvalidOperationException("Multiple user IDs returned.");
                    }

                    if (result.Select(v => v.PasswordHash).Distinct().Count() != 1)
                    {
                        throw new InvalidOperationException("Multiple hashed passwords returned.");
                    }

                    if (this.userManager.PasswordHasher.VerifyHashedPassword(result[0].PasswordHash, password.Value)
                        != PasswordVerificationResult.Failed)
                    {
                        var userId = result[0].Id;
                        var roles = result.Select(v => v.Name).Where(v => v != null).ToList();

                        return new UserIdAndRoles(userId, roles);
                    }
                }

                return null;
            }
        }

        private class DapperResult
        {
            public UserId Id { get; set; }

            public string PasswordHash { get; set; }

            public string Name { get; set; }
        }
    }
}