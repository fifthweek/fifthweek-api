using System;
using System.Data.Entity.Migrations;
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
            if (this.seed != null)
            {
                this.seed(context);
            }
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
        }
    }
}
