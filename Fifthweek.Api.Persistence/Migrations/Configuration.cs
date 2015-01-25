namespace Fifthweek.Api.Persistence.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Migrations;
    using System.Linq;

    using Fifthweek.Api.Persistence.Identity;

    public sealed class Configuration : DbMigrationsConfiguration<FifthweekDbContext>
    {
        public Configuration()
        {
            this.AutomaticMigrationsEnabled = false;
            this.ContextKey = "Fifthweek.Api.Repositories.FifthweekDbContext";
        }

        protected override void Seed(FifthweekDbContext context)
        {
            this.EnsureRolesExist(context);
            this.AssignRolesToTeam(context);
        }

        private void EnsureRolesExist(FifthweekDbContext context)
        {
            this.EnsureRoleExists(context, FifthweekRole.Administrator, FifthweekRole.AdministratorId);
            this.EnsureRoleExists(context, FifthweekRole.Creator, FifthweekRole.CreatorId);
            this.EnsureRoleExists(context, FifthweekRole.Psychic, FifthweekRole.PsychicId);
        }

        private void EnsureRoleExists(FifthweekDbContext context, string roleName, Guid roleId)
        {
            if (context.Roles.All(_ => _.Name != roleName))
            {
                context.Roles.Add(new FifthweekRole
                {
                    Id = roleId,
                    Name = roleName
                });
            }
        }

        private void AssignRolesToTeam(FifthweekDbContext context)
        {
            var administrators = new[] { "lawrence" };
            var psychics = new[] { "lawrence", "ttbarnes" };
            var allUsernames = administrators.Concat(psychics).Distinct();

            foreach (var username in allUsernames)
            {
                var user = context.Users.FirstOrDefault(_ => _.UserName == username);
                if (user == null)
                {
                    continue;    
                }

                this.EnsureUserHasRole(user, administrators, FifthweekRole.AdministratorId);
                this.EnsureUserHasRole(user, psychics, FifthweekRole.PsychicId);
            }
        }

        private void EnsureUserHasRole(FifthweekUser user, IEnumerable<string> usersInRole, Guid roleId)
        {
            if (usersInRole.Contains(user.UserName) && user.Roles.All(_ => _.RoleId != roleId))
            {
                user.Roles.Add(new FifthweekUserRole
                {
                    RoleId = roleId,
                    UserId = user.Id
                });
            }
        }
    }
}
