using System;
using System.Data.Entity.Migrations;
using System.Linq;
using Fifthweek.Api.Persistence.Identity;

namespace Fifthweek.Api.Persistence.Migrations
{
    public sealed class Configuration : DbMigrationsConfiguration<FifthweekDbContext>
    {
        private readonly Action<FifthweekDbContext> seed;

        public Configuration()
        {
            this.AutomaticMigrationsEnabled = false;
            this.ContextKey = "Fifthweek.Api.Repositories.FifthweekDbContext";
        }

        public Configuration(Action<FifthweekDbContext> seed)
        {
            this.seed = seed;
        }

        protected override void Seed(FifthweekDbContext context)
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

            if (this.seed != null)
            {
                this.seed(context);
            }
        }
    }
}
