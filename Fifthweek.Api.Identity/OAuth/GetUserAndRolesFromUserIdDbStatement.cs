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

    [AutoConstructor]
    public partial class GetUserAndRolesFromUserIdDbStatement : IGetUserAndRolesFromUserIdDbStatement
    {
        private static readonly string Query = string.Format(
            @"SELECT u.{8}, role.{2}
              FROM {1} u 
                LEFT OUTER JOIN {4} as userRole
                 ON u.{0} = userRole.{7}
                LEFT OUTER JOIN {3} role
                 ON role.{5} = userRole.{6}
              WHERE u.{0} = @{0}",
            FifthweekUser.Fields.Id,
            FifthweekUser.Table,
            FifthweekRole.Fields.Name,
            FifthweekRole.Table,
            FifthweekUserRole.Table,
            FifthweekRole.Fields.Id,
            FifthweekUserRole.Fields.RoleId,
            FifthweekUserRole.Fields.UserId,
            FifthweekUser.Fields.UserName);

        private readonly IFifthweekDbContext fifthweekDbContext;

        public async Task<UsernameAndRoles> ExecuteAsync(UserId userId)
        {
            userId.AssertNotNull("userId");

            var result = (await this.fifthweekDbContext.Database.Connection.QueryAsync<DapperResult>(
                Query,
                new { Id = userId.Value })).ToList();

            if (result.Count > 0)
            {
                if (result.Select(v => v.UserName).Distinct().Count() != 1)
                {
                    throw new InvalidOperationException("Multiple user IDs returned.");
                }
                
                var username = result[0].UserName;
                var roles = result.Select(v => v.Name).Where(v => v != null).ToList();

                return new UsernameAndRoles(new Username(username), roles);
            }

            return null;
        }

        private class DapperResult
        {
            public string UserName { get; set; }

            public string Name { get; set; }
        }
    }
}