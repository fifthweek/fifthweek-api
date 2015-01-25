namespace Fifthweek.Api.Persistence.Migrations
{
    using System;
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
            if (context.Roles.All(_ => _.Name != FifthweekRole.Administrator))
            {
                context.Roles.Add(new FifthweekRole
                {
                    Id = FifthweekRole.AdministratorId,
                    Name = FifthweekRole.Administrator
                });
            }

            if (context.Roles.All(_ => _.Name != FifthweekRole.Creator))
            {
                context.Roles.Add(new FifthweekRole
                {
                    Id = FifthweekRole.CreatorId,
                    Name = FifthweekRole.Creator
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

                if (administrators.Contains(username))
                {
                    user.Roles.Add(new FifthweekUserRole
                    {
                        RoleId = FifthweekRole.AdministratorId,
                        UserId = user.Id
                    });
                }

                if (psychics.Contains(username))
                {
                    user.Roles.Add(new FifthweekUserRole
                    {
                        RoleId = FifthweekRole.PsychicId,
                        UserId = user.Id
                    });
                }
            }
        }
    }
}
