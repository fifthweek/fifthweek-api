namespace Fifthweek.Api.Identity.Membership
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Dapper;

    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;

    [AutoConstructor]
    public partial class GetUserRolesDbStatement : IGetUserRolesDbStatement
    {
        private static readonly string Sql = string.Format(
            @"SELECT r.* FROM {2} r
                INNER JOIN {3} ur ON r.{0}=ur.{4}
                WHERE ur.{5}=@UserId",
            FifthweekRole.Fields.Id,
            FifthweekRole.Fields.Name,
            FifthweekRole.Table,
            FifthweekUserRole.Table,
            FifthweekUserRole.Fields.RoleId,
            FifthweekUserRole.Fields.UserId);

        private readonly IFifthweekDbConnectionFactory connectionFactory;

        public async Task<UserRoles> ExecuteAsync(UserId userId)
        {
            userId.AssertNotNull("userId");

            using (var connection = this.connectionFactory.CreateConnection())
            {
                var result = await connection.QueryAsync<FifthweekRole>(
                    Sql,
                    new 
                    {
                        UserId = userId.Value
                    });

                return new UserRoles(
                    result.Select(v => new UserRoles.UserRole(v.Id, v.Name)).ToList());
            }
        }

        [AutoConstructor, AutoEqualityMembers]
        public partial class UserRoles
        {
            public IReadOnlyList<UserRole> Roles { get; private set; }

            [AutoConstructor, AutoEqualityMembers]
            public partial class UserRole
            {
                public Guid Id { get; private set; }

                public string Name { get; private set; }
            }
        }
    }
}