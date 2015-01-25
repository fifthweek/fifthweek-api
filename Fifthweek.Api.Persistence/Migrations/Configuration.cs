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
            var teamMembers = new[] { "lawrence", "ttbarnes" };
            foreach (var teamMember in teamMembers)
            {
                var user = context.Users.FirstOrDefault(_ => _.UserName == teamMember);
                if (user == null)
                {
                    continue;    
                }

                user.Roles.Add(new FifthweekUserRole
                {
                    RoleId = FifthweekRole.AdministratorId,
                    UserId = user.Id
                });
            }
        }
    }
}
